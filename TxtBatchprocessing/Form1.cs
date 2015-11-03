/*  2015(c) Socrates Wong
 *  Permissiable use granted under Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International, written permission required for other usage.   
 * 
 * 
 */
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
using System.Text.RegularExpressions;

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

        private void button4_Click(object sender, EventArgs e)
        {
            IO.output();
        }
    }
    public class IO{
        public static List<String> x = new List<String>();
        public static List<String> errormakers = new List<String>();
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
                string work = end == -1 ? str.Substring(start) : str.Substring(start, end - start);
                try
                {
                    
                    work = work.Replace( "\r", "").Replace("\n","");
                    //work.Replace("\n", String.Empty);
                    //Console.Out.Write("\nStart of fun"+work);
                    char[] delimiterChars = { '|',',',',' };
                    input = work.Split(delimiterChars);
                    input[0] = input[0].Trim().ToLower();
                    //Console.Out.Write("Delimited Vales\n");
                    
                    try
                    {
                        string resultString = Regex.Match(input[2].Trim(), @"\d+\.{0,1}\d*").Value;
                        Console.Out.Write("\n" + resultString);
                        int hr = (int)(Double.Parse(resultString)*60);
                        if (!names.ContainsKey(input[0])) {
                            names.Add(input[0], arraymax++);
                            //hours[names[input[0]]] = 0;
                        }
                        //input[2] = input[2].Replace(" ", "");


                        
                        hours[names[input[0]]]+=hr;
                        Console.Out.Write(input[0] + "\t has done: " + Double.Parse(resultString)*60 + "\t Total done: " + hours[names[input[0]]] + "\n");
                    }
                    catch (Exception e) {
                        Console.Out.Write("\n============================Inner Error=========================\n");
                        errormakers.Add(str);
                        Console.Out.Write(work+"\n");
                        Console.Out.Write(e.ToString()+"\n");
                        Console.Out.Write("\n***************************End Error****************************\n");
                    }
                }
                catch (Exception e) {
                    Console.Out.Write("\n"+"#################################Outer error#########################");
                    Console.Out.Write(start+"End:"+end+"\n"+ str);
                    errormakers.Add(str);
                    Console.Out.Write(e.ToString()+"\n");
                    Console.Out.Write("\n***************************End Error****************************\n");
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
        public static void output() {
            StreamWriter writetext = new StreamWriter("output.csv");
            foreach (KeyValuePair<string, int> entry in names)
            {   
                //Console.Out.Write("Name: "+ entry.Key + " Has Done: " + hours[entry.Value]);
                writetext.WriteLine(entry.Key + ",{0:f3}",hours[entry.Value]/60.000);
                // do something with entry.Value or entry.Key
            }
            writetext.Close();
            writetext = new StreamWriter("idiots.csv");
            foreach (String entry in errormakers)
            {
                writetext.WriteLine("\n============================Start Error=========================\n");
                //Console.Out.Write("Name: "+ entry.Key + " Has Done: " + hours[entry.Value]);
                writetext.WriteLine( entry );
                // do something with entry.Value or entry.Key
                writetext.WriteLine("\n****************************End Error***************************\n");
            }
            writetext.Close();
        }
    
    
    
    }
}
