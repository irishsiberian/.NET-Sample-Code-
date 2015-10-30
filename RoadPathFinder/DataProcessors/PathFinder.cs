using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadPathFinder
{
    public class PathFinder
    {
        public PathFinder(RoadMap roadMap)
        {
            if (roadMap == null)
            {
                throw new ApplicationException("Roadmap is not set\n");
            }
            this.roadMap = roadMap;
        }

        RoadMap roadMap;
        List<RoadNode> visitedNodes = new List<RoadNode>();
        RoadPath currentPath = new RoadPath();
        List<RoadPath> pathsFound = new List<RoadPath>();
        RoadNode finishNode;
        RoadNode startNode;

        /// <summary>
        /// Find all paths in road graph from start node to finish node
        /// </summary>
        /// <returns>Found paths</returns>
        public List<RoadPath> FindAllPaths()
        {
            CheckMapErrors(roadMap);

            startNode = roadMap.Nodes.Where(n => n.Role == RoadNodeRole.Start).Single();
            finishNode = roadMap.Nodes.Where(n => n.Role == RoadNodeRole.Finish).Single();
            visitedNodes.Add(startNode);
            currentPath.Push(startNode);
            FindPaths(startNode);

            return pathsFound;
        }

        /// <summary>
        /// Check road map for some errors that may not have been caught by XSD schema. 
        /// Throws ApplicationException with error descriptions if some errors were found. 
        /// Otherwise returns nothing.
        /// </summary>
        /// <param name="roadMap">road map to check</param>
        private void CheckMapErrors(RoadMap roadMap)
        {
            string errors = string.Empty;

            if (roadMap == null)
                errors += "No road map set\n";

            if (roadMap.Nodes.Where(n => n.Role == RoadNodeRole.Start && n.State != RoadNodeState.Crash).Count() != 1)
            {
                errors += "There is no exactly one start and not crashed node\n";
            }

            if (roadMap.Nodes.Where(n => n.Role == RoadNodeRole.Finish && n.State != RoadNodeState.Crash).Count() != 1)
            {
                errors += "There is no exactly one finish and not crashed node\n";
            }

            errors += CheckLinks(roadMap);

            if (!string.IsNullOrEmpty(errors))
            {
                throw new ApplicationException("There are errors in the map: \n" + errors);
            }
        }

        /// <summary>
        /// Check if all links between nodes are correct.
        /// </summary>
        /// <param name="roadMap">road map to check</param>
        /// <returns>String with errors descriptions or empty string if no errors found</returns>
        private string CheckLinks(RoadMap roadMap)
        {
            string errors = string.Empty;
            foreach (var node in roadMap.Nodes)
            {
                foreach (var link in node.Links)
                {
                    try
                    {
                        //check if exactly one node exists by link ref
                        RoadNode neighbourByLink = roadMap.Nodes.Where(n => n.Id == link.RefNodeId).Single();
                        //check if exactly one back link exists and has the same weight as direct link
                        RoadLink backLink = neighbourByLink.Links.Where(l => l.RefNodeId == node.Id && l.Weight == link.Weight).Single();
                    }
                    catch (InvalidOperationException)
                    {
                        errors += string.Format("Node {0} has wrong link\n", node.Id);
                    }
                }
            }
            return errors;
        }

        /// <summary>
        /// Recurcively find all paths from node to finishNode.
        /// Deep graph search algorithm is used.
        /// </summary>
        /// <param name="node">node</param>
        private void FindPaths(RoadNode node)
        {
            if (node == null)
                return;
            if (node == finishNode)
            {
                //if we have reached finish node, remember the current path
                pathsFound.Add((RoadPath)currentPath.Clone());
            }
            else
            {
                List<RoadNode> neighbours = GetNeighbours(node);
                foreach (RoadNode neighbour in neighbours)
                {
                    if (!visitedNodes.Contains(neighbour))
                    {
                        visitedNodes.Add(neighbour);
                        currentPath.Push(neighbour);
                        FindPaths(neighbour);
                        visitedNodes.Remove(neighbour);
                        currentPath.Pop();
                    }
                }
            }
        }

        /// <summary>
        /// Get non-crashed nodes connected with the node
        /// </summary>
        /// <param name="node"></param>
        /// <returns>list of neighbour nodes</returns>
        private List<RoadNode> GetNeighbours(RoadNode node)
        {
            if (node == null)
                return null;
            List<RoadNode> neighbours = new List<RoadNode>();
            foreach (var link in node.Links)
            {
                RoadNode neighbourByLink = roadMap.Nodes.Where(n => n.Id == link.RefNodeId).Single();
                if (neighbourByLink.State == RoadNodeState.Ok)
                {
                    neighbours.Add(neighbourByLink);
                }
            }
            return neighbours;
        }
    }
}
