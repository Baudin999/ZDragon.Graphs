
using Example01.Svg;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.Layout.Layered;
using Microsoft.Msagl.Miscellaneous;
using System.Collections.Generic;

namespace Example01 {
    class Program {
        static void Main(string[] args) {
            var drawingGraph = new Graph();

            drawingGraph.AddNode(new ComponentNode("Foo"));
            drawingGraph.AddNode(new ComponentNode("Bar", "Bar Component", "[Azure Functions]", "This is the Bar component, really really important!"));
            drawingGraph.AddNode(new ComponentNode("Component01", "First Component", null, "Bizar"));
            drawingGraph.AddNode(new ComponentNode("Component02"));
            drawingGraph.AddNode(new ComponentNode("Component03"));
            drawingGraph.AddNode(new ComponentNode("Component04"));
            drawingGraph.AddNode(new LabeledNode("Component05", new System.Collections.Generic.List<string> { "Component Nr. 5" }));
            drawingGraph.AddNode(new ComponentNode("Component06"));
            drawingGraph.AddNode(new ComponentNode("Component07"));
            drawingGraph.AddNode(new ComponentNode("Component08"));


            var labels = new List<Svg.Label> {
              new Svg.Label("Component Nr. 9") {
                Font = new System.Drawing.Font("Consolas", 20f, System.Drawing.FontStyle.Bold),
                Color = Color.Azure
                },
              new Svg.Label("This is the description. It can become quite large and if all goes to plan; it will wrap itself.") {
                  Color = Color.MediumOrchid
              }
            };
            var labeledNode = new LabeledNode("Component09", labels);
            labeledNode.SvgElement.BackgroundColor = Color.Maroon;
            drawingGraph.AddNode(labeledNode);

            drawingGraph.AddEdge("Foo", "Bar");
            drawingGraph.AddEdge("Bar", "Component01");
            drawingGraph.AddEdge("Bar", "Component02");
            drawingGraph.AddEdge("Component01", "Component03");
            drawingGraph.AddEdge("Component01", "Component02");
            drawingGraph.AddEdge("Component02", "Component04");
            drawingGraph.AddEdge("Component02", "Component05");
            drawingGraph.AddEdge("Component02", "Component06");
            drawingGraph.AddEdge("Component02", "Component07");
            drawingGraph.AddEdge("Component02", "Component08");
            drawingGraph.AddEdge("Component03", "Component09");


            drawingGraph.CreateGeometryGraph();

            foreach (var node in drawingGraph.Nodes) {
                if (node is LabeledNode ln) ln.CreateBoundary();
            }


            LayoutHelpers.CalculateLayout(drawingGraph.GeometryGraph, new SugiyamaLayoutSettings(), null);

            var doc = new Diagram(drawingGraph);
            System.Console.WriteLine(doc.ToString());
            TextCopy.ClipboardService.SetText(doc.ToString());
        }
    }
}


/*
 * 
var fooNode = new CkNode("Foo") {
                Title = "Foo Component",
                Description = "This is the description of the Foo components, let's see if we can fit this into the box!"
            };
            var barNode = new CkNode("Bar") {
                Title = "Bar Component",
                Description = "This is the BAR component, really important!",
                Technology = "AWS Glue"
            };

            var otherNode = new CkNode("Other") {
                Title = "Other Something Component"
            };
            var tempNode = new CkNode("Temp");

            var subGraph = new Subgraph("System01");
            drawingGraph.RootSubgraph.AddSubgraph(subGraph);
            subGraph.AddNode(otherNode.ToNode());
            subGraph.AddNode(tempNode.ToNode());


            var subGraph2 = new Subgraph("System02");
            drawingGraph.RootSubgraph.AddSubgraph(subGraph2);
            subGraph2.AddNode(fooNode.ToNode());


            drawingGraph.AddNode(fooNode.ToNode());
            drawingGraph.AddNode(barNode.ToNode());
            drawingGraph.AddNode(otherNode.ToNode());
            drawingGraph.AddNode(tempNode.ToNode());

            drawingGraph.AddEdge(fooNode.Id, barNode.Id);
            drawingGraph.AddEdge(otherNode.Id, barNode.Id);
            drawingGraph.AddEdge(barNode.Id, tempNode.Id);


            drawingGraph.CreateGeometryGraph();

            fooNode.CreateBoundary();
            barNode.CreateBoundary();
            tempNode.CreateBoundary();
            otherNode.CreateBoundary();
*/