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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace OpenPowerCfg.GUI 
{
    public partial class AboutBox : Form 
    {
        public AboutBox() 
        {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
            this.label3.Text = "Version " + 
                System.Windows.Forms.Application.ProductVersion;

            projectLinkLabel.Links.Remove(projectLinkLabel.Links[0]);
            projectLinkLabel.Links.Add(0, projectLinkLabel.Text.Length,
                "http://code.google.com/p/open-powercfg/");

            licenseLinkLabel.Links.Remove(licenseLinkLabel.Links[0]);
            licenseLinkLabel.Links.Add(0, licenseLinkLabel.Text.Length,
                "License.html");
        }

        private void linkLabel_LinkClicked(object sender, 
            LinkLabelLinkClickedEventArgs e) 
        {
            try 
            {
                Process.Start(new ProcessStartInfo(e.Link.LinkData.ToString()));
            } 
            catch 
            { }
        }
    }
}
