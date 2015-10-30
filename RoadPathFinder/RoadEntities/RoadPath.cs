using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadPathFinder
{
    /// <summary>
    /// Class representing path through the road graph as stack of RoadNodes
    /// </summary>
    public class RoadPath : Stack<RoadNode>, ICloneable
    {
        #region ICloneable Members

        public object Clone()
        {
            RoadPath clone = new RoadPath();
            foreach (RoadNode node in this.ToList())
            {
                clone.Push(node);
            }
            return clone;
        }

        #endregion

        /// <summary>
        /// Calculated path's length
        /// </summary>
        public int Length
        {
            get
            {
                int pathLength = 0;
                List<RoadNode> nodeList = this.ToList();
                for (int nodeIndex = 0; nodeIndex < nodeList.Count - 1; nodeIndex++)
                {
                    RoadLink linkToNextNode = nodeList[nodeIndex].Links
                        .Where(l => l.RefNodeId == nodeList[nodeIndex + 1].Id).Single();
                    pathLength += linkToNextNode.Weight;
                }
                return pathLength;
            }
        }
    }
}
