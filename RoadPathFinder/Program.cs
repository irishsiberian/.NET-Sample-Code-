using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml;
using System.IO;

namespace RoadPathFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine(String.Format("Usage: {0} <RoadMapFile.xml>", Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName)));
                Console.ReadLine();
                return;
            }
            else
            {
                string roadMapFileName = args[0];
                XmlDocument roadMapXmlDocument = new XmlDocument();
                try
                {
                    roadMapXmlDocument.Load(roadMapFileName);
                }
                catch (Exception)
                {
                    Console.WriteLine(string.Format("Error: Unable to load XML file {0}. Check if file exists and contains correct XML", roadMapFileName));
                    Console.ReadLine();
                    return;
                }

                try
                {
                    RoadMap roadMap = XmlRoadDataParser.ParseRoadMapXml(roadMapXmlDocument);
                    if (roadMap == null)
                    {
                        Console.WriteLine("Error: Unable to parse road map XML");
                        Console.ReadLine();
                        return;
                    }
                    PathFinder pathFinder = new PathFinder(roadMap);
                    List<RoadPath> paths = pathFinder.FindAllPaths().OrderBy(p => p.Length).ToList();
                    UIPrinter.PrintFoundPaths(paths);
                    Console.ReadLine();
                }
                catch (ApplicationException ex)
                {
                    Console.WriteLine(string.Format("Error: {0}", ex.Message));
                    Console.ReadLine();
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("Unknown error: {0}", ex.Message));
                    Console.ReadLine();
                    return;
                }
            }
        }
    }
}
