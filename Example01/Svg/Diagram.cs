using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Drawing;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace Example01.Svg {
    public class Diagram {

        private MemoryStream ms { get; } = new MemoryStream();
        private XmlWriter xmlWriter { get; }
        private Graph drawingGraph { get;  }

        public Diagram(Graph drawingGraph) {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            try {
                var streamWriter = new StreamWriter(ms);
                var xmlWriterSettings = new XmlWriterSettings { Indent = true };
                xmlWriter = XmlWriter.Create(streamWriter, xmlWriterSettings);
                this.drawingGraph = drawingGraph;

                // flip coords
                TransformGraphByFlippingY();

                Open();

                foreach (var node in drawingGraph.Nodes) {
                    if (node is LabeledNode ln) {
                        ln.WriteTo(xmlWriter);
                    }
                }

                foreach (var edge in drawingGraph.Edges) {
                    new Connector(edge).WriteTo(xmlWriter);
                }

                Close();

                // reset coords
                TransformGraphByFlippingY();
            }
            finally {
                TransformGraphByFlippingY();

            }
        }

        void Open() {
            xmlWriter.WriteComment($"SvgWriter version: {this.GetType().Assembly.GetName().Version}");
            var box = drawingGraph.BoundingBox;
            xmlWriter.WriteStartElement("svg", "http://www.w3.org/2000/svg");
            xmlWriter.WriteAttributeString("xmlns", "xlink", null, "http://www.w3.org/1999/xlink");
            xmlWriter.WriteAttribute("width", box.Width);
            xmlWriter.WriteAttribute("height", box.Height);
            xmlWriter.WriteAttribute("id", "svg2");
            xmlWriter.WriteAttribute("version", "1.1");
            xmlWriter.WriteStartElement("g");
            xmlWriter.WriteAttribute("transform", String.Format("translate({0},{1})", -box.Left, -box.Bottom));
        }

        /// <summary>
        /// writes the end of the file end closes the the stream
        /// </summary>
         void Close() {
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();
        }

        void TransformGraphByFlippingY() {
            var matrix = new PlaneTransformation(1, 0, 0, 0, -1, 0);
            drawingGraph.GeometryGraph.Transform(matrix);
        }


        public override string ToString() {
            ms.Position = 0;
            var sr = new StreamReader(ms);
            var myStr = sr.ReadToEnd();
            var doc = XDocument.Parse(myStr);
            return doc.ToString();
        }


    }
}
