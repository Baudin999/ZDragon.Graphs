# ZDragon Graph

These examples create component diagrams for the ZDragon documentation tool. 

## Microsoft Automatic Graph Layout

The core of this library revolves around the [MSAGL](https://github.com/microsoft/automatic-graph-layout). This library tries to evolve the SvgRenderer towards a more PlantUml style rendering. 

Example:

```csharp
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
```

This will result in the following diagram:

![image](https://user-images.githubusercontent.com/2974147/115988747-6bbdcb80-a5bb-11eb-8ccc-033f55ad2310.png)
