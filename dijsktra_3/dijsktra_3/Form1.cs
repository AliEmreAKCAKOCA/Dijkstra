namespace dijsktra_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var nodes = new Dictionary<string, Node>
            {
                ["A"] = new Node("A"),
                ["B"] = new Node("B"),
                ["C"] = new Node("C"),
                ["D"] = new Node("D"),
                ["E"] = new Node("E"),
                ["F"] = new Node("F"),

            };


            nodes["A"].AddEdge(nodes["B"], 2);
            nodes["A"].AddEdge(nodes["D"], 8);
            nodes["B"].AddEdge(nodes["A"], 1);
            nodes["B"].AddEdge(nodes["D"], 5);
            nodes["B"].AddEdge(nodes["E"], 6);
            nodes["C"].AddEdge(nodes["E"], 9);
            nodes["C"].AddEdge(nodes["F"], 3);
            nodes["D"].AddEdge(nodes["A"], 8);
            nodes["D"].AddEdge(nodes["B"], 5);
            nodes["D"].AddEdge(nodes["E"], 3);
            nodes["D"].AddEdge(nodes["F"], 2);
            nodes["E"].AddEdge(nodes["B"], 6);
            nodes["E"].AddEdge(nodes["D"], 3);
            nodes["E"].AddEdge(nodes["F"], 1);
            nodes["E"].AddEdge(nodes["C"], 9);
            nodes["F"].AddEdge(nodes["C"], 3);
            nodes["F"].AddEdge(nodes["D"], 2);
            nodes["F"].AddEdge(nodes["E"], 1);


            var finalNode = nodes["C"];



            var distances = nodes.ToDictionary(kvp => kvp.Value, kvp => (int?)int.MaxValue);
            var parent = new Dictionary<Node, Node>();
            var undiscoverNodes = new HashSet<Node>(nodes.Values);

            distances[nodes["A"]] = 0;

            while (undiscoverNodes.Count > 0)
            {
                var current = undiscoverNodes.MinBy(node => distances[node]);

                undiscoverNodes.Remove(current);

                if (current == finalNode)
                {
                    break;
                }

                foreach (var (adjacentNode, distance) in current.Edges)
                {
                    var subDistance = distances[current] + distance;
                    if (subDistance < distances[adjacentNode])
                    {
                        distances[adjacentNode] = subDistance;
                        parent[adjacentNode] = current;

                    }

                }

            }


            var pathNodes = new List<Node>();
            var currentNode = finalNode;
            while (currentNode != null)
            {
                pathNodes.Insert(0, currentNode);
                currentNode = parent.TryGetValue(currentNode, out var parentNode) ? parentNode : null;


            }

            Console.WriteLine(string.Join("->", pathNodes.Select(i => i.Name)));

            textBox1.Text += string.Join("->", pathNodes.Select(i => i.Name));
            Console.WriteLine("total distanve: {0}", distances[finalNode]);



            Console.ReadLine();

        }
    }
}