using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

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
    public class Animation
    {
        Bitmap[] images;

        int place = 0;

        public Animation(Bitmap[] Frames)
        {
            images = Frames;
        }

        public Bitmap GiveNextImage()
        {
            Bitmap b = null;

            if (place < images.Length)
            {
                b = images[place++];
            }
            else
            {
                place = 0;
                b = images[place++];
            }
            return b;
        }
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
    }
    /* ============================ */
    /*           PARSING            */
    /* ============================ */
    public class ViewerGraph
    {
        static public void CreateGraph(ref CourseNode[] nodes)
        {
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            List<string> node_name = new List<string>();
            foreach (CourseNode Course in nodes)
            {
                node_name.Add(Course.name);
            }
            foreach (string course in node_name)
            {
                graph.AddNode(course).Attr.Color = Microsoft.Msagl.Drawing.Color.Aqua;
            }
            foreach (CourseNode Course in nodes)
            {
                foreach (string child in Course.children)
                {
                    graph.AddEdge(Course.name, child);
                }
            }
            viewer.Graph = graph;
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            form.ShowDialog();
        }

    }
    public class FileReader
    {
        //ATTRIBUTES

        //METHOD
        static public void ReadFile(ref List<List<string>> list)
        {
            int counter = 0;
            char[] delimiterChars = { ',', '.', ' ' };

            Console.Write("Enter the name of the file (with extension) : ");
            string[] lines = File.ReadAllLines(Console.ReadLine());

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
        static public void DFS(ref CourseNode[] Nodes, List<string> cNode, ref int time)
        {

            //cari cNode di Nodes
            for (int y = 0; y < cNode.Count; y++)
            {
                int index;
                index = 0;
                for (int i = 0; i < Nodes.Length; i++)
                {
                    if (Nodes[i].name == cNode[y])
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
            }
            for (int y = 0; y < cNode.Count; y++)
            {
                int index;
                index = 0;
                for (int i = 0; i < Nodes.Length; i++)
                {
                    if (Nodes[i].name == cNode[y])
                    {
                        index = i;
                        break;
                    }
                }
                if (Nodes[index].children.Count != 0)
                {
                    if ((Nodes[index].startTime == 0) || (Nodes[index].stopTime == 0))
                    {
                        DFS(ref Nodes, Nodes[index].children, ref time);
                    }
                }
            }
            for (int y = 0; y < cNode.Count; y++)
            {
                int index;
                index = 0;
                for (int i = 0; i < Nodes.Length; i++)
                {
                    if (Nodes[i].name == cNode[y])
                    {
                        index = i;
                        break;
                    }
                }
                if (Nodes[index].stopTime == 0)
                {
                    Nodes[index].stopTime = time;
                    time = time + 1;
                }
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
            DFS(ref Nodes, cNodes, ref time);
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
        }


        // Plan Courses with BFS Algorithm
        static public void AddPlanBFS(ref List<List<string>> courses, ref List<List<string>> plan)
        {
            // Timer for error handling
            Stopwatch timer = new Stopwatch();
            timer.Start();

            // Keep repeating until there is no course left
            while (courses.Count > 0)
            {
                // Error Handling
                if (timer.Elapsed.TotalSeconds > 1)
                {
                    throw (new NotPlanable("Cyclic Courses or Undefined Courses"));
                }

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
                DeletePreqs(ref courses, TermPlan); // Delete TermPlan's courses from the requirements of courses
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
        // METHOD
        static void PrintAllCourses(List<List<string>> list)
        {
            Console.WriteLine("Course\tRequired Courses");
            Console.WriteLine("------\t----------------");
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count; j++)
                {
                    if (j == 0)
                    {
                        Console.Write("{0}\t", list[i][j]);
                    }
                    else if (j == (list[i].Count - 1))
                    {
                        Console.Write("{0}.", list[i][j]);
                    }
                    else
                    {
                        Console.Write("{0}, ", list[i][j]);
                    }
                }
                Console.WriteLine("");
            }
        }

        static void PrintCourses(List<string> courses)
        {
            for (int i = 0; i < courses.Count; i++)
            {
                if (i == (courses.Count - 1))
                {
                    Console.Write("{0}.", courses[i]);
                }
                else
                {
                    Console.Write("{0}, ", courses[i]);
                }
            }
            Console.WriteLine("");
        }

        static void PrintPlan(List<List<string>> plan)
        {
            int semester = 1;
            Console.WriteLine("Your Plan : ");

            foreach (List<string> TermPlan in plan)
            {
                Console.Write("Semester {0} = ", semester);
                PrintCourses(TermPlan);
                semester++;
            }
        }

        static void Main(string[] args)
        {
            // KAMUS
            List<List<string>> CoursesList = new List<List<string>>();
            List<List<string>> CoursePlan = new List<List<string>>();

            // ALGORITMA
            try
            {
                FileReader.ReadFile(ref CoursesList);
            }
            catch (IOException)
            {
                Console.WriteLine("File not found!");
                Console.ReadLine();
                return;
            }

            try
            {
                PrintAllCourses(CoursesList);
                Console.WriteLine("");
                CourseNode[] Nodes = new CourseNode[CoursesList.Count];
                // Transform list of courses to DAG (Directed Acyclic Graph)
                Planner.Transform(CoursesList, ref Nodes);
                ViewerGraph.CreateGraph(ref Nodes);

                /*
                Planner.AddPlanDFS(ref CoursesList, ref CoursePlan);
                PrintPlan(CoursePlan);
                ViewerGraph.CreateGraph(ref CoursePlan);
                */
                Console.ReadLine();
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