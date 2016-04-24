using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ReplicaStudio.Editor.Forms;
using ReplicaStudio.Editor.TransverseLayer;
using System.Globalization;
using Ini;
using ReplicaStudio.Editor.TransverseLayer.Constants;
using System.IO;
using System.Configuration;

namespace ReplicaStudio.Editor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //CONFIGURATION DE L'APPLICATION
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //CONFIGURATION DE LA LANGUE COURANTE
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(EditorSettings.Instance.Language);

            //LANCEMENT DE LA FENETRE
            Application.Run(new Forms.Main(args));
        }
    }
}
