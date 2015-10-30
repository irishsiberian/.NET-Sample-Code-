using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadPathFinder
{
    public class UIPrinter
    {
        /// <summary>
        /// Format list of paths and output it to console
        /// </summary>
        /// <param name="paths"></param>
        public static void PrintFoundPaths(List<RoadPath> paths)
        {
            if (paths.Count > 0)
            {
                Console.WriteLine(string.Format("Total paths found: {0}\n", paths.Count));
                foreach (var path in paths)
                {
                    Console.Write((paths.IndexOf(path) + 1) + ". Length = " + path.Length + "; ");
                    Console.Write("Node IDs: ");
                    foreach (RoadNode pathNode in path.ToList())
                    {
                        Console.Write(pathNode.Id + " ");
                    }
                    Console.WriteLine("\n");
                }
            }
            else
            {
                Console.WriteLine("No paths found");
            }
        }
    }
}
