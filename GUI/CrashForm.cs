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
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace OpenPowerCfg.GUI 
{
    public partial class CrashForm : Form 
    {
        private Exception exception;

        public CrashForm() 
        {
            InitializeComponent();
        }

        public Exception Exception 
        {
            get { return exception; }
            set 
            {
                exception = value;
                StringBuilder s = new StringBuilder();
                Version version = typeof(CrashForm).Assembly.GetName().Version;
                s.Append("Version: "); s.AppendLine(version.ToString());                
                s.AppendLine();
                s.AppendLine(exception.ToString());
                s.AppendLine();
                if (exception.InnerException != null) 
                {
                    s.AppendLine(exception.InnerException.ToString());
                    s.AppendLine();
                }
                s.Append("Common Language Runtime: "); 
                s.AppendLine(Environment.Version.ToString());
                s.Append("Operating System: ");
                s.AppendLine(Environment.OSVersion.ToString());
                s.Append("Process Type: ");
                s.AppendLine(IntPtr.Size == 4 ? "32-Bit" : "64-Bit");
                reportTextBox.Text = s.ToString();                
            }
        }

        private void sendButton_Click(object sender, EventArgs e) 
        {
            try 
            {
                Version version = typeof(CrashForm).Assembly.GetName().Version;
                WebRequest request = WebRequest.Create(
                    "http://OpenPowerCfg.org/report.php");
                request.Method = "POST";
                request.Timeout = 5000;
                request.ContentType = "application/x-www-form-urlencoded";

                string report =
                    "type=crash&" +
                    "version=" + Uri.EscapeDataString(version.ToString()) + "&" +
                    "report=" + Uri.EscapeDataString(reportTextBox.Text) + "&" +
                    "comment=" + Uri.EscapeDataString(commentTextBox.Text) + "&" +
                    "email=" + Uri.EscapeDataString(emailTextBox.Text);
                byte[] byteArray = Encoding.UTF8.GetBytes(report);
                request.ContentLength = byteArray.Length;

                try 
                {
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();

                    WebResponse response = request.GetResponse();
                    dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();

                    Close();
                } 
                catch (WebException) 
                {
                    MessageBox.Show("Sending the crash report failed.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } 
            catch 
            {
            }
        }
    }    
}
