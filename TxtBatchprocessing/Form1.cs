using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TxtBatchprocessing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IO.load();
            listBox1.Items.AddRange(IO.x.ToArray<String>());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IO.parse();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IO.print();
        }
    }
    public class IO{
        public static List<String> x = new List<String>();
        public static Dictionary<String, int> names = new Dictionary<String, int>();
        public static int[] hours = new int[50000];
        static int arraymax = 0;
        public static void load(){
            x = new List<string>();
            foreach (string file in Directory.EnumerateFiles("mas", "*.txt"))
            {
                x.Add( File.ReadAllText(file));
            }
        }
        public static void parse() {
            names = new Dictionary<string,int>();
            foreach (string str in x) { 

                string[] input=new string[3];

                int start = str.IndexOf("To: everettpberry@gmail.com") + "To: everettpberry@gmail.com".Length;
                int end= str.IndexOf("--");
                try
                {
                    string work = str.Substring(start, end - start);
                    work = work.Replace( "\r", "").Replace("\n","");
                    //work.Replace("\n", String.Empty);
                    Console.Out.Write("\nStart of fun"+work);
                    char[] delimiterChars = { '|',',' };
                    input = work.Split(delimiterChars);
                    Console.Out.Write("Delimited Vales\n");
                    foreach (string inpu in input) {
                        Console.Out.Write(input + "\n");
                    }
                    try
                    {
                        if (!names.ContainsKey(input[0])) { 
                            names.Add(input[0], arraymax++);
                            names[input[0]] = 0;
                        }
                        //input[2] = input[2].Replace(" ", "");
                        names[input[0]] += Int32.Parse(input[2].Trim());

                        Console.Out.Write(names[input[0]] + "\t has done: " + names[input[0]]);
                    }
                    catch (Exception e) {
                        Console.Out.Write("\nInner Error");
                        foreach (string stri in input) {
                            Console.Out.Write(stri+"\t");
                        }
                        Console.Out.Write("\n");
                    }
                }
                catch (Exception e) {
                    Console.Out.Write("\n"+"Outer error"+"\n"+ str);
                }
                
            }
        }
        public static void print() {
            foreach (KeyValuePair<string, int> entry in names)
            {
                Console.Out.Write("Name: " + entry.Key + " Has Done: " + hours[entry.Value]); Console.Out.Write("\n");
                // do something with entry.Value or entry.Key
            }
            
        }
    
    
    
    
    }
}
