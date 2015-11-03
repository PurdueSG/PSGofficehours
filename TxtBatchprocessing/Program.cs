﻿/*  2015(c) Socrates Wong
 *  Permissiable use granted under Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International, written permission required for other usage.   
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TxtBatchprocessing
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
            Application.Run(new Form1());
        }
    }
}
