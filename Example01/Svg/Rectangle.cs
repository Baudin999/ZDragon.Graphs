﻿using Microsoft.Msagl.Drawing;
using System.Xml;

namespace Example01.Svg {
    public class Rectangle : SvgElement {
        
        public override void WriteTo(XmlWriter writer) {
            writer.WriteStartElement("rect");
            writer.WriteAttribute("x", X);
            writer.WriteAttribute("y", Y);
            writer.WriteAttribute("width", Width);
            writer.WriteAttribute("height", Height);
            writer.WriteAttribute("stroke", BorderColor);
            writer.WriteAttribute("fill", BackgroundColor);
            writer.WriteEndElement();
        }

    }
}
