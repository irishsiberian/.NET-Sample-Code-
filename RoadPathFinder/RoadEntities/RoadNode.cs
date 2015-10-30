using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadPathFinder
{
    public enum RoadNodeState
    {
        Ok,
        Crash
    }

    public enum RoadNodeRole
    {
        Normal,
        Start,
        Finish
    }

    /// <summary>
    /// Represents single road node
    /// </summary>
    public class RoadNode
    {
        public RoadNode(int id, RoadNodeRole role, RoadNodeState state)
        {
            _id = id;
            _state = state;
            _role = role;
            _links = new List<RoadLink>();
        }

        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
        }

        private RoadNodeState _state;
        public RoadNodeState State
        {
            get
            {
                return _state;
            }
        }

        private RoadNodeRole _role;
        public RoadNodeRole Role
        {
            get
            {
                return _role;
            }
        }

        private List<RoadLink> _links;
        public List<RoadLink> Links
        {
            get
            {
                return _links;
            }
        }
    }
}
