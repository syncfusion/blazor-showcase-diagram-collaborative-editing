using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Diagram;
using Syncfusion.Blazor.Diagram.SymbolPalette;
using System.Xml.Linq;
using Orientation = Syncfusion.Blazor.Diagram.Orientation;

namespace DiagramCollaboration
{
    public partial class DiagramSymbolpalette
    {
        public string? OrPathData { get; set; }
        public string? AndPathData { get; set; }
        public string? NotPathData { get; set; }
        public string? NorPathData { get; set; }
        public string? NandPathData { get; set; }
        public string? BufferPathData { get; set; }
        public string? NorGatePathData { get; set; }
        public string? XorGatePathData { get; set; }
        public string? EmitterPathData { get; set; }
        public string? NandGatePathData { get; set; }
        public string? LatchPathData { get; set; }
        public string? FlipFlopPathData { get; set; }
        public string? SyncPathData { get; set; }
        public string? JKPathData { get; set; }
        public string? DPathData { get; set; }
        public DiagramObjectCollection<PointPort>? OrGatePorts { get; set; }
        public DiagramObjectCollection<PointPort>? OrGatePalettePorts { get; set; }
        public DiagramObjectCollection<PointPort>? NotGatePorts { get; set; }
        public DiagramObjectCollection<PointPort>? AndGatePorts { get; set; }
        public DiagramObjectCollection<PointPort>? AndGatePalettePorts { get; set; }
        public DiagramObjectCollection<PointPort>? FlipFlopPorts { get; set; }
        public DiagramObjectCollection<PointPort>? JKFlipFlopPorts { get; set; }
        public DiagramObjectCollection<PointPort>? JKFlipFlopPalettePorts { get; set; }
        public DiagramObjectCollection<PointPort>? JKFlipFlopPalettePortsAlt { get; set; }
        public DiagramObjectCollection<PointPort>? ResistorPorts { get; set; }
        public DiagramObjectCollection<PointPort>? ResistorPalettePorts { get; set; }

        private void InitializePalettes()
        {
            FlowShapeList = new DiagramObjectCollection<NodeBase>();
            CreatePaletteNode(NodeFlowShapes.Terminator, "Terminator");
            CreatePaletteNode(NodeFlowShapes.Process, "Process");
            CreatePaletteNode(NodeFlowShapes.Decision, "Decision");
            CreatePaletteNode(NodeFlowShapes.Document, "Document");
            CreatePaletteNode(NodeFlowShapes.PreDefinedProcess, "PredefinedProcess");
            CreatePaletteNode(NodeFlowShapes.DirectData, "DirectData");
            CreatePaletteNode(NodeFlowShapes.Sort, "Sort");
            CreatePaletteNode(NodeFlowShapes.InternalStorage, "InternalStorage");
            CreatePaletteNode(NodeFlowShapes.SequentialData, "SequentialData");
            CreatePaletteNode(NodeFlowShapes.PaperTap, "PunchedTape");
            CreatePaletteNode(NodeFlowShapes.MultiDocument, "Multidocument");
            CreatePaletteNode(NodeFlowShapes.Collate, "Collate");
            CreatePaletteNode(NodeFlowShapes.SummingJunction, "SummingJunction");
            CreatePaletteNode(NodeFlowShapes.Or, "Or");
            CreatePaletteNode(NodeFlowShapes.Extract, "Extract");

            FlowShapePalette = new Palette() { Symbols = FlowShapeList, Title = "Flow Shapes", ID = "Flow Shapes" };

            BasicShapeList = new DiagramObjectCollection<NodeBase>();
            CreateBasicPaletteNode(NodeBasicShapes.Rectangle, "Rectangle");
            CreateBasicPaletteNode(NodeBasicShapes.Ellipse, "Ellipse");
            CreateBasicPaletteNode(NodeBasicShapes.Hexagon, "Hexagon");
            CreateBasicPaletteNode(NodeBasicShapes.Parallelogram, "Parallelogram");
            CreateBasicPaletteNode(NodeBasicShapes.Triangle, "Triangle");
            CreateBasicPaletteNode(NodeBasicShapes.Plus, "Plus");
            CreateBasicPaletteNode(NodeBasicShapes.Star, "Star");
            CreateBasicPaletteNode(NodeBasicShapes.Pentagon, "Pentagon");
            CreateBasicPaletteNode(NodeBasicShapes.Heptagon, "Heptagon");
            CreateBasicPaletteNode(NodeBasicShapes.Octagon, "Octagon");
            CreateBasicPaletteNode(NodeBasicShapes.Trapezoid, "Trapezoid");
            CreateBasicPaletteNode(NodeBasicShapes.Decagon, "Decagon");
            CreateBasicPaletteNode(NodeBasicShapes.RightTriangle, "RightTriangle");
            CreateBasicPaletteNode(NodeBasicShapes.Cylinder, "Cylinder");
            CreateBasicPaletteNode(NodeBasicShapes.Diamond, "Diamond");
            BasicShapePalette = new Palette() { Symbols = BasicShapeList, Title = "Basic Shapes", ID = "Basic Shapes" };

            ConnectorList = new DiagramObjectCollection<NodeBase>();
            CreatePaletteConnector("OrthogonalWithArrow", ConnectorSegmentType.Orthogonal, DecoratorShape.Arrow);
            CreatePaletteConnector("Orthogonal", ConnectorSegmentType.Orthogonal, DecoratorShape.None);
            CreatePaletteConnector("StraightWithArrow", ConnectorSegmentType.Straight, DecoratorShape.Arrow);
            CreatePaletteConnector("Straight", ConnectorSegmentType.Straight, DecoratorShape.None);
            CreatePaletteConnector("BezierWithArrow", ConnectorSegmentType.Bezier, DecoratorShape.Arrow);
            ConnectorPalette = new Palette() { Symbols = ConnectorList, Title = "Connectors", ID = "Connector Shapes" };

            InitializeBpmnShapes();
        }

        private void CreatePaletteConnector(string id, ConnectorSegmentType type, DecoratorShape decoratorShape)
        {
            Connector diagramConnector = new Connector()
            {
                ID = id,
                Type = type,
                SourcePoint = new Syncfusion.Blazor.Diagram.DiagramPoint() { X = 0, Y = 0 },
                TargetPoint = new Syncfusion.Blazor.Diagram.DiagramPoint() { X = 60, Y = 60 },
                Style = new ShapeStyle() { StrokeWidth = 2 },
                TargetDecorator = new DecoratorSettings() { Shape = decoratorShape }
            };

            ConnectorList.Add(diagramConnector);
        }
        private void CreatePaletteNode(NodeFlowShapes flowShape, string id)
        {
            Node diagramNode = new Node()
            {
                ID = id,
                Shape = new FlowShape() { Type = NodeShapes.Flow, Shape = flowShape },
                SearchTags = new List<string>() { "Flow" },
                Style = new ShapeStyle() { StrokeWidth = 2 }
            };
            if (id == "Terminator") 
            {
                diagramNode.Height = 37;
                diagramNode.Width = 94;
            }
            if (id == "Process" || id == "Decision" || id == "Sort" || id == "Document" || id == "MultiDocument" || id == "PaperTap" || id == "DirectData" || id == "PredefinedProcess" || id == "InternalStorage")
            {
                diagramNode.Height = 56;
                diagramNode.Width = 94;
            }
            if (id == "SequentialData")
            {
                diagramNode.Height = 94;
                diagramNode.Width = 94;
            }
            if (id == "Collate" || id == "SummingJunction" || id == "Or" || id == "Extract")
            {
                diagramNode.Height = 75;
                diagramNode.Width = 75;
            }
            FlowShapeList.Add(diagramNode);
        }

        private void CreateBasicPaletteNode(NodeBasicShapes basicShape, string id)
        {
            Node diagramNode = new Node()
            {
                ID = id,
                Shape = new BasicShape() { Type = NodeShapes.Basic, Shape = basicShape },
                SearchTags = new List<string>() { "Basic" },
                Style = new ShapeStyle() { StrokeWidth = 2 }
            };
            if(id == "Rectangle" || id == "Ellipse" || id == "RightTriangle" || id == "Diamond")
            {
                diagramNode.Height = 113;
                diagramNode.Width = 150;
            }
            if(id == "Hexagon" || id == "Triangle" || id == "Plus" || id == "Pentagon" || id == "Hexagon" || id == "Heptagon" || id == "Octagon" || id == "Trapezoid")
            {
                diagramNode.Height = 150;
                diagramNode.Width = 150;
            }
            if(id == "Parallelogram")
            {
                diagramNode.Height = 113;
                diagramNode.Width = 150;
            }
            if (id== "Decagon" || id == "Cylinder" || id == "Star")
            {
                diagramNode.Height = 113;
                diagramNode.Width = 113;
            }
            BasicShapeList.Add(diagramNode);
        }

