﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace worksheet
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new MainPage());
            }
            catch (Exception ex)
            {
            }
        }
    }
}
