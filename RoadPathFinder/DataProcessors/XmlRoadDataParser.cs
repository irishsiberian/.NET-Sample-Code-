using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace RoadPathFinder
{
    public static class XmlRoadDataParser
    {
        private const string schemaFileName = @".\DataSchema\RoadMapXmlSchema.xsd";

        /// <summary>
        /// Parse source road map XML document
        /// </summary>
        /// <param name="sourceXml">road map XML document</param>
        /// <returns>parsed RoadMap object</returns>
        public static RoadMap ParseRoadMapXml(XmlDocument sourceXml)
        {
            if (sourceXml == null)
                return null;
            RoadMap roadMap = new RoadMap();
            if (ValidateSchema(sourceXml))
            {
                XmlNode graph = sourceXml.SelectNodes("/graph").Item(0);
                XmlNodeList nodes = graph.SelectNodes("node");
                foreach (XmlNode node in nodes)
                {
                    RoadNodeRole nodeRole = RoadNodeRole.Normal;
                    if (node.Attributes["role"] != null && node.Attributes["role"].Value == "start")
                    {
                        nodeRole = RoadNodeRole.Start;
                    }
                    else if (node.Attributes["role"] != null &&node.Attributes["role"].Value == "finish")
                    {
                        nodeRole = RoadNodeRole.Finish;
                    }
                    RoadNodeState nodeState = RoadNodeState.Ok;
                    if (node.Attributes["status"] != null && node.Attributes["status"].Value == "crash")
                    {
                        nodeState = RoadNodeState.Crash;
                    }
                    int nodeId = Convert.ToInt32(node.Attributes["id"].Value);
                    RoadNode roadNode = new RoadNode(nodeId, nodeRole, nodeState);
                    XmlNodeList links = node.SelectNodes("link");
                    foreach (XmlNode link in links)
                    {
                        int linkRef = Convert.ToInt32(link.Attributes["ref"].Value);
                        int linkWeight = Convert.ToInt32(link.Attributes["weight"].Value);
                        RoadLink roadLink = new RoadLink(linkWeight, linkRef);
                        roadNode.Links.Add(roadLink);
                    }
                    roadMap.Nodes.Add(roadNode);
                }
            }
            return roadMap;
        }

        /// <summary>
        /// Validate road map XML with XSD schema
        /// </summary>
        /// <param name="sourceXml"></param>
        /// <returns></returns>
        private static bool ValidateSchema(XmlDocument sourceXml)
        {
            XmlDocument xsdSchemaDocument = new XmlDocument();
            XmlSchemaSet xsdSchemaSet = new XmlSchemaSet();
            //Load XSD-schema
            try
            {
                xsdSchemaDocument.Load(schemaFileName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Can't load XSD schema {0}", schemaFileName), ex);
            }
            xsdSchemaSet.Add(null, new XmlNodeReader(xsdSchemaDocument));
            //Validate XML with XSD-schema
            sourceXml.Schemas.Add(xsdSchemaSet);
            sourceXml.Validate(new ValidationEventHandler(ValidationHandler));
            return true;
        }

        /// <summary>
        /// Handler for XSD validation errors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="_args"></param>
        private static void ValidationHandler(object sender, ValidationEventArgs _args)
        {
            if (_args.Severity == XmlSeverityType.Warning)
                throw new ApplicationException("Warning while parsing source XML: " + _args.Message, _args.Exception);
            else
                throw new ApplicationException("Error while parsing source XML: " + _args.Message, _args.Exception);
        }
    }
}
