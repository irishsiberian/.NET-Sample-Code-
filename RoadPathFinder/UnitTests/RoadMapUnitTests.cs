using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml;

namespace RoadPathFinder.UnitTests
{
    [TestFixture]
    public class RoadMapUnitTests
    {
        [Test]
        public void EverythingOk()
        {
            XmlDocument roadMapXmlDocument = new XmlDocument();
            roadMapXmlDocument.Load(@".\UnitTests\TestXmlData\test-everything-is-ok.xml");

            RoadMap roadMap = XmlRoadDataParser.ParseRoadMapXml(roadMapXmlDocument);

            Assert.AreNotEqual(roadMap, null);

            PathFinder pathFinder = new PathFinder(roadMap);

            List<RoadPath> paths = pathFinder.FindAllPaths().OrderBy(p => p.Length).ToList();

            Assert.AreEqual(paths.Count, 7);

            Assert.AreEqual(paths[0].Length, 22);

            Assert.AreEqual(paths[0].ToList()[0].Id, 1);
            Assert.AreEqual(paths[0].ToList()[1].Id, 6);
            Assert.AreEqual(paths[0].ToList()[2].Id, 8);
            Assert.AreEqual(paths[0].ToList()[3].Id, 9);
            Assert.AreEqual(paths[0].ToList()[4].Id, 10);

            Assert.AreEqual(paths[1].Length, 25);

            Assert.AreEqual(paths[2].Length, 28);

            Assert.AreEqual(paths[3].Length, 35);

            Assert.AreEqual(paths[4].Length, 38);

            Assert.AreEqual(paths[4].ToList()[0].Id, 1);
            Assert.AreEqual(paths[4].ToList()[1].Id, 6);
            Assert.AreEqual(paths[4].ToList()[2].Id, 8);
            Assert.AreEqual(paths[4].ToList()[3].Id, 9);
            Assert.AreEqual(paths[4].ToList()[4].Id, 5);
            Assert.AreEqual(paths[4].ToList()[5].Id, 4);
            Assert.AreEqual(paths[4].ToList()[6].Id, 10);

            Assert.AreEqual(paths[5].Length, 58);

            Assert.AreEqual(paths[6].Length, 59);

            Assert.AreEqual(paths[6].ToList()[0].Id, 1);
            Assert.AreEqual(paths[6].ToList()[1].Id, 2);
            Assert.AreEqual(paths[6].ToList()[2].Id, 3);
            Assert.AreEqual(paths[6].ToList()[3].Id, 4);
            Assert.AreEqual(paths[6].ToList()[4].Id, 5);
            Assert.AreEqual(paths[6].ToList()[5].Id, 6);
            Assert.AreEqual(paths[6].ToList()[6].Id, 8);
            Assert.AreEqual(paths[6].ToList()[7].Id, 9);
            Assert.AreEqual(paths[6].ToList()[8].Id, 10);
        }

        [Test]
        public void NoPathAtAll()
        {
            XmlDocument roadMapXmlDocument = new XmlDocument();
            roadMapXmlDocument.Load(@".\UnitTests\TestXmlData\test-no-path-at-all.xml");

            RoadMap roadMap = XmlRoadDataParser.ParseRoadMapXml(roadMapXmlDocument);

            Assert.AreNotEqual(roadMap, null);

            PathFinder pathFinder = new PathFinder(roadMap);
            List<RoadPath> paths = pathFinder.FindAllPaths().OrderBy(p => p.Length).ToList();

            Assert.AreEqual(paths.Count, 0);
        }

