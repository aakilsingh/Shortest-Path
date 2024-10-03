namespace Shortest_Path
{
    public class Program
    {
        public static int shortestPath(List<List<string>> edges, string nodeA, string nodeB) {


            Dictionary<string, List<string>> graph = buildGraph(edges); // builds graph
            HashSet<string> visited = new HashSet<string>(); //  to keep track of visited nodes


            // on the queue we store the pairs with distance, not in the actual graph, we do it this way so we can increment the distance when we add something new to the queue
            Queue<(string,int)> queue = new Queue<(string,int)> ();

            queue.Enqueue((nodeA,0)); // add start to the queue and 0 distance
            visited.Add(nodeA);

            while (queue.Count > 0) { 
            
                (string,int) currentNode = queue.Dequeue ();
                if(currentNode.Item1 == nodeB)
                {
                    return currentNode.Item2;
                }


                foreach(string neighbour in graph.GetValueOrDefault(currentNode.Item1))
                {
                    if (!visited.Contains(neighbour))
                    {
                        queue.Enqueue((neighbour,currentNode.Item2 +1)); // this was my issue, had to add the current nodes distance +1 and not just increment a distance variable because that would increment for each neighbour
                        visited.Add(neighbour);
                    }
                }
            
            
            }

            return -1; // return -1 if there is no path
        
        }

        public static Dictionary<string,List<string>> buildGraph(List<List<string>> edges)
        {
            Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();

            foreach (List<string> edge in edges)
            {
                string a = edge.ElementAt(0);
                string b = edge.ElementAt(1);

                if (!graph.ContainsKey(a))
                {
                    graph.Add(a, new List<string>());
                }

                if (!graph.ContainsKey(b))
                {
                    graph.Add(b, new List<string>());
                }

                graph[a].Add(b);
                graph[b].Add(a);
                
            }

            return graph;
        }




        public static void Main(string[] args)
        {

            List<List<string>> edges = new List<List<string>>
            {
                new List<string> {"w", "x" },
                new List<string> {"x", "y"},
                new List<string> {"z", "y" },
                new List<string> {"z", "v" },
                new List<string> {"w", "v"},

        };
            Console.WriteLine(shortestPath(edges, "w", "z"));

            
            
            }
        }
    }

