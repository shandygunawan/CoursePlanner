using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoursePlanner;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        { 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form = new Form1();
            Application.Run(form);
        }
    }
    public class ViewerGraph
    {
        static public Graph CreateGraph(CourseNode[] nodes)
        {
            Graph graph = new Graph("Graph");
            List<string> node_name = new List<string>();
            foreach (CourseNode Course in nodes)
            {
                node_name.Add(Course.name);
            }
            foreach (string course in node_name)
            {
                graph.AddNode(course).Attr.Color = Microsoft.Msagl.Drawing.Color.Aqua;
                graph.Attr.OptimizeLabelPositions = true;
            }
            foreach (CourseNode Course in nodes)
            {
                foreach (string child in Course.children)
                {
                    graph.AddEdge(Course.name, child);
                }
            }
            return graph;
        }
        static public GViewer CreateGViewer(Graph graph)
        {
            GViewer viewer = new GViewer();
            viewer.Graph = graph;
            return viewer;
        }


        static public void nextBFS()
        {
            foreach (string course in CoursePlanner.Program.CoursePlan[0])
            {
                Planner.final_graph.FindNode(course).Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightBlue;
            }
            CoursePlanner.Program.CoursePlan.RemoveAt(0);
        }

        static public void nextDFS()
        {
            foreach (CourseNode course in Planner.NodeForDFS)
            {
                if (course.startTime == Planner.iterDFS || course.stopTime == Planner.iterDFS)
                {
                    foreach(Node whiteNode in Planner.final_graph.Nodes)
                    {
                        if(whiteNode.Id != course.name)
                        {
                            whiteNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
                        }
                    }
                    Planner.final_graph.Attr.Color = Microsoft.Msagl.Drawing.Color.White;
                    Planner.final_graph.FindNode(course.name).Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightBlue;
                    Planner.iterDFS++;
                    break;
                }
            }
            
        }
    }
}

namespace CoursePlanner
{
    public struct CourseNode
    {
        // ATTRIBUTE
        public string name;
        public List<string> children;
        public int startTime;
        public int stopTime;
    }
    /* ============================ */
    /*           PARSING            */
    /* ============================ */
    public class Parser
    {
        //ATTRIBUTES

        //METHOD
        static public void ParseFile(ref List<List<string>> list, string[] lines)
        {
            int counter = 0;
            char[] delimiterChars = { ',', '.', ' ' };

            foreach (string line in lines)
            {
                List<string> Requirements = new List<string>();
                string[] courses = line.Split(delimiterChars);
                foreach (string course in courses)
                {
                    if (course != null && course != "")
                    {
                        Requirements.Add(course);
                    }
                }
                counter += 1;
                list.Add(Requirements);
            }
        }
    }

    /* ============================ */
    /*            BFS-DFS           */
    /* ============================ */

    class Planner
    {
        // ATTRIBUTES
        public static Graph final_graph;
        public static GViewer viewer;
        public static string metode;
        public static int counter = 0;
        public static List<Graph> list_graph;
        public static CourseNode[] NodeForDFS;
        public static int iterDFS = 1;

        // METHOD
        static public void Transform(List<List<string>> courses, ref CourseNode[] Nodes)
        {
            // initialization
            for (int i = 0; i < courses.Count; ++i)
            {
                Nodes[i].name = courses[i][0];
                Nodes[i].children = new List<string>();
                Nodes[i].startTime = 0;
                Nodes[i].stopTime = 0;
            }

            // Searching for each of courses' children
            for (int i = 0; i < Nodes.Length; ++i)
            // Transverse on each node
            {
                foreach (List<string> course in courses)
                // transverse on each course & it's preqs
                {
                    int k = 0;
                    // if the node's name appear in the list of course & not placed at first then
                    // the course's name will be the node's child.
                    foreach (string C in course)
                    {
                        if (C.Equals(Nodes[i].name) && k > 0)
                        {
                            Nodes[i].children.Add(course[0]);
                        }
                        k++;
                    }
                }
            }

        }
        static public void DFS(ref CourseNode[] Nodes, string Node, ref int time)
        {

            //cari cNode di Nodes

            int index;
            index = 0;
            for (int i = 0; i < Nodes.Length; i++)
            {
                if (Nodes[i].name == Node)
                {
                    index = i;
                    break;
                }
            }
            if (Nodes[index].startTime == 0)
            {
                Nodes[index].startTime = time;
                time = time + 1;
            }

            if (Nodes[index].children.Count != 0)
            {
                if ((Nodes[index].startTime == 0) || (Nodes[index].stopTime == 0))
                {
                    foreach (string nd in Nodes[index].children)
                    {
                        DFS(ref Nodes, nd, ref time);
                    }
                }
            }


            if (Nodes[index].stopTime == 0)
            {
                Nodes[index].stopTime = time;
                time = time + 1;
            }
        }