        [Test]
        public void NoPathBecauseOfCrashNode()
        {
            XmlDocument roadMapXmlDocument = new XmlDocument();
            roadMapXmlDocument.Load(@".\UnitTests\TestXmlData\test-no-path-because-of-crash-node.xml");

            RoadMap roadMap = XmlRoadDataParser.ParseRoadMapXml(roadMapXmlDocument);

            Assert.AreNotEqual(roadMap, null);

            PathFinder pathFinder = new PathFinder(roadMap);
            List<RoadPath> paths = pathFinder.FindAllPaths().OrderBy(p => p.Length).ToList();

            Assert.AreEqual(paths.Count, 0);
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void NodeByRefMissed()
        {
            XmlDocument roadMapXmlDocument = new XmlDocument();
            roadMapXmlDocument.Load(@".\UnitTests\TestXmlData\test-node-by-ref-missed.xml");

            RoadMap roadMap = XmlRoadDataParser.ParseRoadMapXml(roadMapXmlDocument);

            Assert.AreNotEqual(roadMap, null);

            PathFinder pathFinder = new PathFinder(roadMap);
            List<RoadPath> paths = pathFinder.FindAllPaths().OrderBy(p => p.Length).ToList();
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void WrongBackLinkWeight()
        {
            XmlDocument roadMapXmlDocument = new XmlDocument();
            roadMapXmlDocument.Load(@".\UnitTests\TestXmlData\test-wrong-back-link-weight.xml");

            RoadMap roadMap = XmlRoadDataParser.ParseRoadMapXml(roadMapXmlDocument);

            Assert.AreNotEqual(roadMap, null);

            PathFinder pathFinder = new PathFinder(roadMap);
            List<RoadPath> paths = pathFinder.FindAllPaths().OrderBy(p => p.Length).ToList();
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void NoStartNode()
        {
            XmlDocument roadMapXmlDocument = new XmlDocument();
            roadMapXmlDocument.Load(@".\UnitTests\TestXmlData\test-no-start-node.xml");

            RoadMap roadMap = XmlRoadDataParser.ParseRoadMapXml(roadMapXmlDocument);

            Assert.AreNotEqual(roadMap, null);

            PathFinder pathFinder = new PathFinder(roadMap);
            List<RoadPath> paths = pathFinder.FindAllPaths().OrderBy(p => p.Length).ToList();
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void NoFinishtNode()
        {
            XmlDocument roadMapXmlDocument = new XmlDocument();
            roadMapXmlDocument.Load(@".\UnitTests\TestXmlData\test-no-finish-node.xml");

            RoadMap roadMap = XmlRoadDataParser.ParseRoadMapXml(roadMapXmlDocument);

            Assert.AreNotEqual(roadMap, null);

            PathFinder pathFinder = new PathFinder(roadMap);
            List<RoadPath> paths = pathFinder.FindAllPaths().OrderBy(p => p.Length).ToList();
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void NoNodes()
        {
            XmlDocument roadMapXmlDocument = new XmlDocument();
            roadMapXmlDocument.Load(@".\UnitTests\TestXmlData\test-no-nodes.xml");

            RoadMap roadMap = XmlRoadDataParser.ParseRoadMapXml(roadMapXmlDocument);
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void StartNodeIsCrashed()
        {
            XmlDocument roadMapXmlDocument = new XmlDocument();
            roadMapXmlDocument.Load(@".\UnitTests\TestXmlData\test-start-node-is-crashed.xml");

            RoadMap roadMap = XmlRoadDataParser.ParseRoadMapXml(roadMapXmlDocument);

            Assert.AreNotEqual(roadMap, null);

            PathFinder pathFinder = new PathFinder(roadMap);
            List<RoadPath> paths = pathFinder.FindAllPaths().OrderBy(p => p.Length).ToList();
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void FinishNodeIsCrashed()
        {
            XmlDocument roadMapXmlDocument = new XmlDocument();
            roadMapXmlDocument.Load(@".\UnitTests\TestXmlData\test-finish-node-is-crashed.xml");

            RoadMap roadMap = XmlRoadDataParser.ParseRoadMapXml(roadMapXmlDocument);

            Assert.AreNotEqual(roadMap, null);

            PathFinder pathFinder = new PathFinder(roadMap);
            List<RoadPath> paths = pathFinder.FindAllPaths().OrderBy(p => p.Length).ToList();
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void SomeNodeIdMissed()
        {
            XmlDocument roadMapXmlDocument = new XmlDocument();
            roadMapXmlDocument.Load(@".\UnitTests\TestXmlData\test-some-node-id-missed.xml");

            RoadMap roadMap = XmlRoadDataParser.ParseRoadMapXml(roadMapXmlDocument);
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void SomeRefMissed()
        {
            XmlDocument roadMapXmlDocument = new XmlDocument();
            roadMapXmlDocument.Load(@".\UnitTests\TestXmlData\test-some-ref-missed.xml");

            RoadMap roadMap = XmlRoadDataParser.ParseRoadMapXml(roadMapXmlDocument);
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void SomeWeightMissed()
        {
            XmlDocument roadMapXmlDocument = new XmlDocument();
            roadMapXmlDocument.Load(@".\UnitTests\TestXmlData\test-some-weight-missed.xml");

            RoadMap roadMap = XmlRoadDataParser.ParseRoadMapXml(roadMapXmlDocument);
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void WrongNodeRole()
        {
            XmlDocument roadMapXmlDocument = new XmlDocument();
            roadMapXmlDocument.Load(@".\UnitTests\TestXmlData\test-wrong-node-role.xml");

            RoadMap roadMap = XmlRoadDataParser.ParseRoadMapXml(roadMapXmlDocument);
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void WrongNodeState()
        {
            XmlDocument roadMapXmlDocument = new XmlDocument();
            roadMapXmlDocument.Load(@".\UnitTests\TestXmlData\test-wrong-node-state.xml");

            RoadMap roadMap = XmlRoadDataParser.ParseRoadMapXml(roadMapXmlDocument);
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void WrongRootElement()
        {
            XmlDocument roadMapXmlDocument = new XmlDocument();
            roadMapXmlDocument.Load(@".\UnitTests\TestXmlData\test-wrong-root-element.xml");

            RoadMap roadMap = XmlRoadDataParser.ParseRoadMapXml(roadMapXmlDocument);
        }
    }
}
