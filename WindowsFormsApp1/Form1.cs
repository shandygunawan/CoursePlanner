using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoursePlanner;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;

namespace WindowsFormsApp1
{   
    public partial class Form1 : Form
    {
        public string filename;
        public Form1()
        {
            InitializeComponent();
            nextButton.Visible = false;
            dfsButton.Visible = false;
            bfsButton.Visible = false;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            filename = null;
            openFileDialog1.InitialDirectory = "";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileInfo finfo = new System.IO.FileInfo(openFileDialog1.FileName);
                string strfilename = finfo.Name;
                string filepath = finfo.DirectoryName;
                string fullpath = filepath + "\\" + strfilename;
                //MessageBox.Show(fullpath);
                filename = fullpath;
                //filename = System.IO.Path.GetFileName();
                //MessageBox.Show(filename);
                browseText.Text = fullpath;
            } else
            {
                MessageBox.Show("Error");
            }
            List<List<string>> tempstr = new List<List<string>>();
            try
            {
                if (filename != null)
                {
                    string[] buffer = ExternalFile.Reader(filename);
                    foreach (string member in buffer)
                    {
                        textBox1.Text += member + "\r\n";
                    }
                    
                }
                string str = textBox1.Text;
                dfsButton.Visible = true;
                bfsButton.Visible = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Could not read file");
            }
        }

        /*private void textBox1_Click(object sender, EventArgs e)
        {
            List<List<string>> tempstr = new List<List<string>>();
            try
            {
                if (filename != null)
                {
                    string[] buffer = ExternalFile.Reader(filename);
                    foreach(string member in buffer)
                    {
                        textBox1.Text += member + "\r\n";
                    }
                }
                string str = textBox1.Text;
            }
            catch (Exception)
            {
                MessageBox.Show("Could not read file");
            }
        }*/

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Show();
            textBox1.ScrollBars = ScrollBars.Vertical;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void bfsButton_Click(object sender, EventArgs e)
        {
            nextButton.Visible = true;
            dfsButton.Visible = false;
            bfsButton.Visible = false;
            CoursePlanner.Program.Execute("BFS", filename);
            CoursePlanner.Planner.metode = "BFS";
            PrintPlan(CoursePlanner.Program.CoursePlan);
            GraphRenderer renderer = new GraphRenderer(Planner.final_graph);
            renderer.CalculateLayout();
            int width = 100;
            Bitmap bitmap = new Bitmap(width, (int)(Planner.final_graph.Height * (width / Planner.final_graph.Width)), PixelFormat.Format32bppPArgb);
            renderer.Render(bitmap);
            bitmap.Save("Test.png");

            GraphPictureBox.Image = Image.FromFile("Test.png");
            GraphPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void dfsButton_Click(object sender, EventArgs e)
        {
            nextButton.Visible = true;
            dfsButton.Visible = false;
            bfsButton.Visible = false;
            CoursePlanner.Program.Execute("DFS", filename);
            CoursePlanner.Planner.metode = "DFS";
            PrintPlan(CoursePlanner.Program.CoursePlan);
            GraphRenderer renderer = new GraphRenderer(Planner.final_graph);
            renderer.CalculateLayout();
            int width = 100;
            Bitmap bitmap = new Bitmap(width, (int)(Planner.final_graph.Height * (width / Planner.final_graph.Width)), PixelFormat.Format32bppPArgb);
            renderer.Render(bitmap);
            bitmap.Save("Test.png");
            CoursePlanner.Planner.counter++;

            GraphPictureBox.Image = Image.FromFile("Test.png");
            GraphPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if(CoursePlanner.Planner.metode == "DFS")
            {
                WindowsFormsApp1.ViewerGraph.nextDFS();
                GraphRenderer renderer = new GraphRenderer(Planner.final_graph);
                renderer.CalculateLayout();
                int width = 100;
                Bitmap bitmap = new Bitmap(width, (int)(Planner.final_graph.Height * (width / Planner.final_graph.Width)), PixelFormat.Format32bppPArgb);
                renderer.Render(bitmap);
                string savename = "Test" + CoursePlanner.Planner.counter + ".png";
                bitmap.Save(savename);
                CoursePlanner.Planner.counter++;

                GraphPictureBox.Image = Image.FromFile(savename);
                GraphPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
                
            } 
            else // BFS
            {
                if (CoursePlanner.Program.CoursePlan.Count > 0)
                {
                    WindowsFormsApp1.ViewerGraph.nextBFS();
                    GraphRenderer renderer = new GraphRenderer(Planner.final_graph);
                    renderer.CalculateLayout();
                    int width = 100;
                    Bitmap bitmap = new Bitmap(width, (int)(Planner.final_graph.Height * (width / Planner.final_graph.Width)), PixelFormat.Format32bppPArgb);
                    renderer.Render(bitmap);
                    string savename = "Test" + CoursePlanner.Planner.counter + ".png";
                    bitmap.Save(savename);
                    CoursePlanner.Planner.counter++;

                    GraphPictureBox.Image = Image.FromFile(savename);
                    GraphPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;

                } 
                else
                {
                    nextButton.Visible = false;
                }
            }
            
        }

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

        public void PrintCourses(List<string> courses)
        {
            for (int i = 0; i < courses.Count; i++)
            {
                if (i == (courses.Count - 1))
                {
                    string toPrint = courses[i] + ".";
                    textBox1.Text += toPrint;
                }
                else
                {
                    string toPrint = courses[i] + ",";
                    textBox1.Text += toPrint;
                }
            }
            textBox1.Text += "\r\n";
        }

        private void PrintPlan(List<List<string>> plan)
        {
            textBox1.Clear();
            int semester = 1;
            textBox1.Text += "Your Plan : \r\n";

            foreach (List<string> TermPlan in plan)
            {
                string toPrint = "semester " + semester + " = ";
                textBox1.Text += toPrint;
                PrintCourses(TermPlan);
                semester++;
            }
        }
    }
    public class ExternalFile
    {
        static public string[] Reader(string filename)
        {
            char[] delimiterChars = { ',', '.', ' ' };
            string[] lines = File.ReadAllLines(filename);

            return lines;
        }
    }
    /*
    public partial class Form2 : Form
    {
        public Form2()
        {
            this.Size = new Size(500, 600);
            this.nextButton = new System.Windows.Forms.Button();
            this.nextButton.Location = new System.Drawing.Point(20, 20);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 5;
            this.nextButton.Text = "Next Step";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            currentgraph = 0;
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            viewer.Size = new Size(300, 600);
            viewer.Graph = Planner.final_graph;
            viewer.Location = new Point(100, 0);
            this.Controls.Add(viewer);
            this.ResumeLayout();
            this.nextButton.Show();
            this.nextButton.Visible = true;
            System.Diagnostics.Stopwatch t = new System.Diagnostics.Stopwatch();
            t.Start();
            while (true)
            {
                if (t.Elapsed.TotalSeconds % 2 == 0)
                {
                    WindowsFormsApp1.ViewerGraph.Animate(viewer, Planner.final_graph, currentgraph);
                    currentgraph++;
                    this.ShowDialog();
                }
            }
        }
        private System.Windows.Forms.Button nextButton;
        public int currentgraph;
        public Microsoft.Msagl.GraphViewerGdi.GViewer viewer;
        private void nextButton_Click(object sender, EventArgs e)
        {
            currentgraph++;
           // WindowsFormsApp1.ViewerGraph.Animate(viewer, Planner.graph_state, currentgraph);
        }

    }
    */
}