        public void InitializeNetworkShapes()
        {
            NetworkShapesList = new DiagramObjectCollection<NodeBase>();
            Node NetworkShapes0 = new Node()
            {
                SearchTags = new List<string>() { "Network" },
                ID = "NetworkuKVMSwitch1",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M2.1,32.8157654 C1.49248678,32.8157654 1,32.3232787 1,31.7157654 L1,16.1 C1,15.4924868 1.49248678,15 2.1,15 L47.9,15 C48.5075132,15 49,15.4924868 49,16.1 L49,31.7157654 C49,32.3232787 48.5075132,32.8157654 47.9,32.8157654 L2.1,32.8157654 Z"
                },
                Width = 48,
                Height = 17.815765380859375,
                OffsetX = 25,
                OffsetY = 23.907882690429688,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes0);
            Node NetworkShapes1 = new Node()
            {
                SearchTags = new List<string>() { "Network" },
                ID = "NetworkuKVMSwitch2",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M47,30.8157654 L3,30.8157654 L3,17 L47,17 L47,30.8157654 Z M9.1875,26.0625 C9.1875,25.269 8.5435,24.625 7.75,24.625 C6.95554167,24.625 6.3125,25.269 6.3125,26.0625 C6.3125,26.856 6.95554167,27.5 7.75,27.5 C8.5435,27.5 9.1875,26.856 9.1875,26.0625 Z M19.25,26.0625 C19.25,25.269 18.606,24.625 17.8125,24.625 C17.0180417,24.625 16.375,25.269 16.375,26.0625 C16.375,26.856 17.0180417,27.5 17.8125,27.5 C18.606,27.5 19.25,26.856 19.25,26.0625 Z M13.5,26.0625 C13.5,25.269 12.856,24.625 12.0625,24.625 C11.2680417,24.625 10.625,25.269 10.625,26.0625 C10.625,26.856 11.2680417,27.5 12.0625,27.5 C12.856,27.5 13.5,26.856 13.5,26.0625 Z M23.5625,26.0625 C23.5625,25.269 22.9185,24.625 22.125,24.625 C21.3305417,24.625 20.6875,25.269 20.6875,26.0625 C20.6875,26.856 21.3305417,27.5 22.125,27.5 C22.9185,27.5 23.5625,26.856 23.5625,26.0625 Z M29.3125,26.0625 C29.3125,25.269 28.6685,24.625 27.875,24.625 C27.0805417,24.625 26.4375,25.269 26.4375,26.0625 C26.4375,26.856 27.0805417,27.5 27.875,27.5 C28.6685,27.5 29.3125,26.856 29.3125,26.0625 Z M33.625,26.0625 C33.625,25.269 32.981,24.625 32.1875,24.625 C31.3930417,24.625 30.75,25.269 30.75,26.0625 C30.75,26.856 31.3930417,27.5 32.1875,27.5 C32.981,27.5 33.625,26.856 33.625,26.0625 Z M39.375,26.0625 C39.375,25.269 38.731,24.625 37.9375,24.625 C37.1430417,24.625 36.5,25.269 36.5,26.0625 C36.5,26.856 37.1430417,27.5 37.9375,27.5 C38.731,27.5 39.375,26.856 39.375,26.0625 Z M43.6875,26.0625 C43.6875,25.269 43.0435,24.625 42.25,24.625 C41.4555417,24.625 40.8125,25.269 40.8125,26.0625 C40.8125,26.856 41.4555417,27.5 42.25,27.5 C43.0435,27.5 43.6875,26.856 43.6875,26.0625 Z"
                },
                Width = 44,
                Height = 13.815765380859375,
                OffsetX = 25,
                OffsetY = 23.907882690429688,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes1);
            Node NetworkShapes2 = new Node()
            {
                ID = "NetworkuKVMSwitch3",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M9.1875,26.0625 C9.1875,26.856 8.5435,27.5 7.75,27.5 C6.95554167,27.5 6.3125,26.856 6.3125,26.0625 C6.3125,25.269 6.95554167,24.625 7.75,24.625 C8.5435,24.625 9.1875,25.269 9.1875,26.0625 Z M19.25,26.0625 C19.25,26.856 18.606,27.5 17.8125,27.5 C17.0180417,27.5 16.375,26.856 16.375,26.0625 C16.375,25.269 17.0180417,24.625 17.8125,24.625 C18.606,24.625 19.25,25.269 19.25,26.0625 Z M29.3125,26.0625 C29.3125,26.856 28.6685,27.5 27.875,27.5 C27.0805417,27.5 26.4375,26.856 26.4375,26.0625 C26.4375,25.269 27.0805417,24.625 27.875,24.625 C28.6685,24.625 29.3125,25.269 29.3125,26.0625 Z M39.375,26.0625 C39.375,26.856 38.731,27.5 37.9375,27.5 C37.1430417,27.5 36.5,26.856 36.5,26.0625 C36.5,25.269 37.1430417,24.625 37.9375,24.625 C38.731,24.625 39.375,25.269 39.375,26.0625 Z"
                },
                Width = 33.0625,
                Height = 2.875,
                OffsetX = 22.84375,
                OffsetY = 26.0625,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes2);
            Node NetworkShapes3 = new Node()
            {
                SearchTags = new List<string>() { "Network" },
                ID = "NetworkuKVMSwitch4",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M6.3125,20.3125 L13.5,20.3125 L13.5,23.1875 L6.3125,23.1875 L6.3125,20.3125 Z M16.375,20.3125 L23.5625,20.3125 L23.5625,23.1875 L16.375,23.1875 L16.375,20.3125 Z M26.4375,20.3125 L33.625,20.3125 L33.625,23.1875 L26.4375,23.1875 L26.4375,20.3125 Z M36.5,20.3125 L43.6875,20.3125 L43.6875,23.1875 L36.5,23.1875 L36.5,20.3125 Z"
                },
                Width = 37.375,
                Height = 2.875,
                OffsetX = 25,
                OffsetY = 21.75,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes3);
            NodeGroup NetworkShapes4 = new NodeGroup()
            {
                SearchTags = new List<string>() { "Network" },
                ID = "NetworkSwitch",
                Children = new string[] {
               "NetworkuKVMSwitch1",
               "NetworkuKVMSwitch2",
               "NetworkuKVMSwitch3",
               "NetworkuKVMSwitch4"
            }
            };
            NetworkShapesList.Add(NetworkShapes4);
            Node NetworkShapes5 = new Node()
            {
                ID = "NetworkuSpacer1",
                SearchTags = new List<string>() { "Network" },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M2.1,33.3259514 C1.49248678,33.3259514 1,32.8334646 1,32.2259514 L1,16.1 C1,15.4924868 1.49248678,15 2.1,15 L47.9,15 C48.5075132,15 49,15.4924868 49,16.1 L49,32.2259514 C49,32.8334646 48.5075132,33.3259514 47.9,33.3259514 L2.1,33.3259514 Z"
                },
                Width = 48,
                Height = 18.325950622558594,
                OffsetX = 25,
                OffsetY = 24.162975311279297,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes5);
            Node NetworkShapes6 = new Node()
            {
                ID = "NetworkuSpacer2",
                SearchTags = new List<string>() { "Network" },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M3 31.3259514 47 31.3259514 47 17 3 17"
                },
                Width = 44,
                Height = 14.325950622558594,
                OffsetX = 25,
                OffsetY = 24.162975311279297,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes6);
            Node NetworkShapes7 = new Node()
            {
                ID = "NetworkuSpacer3",
                SearchTags = new List<string>() { "Network" },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M6.4516129,27.8709677 L6.4516129,20.4516129 L7.93548387,20.4516129 L7.93548387,27.8709677 L6.4516129,27.8709677 Z M24.2580645,27.8709677 L24.2580645,20.4516129 L25.7419355,20.4516129 L25.7419355,27.8709677 L24.2580645,27.8709677 Z M12.3870968,27.8709677 L12.3870968,20.4516129 L13.8709677,20.4516129 L13.8709677,27.8709677 L12.3870968,27.8709677 Z M30.1935484,27.8709677 L30.1935484,20.4516129 L31.6774194,20.4516129 L31.6774194,27.8709677 L30.1935484,27.8709677 Z M18.3225806,27.8709677 L18.3225806,20.4516129 L19.8064516,20.4516129 L19.8064516,27.8709677 L18.3225806,27.8709677 Z M36.1290323,27.8709677 L36.1290323,20.4516129 L37.6129032,20.4516129 L37.6129032,27.8709677 L36.1290323,27.8709677 Z M9.41935484,27.8709677 L9.41935484,20.4516129 L10.9032258,20.4516129 L10.9032258,27.8709677 L9.41935484,27.8709677 Z M27.2258065,27.8709677 L27.2258065,20.4516129 L28.7096774,20.4516129 L28.7096774,27.8709677 L27.2258065,27.8709677 Z M15.3548387,27.8709677 L15.3548387,20.4516129 L16.8387097,20.4516129 L16.8387097,27.8709677 L15.3548387,27.8709677 Z M33.1612903,27.8709677 L33.1612903,20.4516129 L34.6451613,20.4516129 L34.6451613,27.8709677 L33.1612903,27.8709677 Z M42.0645161,27.8709677 L42.0645161,20.4516129 L43.5483871,20.4516129 L43.5483871,27.8709677 L42.0645161,27.8709677 Z M21.2903226,27.8709677 L21.2903226,20.4516129 L22.7741935,20.4516129 L22.7741935,27.8709677 L21.2903226,27.8709677 Z M39.0967742,27.8709677 L39.0967742,20.4516129 L40.5806452,20.4516129 L40.5806452,27.8709677 L39.0967742,27.8709677 Z"
                },
                Width = 37.096771240234375,
                Height = 7.419355392456055,
                OffsetX = 24.999998569488525,
                OffsetY = 24.161290168762207,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes7);
            NodeGroup NetworkShapes8 = new NodeGroup()
            {
                SearchTags = new List<string>() { "Network" },
                ID = "NetworkSpacer",
                Children = new string[] {
               "NetworkuSpacer1",
               "NetworkuSpacer2",
               "NetworkuSpacer3"
            }
            };
            NetworkShapesList.Add(NetworkShapes8);
            Node NetworkShapes9 = new Node()
            {
                ID = "NetworkuTray1",
                SearchTags = new List<string>() { "Network" },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M1,32.7098224 L1,15.1 C1,14.4924868 1.49248678,14 2.1,14 L47.9,14 C48.5075132,14 49,14.4924868 49,15.1 L49,32.7098224 C49,33.3173356 48.5075132,33.8098224 47.9,33.8098224 L2.1,33.8098224 C1.49248678,33.8098224 1,33.3173356 1,32.7098224 Z"
                },
                Width = 48,
                Height = 19.80982208251953,
                OffsetX = 25,
                OffsetY = 23.904911041259766,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes9);
            Node NetworkShapes10 = new Node()
            {
                SearchTags = new List<string>() { "Network" },
                ID = "NetworkuTray2",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M3,31.8098224 L3,16 L47,16 L47,31.8098224 L3,31.8098224 Z M25.7419355,29.8387097 C29.0200127,29.8387097 31.6774194,27.181303 31.6774194,23.9032258 C31.6774194,20.6251486 29.0200127,17.9677419 25.7419355,17.9677419 C22.4638583,17.9677419 19.8064516,20.6251486 19.8064516,23.9032258 C19.8064516,27.181303 22.4638583,29.8387097 25.7419355,29.8387097 Z"
                },
                Width = 44,
                Height = 15.809822082519531,
                OffsetX = 25,
                OffsetY = 23.904911041259766,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes10);
            Node NetworkShapes11 = new Node()
            {
                SearchTags = new List<string>() { "Network" },
                ID = "NetworkuTray3",
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Rectangle
                },
                Width = 2.9677419662475586,
                Height = 5.935483932495117,
                OffsetX = 25.74193525314331,
                OffsetY = 23.903225898742676,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes11);
            NodeGroup NetworkShapes12 = new NodeGroup()
            {
                ID = "NetworkTray",
                SearchTags = new List<string>() { "Network" },
                Children = new string[] {
               "NetworkuTray1",
               "NetworkuTray2",
               "NetworkuTray3"
            }
            };
            NetworkShapesList.Add(NetworkShapes12);
            Node NetworkShapes13 = new Node()
            {
                ID = "NetworkuLCDCopy1",
                SearchTags = new List<string>() { "Network" },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M2.1,30.6875 C1.49248678,30.6875 1,30.1950132 1,29.5875 L1,12.5342346 C1,11.9267213 1.49248678,11.4342346 2.1,11.4342346 L47.9,11.4342346 C48.5075132,11.4342346 49,11.9267213 49,12.5342346 L49,29.5875 C49,30.1950132 48.5075132,30.6875 47.9,30.6875 L2.1,30.6875 Z"
                },
                Width = 48,
                Height = 19.253265380859375,
                OffsetX = 25,
                OffsetY = 21.060867309570312,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes13);
            Node NetworkShapes14 = new Node()
            {
                SearchTags = new List<string>() { "Network" },
                ID = "NetworkuLCDCopy2",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M3 28.6875 47 28.6875 47 13.4342346 3 13.4342346"
                },
                Width = 44,
                Height = 15.253265380859375,
                OffsetX = 25,
                OffsetY = 21.060867309570312,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes14);
            Node NetworkShapes15 = new Node()
            {
                SearchTags = new List<string>() { "Network" },
                ID = "NetworkuLCDCopy3",
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M6.3125,16.75 L9.1875,16.75 L9.1875,19.625 L6.3125,19.625 L6.3125,16.75 Z M6.3125,22.5 L9.1875,22.5 L9.1875,25.375 L6.3125,25.375 L6.3125,22.5 Z M12.0625,16.75 L14.9375,16.75 L14.9375,19.625 L12.0625,19.625 L12.0625,16.75 Z M12.0625,22.5 L14.9375,22.5 L14.9375,25.375 L12.0625,25.375 L12.0625,22.5 Z M17.8125,16.75 L20.6875,16.75 L20.6875,19.625 L17.8125,19.625 L17.8125,16.75 Z M17.8125,22.5 L20.6875,22.5 L20.6875,25.375 L17.8125,25.375 L17.8125,22.5 Z M23.5625,16.75 L26.4375,16.75 L26.4375,19.625 L23.5625,19.625 L23.5625,16.75 Z M23.5625,22.5 L26.4375,22.5 L26.4375,25.375 L23.5625,25.375 L23.5625,22.5 Z M29.3125,16.75 L32.1875,16.75 L32.1875,19.625 L29.3125,19.625 L29.3125,16.75 Z M29.3125,22.5 L32.1875,22.5 L32.1875,25.375 L29.3125,25.375 L29.3125,22.5 Z M35.0625,16.75 L37.9375,16.75 L37.9375,19.625 L35.0625,19.625 L35.0625,16.75 Z M35.0625,22.5 L37.9375,22.5 L37.9375,25.375 L35.0625,25.375 L35.0625,22.5 Z M40.8125,16.75 L43.6875,16.75 L43.6875,19.625 L40.8125,19.625 L40.8125,16.75 Z M40.8125,22.5 L43.6875,22.5 L43.6875,25.375 L40.8125,25.375 L40.8125,22.5 Z"
                },
                Width = 37.375,
                Height = 8.625,
                OffsetX = 25,
                OffsetY = 21.0625,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes15);
            NodeGroup NetworkShapes16 = new NodeGroup()
            {
                ID = "NetworkLCD-Copy",
                SearchTags = new List<string>() { "Network" },
                Children = new string[] {
               "NetworkuLCDCopy1",
               "NetworkuLCDCopy2",
               "NetworkuLCDCopy3"
            }
            };
            NetworkShapesList.Add(NetworkShapes16);
            Node NetworkShapes17 = new Node()
            {
                ID = "NetworkuLCD1",
                SearchTags = new List<string>() { "Network" },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M2.1,45.3298502 C1.49248678,45.3298502 1,44.8373634 1,44.2298502 L1,7.6298502 C1,7.02233697 1.49248678,6.5298502 2.1,6.5298502 L47.9,6.5298502 C48.5075132,6.5298502 49,7.02233697 49,7.6298502 L49,44.2298502 C49,44.8373634 48.5075132,45.3298502 47.9,45.3298502 L2.1,45.3298502 Z"
                },
                Width = 48,
                Height = 38.79999923706055,
                OffsetX = 25,
                OffsetY = 25.92984962463379,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes17);
            Node NetworkShapes18 = new Node()
            {
                ID = "NetworkuLCD2",
                SearchTags = new List<string>() { "Network" },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M3 43.3298502 47 43.3298502 47 8.5298502 3 8.5298502"
                },
                Width = 44,
                Height = 34.79999923706055,
                OffsetX = 25,
                OffsetY = 25.92984962463379,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes18);
            Node NetworkShapes19 = new Node()
            {
                ID = "NetworkuLCD3",
                SearchTags = new List<string>() { "Network" },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M40.3333333,39.7298502 C40.3333333,38.8834502 41.0187333,38.1965169 41.8666667,38.1965169 C42.7146,38.1965169 43.4,38.8834502 43.4,39.7298502 C43.4,40.5762502 42.7146,41.2631835 41.8666667,41.2631835 C41.0187333,41.2631835 40.3333333,40.5762502 40.3333333,39.7298502 Z M35.7333333,39.7298502 C35.7333333,38.8834502 36.4187333,38.1965169 37.2666667,38.1965169 C38.1146,38.1965169 38.8,38.8834502 38.8,39.7298502 C38.8,40.5762502 38.1146,41.2631835 37.2666667,41.2631835 C36.4187333,41.2631835 35.7333333,40.5762502 35.7333333,39.7298502 Z M7.6,36.6631835 C7.04771525,36.6631835 6.6,36.2154683 6.6,35.6631835 L6.6,13.1298502 C6.6,12.5775654 7.04771525,12.1298502 7.6,12.1298502 L42.4,12.1298502 C42.9522847,12.1298502 43.4,12.5775654 43.4,13.1298502 L43.4,35.6631835 C43.4,36.2154683 42.9522847,36.6631835 42.4,36.6631835 L7.6,36.6631835 Z"
                },
                Width = 36.80000305175781,
                Height = 29.133333206176758,
                OffsetX = 25.000001430511475,
                OffsetY = 26.69651699066162,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes19);
            NodeGroup NetworkShapes20 = new NodeGroup()
            {
                ID = "NetworkLCD",
                SearchTags = new List<string>() { "Network" },
                Children = new string[] {
               "NetworkuLCD1",
               "NetworkuLCD2",
               "NetworkuLCD3"
            }
            };
            NetworkShapesList.Add(NetworkShapes20);
            Node NetworkShapes42 = new Node()
            {
                ID = "NetworkFax1",
                SearchTags = new List<string>() { "Network" },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M21.3046875,9.21428571 L21.3046875,2.2953125 C21.3046875,1.96394165 21.5733167,1.6953125 21.9046875,1.6953125 L41.2649554,1.6953125 C41.5963262,1.6953125 41.8649554,1.96394165 41.8649554,2.2953125 L41.8649554,9.21428571 L46.6142857,9.21428571 C47.2217989,9.21428571 47.7142857,9.70677249 47.7142857,10.3142857 L47.7142857,47.9 C47.7142857,48.5075132 47.2217989,49 46.6142857,49 L4.1,49 C3.49248678,49 3,48.5075132 3,47.9 L3,10.3142857 C3,9.70677249 3.49248678,9.21428571 4.1,9.21428571 L21.3046875,9.21428571 Z"
                },
                Width = 44.71428680419922,
                Height = 47.3046875,
                OffsetX = 25.35714340209961,
                OffsetY = 25.34765625,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes42);
            Node NetworkShapes43 = new Node()
            {
                ID = "NetworkFax2",
                SearchTags = new List<string>() { "Network" },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M41.864603,16.2857143 C41.8648373,16.2788235 41.8649554,16.2719032 41.8649554,16.2649554 L41.8649554,10.5864662 L46.4285714,10.5864662 L46.4285714,47.6278195 L4.28571429,47.6278195 L4.28571429,10.5864662 L21.3046875,10.5864662 L21.3046875,16.2649554 C21.3046875,16.2719032 21.3048056,16.2788235 21.3050399,16.2857143 L20.4285714,16.2857143 C20.1524291,16.2857143 19.9285714,16.5095719 19.9285714,16.7857143 C19.9285714,17.0618567 20.1524291,17.2857143 20.4285714,17.2857143 L42.9685719,17.2857143 C43.2447142,17.2857143 43.4685719,17.0618567 43.4685719,16.7857143 C43.4685719,16.5095719 43.2447142,16.2857143 42.9685719,16.2857143 L41.864603,16.2857143 Z M10.0714286,40.9285714 C10.0714286,41.4808562 10.5191438,41.9285714 11.0714286,41.9285714 L16.6428571,41.9285714 C17.1951419,41.9285714 17.6428571,41.4808562 17.6428571,40.9285714 L17.6428571,17.2857143 C17.6428571,16.7334295 17.1951419,16.2857143 16.6428571,16.2857143 L11.0714286,16.2857143 C10.5191438,16.2857143 10.0714286,16.7334295 10.0714286,17.2857143 L10.0714286,40.9285714 Z M42.1534729,24.1534729 L42.1534729,20.2249015 C42.1534729,19.6726167 41.7057577,19.2249015 41.1534729,19.2249015 L22.7750985,19.2249015 C22.2228138,19.2249015 21.7750985,19.6726167 21.7750985,20.2249015 L21.7750985,24.1534729 C21.7750985,24.7057577 22.2228138,25.1534729 22.7750985,25.1534729 L41.1534729,25.1534729 C41.7057577,25.1534729 42.1534729,24.7057577 42.1534729,24.1534729 Z M23.7142857,30.4285714 L25.3571429,30.4285714 C25.6332852,30.4285714 25.8571429,30.2047138 25.8571429,29.9285714 C25.8571429,29.6524291 25.6332852,29.4285714 25.3571429,29.4285714 L23.7142857,29.4285714 C23.4381433,29.4285714 23.2142857,29.6524291 23.2142857,29.9285714 C23.2142857,30.2047138 23.4381433,30.4285714 23.7142857,30.4285714 Z M28.6428571,30.4285714 L30.2857143,30.4285714 C30.5618567,30.4285714 30.7857143,30.2047138 30.7857143,29.9285714 C30.7857143,29.6524291 30.5618567,29.4285714 30.2857143,29.4285714 L28.6428571,29.4285714 C28.3667148,29.4285714 28.1428571,29.6524291 28.1428571,29.9285714 C28.1428571,30.2047138 28.3667148,30.4285714 28.6428571,30.4285714 Z M33.5714286,30.4285714 L35.2142857,30.4285714 C35.4904281,30.4285714 35.7142857,30.2047138 35.7142857,29.9285714 C35.7142857,29.6524291 35.4904281,29.4285714 35.2142857,29.4285714 L33.5714286,29.4285714 C33.2952862,29.4285714 33.0714286,29.6524291 33.0714286,29.9285714 C33.0714286,30.2047138 33.2952862,30.4285714 33.5714286,30.4285714 Z M38.5,30.4285714 L40.1428571,30.4285714 C40.4189995,30.4285714 40.6428571,30.2047138 40.6428571,29.9285714 C40.6428571,29.6524291 40.4189995,29.4285714 40.1428571,29.4285714 L38.5,29.4285714 C38.2238576,29.4285714 38,29.6524291 38,29.9285714 C38,30.2047138 38.2238576,30.4285714 38.5,30.4285714 Z M23.7142857,33.7142857 L25.3571429,33.7142857 C25.6332852,33.7142857 25.8571429,33.4904281 25.8571429,33.2142857 C25.8571429,32.9381433 25.6332852,32.7142857 25.3571429,32.7142857 L23.7142857,32.7142857 C23.4381433,32.7142857 23.2142857,32.9381433 23.2142857,33.2142857 C23.2142857,33.4904281 23.4381433,33.7142857 23.7142857,33.7142857 Z M28.6428571,33.7142857 L30.2857143,33.7142857 C30.5618567,33.7142857 30.7857143,33.4904281 30.7857143,33.2142857 C30.7857143,32.9381433 30.5618567,32.7142857 30.2857143,32.7142857 L28.6428571,32.7142857 C28.3667148,32.7142857 28.1428571,32.9381433 28.1428571,33.2142857 C28.1428571,33.4904281 28.3667148,33.7142857 28.6428571,33.7142857 Z M33.5714286,33.7142857 L35.2142857,33.7142857 C35.4904281,33.7142857 35.7142857,33.4904281 35.7142857,33.2142857 C35.7142857,32.9381433 35.4904281,32.7142857 35.2142857,32.7142857 L33.5714286,32.7142857 C33.2952862,32.7142857 33.0714286,32.9381433 33.0714286,33.2142857 C33.0714286,33.4904281 33.2952862,33.7142857 33.5714286,33.7142857 Z M38.5,33.7142857 L40.1428571,33.7142857 C40.4189995,33.7142857 40.6428571,33.4904281 40.6428571,33.2142857 C40.6428571,32.9381433 40.4189995,32.7142857 40.1428571,32.7142857 L38.5,32.7142857 C38.2238576,32.7142857 38,32.9381433 38,33.2142857 C38,33.4904281 38.2238576,33.7142857 38.5,33.7142857 Z M23.7142857,37 L25.3571429,37 C25.6332852,37 25.8571429,36.7761424 25.8571429,36.5 C25.8571429,36.2238576 25.6332852,36 25.3571429,36 L23.7142857,36 C23.4381433,36 23.2142857,36.2238576 23.2142857,36.5 C23.2142857,36.7761424 23.4381433,37 23.7142857,37 Z M28.6428571,37 L30.2857143,37 C30.5618567,37 30.7857143,36.7761424 30.7857143,36.5 C30.7857143,36.2238576 30.5618567,36 30.2857143,36 L28.6428571,36 C28.3667148,36 28.1428571,36.2238576 28.1428571,36.5 C28.1428571,36.7761424 28.3667148,37 28.6428571,37 Z M33.5714286,37 L35.2142857,37 C35.4904281,37 35.7142857,36.7761424 35.7142857,36.5 C35.7142857,36.2238576 35.4904281,36 35.2142857,36 L33.5714286,36 C33.2952862,36 33.0714286,36.2238576 33.0714286,36.5 C33.0714286,36.7761424 33.2952862,37 33.5714286,37 Z M38.5,37 L40.1428571,37 C40.4189995,37 40.6428571,36.7761424 40.6428571,36.5 C40.6428571,36.2238576 40.4189995,36 40.1428571,36 L38.5,36 C38.2238576,36 38,36.2238576 38,36.5 C38,36.7761424 38.2238576,37 38.5,37 Z M23.7142857,40.2857143 L25.3571429,40.2857143 C25.6332852,40.2857143 25.8571429,40.0618567 25.8571429,39.7857143 C25.8571429,39.5095719 25.6332852,39.2857143 25.3571429,39.2857143 L23.7142857,39.2857143 C23.4381433,39.2857143 23.2142857,39.5095719 23.2142857,39.7857143 C23.2142857,40.0618567 23.4381433,40.2857143 23.7142857,40.2857143 Z M28.6428571,40.2857143 L30.2857143,40.2857143 C30.5618567,40.2857143 30.7857143,40.0618567 30.7857143,39.7857143 C30.7857143,39.5095719 30.5618567,39.2857143 30.2857143,39.2857143 L28.6428571,39.2857143 C28.3667148,39.2857143 28.1428571,39.5095719 28.1428571,39.7857143 C28.1428571,40.0618567 28.3667148,40.2857143 28.6428571,40.2857143 Z M33.5714286,40.2857143 L35.2142857,40.2857143 C35.4904281,40.2857143 35.7142857,40.0618567 35.7142857,39.7857143 C35.7142857,39.5095719 35.4904281,39.2857143 35.2142857,39.2857143 L33.5714286,39.2857143 C33.2952862,39.2857143 33.0714286,39.5095719 33.0714286,39.7857143 C33.0714286,40.0618567 33.2952862,40.2857143 33.5714286,40.2857143 Z M38.5,40.2857143 L40.1428571,40.2857143 C40.4189995,40.2857143 40.6428571,40.0618567 40.6428571,39.7857143 C40.6428571,39.5095719 40.4189995,39.2857143 40.1428571,39.2857143 L38.5,39.2857143 C38.2238576,39.2857143 38,39.5095719 38,39.7857143 C38,40.0618567 38.2238576,40.2857143 38.5,40.2857143 Z M41.1534729,24.1534729 L22.7750985,24.1534729 L22.7750985,20.2249015 L41.1534729,20.2249015 L41.1534729,24.1534729 Z M16.6428571,40.9285714 L11.0714286,40.9285714 L11.0714286,17.2857143 L16.6428571,17.2857143 L16.6428571,40.9285714 Z"
                },
                Width = 42.14285659790039,
                Height = 37.041351318359375,
                OffsetX = 25.357142448425293,
                OffsetY = 29.107141494750977,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes43);
            Node NetworkShapes44 = new Node()
            {
                ID = "NetworkFax3",
                SearchTags = new List<string>() { "Network" },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M38.1696429 18.5602679 38.1696429 0 25 2.22044605e-16 25 18.5602679"
                },
                Width = 13.16964340209961,
                Height = 18.56026840209961,
                OffsetX = 31.584821701049805,
                OffsetY = 9.280134201049805,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            NetworkShapesList.Add(NetworkShapes44);
            NodeGroup NetworkShapes45 = new NodeGroup()
            {
                ID = "NetworkFax",
                SearchTags = new List<string>() { "Network" },
                Children = new string[] {
               "NetworkFax1",
               "NetworkFax2",
               "NetworkFax3"
            }
            };
            NetworkShapesList.Add(NetworkShapes45);
            NetworkShapes = new Palette() { ID = "NetworkShapes", IsExpanded = false, Symbols = NetworkShapesList, Title = "Network Shapes" };
        }

        public void InitializeElectricalShapes()
        {
            ElectricalShapeList = new DiagramObjectCollection<NodeBase>();
            DPathData = "M21.2,13.5h57v73h-57V13.5z M78.2,29.5h20 M78.2,70.5h20 M1.2,29.5h20 M1.2,70.5h20 M26.6,33.4  c0.6,0,1.2-0.1,1.7-0.4c0.5-0.3,0.8-0.7,1.1-1.2c0.3-0.5,0.4-1.1,0.4-1.8v-0.5c0-0.7-0.1-1.2-0.4-1.7c-0.3-0.5-0.6-0.9-1.1-1.2  s-1-0.4-1.6-0.4h-2v7.1H26.6z M26.7,27c0.7,0,1.2,0.2,1.6,0.7c0.4,0.4,0.6,1.1,0.6,1.9V30c0,0.8-0.2,1.5-0.6,1.9  c-0.4,0.4-1,0.7-1.7,0.7h-1V27H26.7z M48.9,56.6c1,0,1.9-0.2,2.7-0.7c0.8-0.4,1.4-1.1,1.8-1.9c0.4-0.8,0.6-1.8,0.6-2.8v-0.6  c0-1.1-0.2-2-0.6-2.8c-0.4-0.8-1-1.5-1.8-1.9c-0.8-0.4-1.7-0.7-2.7-0.7h-3.4v11.4H48.9z M48.9,46.8c1,0,1.8,0.3,2.3,1  c0.5,0.7,0.8,1.6,0.8,2.8v0.6c0,1.2-0.3,2.1-0.8,2.8s-1.4,1-2.4,1h-1.3v-8.2H48.9z M74.1,29.6c0-0.7-0.1-1.3-0.4-1.8s-0.6-0.9-1-1.2  c-0.4-0.3-0.9-0.4-1.5-0.4c-0.6,0-1.1,0.1-1.5,0.4c-0.4,0.3-0.8,0.7-1,1.2s-0.4,1.1-0.4,1.8v0.5c0,0.7,0.1,1.3,0.4,1.8  c0.2,0.5,0.6,0.9,1,1.2s0.9,0.4,1.5,0.4c0.3,0,0.5,0,0.7-0.1l1.5,1.2l0.6-0.6l-1.3-1c0.4-0.3,0.7-0.7,0.9-1.2s0.3-1.1,0.3-1.8V29.6z   M73.1,30.1c0,0.8-0.2,1.5-0.5,1.9s-0.8,0.7-1.4,0.7s-1.1-0.2-1.4-0.7c-0.3-0.5-0.5-1.1-0.5-1.9v-0.5c0-0.8,0.2-1.4,0.5-1.9  c0.3-0.5,0.8-0.7,1.4-0.7c0.6,0,1.1,0.2,1.4,0.7c0.3,0.5,0.5,1.1,0.5,1.9V30.1z M74.1,70.6c0-0.7-0.1-1.3-0.4-1.8s-0.6-0.9-1-1.2  c-0.4-0.3-0.9-0.4-1.5-0.4c-0.6,0-1.1,0.1-1.5,0.4c-0.4,0.3-0.8,0.7-1,1.2s-0.4,1.1-0.4,1.8v0.5c0,0.7,0.1,1.3,0.4,1.8  c0.2,0.5,0.6,0.9,1,1.2s0.9,0.4,1.5,0.4c0.3,0,0.5,0,0.7-0.1l1.5,1.2l0.6-0.6l-1.3-1c0.4-0.3,0.7-0.7,0.9-1.2s0.3-1.1,0.3-1.8V70.6z   M73.1,71.1c0,0.8-0.2,1.5-0.5,1.9s-0.8,0.7-1.4,0.7s-1.1-0.2-1.4-0.7c-0.3-0.5-0.5-1.1-0.5-1.9v-0.5c0-0.8,0.2-1.4,0.5-1.9  c0.3-0.5,0.8-0.7,1.4-0.7c0.6,0,1.1,0.2,1.4,0.7c0.3,0.5,0.5,1.1,0.5,1.9V71.1z M68.2,65.5h6 M31.2,70.5l-10-5v10L31.2,70.5z";
            JKPathData = "M22.5,14.5h56v72h-56V14.5z M28.9,31.4c0,0.4-0.1,0.7-0.3,1s-0.5,0.4-0.9,0.4c-0.4,0-0.8-0.1-1-0.3  s-0.3-0.6-0.3-1h-0.9c0,0.7,0.2,1.2,0.6,1.5c0.4,0.3,0.9,0.5,1.6,0.5c0.6,0,1.2-0.2,1.6-0.5s0.6-0.9,0.6-1.5v-5.1h-0.9V31.4z   M30.4,73.4h1.1l-3-3.7l2.8-3.4h-1l-2.6,3.2H27v-3.2H26v7.1H27v-3.2h0.9L30.4,73.4z M74.5,29.2c0-0.9-0.3-1.6-0.8-2.2  c-0.5-0.6-1.2-0.9-2.1-0.9c-0.8,0-1.5,0.3-2,0.9s-0.8,1.3-0.8,2.2v1.3c0,0.9,0.3,1.6,0.8,2.2s1.2,0.9,2,0.9c0.2,0,0.3,0,0.5,0  c0.2,0,0.3-0.1,0.5-0.1l1.3,1.2l0.6-0.6l-1.2-1.1c0.4-0.3,0.7-0.6,0.9-1.1s0.3-0.9,0.3-1.4V29.2z M73.6,30.4c0,0.7-0.2,1.2-0.5,1.6  c-0.4,0.4-0.8,0.6-1.5,0.6c-0.6,0-1-0.2-1.4-0.6c-0.3-0.4-0.5-1-0.5-1.6v-1.3c0-0.7,0.2-1.2,0.5-1.6c0.3-0.4,0.8-0.6,1.4-0.6  c0.6,0,1.1,0.2,1.5,0.6s0.5,1,0.5,1.6V30.4z M74.5,70.2c0-0.9-0.3-1.6-0.8-2.2c-0.5-0.6-1.2-0.9-2.1-0.9c-0.8,0-1.5,0.3-2,0.9  s-0.8,1.3-0.8,2.2v1.3c0,0.9,0.3,1.6,0.8,2.2s1.2,0.9,2,0.9c0.2,0,0.3,0,0.5,0c0.2,0,0.3-0.1,0.5-0.1l1.3,1.2l0.6-0.6l-1.2-1.1  c0.4-0.3,0.7-0.6,0.9-1.1s0.3-0.9,0.3-1.4V70.2z M73.6,71.4c0,0.7-0.2,1.2-0.5,1.6c-0.4,0.4-0.8,0.6-1.5,0.6c-0.6,0-1-0.2-1.4-0.6  c-0.3-0.4-0.5-1-0.5-1.6v-1.3c0-0.7,0.2-1.2,0.5-1.6c0.3-0.4,0.8-0.6,1.4-0.6c0.6,0,1.1,0.2,1.5,0.6s0.5,1,0.5,1.6V71.4z M50.7,80.3  c0.4,0,0.7,0.1,0.9,0.3s0.3,0.5,0.3,0.9v0.6c0,0.2,0,0.4,0.1,0.7s0.1,0.4,0.2,0.5h0.9v-0.1c-0.1-0.1-0.2-0.3-0.3-0.4  s-0.1-0.4-0.1-0.6v-0.7c0-0.4-0.1-0.8-0.3-1.1c-0.2-0.3-0.5-0.5-0.9-0.6c0.4-0.2,0.7-0.4,0.9-0.6c0.2-0.3,0.3-0.6,0.3-1  c0-0.7-0.2-1.2-0.6-1.5c-0.4-0.3-1-0.5-1.8-0.5H48v7.1h0.9v-3.1H50.7z M48.9,77h1.6c0.5,0,0.9,0.1,1.1,0.3s0.4,0.5,0.4,0.9  c0,0.4-0.1,0.8-0.4,1c-0.2,0.2-0.6,0.3-1.2,0.3h-1.6V77z M51.5,23.4c-0.3,0.2-0.7,0.3-1.2,0.3c-0.5,0-0.9-0.1-1.3-0.4  c-0.3-0.2-0.5-0.6-0.5-1.1h-0.9c0,0.7,0.3,1.2,0.8,1.6c0.5,0.4,1.2,0.6,1.9,0.6c0.8,0,1.4-0.2,1.8-0.5c0.5-0.3,0.7-0.8,0.7-1.4  c0-0.5-0.2-1-0.6-1.3c-0.4-0.4-1-0.6-1.7-0.8c-0.7-0.2-1.1-0.3-1.4-0.5c-0.3-0.2-0.4-0.5-0.4-0.8c0-0.4,0.1-0.6,0.4-0.9  s0.6-0.3,1.1-0.3c0.5,0,0.9,0.1,1.2,0.4s0.4,0.6,0.4,1h0.9c0-0.6-0.2-1.1-0.7-1.5s-1.1-0.6-1.9-0.6c-0.7,0-1.3,0.2-1.8,0.5  c-0.4,0.4-0.7,0.8-0.7,1.4c0,0.5,0.2,1,0.6,1.3s1,0.6,1.8,0.8c0.6,0.2,1.1,0.3,1.3,0.6s0.4,0.5,0.4,0.8  C51.9,22.9,51.8,23.2,51.5,23.4z M78.5,29.5h20 M78.5,70.5h20 M2.5,29.5h20 M2.5,70.5h20 M68.5,65.5h6 M2.5,50.5h20 M31.9,51.5  l-9.4-4.7v9.4L31.9,51.5z M50.5,14.5v-10 M50.5,96.5v-10";
            SyncPathData = "M21.5,11.5h56v72h-56V11.5z M28.5,29.4c-0.3,0.2-0.7,0.3-1.2,0.3c-0.5,0-0.9-0.1-1.3-0.4  c-0.3-0.2-0.5-0.6-0.5-1.1h-0.9c0,0.7,0.3,1.2,0.8,1.6c0.5,0.4,1.2,0.6,1.9,0.6c0.8,0,1.4-0.2,1.8-0.5c0.5-0.3,0.7-0.8,0.7-1.4  c0-0.5-0.2-1-0.6-1.3c-0.4-0.4-1-0.6-1.7-0.8c-0.7-0.2-1.1-0.3-1.4-0.5c-0.3-0.2-0.4-0.5-0.4-0.8c0-0.4,0.1-0.6,0.4-0.9  s0.6-0.3,1.1-0.3c0.5,0,0.9,0.1,1.2,0.4s0.4,0.6,0.4,1h0.9c0-0.6-0.2-1.1-0.7-1.5s-1.1-0.6-1.9-0.6c-0.7,0-1.3,0.2-1.8,0.5  c-0.4,0.4-0.7,0.8-0.7,1.4c0,0.5,0.2,1,0.6,1.3s1,0.6,1.8,0.8c0.6,0.2,1.1,0.3,1.3,0.6s0.4,0.5,0.4,0.8  C28.9,28.9,28.8,29.2,28.5,29.4z M27.7,67.3c0.4,0,0.7,0.1,0.9,0.3s0.3,0.5,0.3,0.9v0.6c0,0.2,0,0.4,0.1,0.7s0.1,0.4,0.2,0.5h0.9  v-0.1c-0.1-0.1-0.2-0.3-0.3-0.4s-0.1-0.4-0.1-0.6v-0.7c0-0.4-0.1-0.8-0.3-1.1c-0.2-0.3-0.5-0.5-0.9-0.6c0.4-0.2,0.7-0.4,0.9-0.6  c0.2-0.3,0.3-0.6,0.3-1c0-0.7-0.2-1.2-0.6-1.5c-0.4-0.3-1-0.5-1.8-0.5H25v7.1h0.9v-3.1H27.7z M25.9,64h1.6c0.5,0,0.9,0.1,1.1,0.3  s0.4,0.5,0.4,0.9c0,0.4-0.1,0.8-0.4,1c-0.2,0.2-0.6,0.3-1.2,0.3h-1.6V64z M73.5,26.2c0-0.9-0.3-1.6-0.8-2.2  c-0.5-0.6-1.2-0.9-2.1-0.9c-0.8,0-1.5,0.3-2,0.9s-0.8,1.3-0.8,2.2v1.3c0,0.9,0.3,1.6,0.8,2.2s1.2,0.9,2,0.9c0.2,0,0.3,0,0.5,0  c0.2,0,0.3-0.1,0.5-0.1l1.3,1.2l0.6-0.6l-1.2-1.1c0.4-0.3,0.7-0.6,0.9-1.1s0.3-0.9,0.3-1.4V26.2z M72.6,27.4c0,0.7-0.2,1.2-0.5,1.6  c-0.4,0.4-0.8,0.6-1.5,0.6c-0.6,0-1-0.2-1.4-0.6c-0.3-0.4-0.5-1-0.5-1.6v-1.3c0-0.7,0.2-1.2,0.5-1.6c0.3-0.4,0.8-0.6,1.4-0.6  c0.6,0,1.1,0.2,1.5,0.6s0.5,1,0.5,1.6V27.4z M73.5,67.2c0-0.9-0.3-1.6-0.8-2.2c-0.5-0.6-1.2-0.9-2.1-0.9c-0.8,0-1.5,0.3-2,0.9  s-0.8,1.3-0.8,2.2v1.3c0,0.9,0.3,1.6,0.8,2.2s1.2,0.9,2,0.9c0.2,0,0.3,0,0.5,0c0.2,0,0.3-0.1,0.5-0.1l1.3,1.2l0.6-0.6l-1.2-1.1  c0.4-0.3,0.7-0.6,0.9-1.1s0.3-0.9,0.3-1.4V67.2z M72.6,68.4c0,0.7-0.2,1.2-0.5,1.6c-0.4,0.4-0.8,0.6-1.5,0.6c-0.6,0-1-0.2-1.4-0.6  c-0.3-0.4-0.5-1-0.5-1.6v-1.3c0-0.7,0.2-1.2,0.5-1.6c0.3-0.4,0.8-0.6,1.4-0.6c0.6,0,1.1,0.2,1.5,0.6s0.5,1,0.5,1.6V68.4z M77.5,26.5  h20 M77.5,67.5h20 M1.5,26.5h20 M1.5,67.5h20 M67.5,62.5h6 M1.5,47.5h20 M30.9,47.5l-9.4-4.7v9.4L30.9,47.5z";
            LatchPathData = "M21.5,11.5h57v73h-57V11.5z M28.5,30.4c-0.3,0.2-0.7,0.3-1.2,0.3c-0.5,0-0.9-0.1-1.3-0.4  c-0.3-0.2-0.5-0.6-0.5-1.1h-0.9c0,0.7,0.3,1.2,0.8,1.6c0.5,0.4,1.2,0.6,1.9,0.6c0.8,0,1.4-0.2,1.8-0.5c0.5-0.3,0.7-0.8,0.7-1.4  c0-0.5-0.2-1-0.6-1.3c-0.4-0.4-1-0.6-1.7-0.8c-0.7-0.2-1.1-0.3-1.4-0.5c-0.3-0.2-0.4-0.5-0.4-0.8c0-0.4,0.1-0.6,0.4-0.9  s0.6-0.3,1.1-0.3c0.5,0,0.9,0.1,1.2,0.4s0.4,0.6,0.4,1h0.9c0-0.6-0.2-1.1-0.7-1.5s-1.1-0.6-1.9-0.6c-0.7,0-1.3,0.2-1.8,0.5  c-0.4,0.4-0.7,0.8-0.7,1.4c0,0.5,0.2,1,0.6,1.3s1,0.6,1.8,0.8c0.6,0.2,1.1,0.3,1.3,0.6s0.4,0.5,0.4,0.8  C28.9,29.9,28.8,30.2,28.5,30.4z M27.7,69.3c0.4,0,0.7,0.1,0.9,0.3s0.3,0.5,0.3,0.9v0.6c0,0.2,0,0.4,0.1,0.7s0.1,0.4,0.2,0.5h0.9  v-0.1c-0.1-0.1-0.2-0.3-0.3-0.4s-0.1-0.4-0.1-0.6v-0.7c0-0.4-0.1-0.8-0.3-1.1c-0.2-0.3-0.5-0.5-0.9-0.6c0.4-0.2,0.7-0.4,0.9-0.6  c0.2-0.3,0.3-0.6,0.3-1c0-0.7-0.2-1.2-0.6-1.5c-0.4-0.3-1-0.5-1.8-0.5H25v7.1h0.9v-3.1H27.7z M25.9,66h1.6c0.5,0,0.9,0.1,1.1,0.3  s0.4,0.5,0.4,0.9c0,0.4-0.1,0.8-0.4,1c-0.2,0.2-0.6,0.3-1.2,0.3h-1.6V66z M49.5,54.6c1.6,0,2.9-0.4,3.8-1.3c0.9-0.9,1.4-2.1,1.4-3.6  v-1.5c0-1.5-0.5-2.7-1.4-3.6c-0.9-0.9-2.1-1.4-3.7-1.4H46v11.4H49.5z M49.5,44.7c1.1,0,1.9,0.3,2.4,0.9c0.5,0.6,0.8,1.4,0.8,2.5v1.5  c0,1.1-0.3,1.9-0.8,2.5s-1.4,0.9-2.5,0.9h-1.5v-8.3H49.5z M74.5,27.2c0-0.9-0.3-1.6-0.8-2.2c-0.5-0.6-1.2-0.9-2.1-0.9  c-0.8,0-1.5,0.3-2,0.9s-0.8,1.3-0.8,2.2v1.3c0,0.9,0.3,1.6,0.8,2.2s1.2,0.9,2,0.9c0.2,0,0.3,0,0.5,0c0.2,0,0.3-0.1,0.5-0.1l1.3,1.2  l0.6-0.6l-1.2-1.1c0.4-0.3,0.7-0.6,0.9-1.1s0.3-0.9,0.3-1.4V27.2z M73.6,28.4c0,0.7-0.2,1.2-0.5,1.6c-0.4,0.4-0.8,0.6-1.5,0.6  c-0.6,0-1-0.2-1.4-0.6c-0.3-0.4-0.5-1-0.5-1.6v-1.3c0-0.7,0.2-1.2,0.5-1.6c0.3-0.4,0.8-0.6,1.4-0.6c0.6,0,1.1,0.2,1.5,0.6  s0.5,1,0.5,1.6V28.4z M74.5,68.2c0-0.9-0.3-1.6-0.8-2.2c-0.5-0.6-1.2-0.9-2.1-0.9c-0.8,0-1.5,0.3-2,0.9s-0.8,1.3-0.8,2.2v1.3  c0,0.9,0.3,1.6,0.8,2.2s1.2,0.9,2,0.9c0.2,0,0.3,0,0.5,0c0.2,0,0.3-0.1,0.5-0.1l1.3,1.2l0.6-0.6l-1.2-1.1c0.4-0.3,0.7-0.6,0.9-1.1  s0.3-0.9,0.3-1.4V68.2z M73.6,69.4c0,0.7-0.2,1.2-0.5,1.6c-0.4,0.4-0.8,0.6-1.5,0.6c-0.6,0-1-0.2-1.4-0.6c-0.3-0.4-0.5-1-0.5-1.6  v-1.3c0-0.7,0.2-1.2,0.5-1.6c0.3-0.4,0.8-0.6,1.4-0.6c0.6,0,1.1,0.2,1.5,0.6s0.5,1,0.5,1.6V69.4z M78.5,27.5h20 M78.5,68.5h20   M1.5,27.5h20 M1.5,68.5h20 M68.5,63.5h6";
            FlipFlopPathData = "M21.5,13.5h57v73h-57V13.5z M29.8,26.2h-5.4V27h2.2v6.4h0.9V27h2.2V26.2z M54.2,45.2h-9v1.5h3.5v9.9h1.9v-9.9  h3.6V45.2z M74.5,29.2c0-0.9-0.3-1.6-0.8-2.2c-0.5-0.6-1.2-0.9-2.1-0.9c-0.8,0-1.5,0.3-2,0.9s-0.8,1.3-0.8,2.2v1.3  c0,0.9,0.3,1.6,0.8,2.2s1.2,0.9,2,0.9c0.2,0,0.3,0,0.5,0c0.2,0,0.3-0.1,0.5-0.1l1.3,1.2l0.6-0.6l-1.2-1.1c0.4-0.3,0.7-0.6,0.9-1.1  s0.3-0.9,0.3-1.4V29.2z M73.6,30.4c0,0.7-0.2,1.2-0.5,1.6c-0.4,0.4-0.8,0.6-1.5,0.6c-0.6,0-1-0.2-1.4-0.6c-0.3-0.4-0.5-1-0.5-1.6  v-1.3c0-0.7,0.2-1.2,0.5-1.6c0.3-0.4,0.8-0.6,1.4-0.6c0.6,0,1.1,0.2,1.5,0.6s0.5,1,0.5,1.6V30.4z M74.5,70.2c0-0.9-0.3-1.6-0.8-2.2  c-0.5-0.6-1.2-0.9-2.1-0.9c-0.8,0-1.5,0.3-2,0.9s-0.8,1.3-0.8,2.2v1.3c0,0.9,0.3,1.6,0.8,2.2s1.2,0.9,2,0.9c0.2,0,0.3,0,0.5,0  c0.2,0,0.3-0.1,0.5-0.1l1.3,1.2l0.6-0.6l-1.2-1.1c0.4-0.3,0.7-0.6,0.9-1.1s0.3-0.9,0.3-1.4V70.2z M73.6,71.4c0,0.7-0.2,1.2-0.5,1.6  c-0.4,0.4-0.8,0.6-1.5,0.6c-0.6,0-1-0.2-1.4-0.6c-0.3-0.4-0.5-1-0.5-1.6v-1.3c0-0.7,0.2-1.2,0.5-1.6c0.3-0.4,0.8-0.6,1.4-0.6  c0.6,0,1.1,0.2,1.5,0.6s0.5,1,0.5,1.6V71.4z M78.5,29.5h20 M78.5,70.5h20 M1.5,29.5h20 M1.5,70.5h20 M68.5,65.5h6 M31.5,70.5l-10-5  v10L31.5,70.5z";
            NandGatePathData = "M173.2472,549L173.2472,539 M173.2472,466L173.2472,456 M154.6192,523L145.2472,518.305L145.2472,527.695L154.6192,523z M191.2472,518L197.2472,518 M125.2472,523L145.2472,523 M125.2472,482L145.2472,482 M201.2472,523L221.2472,523 M201.2472,482L221.2472,482 M196.2262,523.597C196.2162,524.421,196.0452,525.053,195.7132,525.494C195.3812,525.935,194.9122,526.156,194.3072,526.156C193.7012,526.156,193.2282,525.921,192.8892,525.453C192.5482,524.984,192.3782,524.343,192.3782,523.529L192.3782,523.006C192.3882,522.206,192.5622,521.58,192.9022,521.129C193.2432,520.678,193.7082,520.453,194.2972,520.453C194.9152,520.453,195.3922,520.68,195.7252,521.134C196.0592,521.588,196.2262,522.233,196.2262,523.07L196.2262,523.597z M197.1632,523.075C197.1632,522.381,197.0462,521.775,196.8112,521.256C196.5772,520.737,196.2422,520.339,195.8092,520.062C195.3732,519.785,194.8702,519.647,194.2972,519.647C193.7372,519.647,193.2402,519.786,192.8052,520.064C192.3712,520.343,192.0342,520.744,191.7972,521.268C191.5592,521.792,191.4402,522.396,191.4402,523.08L191.4402,523.592C191.4472,524.263,191.5702,524.853,191.8092,525.362C192.0492,525.872,192.3852,526.264,192.8172,526.539C193.2502,526.814,193.7472,526.952,194.3072,526.952C194.5672,526.952,194.8142,526.922,195.0492,526.864L196.5482,528.055L197.1872,527.464L195.9182,526.468C196.3112,526.188,196.6182,525.8,196.8362,525.304C197.0542,524.807,197.1632,524.216,197.1632,523.529L197.1632,523.075z M174.1562,473.249C174.4002,473.439,174.5222,473.709,174.5222,474.057C174.5222,474.405,174.3912,474.681,174.1272,474.882C173.8632,475.084,173.4842,475.185,172.9892,475.185C172.4592,475.185,172.0352,475.061,171.7172,474.812C171.4002,474.562,171.2412,474.223,171.2412,473.793L170.2992,473.793C170.2992,474.207,170.4142,474.576,170.6432,474.902C170.8732,475.228,171.1992,475.484,171.6222,475.671C172.0452,475.858,172.5012,475.952,172.9892,475.952C173.7412,475.952,174.3422,475.778,174.7912,475.432C175.2402,475.085,175.4652,474.624,175.4652,474.047C175.4652,473.686,175.3852,473.372,175.2232,473.105C175.0622,472.838,174.8142,472.604,174.4802,472.404C174.1472,472.204,173.6852,472.021,173.0942,471.854C172.5042,471.688,172.0852,471.505,171.8402,471.304C171.5942,471.104,171.4712,470.858,171.4712,470.568C171.4712,470.206,171.6002,469.923,171.8592,469.719C172.1172,469.516,172.4782,469.414,172.9402,469.414C173.4382,469.414,173.8242,469.537,174.0982,469.784C174.3712,470.03,174.5082,470.369,174.5082,470.8L175.4502,470.8C175.4502,470.406,175.3462,470.043,175.1352,469.711C174.9262,469.379,174.6292,469.119,174.2462,468.93C173.8642,468.741,173.4292,468.647,172.9402,468.647C172.2252,468.647,171.6432,468.83,171.1952,469.196C170.7472,469.562,170.5232,470.025,170.5232,470.585C170.5232,471.08,170.7072,471.496,171.0722,471.833C171.4392,472.17,172.0242,472.454,172.8282,472.685C173.4702,472.871,173.9122,473.059,174.1562,473.249z M171.6712,528.511L173.1062,528.511C173.6012,528.515,173.9792,528.633,174.2422,528.868C174.5042,529.103,174.6352,529.44,174.6352,529.881C174.6352,530.285,174.4972,530.608,174.2232,530.85C173.9472,531.091,173.5772,531.211,173.1112,531.211L171.6712,531.211L171.6712,528.511z M174.8842,534.854L175.8902,534.854L175.8902,534.795L174.2202,531.778C174.6462,531.615,174.9782,531.369,175.2192,531.041C175.4572,530.712,175.5772,530.326,175.5772,529.883C175.5772,529.187,175.3612,528.656,174.9302,528.292C174.4992,527.927,173.8832,527.745,173.0822,527.745L170.7282,527.745L170.7282,534.854L171.6712,534.854L171.6712,531.978L173.3412,531.978L174.8842,534.854z M196.2262,482.597C196.2162,483.421,196.0452,484.053,195.7132,484.494C195.3812,484.935,194.9122,485.156,194.3072,485.156C193.7012,485.156,193.2282,484.921,192.8892,484.453C192.5482,483.984,192.3782,483.343,192.3782,482.529L192.3782,482.006C192.3882,481.206,192.5622,480.58,192.9022,480.129C193.2432,479.678,193.7082,479.453,194.2972,479.453C194.9152,479.453,195.3922,479.68,195.7252,480.134C196.0592,480.588,196.2262,481.233,196.2262,482.07L196.2262,482.597z M197.1632,482.075C197.1632,481.381,197.0462,480.775,196.8112,480.256C196.5772,479.737,196.2422,479.339,195.8092,479.062C195.3732,478.785,194.8702,478.647,194.2972,478.647C193.7372,478.647,193.2402,478.786,192.8052,479.064C192.3712,479.343,192.0342,479.744,191.7972,480.268C191.5592,480.792,191.4402,481.396,191.4402,482.08L191.4402,482.592C191.4472,483.263,191.5702,483.853,191.8092,484.362C192.0492,484.872,192.3852,485.264,192.8172,485.539C193.2502,485.814,193.7472,485.952,194.3072,485.952C194.5672,485.952,194.8142,485.922,195.0492,485.864L196.5482,487.055L197.1872,486.464L195.9182,485.468C196.3112,485.188,196.6182,484.8,196.8362,484.304C197.0542,483.807,197.1632,483.216,197.1632,482.529L197.1632,482.075z M173.0302,499.315C174.0512,499.315,174.8302,499.641,175.3662,500.29C175.9022,500.94,176.1712,501.881,176.1712,503.113L176.1712,503.745C176.1602,504.956,175.8782,505.885,175.3232,506.532C174.7682,507.179,173.9732,507.503,172.9362,507.503L171.6472,507.503L171.6472,499.315L173.0302,499.315z M172.9442,509.097C173.9712,509.097,174.8812,508.875,175.6752,508.433C176.4692,507.99,177.0812,507.36,177.5112,506.542C177.9402,505.725,178.1552,504.776,178.1552,503.698L178.1552,503.128C178.1552,502.065,177.9412,501.123,177.5152,500.3C177.0882,499.477,176.4842,498.842,175.7062,498.394C174.9282,497.946,174.0352,497.722,173.0302,497.722L169.6712,497.722L169.6712,509.097L172.9442,509.097z M150.7652,479.511C151.4612,479.518,151.9992,479.745,152.3792,480.193C152.7582,480.641,152.9472,481.269,152.9472,482.077L152.9472,482.492C152.9472,483.323,152.7462,483.963,152.3442,484.413C151.9422,484.863,151.3802,485.087,150.6572,485.087L149.6712,485.087L149.6712,479.511L150.7652,479.511z M150.7012,485.854C151.3392,485.848,151.8982,485.709,152.3792,485.439C152.8582,485.169,153.2282,484.781,153.4862,484.277C153.7462,483.772,153.8752,483.185,153.8752,482.514L153.8752,482.06C153.8722,481.406,153.7402,480.826,153.4822,480.322C153.2232,479.817,152.8562,479.428,152.3812,479.155C151.9052,478.881,151.3582,478.745,150.7402,478.745L148.7332,478.745L148.7332,485.854L150.7012,485.854z M145.2382,466L201.2552,466L201.2552,539L145.2382,539z";
            EmitterPathData = "M75.5,49.5l-52,28v-56L75.5,49.5z M75.5,49.5h24 M1.5,49.5h22 M44.5,45.5h-5v8h5V45.5z M39.5,45.5h10   M34.5,53.5h10";
            XorGatePathData = "M21.7,76.5L21.7,76.5c6.4-18.1,6.4-37.8,0-55.9l0-0.1h1.6c21.5,0,41.7,10.4,54.2,28l0,0l0,0  c-12.5,17.6-32.7,28-54.2,28H21.7z M73.4,48.5L73.4,48.5 M17.5,76.8L17.5,76.8c6.7-18.2,6.7-38.1,0-56.3l0-0.1 M77.5,48.5h22 M0,32.5h21 M0,65.5h21";
            NorGatePathData = "M14.5055,0.00562288 C14.3031,0.00562288 14.1078,0.0433293 13.9072,0.0542443 L13.9072,49.959 C14.1078,49.9699 14.3031,50.0079 14.5055,50.0079 C23.223,50.0079 30.29,28.3205 30.29,25.004 C30.29,22.2835 23.223,0.00562288 14.5055,0.00562288 z M13.9074,9.48911 L-0.00120828,9.48911 M13.9074,40.5218 L-0.00120828,40.5218 M36.3023,25.005 L50.0033,25.005 M36.2352,25.005 C36.2352,29.5191 35.0457,33.1879 33.5781,33.1879 C32.1105,33.1879 30.921,29.5191 30.921,25.005 C30.921,20.4908 32.1105,16.8223 33.5781,16.8223 C35.0457,16.8223 36.2352,20.4908 36.2352,25.005 z";
            BufferPathData = "M170.354,66.6523000000002L199.753,83.6253000000002L170.354,100.5983L170.354,66.6523000000002zM199.7534,83.6255000000001L214.5004,83.6255000000001M154.5,83.6255000000001L170.354,83.6255000000001";
            NandPathData = "M49.5,76.5h-28v-56h28c15.5,0,28,12.5,28,28v0C77.5,64,65,76.5,49.5,76.5z M83.5,48.5h16 M1.5,32.5h20   M1.5,65.5h20 M80.5,45.5c-1.7,0-3,1.3-3,3s1.3,3,3,3s3-1.3,3-3S82.2,45.5,80.5,45.5z";
            NorPathData = "M21.7,76.5L21.7,76.5c6.4-18.1,6.4-37.8,0-55.9l0-0.1h1.6c21.5,0,41.7,10.4,54.2,28l0,0l0,0  c-12.5,17.6-32.7,28-54.2,28H21.7z M83.5,48.5h16 M1.5,32.5h24 M1.5,64.5h24 M80.5,45.5c-1.7,0-3,1.3-3,3s1.3,3,3,3s3-1.3,3-3  S82.2,45.5,80.5,45.5z";
            OrPathData = "M21.7,76.5L21.7,76.5c6.4-18.1,6.4-37.8,0-55.9l0-0.1h1.6c21.5,0,41.7,10.4,54.2,28l0,0l0,0  c-12.5,17.6-32.7,28-54.2,28H21.7z M99.5,48.5l-22,0 M0,31.5h25 M0,65.5h25";
            AndPathData = "M21.5,20.5h28a28,28,0,0,1,28,28v0a28,28,0,0,1-28,28h-28a0,0,0,0,1,0,0v-56a0,0,0,0,1,0,0Z M78,48.5 L 100,48.5Z M0,32.5 L 21.4,32.5Z M0,65.5 L 21.4,65.5z";
            NotPathData = "M75.5,50.5l-52,28v-56L75.5,50.5z M81.5,50.5h18 M1.5,50.5h22 M78.5,47.5c-1.7,0-3,1.3-3,3s1.3,3,3,3s3-1.3,3-3  S80.2,47.5,78.5,47.5z";

            OrGatePorts = new DiagramObjectCollection<PointPort>();
            OrGatePorts.Add(AddPorts("Or_port1", 0.01, 0.1963));
            OrGatePorts.Add(AddPorts("Or_port2", 0.26, 0.5));
            OrGatePorts.Add(AddPorts("Or_port3", 0.01, 0.805));
            OrGatePorts.Add(AddPorts("Or_port4", 0.99, 0.5));

            //pallatte or port
            OrGatePalettePorts = new DiagramObjectCollection<PointPort>();
            OrGatePalettePorts.Add(AddPorts("Or_port1", 0.01, 0.1963));
            OrGatePalettePorts.Add(AddPorts("Or_port3", 0.01, 0.805));
            OrGatePalettePorts.Add(AddPorts("Or_port4", 0.99, 0.5));

            AndGatePorts = new DiagramObjectCollection<PointPort>();
            AndGatePorts.Add(AddPorts("And_port1", 0.01, 0.215));
            AndGatePorts.Add(AddPorts("And_port2", 0.22, 0.5));
            AndGatePorts.Add(AddPorts("And_port3", 0.01, 0.805));
            AndGatePorts.Add(AddPorts("And_port4", 0.99, 0.5));

            //pallette and ports
            AndGatePalettePorts = new DiagramObjectCollection<PointPort>();
            AndGatePalettePorts.Add(AddPorts("And_port1", 0.01, 0.215));
            AndGatePalettePorts.Add(AddPorts("And_port3", 0.01, 0.805));
            AndGatePalettePorts.Add(AddPorts("And_port4", 0.99, 0.5));

            NotGatePorts = new DiagramObjectCollection<PointPort>();
            NotGatePorts.Add(AddPorts("Not_port1", 0.01, 0.5));
            NotGatePorts.Add(AddPorts("Not_port2", 0.99, 0.5));

            FlipFlopPorts = new DiagramObjectCollection<PointPort>();
            FlipFlopPorts.Add(AddPorts(null, 0.01, 0.221));
            FlipFlopPorts.Add(AddPorts(null, 0.01, 0.779));
            FlipFlopPorts.Add(AddPorts(null, 0.99, 0.221));
            FlipFlopPorts.Add(AddPorts(null, 0.99, 0.779));

            JKFlipFlopPorts = new DiagramObjectCollection<PointPort>();
            JKFlipFlopPorts.Add(AddPorts(null, 0.01, 0.27));
            JKFlipFlopPorts.Add(AddPorts(null, 0.01, 0.5));
            JKFlipFlopPorts.Add(AddPorts(null, 0.01, 0.720));
            JKFlipFlopPorts.Add(AddPorts(null, 0.99, 0.270));
            JKFlipFlopPorts.Add(AddPorts(null, 0.99, 0.720));
            JKFlipFlopPorts.Add(AddPorts(null, 0.5, 0.01));
            JKFlipFlopPorts.Add(AddPorts(null, 0.5, 0.99));

            //pallette jk ports
            JKFlipFlopPalettePorts = new DiagramObjectCollection<PointPort>();
            JKFlipFlopPalettePorts.Add(AddPorts(null, 0.01, 0.27));
            JKFlipFlopPalettePorts.Add(AddPorts(null, 0.01, 0.720));
            JKFlipFlopPalettePorts.Add(AddPorts(null, 0.99, 0.270));
            JKFlipFlopPalettePorts.Add(AddPorts(null, 0.99, 0.720));
            JKFlipFlopPalettePorts.Add(AddPorts(null, 0.5, 0.01));
            JKFlipFlopPalettePorts.Add(AddPorts(null, 0.5, 0.99));

            //pallatte jk flipflop 
            JKFlipFlopPalettePortsAlt = new DiagramObjectCollection<PointPort>();
            JKFlipFlopPalettePortsAlt.Add(AddPorts(null, 0.01, 0.27));
            JKFlipFlopPalettePortsAlt.Add(AddPorts(null, 0.01, 0.495));
            JKFlipFlopPalettePortsAlt.Add(AddPorts(null, 0.01, 0.720));
            JKFlipFlopPalettePortsAlt.Add(AddPorts(null, 0.99, 0.270));
            JKFlipFlopPalettePortsAlt.Add(AddPorts(null, 0.99, 0.720));
            JKFlipFlopPalettePortsAlt.Add(AddPorts(null, 0.5, 0.01));
            JKFlipFlopPalettePortsAlt.Add(AddPorts(null, 0.5, 0.99));

            ResistorPorts = new DiagramObjectCollection<PointPort>();
            ResistorPorts.Add(AddPorts(null, 0.01, 0.210));
            ResistorPorts.Add(AddPorts(null, 0.01, 0.778));
            ResistorPorts.Add(AddPorts(null, 0.5, 0.218));
            ResistorPorts.Add(AddPorts(null, 0.99, 0.210));
            ResistorPorts.Add(AddPorts(null, 0.99, 0.778));

            //pallatte or ports
            ResistorPalettePorts = new DiagramObjectCollection<PointPort>();
            ResistorPalettePorts.Add(AddPorts(null, 0.01, 0.210));
            ResistorPalettePorts.Add(AddPorts(null, 0.01, 0.494));
            ResistorPalettePorts.Add(AddPorts(null, 0.01, 0.778));
            ResistorPalettePorts.Add(AddPorts(null, 0.99, 0.210));
            ResistorPalettePorts.Add(AddPorts(null, 0.99, 0.778));
            PointPort port1 = new PointPort()
            {
                Offset = new DiagramPoint
                {
                    X = 0.01,
                    Y = 0.19
                },
            };
            PointPort port2 = new PointPort()
            {
                Offset = new DiagramPoint
                {
                    X = 0.01,
                    Y = 0.809
                }
            };
            PointPort port3 = new PointPort()
            {
                Offset = new DiagramPoint
                {
                    X = 0.99,
                    Y = 0.5
                }
            };
            PointPort port4 = new PointPort()
            {
                Offset = new DiagramPoint
                {
                    X = 0.01,
                    Y = 0.5
                },
            };
            PointPort port5 = new PointPort()
            {
                Offset = new DiagramPoint
                {
                    X = 0.99,
                    Y = 0.5
                },
            };
            DiagramObjectCollection<PointPort> norGatePorts = new DiagramObjectCollection<PointPort>() { port1, port2, port3 };
            DiagramObjectCollection<PointPort> bufferPorts = new DiagramObjectCollection<PointPort>() { port4, port5 };

            CreatedElectricalPaletteNode("ElectricalOr", OrPathData, OrGatePalettePorts);
            CreatedElectricalPaletteNode("ElectricalNor", NorPathData, OrGatePalettePorts);
            CreatedElectricalPaletteNode("ElectricalAnd", AndPathData, AndGatePalettePorts);
            CreatedElectricalPaletteNode("ElectricalNand", NandPathData, OrGatePalettePorts);
            CreatedElectricalPaletteNode("ElectricalNot", NotPathData, NotGatePorts);
            CreatedElectricalPaletteNode("ElectricalBuffer", BufferPathData, bufferPorts);
            CreatedElectricalPaletteNode("ElectricalNorGate", NorGatePathData, norGatePorts);
            CreatedElectricalPaletteNode("ElectricalXorGate", XorGatePathData, OrGatePalettePorts);
            CreatedElectricalPaletteNode("ElectricalChEmitter", EmitterPathData, NotGatePorts);
            CreatedElectricalPaletteNode("ElectricalNandGate1", NandGatePathData, JKFlipFlopPalettePorts);
            CreatedElectricalPaletteNode("ElectricalFlipflop", FlipFlopPathData, FlipFlopPorts);
            CreatedElectricalPaletteNode("ElectricalRSLatch", LatchPathData, FlipFlopPorts);
            CreatedElectricalPaletteNode("ElectricalRSLatchSync", SyncPathData, ResistorPalettePorts);
            CreatedElectricalPaletteNode("ElectricalJKflipflop", JKPathData, JKFlipFlopPalettePortsAlt);
            CreatedElectricalPaletteNode("ElectricalDflipflop", DPathData, FlipFlopPorts);

            ElectericalShapes = new Palette() { ID = "ElectricalShapes", IsExpanded = false, Symbols = ElectricalShapeList, Title = "Electrical Shapes" };

        }

        private PointPort AddPorts(string? id, double x, double y)
        {
            return new PointPort()
            {
                ID = !(string.IsNullOrEmpty(id)) ? id : null,
                Offset = new DiagramPoint() { X = x, Y = y },
            };
        }

        private void CreatedElectricalPaletteNode(string id, string? pathData, DiagramObjectCollection<PointPort>? ports)
        {
            Node diagramNode = new Node()
            {
                ID = id,
                AdditionalInfo = new Dictionary<string, object>() { { "type", "ElectricalShapes" } },
                Shape = new PathShape() { Type = NodeShapes.Path, Data = pathData },
                SearchTags = new List<string>() { "Electrical" },
            };
            if (ports != null)
            {
                diagramNode.Ports = ports;
            }
            ElectricalShapeList!.Add(diagramNode);

        }

        public void InitializeFloorShapes()
        {
            FloorPlaneShapesList = new DiagramObjectCollection<NodeBase>();
            Node FloorPlaneShapes1200 = new Node()
            {
                ID = "WashBasin_Mirror2siLuxk",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M0,32H160a0,0,0,0,1,0,0v79a10,10,0,0,1-10,10H10A10,10,0,0,1,0,111V32A0,0,0,0,1,0,32Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#270805",
                    StrokeWidth = 0
                },
                Width = 160,
                Height = 89,
                OffsetX = 80,
                OffsetY = 76.5,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1200);
            Node FloorPlaneShapes1201 = new Node()
            {
                ID = "WashBasin_Mirror3Q9PxVP",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Rectangle
                },
                Style = new ShapeStyle()
                {
                    Fill = "#dbdcdd"
                },
                Width = 160,
                Height = 7.819999694824219,
                OffsetX = 80,
                OffsetY = 35.90999984741211,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1201);
            Node FloorPlaneShapes1202 = new Node()
            {
                ID = "WashBasin_Mirror4a4GtBc",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Rectangle
                },
                Style = new ShapeStyle()
                {
                    Fill = "#c2c3c4"
                },
                Width = 160,
                Height = 7.819999694824219,
                OffsetX = 80,
                OffsetY = 43.72999954223633,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1202);
            Node FloorPlaneShapes1203 = new Node()
            {
                ID = "WashBasin_Mirror5Ec581y",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Ellipse,
                    CornerRadius = 44.5
                },
                Style = new ShapeStyle()
                {
                    Fill = "#dbdcdd"
                },
                Width = 89,
                Height = 48,
                OffsetX = 81.5,
                OffsetY = 85,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1203);
            Node FloorPlaneShapes1204 = new Node()
            {
                ID = "WashBasin_Mirror6xYjsZx",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Ellipse,
                    CornerRadius = 40.5
                },
                Style = new ShapeStyle()
                {
                    Fill = "#7f7f7f"
                },
                Width = 81,
                Height = 42,
                OffsetX = 81.5,
                OffsetY = 85,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1204);
            Node FloorPlaneShapes1205 = new Node()
            {
                ID = "WashBasin_Mirror7H0IdfR",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Ellipse
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fcfcfc"
                },
                Width = 6.420000076293945,
                Height = 6.420000076293945,
                OffsetX = 81.59999942779541,
                OffsetY = 77.32000064849854,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1205);
            Node FloorPlaneShapes1206 = new Node()
            {
                ID = "WashBasin_Mirror8JGH6zw",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Rectangle,
                    CornerRadius = 1
                },
                Style = new ShapeStyle()
                {
                    Fill = "#2080ce"
                },
                Width = 7.4599995613098145,
                Height = 10.529999732971191,
                OffsetX = 146.5499918460846,
                OffsetY = 86.38500261306763,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1206);
            Node FloorPlaneShapes1207 = new Node()
            {
                ID = "WashBasin_Mirror9K7WbYf",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M81.09,53.8h0a2.18,2.18,0,0,1,2.17,2.29l-.63,11.68a1.55,1.55,0,0,1-1.54,1.46h0a1.55,1.55,0,0,1-1.54-1.46l-.63-11.68A2.17,2.17,0,0,1,81.09,53.8Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 4.346092224121094,
                Height = 15.430004119873047,
                OffsetX = 81.08972549438477,
                OffsetY = 61.51500129699707,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1207);
            Node FloorPlaneShapes1208 = new Node()
            {
                ID = "WashBasin_Mirror10UE5NjR",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M87.65,56.31h0a2.18,2.18,0,0,1,2.29-2.17l11.68.63a1.54,1.54,0,0,1,1.45,1.54h0a1.54,1.54,0,0,1-1.45,1.54l-11.68.63A2.17,2.17,0,0,1,87.65,56.31Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 15.420013427734375,
                Height = 4.346099853515625,
                OffsetX = 95.36000061035156,
                OffsetY = 56.310272216796875,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1208);
            Node FloorPlaneShapes1209 = new Node()
            {
                ID = "WashBasin_Mirror116kqZx9",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M74.91,56.31h0a2.18,2.18,0,0,0-2.29-2.17l-11.68.63a1.54,1.54,0,0,0-1.45,1.54h0a1.54,1.54,0,0,0,1.45,1.54l11.68.63A2.17,2.17,0,0,0,74.91,56.31Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 15.420013427734375,
                Height = 4.346099853515625,
                OffsetX = 67.20000457763672,
                OffsetY = 56.310272216796875,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1209);
            Node FloorPlaneShapes1210 = new Node()
            {
                ID = "WashBasin_Mirror125Y2eYg",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M133.54,47.64h18.55a0,0,0,0,1,0,0V49.7a1,1,0,0,1-1,1H134.54a1,1,0,0,1-1-1V47.64A0,0,0,0,1,133.54,47.64Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 18.550003051757812,
                Height = 3.0600013732910156,
                OffsetX = 142.81499481201172,
                OffsetY = 49.170000076293945,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1210);
            Node FloorPlaneShapes1211 = new Node()
            {
                ID = "WashBasin_Mirror13dEOJiO",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M10.2,47.64H28.75a0,0,0,0,1,0,0V49.7a1,1,0,0,1-1,1H11.2a1,1,0,0,1-1-1V47.64A0,0,0,0,1,10.2,47.64Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 18.549999237060547,
                Height = 3.0600013732910156,
                OffsetX = 19.47499942779541,
                OffsetY = 49.170000076293945,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1211);
            Node FloorPlaneShapes1212 = new Node()
            {
                ID = "WashBasin_Mirror14YGyxGd",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M143.82,91.65a1,1,0,0,1-1-1V82.12a1,1,0,0,1,1-1h5.45a1,1,0,0,1,1,1Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 7.4499969482421875,
                Height = 10.529998779296875,
                OffsetX = 146.54500579833984,
                OffsetY = 86.38500213623047,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1212);
            NodeGroup FloorPlaneShapes1213 = new NodeGroup()
            {
                ID = "WashBasin_Mirror",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                SearchTags = new List<string>() { "Floor" },
                Children = new string[] {
               "WashBasin_Mirror2siLuxk",
               "WashBasin_Mirror3Q9PxVP",
               "WashBasin_Mirror4a4GtBc",
               "WashBasin_Mirror5Ec581y",
               "WashBasin_Mirror6xYjsZx",
               "WashBasin_Mirror7H0IdfR",
               "WashBasin_Mirror8JGH6zw",
               "WashBasin_Mirror9K7WbYf",
               "WashBasin_Mirror10UE5NjR",
               "WashBasin_Mirror116kqZx9",
               "WashBasin_Mirror125Y2eYg",
               "WashBasin_Mirror13dEOJiO",
               "WashBasin_Mirror14YGyxGd"
            }
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1213);
            Node FloorPlaneShapes1194 = new Node()
            {
                ID = "Washbasin22mwWtpz",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M42,118.5V44.6A5.6,5.6,0,0,1,47.6,39h75.53a5.58,5.58,0,0,1,5.6,5.73c-.42,19-7.58,78.46-81,79.37A5.61,5.61,0,0,1,42,118.5Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#dbdcdd",
                    StrokeWidth = 0
                },
                Width = 86.73202514648438,
                Height = 85.10133361816406,
                OffsetX = 85.36600875854492,
                OffsetY = 81.55062103271484,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1194);
            Node FloorPlaneShapes1195 = new Node()
            {
                ID = "Washbasin23RiEEC4",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M48,109.94V49.57A4.58,4.58,0,0,1,52.57,45h61.7a4.57,4.57,0,0,1,4.57,4.68c-.34,15.48-6.19,64.13-66.26,64.83A4.55,4.55,0,0,1,48,109.94Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#7f7f7f",
                    StrokeWidth = 0
                },
                Width = 70.84136962890625,
                Height = 69.51010131835938,
                OffsetX = 83.4206428527832,
                OffsetY = 79.75504684448242,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1195);
            Node FloorPlaneShapes1196 = new Node()
            {
                ID = "Washbasin24R1ibaH",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M48,103V50a5,5,0,0,1,5-5h52S105,89.12,48,103Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 57,
                Height = 58,
                OffsetX = 76.5,
                OffsetY = 74,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1196);
            Node FloorPlaneShapes1197 = new Node()
            {
                ID = "Washbasin258VaJeD",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Ellipse
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff"
                },
                Width = 7,
                Height = 7,
                OffsetX = 65,
                OffsetY = 61,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1197);
            Node FloorPlaneShapes1198 = new Node()
            {
                ID = "Washbasin26XjKRF6",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M84.34,41.29h0a2.31,2.31,0,0,1,2.3,2.43L86,56.07a1.63,1.63,0,0,1-1.63,1.54h0a1.62,1.62,0,0,1-1.62-1.54l-.67-12.35A2.3,2.3,0,0,1,84.34,41.29Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 4.5668182373046875,
                Height = 16.320003509521484,
                OffsetX = 84.3597183227539,
                OffsetY = 49.450002670288086,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1198);
            NodeGroup FloorPlaneShapes1199 = new NodeGroup()
            {
                ID = "Washbasin2",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                SearchTags = new List<string>() { "Floor" },
                Children = new string[] {
               "Washbasin22mwWtpz",
               "Washbasin23RiEEC4",
               "Washbasin24R1ibaH",
               "Washbasin258VaJeD",
               "Washbasin26XjKRF6"
            }
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1199);
            Node FloorPlaneShapes1188 = new Node()
            {
                ID = "Washbasin12HCS1Cb",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M84.13,112.33h0A50.88,50.88,0,0,1,33.32,64.12,12.87,12.87,0,0,1,34,59.2V51a3.29,3.29,0,0,1,3.29-3.29h92.9a4,4,0,0,1,4,4V59.2a13.06,13.06,0,0,1,.71,4.92A50.89,50.89,0,0,1,84.13,112.33Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#dbdcdd",
                    StrokeWidth = 0
                },
                Width = 101.6159439086914,
                Height = 64.62001037597656,
                OffsetX = 84.1071891784668,
                OffsetY = 80.02000427246094,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1188);
            Node FloorPlaneShapes1189 = new Node()
            {
                ID = "Washbasin13fWI7lW",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M84.13,106.33h0C62.9,106.33,45.4,93,43,75.87a13.27,13.27,0,0,1,.91-7l.8-1.91c1.14-2.72,4.23-4.55,7.69-4.55h63.51c3.46,0,6.55,1.83,7.69,4.55l.79,1.91a13.18,13.18,0,0,1,.92,7C122.86,93,105.36,106.33,84.13,106.33Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#7f7f7f",
                    StrokeWidth = 0
                },
                Width = 82.56951904296875,
                Height = 43.92000198364258,
                OffsetX = 84.15504455566406,
                OffsetY = 84.3700008392334,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1189);
            Node FloorPlaneShapes1190 = new Node()
            {
                ID = "Washbasin14bwxPiq",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M55,96.21s28.25-41,58,0C113,96.21,85.16,118.24,55,96.21Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 58,
                Height = 28.013328552246094,
                OffsetX = 84,
                OffsetY = 91.99444961547852,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1190);
            Node FloorPlaneShapes1191 = new Node()
            {
                ID = "Washbasin156XUxTC",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Ellipse
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff"
                },
                Width = 9,
                Height = 9,
                OffsetX = 84.5,
                OffsetY = 84.5,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1191);
            Node FloorPlaneShapes1192 = new Node()
            {
                ID = "Washbasin16SjrokS",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M83.38,50.32h0a3.7,3.7,0,0,1,3.7,3.9L86,74.12a2.62,2.62,0,0,1-2.62,2.48h0a2.63,2.63,0,0,1-2.62-2.48l-1.07-19.9A3.69,3.69,0,0,1,83.38,50.32Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 7.401397705078125,
                Height = 26.280014038085938,
                OffsetX = 83.38471221923828,
                OffsetY = 63.459999084472656,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1192);
            NodeGroup FloorPlaneShapes1193 = new NodeGroup()
            {
                ID = "Washbasin1",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                SearchTags = new List<string>() { "Floor" },
                Children = new string[] {
               "Washbasin12HCS1Cb",
               "Washbasin13fWI7lW",
               "Washbasin14bwxPiq",
               "Washbasin156XUxTC",
               "Washbasin16SjrokS"
            }
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1193);
            Node FloorPlaneShapes1183 = new Node()
            {
                ID = "Washbasin2xLatbz",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Rectangle,
                    CornerRadius = 43.5
                },
                Style = new ShapeStyle()
                {
                    Fill = "#707070"
                },
                Width = 87,
                Height = 87,
                OffsetX = 82.5,
                OffsetY = 80.5,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1183);
            Node FloorPlaneShapes1184 = new Node()
            {
                ID = "Washbasin30qnQfG",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M84,50H81A34,34,0,0,0,47,84h0a34,34,0,0,0,34,34h3a34,34,0,0,0,34-34h0A34,34,0,0,0,84,50ZM82,86a6,6,0,1,1,6-6A6,6,0,0,1,82,86Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#d1d2d3",
                    StrokeWidth = 0
                },
                Width = 71,
                Height = 68,
                OffsetX = 82.5,
                OffsetY = 84,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1184);
            Node FloorPlaneShapes1185 = new Node()
            {
                ID = "Washbasin4cmVZFl",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M57.21,107.79a33.77,33.77,0,0,1,0-47.75h0C70.45,46.85,93.83,45.83,107,59Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 59.677005767822266,
                Height = 58.17007827758789,
                OffsetX = 77.16149711608887,
                OffsetY = 78.7049617767334,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1185);
            Node FloorPlaneShapes1186 = new Node()
            {
                ID = "Washbasin5WpMfbs",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M82.3,38.32h0A3.69,3.69,0,0,1,86,42.22l-1.07,19.9A2.63,2.63,0,0,1,82.3,64.6h0a2.62,2.62,0,0,1-2.62-2.48L78.6,42.22A3.71,3.71,0,0,1,82.3,38.32Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#fff",
                    StrokeWidth = 0
                },
                Width = 7.410850524902344,
                Height = 26.280040740966797,
                OffsetX = 82.30055618286133,
                OffsetY = 51.45998573303223,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1186);
            NodeGroup FloorPlaneShapes1187 = new NodeGroup()
            {
                ID = "Washbasin",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                SearchTags = new List<string>() { "Floor" },
                Children = new string[] {
               "Washbasin2xLatbz",
               "Washbasin30qnQfG",
               "Washbasin4cmVZFl",
               "Washbasin5WpMfbs"
            }
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1187);
            Node FloorPlaneShapes1175 = new Node()
            {
                ID = "VerticalWall2DppOoX",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Rectangle
                },
                Style = new ShapeStyle()
                {
                    Fill = "#464747"
                },
                Width = 113,
                Height = 14,
                OffsetX = 81,
                OffsetY = 80.5,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1175);
            NodeGroup FloorPlaneShapes1176 = new NodeGroup()
            {
                ID = "Vertical Wall",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Children = new string[] {
               "VerticalWall2DppOoX"
            }
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1176);
            Node FloorPlaneShapes1177 = new Node()
            {
                ID = "Wardrobe2DkdyWy",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Rectangle
                },
                Style = new ShapeStyle()
                {
                    Fill = "#2b1d0e"
                },
                Width = 77,
                Height = 4,
                OffsetX = 38.5,
                OffsetY = 100,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1177);
            Node FloorPlaneShapes1178 = new Node()
            {
                ID = "Wardrobe3I9qfIw",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M160 101.29 83.92 111.29 82.92 107.54 160 97.54 160 101.29"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#2b1d0e"
                },
                Width = 77.08000183105469,
                Height = 13.75,
                OffsetX = 121.45999908447266,
                OffsetY = 104.41500091552734,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1178);
            Node FloorPlaneShapes1179 = new Node()
            {
                ID = "Wardrobe41tIQyC",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new PathShape()
                {
                    Type = NodeShapes.Path,
                    Data = "M4,54H156a4,4,0,0,1,4,4V98a0,0,0,0,1,0,0H0a0,0,0,0,1,0,0V58A4,4,0,0,1,4,54Z"
                },
                Style = new ShapeStyle()
                {
                    Fill = "#522c0a",
                    StrokeWidth = 0
                },
                Width = 160,
                Height = 44,
                OffsetX = 80,
                OffsetY = 76,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1179);
            Node FloorPlaneShapes1180 = new Node()
            {
                ID = "Wardrobe5Kkgznd",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Rectangle,
                    CornerRadius = 0.5
                },
                Style = new ShapeStyle()
                {
                    Fill = "#522c0a"
                },
                Width = 3,
                Height = 1,
                OffsetX = 49.5,
                OffsetY = 102.5,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1180);
            Node FloorPlaneShapes1181 = new Node()
            {
                ID = "Wardrobe6o9EXmW",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                Shape = new BasicShape()
                {
                    Type = NodeShapes.Basic,
                    Shape = NodeBasicShapes.Rectangle,
                    CornerRadius = 0.5
                },
                Style = new ShapeStyle()
                {
                    Fill = "#522c0a"
                },
                Width = 3,
                Height = 1,
                OffsetX = 115.17000579833984,
                OffsetY = 107.69000244140625,
                Constraints = NodeConstraints.Default & ~NodeConstraints.Select
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1181);
            NodeGroup FloorPlaneShapes1182 = new NodeGroup()
            {
                ID = "Wardrobe",
                AdditionalInfo = new Dictionary<string, object>() { { "type", "FloorPlanShapes" } },
                SearchTags = new List<string>() { "Floor" },
                Children = new string[] {
               "Wardrobe2DkdyWy",
               "Wardrobe3I9qfIw",
               "Wardrobe41tIQyC",
               "Wardrobe5Kkgznd",
               "Wardrobe6o9EXmW"
            }
            };
            FloorPlaneShapesList.Add(FloorPlaneShapes1182);
            FloorPlaneShapes = new Palette() { ID = "FloorPlaneShapes", IsExpanded = false, Symbols = FloorPlaneShapesList, Title = "Floor Plane Shapes"};
        }

        public void InitializeBpmnShapes()
        {
            BPMNShapeList = new DiagramObjectCollection<NodeBase>();
            Node taskNode = new Node()
            {
                ID = "Task",
                Width = 35,
                Height = 30,
                SearchTags = new List<string>() { "Bpmn" },
                Shape = new BpmnActivity() { ActivityType = BpmnActivityType.Task, TaskType = BpmnTaskType.None }
            };
            BPMNShapeList.Add(taskNode);
            Node gatewayNode = new Node()
            {
                ID = "Gateway",
                Width = 96,
                Height = 72,
                SearchTags = new List<string>() { "Bpmn" },
                Shape = new BpmnGateway() { GatewayType = BpmnGatewayType.None }
            };
            BPMNShapeList.Add(gatewayNode);
            Node intermediateEventNode = new Node()
            {
                ID = "IntermediateEvent",
                Width = 30,
                Height = 30,
                SearchTags = new List<string>() { "Bpmn" },
                Shape = new BpmnEvent() { EventType = BpmnEventType.Intermediate, Trigger = BpmnEventTrigger.None },
                Tooltip = new DiagramTooltip()
                {
                    Content = "Intermediate Event"
                },
                Constraints = NodeConstraints.Default | NodeConstraints.Tooltip
            };
            BPMNShapeList.Add(intermediateEventNode);
            Node endEventNode = new Node()
            {
                ID = "EndEvent",
                Width = 30,
                Height = 30,
                SearchTags = new List<string>() { "Bpmn" },
                Shape = new BpmnEvent() { EventType = BpmnEventType.End, Trigger = BpmnEventTrigger.None },
                Tooltip = new DiagramTooltip()
                {
                    Content = "End Event"
                },
                Constraints = NodeConstraints.Default | NodeConstraints.Tooltip
            };
            BPMNShapeList.Add(endEventNode);
            Node startEventNode = new Node()
            {
                ID = "StartEvent",
                Width = 30,
                Height = 30,
                SearchTags = new List<string>() { "Bpmn" },
                Shape = new BpmnEvent() { EventType = BpmnEventType.Start, Trigger = BpmnEventTrigger.None },
                Tooltip = new DiagramTooltip()
                {
                    Content = "Start Event"
                },
                Constraints = NodeConstraints.Default | NodeConstraints.Tooltip
            };
            BPMNShapeList.Add(startEventNode);
            Node collapsedSubProcessNode = new Node()
            {
                ID = "CollapsedSub-Process",
                Width = 96,
                Height = 72,
                SearchTags = new List<string>() { "Bpmn" },
                Shape = new BpmnActivity() { ActivityType = BpmnActivityType.CollapsedSubProcess },
                Tooltip = new DiagramTooltip()
                {
                    Content = "Collapsed Sub-Process"
                },
                Constraints = NodeConstraints.Default | NodeConstraints.Tooltip
            };
            BPMNShapeList.Add(collapsedSubProcessNode);
            Node expandedSubProcessNode = new Node()
            {
                ID = "ExpandedSub-Process",
                Width = 96,
                Height = 72,
                SearchTags = new List<string>() { "Bpmn" },
                Shape = new BpmnExpandedSubProcess()
                {
                    SubProcessType = BpmnSubProcessType.Transaction
                },
                Tooltip = new DiagramTooltip()
                {
                    Content = "Expanded Sub-Process"
                },
                Constraints = NodeConstraints.Default | NodeConstraints.Tooltip
            };
            BPMNShapeList.Add(expandedSubProcessNode);
            Node textAnnotationNode = new Node()
            {
                ID = "TextAnnotation",
                Width = 96,
                Height = 72,
                Shape = new BpmnTextAnnotation(),
                Tooltip = new DiagramTooltip()
                {
                    Content = "Text Annotation"
                },
                SearchTags = new List<string>() { "Bpmn" },
                Constraints = NodeConstraints.Default | NodeConstraints.Tooltip
            };
            BPMNShapeList.Add(textAnnotationNode);
            Connector sequenceFlowConnector = new Connector()
            {
                ID = "SequenceFlow",
                SourcePoint = new DiagramPoint() { X = 0, Y = 0 },
                TargetPoint = new DiagramPoint() { X = 60, Y = 60 },
                Type = ConnectorSegmentType.Straight,
                Shape = new BpmnFlow() { Flow = BpmnFlowType.SequenceFlow },
                Tooltip = new DiagramTooltip()
                {
                    Content = "Sequence Flow"
                },
                SearchTags = new List<string>() { "Bpmn" },
                Constraints = ConnectorConstraints.Default | ConnectorConstraints.Tooltip
            };
            BPMNShapeList.Add(sequenceFlowConnector);
            Connector associationConnector = new Connector() { ID = "Association", SourcePoint = new DiagramPoint() { X = 0, Y = 0 }, 
                SearchTags = new List<string>() { "Bpmn" },
                TargetPoint = new DiagramPoint() { X = 60, Y = 60 }, Type = ConnectorSegmentType.Straight, 
                Shape = new BpmnFlow() { Flow = BpmnFlowType.AssociationFlow }, };
            BPMNShapeList.Add(associationConnector);
            Connector messageFlowConnector = new Connector()
            {
                ID = "MessageFlow",
                SourcePoint = new DiagramPoint() { X = 0, Y = 0 },
                TargetPoint = new DiagramPoint() { X = 60, Y = 60 },
                Type = ConnectorSegmentType.Straight,
                SearchTags = new List<string>() { "Bpmn" },
                TargetDecorator = new DecoratorSettings() { Style = new ShapeStyle() { Fill = "white" } },
                Style = new ShapeStyle() { StrokeDashArray = "5 5" },
                Tooltip = new DiagramTooltip()
                {
                    Content = "Message Flow"
                },
                Constraints = ConnectorConstraints.Default | ConnectorConstraints.Tooltip
            };
            BPMNShapeList.Add(messageFlowConnector);
            Node messageNode = new Node()
            {
                ID = "Message",
                Width = 72,
                Height = 48,
                SearchTags = new List<string>() { "Bpmn" },
                Shape = new BpmnMessage()
            };
            BPMNShapeList.Add(messageNode);
            Node dataObjectNode = new Node()
            {
                ID = "DataObject",
                Width = 48,
                Height = 62,
                Shape = new BpmnDataObject() { IsCollectiveData = false, DataObjectType = BpmnDataObjectType.None },
                Tooltip = new DiagramTooltip()
                {
                    Content = "Data Object"
                },
                SearchTags = new List<string>() { "Bpmn" },
                Constraints = NodeConstraints.Default | NodeConstraints.Tooltip
            };
            BPMNShapeList.Add(dataObjectNode);
            Node dataStoreNode = new Node()
            {
                ID = "DataStore",
                Width = 96,
                Height = 76,
                Shape = new BpmnDataStore(),
                Tooltip = new DiagramTooltip()
                {
                    Content = "Data Store"
                },
                SearchTags = new List<string>() { "Bpmn" },
                Constraints = NodeConstraints.Default | NodeConstraints.Tooltip
            };
            BPMNShapeList.Add(dataStoreNode);
            BpmnShapePalette = new Palette() { ID = "BPMNShapes", IsExpanded = false, Symbols = BPMNShapeList, Title = "BPMN Shapes"};

        }

        public void InitializeSwimlaneShapes()
        {
            SwimlaneShapeList = new DiagramObjectCollection<NodeBase>();
            Lane horizontalLane = new Lane()
            {
                ID = "HorizontalSwimlane",
                Orientation = Orientation.Horizontal,
                Height = 100,
                Width = 150,
                SearchTags = new List<string>() { "Swimlane" },
                Header = new SwimlaneHeader()
                {
                    Annotation = new ShapeAnnotation() { Content = "Lane Title" },
                    Style = new TextStyle() { Fill = "lightblue", StrokeColor = "black" },
                    Width = 25,
                    Height = 100
                },
            };
            SwimlaneShapeList.Add(horizontalLane);
            Lane verticalLane = new Lane()
            {
                ID = "VerticalSwimlane",
                Orientation = Orientation.Vertical,
                Height = 150,
                Width = 100,
                // Style = new TextStyle() { Fill = "orange", StrokeColor = "black" },
                SearchTags = new List<string>() { "Swimlane" },
                Header = new SwimlaneHeader()
                {
                    Annotation = new ShapeAnnotation() { Content = "Lane Title" },
                    Style = new TextStyle() { Fill = "lightblue", StrokeColor = "black" },
                    Width = 100,
                    Height = 25
                },
            };
            SwimlaneShapeList.Add(verticalLane);

            //Create a horizontal phase.
            Phase horizontalPhase = new Phase() { ID = "HorizontalPhase", Orientation = Orientation.Horizontal, 
                SearchTags = new List<string>() { "Swimlane" },
                Width = 80, Height = 1, Style = new ShapeStyle() { Fill = "#5b9bd5", StrokeColor = "#5b9bd5" } };
            SwimlaneShapeList.Add(horizontalPhase);

            //Create a vertical phase.
            Phase verticalPhase = new Phase() { ID = "VerticalPhase", Orientation = Orientation.Vertical, 
                SearchTags = new List<string>() { "Swimlane" },
                Width = 1, Height = 80, Style = new ShapeStyle() { Fill = "#5b9bd5", StrokeColor = "#5b9bd5" } };
            SwimlaneShapeList.Add(verticalPhase);
            SwimlaneShapePalette = new Palette() { ID = "SwimlaneShapes", IsExpanded = false, Symbols = SwimlaneShapeList, Title = "Swimlane Shapes"};

        }

        internal async Task UpdatePalettes(List<DiagramMoreShapes.ListViewDataFields> SelectedItems)
        {
            DiagramObjectCollection<Palette> addPalettes = new DiagramObjectCollection<Palette>();
            List<string> RemovePalette = new List<string>();
            foreach (DiagramMoreShapes.ListViewDataFields data in SelectedItems)
            {
                string paletteName = data.Text; bool isChecked = data.Checked;
                switch (paletteName)
                {
                    case "Flow":
                        if (isChecked)
                        {
                            if (!Palettes.Contains(FlowShapePalette))
                                addPalettes.Add(FlowShapePalette);
                            PaletteInstance.AddPalettes(addPalettes);
                        }
                        else
                            PaletteInstance.RemovePalettes(FlowShapePalette.ID);
                        break;
                    case "Basic":
                        if (isChecked)
                        {
                            if (!Palettes.Contains(BasicShapePalette))
                                addPalettes.Add(BasicShapePalette);
                            PaletteInstance.AddPalettes(addPalettes);
                        }
                        else
                            PaletteInstance.RemovePalettes(BasicShapePalette.ID);
                        break;
                    case "BPMN":
                        if (isChecked)
                        {
                            if (!Palettes.Contains(BpmnShapePalette))
                                addPalettes.Add(BpmnShapePalette);
                            PaletteInstance.AddPalettes(addPalettes);
                        }
                        else
                            PaletteInstance.RemovePalettes(BpmnShapePalette.ID);
                        break;
                    case "Connectors":
                        if (isChecked)
                        {
                            if (!Palettes.Contains(ConnectorPalette))
                                addPalettes.Add(ConnectorPalette);
                            PaletteInstance.AddPalettes(addPalettes);
                        }
                        else
                            PaletteInstance.RemovePalettes(ConnectorPalette.ID);
                        break;
                    case "Swimlane":
                        if (isChecked)
                        {
                            if (!Palettes.Contains(SwimlaneShapePalette))
                                addPalettes.Add(SwimlaneShapePalette);
                            PaletteInstance.AddPalettes(addPalettes);
                        }
                        else
                            PaletteInstance.RemovePalettes(SwimlaneShapePalette.ID);
                        break; 
                    case "Network":
                        if (isChecked)
                        {
                            if (!Palettes.Contains(NetworkShapes))
                                addPalettes.Add(NetworkShapes);
                            PaletteInstance.AddPalettes(addPalettes);

                        }
                        else
                            PaletteInstance.RemovePalettes(NetworkShapes.ID);
                        break; 
                    case "Electrical":
                        if (isChecked)
                        {
                            if (!Palettes.Contains(ElectericalShapes))
                                addPalettes.Add(ElectericalShapes);
                            PaletteInstance.AddPalettes(addPalettes);
                        }
                        else
                            PaletteInstance.RemovePalettes(ElectericalShapes.ID);
                        break;
                    case "Floorplan":
                        if (isChecked)
                        {
                            if (!Palettes.Contains(FloorPlaneShapes))
                                addPalettes.Add(FloorPlaneShapes);
                            PaletteInstance.AddPalettes(addPalettes);
                        }
                        else
                            PaletteInstance.RemovePalettes(FloorPlaneShapes.ID);
                        break;
                }
            }
            PaletteInstance.AddPalettes(addPalettes);
            StateHasChanged();
        }
    }
}
