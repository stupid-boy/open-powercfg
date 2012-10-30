/*
        This program is free software: you can redistribute it and/or modify
        it under the terms of the GNU General Public License as published by
        the Free Software Foundation, either version 3 of the License, or
        (at your option) any later version.

        This program is distributed in the hope that it will be useful,
        but WITHOUT ANY WARRANTY; without even the implied warranty of
        MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.    See the
        GNU General Public License for more details.

        You should have received a copy of the GNU General Public License
        along with this program.    If not, see <http://www.gnu.org/licenses/>.
    
        Copyright (C) 2012 Andrey Mushatov ( openPowerCfg@gmail.com )
*/

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using OpenPowerCfg.GUI;

namespace OpenPowerCfg
{
    public static class Program 
    {

        [STAThread]
        public static void Main() 
        {
            #if !DEBUG
                Application.ThreadException += 
                    new ThreadExceptionEventHandler(Application_ThreadException);
                Application.SetUnhandledExceptionMode(
                    UnhandledExceptionMode.CatchException);

                AppDomain.CurrentDomain.UnhandledException += 
                    new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            #endif

            if (!IsOSSupported())
                Environment.Exit(0);

            if (!AllRequiredFilesAvailable())
                Environment.Exit(0);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (GUI.MainForm form = new GUI.MainForm()) {
                form.FormClosed += delegate(Object sender, FormClosedEventArgs e) {
                    Application.Exit();
                };                
                Application.Run();
            }
        }

        private static bool IsOSSupported()
        {
            // OS > XP, 2000 and 2003
            if (!((Environment.OSVersion.Platform == PlatformID.Win32NT) && (Environment.OSVersion.Version.Major > 5)) )
            {
                MessageBox.Show("Unsupported OS version.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private static bool IsFileAvailable(string fileName) 
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) +
                Path.DirectorySeparatorChar;

            if (!File.Exists(path + fileName)) 
            {
                MessageBox.Show("The following file could not be found: " + fileName + 
                    "\nPlease extract all files from the archive.", "Error",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private static bool AllRequiredFilesAvailable() 
        {
            return IsFileAvailable("Aga.Controls.dll");
        }

        private static void ReportException(Exception e) 
        {
        /*  CrashForm form = new CrashForm();
            form.Exception = e;
            form.ShowDialog();
        */
        }

        public static void Application_ThreadException(object sender, 
            ThreadExceptionEventArgs e) 
        {
            try 
            {
                ReportException(e.Exception);
            } 
            catch 
            {
            } 
            finally 
            {
                Application.Exit();
            }
        }

        public static void CurrentDomain_UnhandledException(object sender, 
            UnhandledExceptionEventArgs args) 
        {
            try 
            {
                Exception e = args.ExceptionObject as Exception;
                if (e != null)
                    ReportException(e);
            } 
            catch 
            {
            } 
            finally 
            {
                Environment.Exit(0);
            }
        }     
    }
}
