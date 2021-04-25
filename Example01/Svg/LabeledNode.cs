using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Example01.Svg {
    /// <summary>
    /// The Labeled Node is a Node which can contain labels. Each label will
    /// be printed in a vertical column.
    /// </summary>
    public class LabeledNode : Node {
        public List<Label> Labels { get; }
        public SvgElement SvgElement { get; }
        

        public LabeledNode(string id, List<Label> labels) : base(id) {
            this.Labels = labels;
            this.SvgElement = SvgElement.DefaultElement();
        }
        public LabeledNode(string id, List<string> labels) : base(id) {
            this.Labels = labels.Where(s => s != null).Select(s => new Svg.Label(s)).ToList();
            this.SvgElement = SvgElement.DefaultElement();
        }

        public void CreateBoundary() {

            var height = (this.Labels.FirstOrDefault()?.Height / 1.45) ?? 0d; // minimal height, the 1.45 is a magical number, found through experimentation
            var width = 100d;
            foreach (var label in this.Labels) {
                // the padding is 12 on each side, so we'll pad the label width with 24
                if (label.Width + 24 > width) width = label.Width + 24;
                height += label.Height;
            }

            this.GeometryNode.BoundaryCurve = CurveFactory.CreateRectangle(width, height, new Point(0, 0));
        }

        public void WriteTo(XmlWriter writer) {
            writer.WriteStartElement("svg");
            writer.WriteAttribute("X", this.BoundingBox.Left);
            writer.WriteAttribute("Y", this.BoundingBox.Bottom);
            writer.WriteAttribute("Width", this.BoundingBox.Width);
            writer.WriteAttribute("Height", this.BoundingBox.Height);

            new Rectangle {
                X = 0,
                Y = 0,
                Width = this.BoundingBox.Width,
                Height = this.BoundingBox.Height,
                BackgroundColor = this.SvgElement.BackgroundColor,
                BorderColor = this.SvgElement.BorderColor,
                FontColor = this.SvgElement.FontColor
            }.WriteTo(writer);

            
            var y = this.Labels.FirstOrDefault()?.Height ?? 0d;
            foreach (var label in this.Labels) {
                label.Color = label.Color ?? this.SvgElement.FontColor;
                label.WriteTo(writer, this.BoundingBox.Width, y);
                y += label.Height;
            }

            writer.WriteEndElement();
        }
    }

    public class ComponentNode : LabeledNode {
        public ComponentNode(string id, string title = null, string technology = null, string description = null) 
            : base(id, new List<string>()) {

            this.Labels.Add(new Svg.Label(title ?? id));
            if (technology != null) {
                this.Labels.Add(new Svg.Label(technology, new System.Drawing.Font("Verdana", 8f, System.Drawing.FontStyle.Bold)));
            }
            if (description != null) {
                var descriptionLabel = new Svg.Label(description);
                descriptionLabel.PaddingTop = 10;
                this.Labels.Add(descriptionLabel);
            }

            this.SvgElement.BackgroundColor = Utils.backgroundColor;
            this.SvgElement.BorderColor = Utils.borderColor;
            this.SvgElement.FontColor = Utils.fontColor;

        }
    }
}
