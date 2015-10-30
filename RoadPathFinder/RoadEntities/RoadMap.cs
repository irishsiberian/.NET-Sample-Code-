using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RoadPathFinder
{
    /// <summary>
    /// Contains list of road graph nodes with links
    /// </summary>
    public class RoadMap
    {
        private List<RoadNode> _nodes = new List<RoadNode>();
        public List<RoadNode> Nodes
        {
            get
            {
                return _nodes;
            }
        }
    }
}