        static public void selectSort(ref CourseNode[] Nodes)
        {
            //pos_min is short for position of min
            int pos_max;
            CourseNode temp;

            for (int i = 0; i < Nodes.Length - 1; i++)
            {
                pos_max = i;//set pos_max to the current index of array

                for (int j = i + 1; j < Nodes.Length; j++)
                {
                    if (Nodes[j].stopTime > Nodes[pos_max].stopTime)
                    {
                        //pos_max will keep track of the index that max is in, this is needed when a swap happens
                        pos_max = j;
                    }
                }

                //if pos_max no longer equals i than a smaller value must have been found, so a swap must occur
                if (pos_max != i)
                {
                    temp = Nodes[i];
                    Nodes[i] = Nodes[pos_max];
                    Nodes[pos_max] = temp;
                }
            }
        }
        static public void AddPlanDFS(ref List<List<string>> courses, ref List<List<string>> plan)
        // Plan courses with DFS Algorithm
        {
            CourseNode[] Nodes = new CourseNode[courses.Count];
            // Transform list of courses to DAG (Directed Acyclic Graph)
            Transform(courses, ref Nodes);

            List<string> cNodes = new List<string>();
            //buat nyari node yang pertama (ga punya ortu)
            for (int i = 0; i < Nodes.Length; i++)
            {
                cNodes.Add(Nodes[i].name);
            }
            for (int i = 0; i < Nodes.Length; i++)
            {
                foreach (string nama in Nodes[i].children)
                {
                    int j = 0;
                    int y = cNodes.Count;
                    for (int k = 0; k < y; k++)
                    {
                        if (cNodes[j] == nama)
                        {
                            cNodes.RemoveAt(j);
                        }
                        else
                        {
                            j = j + 1;
                        }
                    }
                }
            }

            //rekursi dfs
            int time = 1;
            foreach (string node in cNodes)
            {
                DFS(ref Nodes, node, ref time);
            }
            //ngurutin
            selectSort(ref Nodes);
            for (int i = 0; i < Nodes.Length; i++)
            {
                Console.Write("{0}\t", Nodes[i].name);
                Console.Write("{0}\t", Nodes[i].startTime);
                Console.WriteLine("{0}", Nodes[i].stopTime);
            }
            for (int i = 0; i < Nodes.Length; i++)
            {
                List<string> TermPlan = new List<string>();
                TermPlan.Add(Nodes[i].name);
                plan.Add(TermPlan);
            }

            NodeForDFS = Nodes;
        }

        // Plan Courses with BFS Algorithm
        static public void AddPlanBFS(ref List<List<string>> courses, ref List<List<string>> plan)
        {

            // Keep repeating until there is no course left
            while (courses.Count > 0)
            { 
                // BFS Algorithm
                // 1. Search for course(s) with zero requirement
                // 2. Add the course(s) to plan for a term
                // 3. Delete the course(s) from the Course list & from the requirement for courses
                // 4. Add the Term plan to the plan list.
                List<string> TermPlan = new List<string>(); // Ctor List of TermPlan

                foreach (List<string> course in courses.ToList()) // Transverse in the list of courses
                {
                    // Add and remove if the course doesn't have any requirements
                    if (course.Count == 1)
                    {
                        TermPlan.Add(course[0]);
                        courses.Remove(course);
                    }
                }
                // Delete TermPlan's courses from the requirements of courses
                DeletePreqs(ref courses, TermPlan);
                plan.Add(TermPlan); //add Term plan to final plan.
            }
        }

        // Remove course's preq
        static public void DeletePreqs(ref List<List<string>> courses, List<string> plan)
        {
            foreach (string toDelete in plan)
            {
                foreach (List<string> req in courses)
                {
                    req.Remove(toDelete);
                }
            }
        }
    }

    /* ============================ */
    /*           EXCEPTION          */
    /* ============================ */
    public class NotPlanable : Exception
    {
        public NotPlanable(string message) : base(message)
        {

        }
    }

    /* ============================ */
    /*             MAIN             */
    /* ============================ */
    class Program
    {
        static public List<List<string>> CoursesList = new List<List<string>>();
        static public List<List<string>> CoursePlan = new List<List<string>>();

        static public int getCount()
        {
            return CoursesList.Count;
        }


        public static void Execute(string method, string filename)
        {
            try
            {
                Parser.ParseFile(ref CoursesList, WindowsFormsApp1.ExternalFile.Reader(filename));

            }
            catch (IOException)
            {
                Console.WriteLine("File not found!");
                Console.ReadLine();
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Please Select an item first");
            }


            try
            {
                // Transform list of courses to DAG (Directed Acyclic Graph)
                CourseNode[] Nodes = new CourseNode[CoursesList.Count];
                Planner.Transform(CoursesList, ref Nodes);
                Planner.final_graph = WindowsFormsApp1.ViewerGraph.CreateGraph(Nodes);
               

                if (method == "DFS")
                {
                    Planner.list_graph = new List<Graph>();
                    Planner.AddPlanDFS(ref CoursesList, ref CoursePlan);
                }
                else   // method == "BFS"
                {
                    Planner.AddPlanBFS(ref CoursesList, ref CoursePlan);
                }
            }
            catch (NotPlanable e)
            {
                Console.WriteLine("Failed to make a plan.");
                Console.WriteLine("Reason: {0}.", e.Message);
                Console.ReadLine();
            }
        }
    }
}
