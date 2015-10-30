using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadPathFinder
{
    /// <summary>
    /// Link to one road node.
    /// </summary>
    public class RoadLink
    {
        public RoadLink(int weight, int refNodeId)
        {
            _weight = weight;
            _refNodeId = refNodeId;
        }

        private int _weight;
        public int Weight
        {
            get
            {
                return _weight;
            }
        }

        private int _refNodeId;
        public int RefNodeId
        {
            get
            {
                return _refNodeId;
            }
        }
    }
}
