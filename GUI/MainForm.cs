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
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using OpenPowerCfg.PowerManagement;

namespace OpenPowerCfg.GUI 
{
    public partial class MainForm : Form 
    {
        private PersistentSettings settings;
        private ComputerNode root;
        private TreeModel treeModel;
        private UserOption showHiddenSettings;
        private UserOption applyChanges;
        private UserOption forcePowerManager;

        public MainForm() 
        {
            InitializeComponent();

            this.settings = new PersistentSettings();
            this.settings.Load(Path.ChangeExtension(Application.ExecutablePath, ".config"));

            // make sure the buffers used for double buffering are not disposed 
            // after each draw call
            BufferedGraphicsManager.Current.MaximumBuffer = Screen.PrimaryScreen.Bounds.Size;

            // set the DockStyle here, to avoid conflicts with the MainMenu
            this.splitContainer.Dock = DockStyle.Fill;
                        
            this.Font = SystemFonts.MessageBoxFont;
            treeView.Font = SystemFonts.MessageBoxFont;

            nodeTextBoxText.DrawText += nodeTextBoxText_DrawText;
            nodeTextBoxACValue.DrawText += nodeTextBoxText_DrawText;
            nodeTextBoxDCValue.DrawText += nodeTextBoxText_DrawText;
            nodeTextBoxUnit.DrawText += nodeTextBoxText_DrawText;
            nodeTextBoxText.EditorShowing += nodeTextBoxText_EditorShowing;

            foreach (TreeColumn column in treeView.Columns)
            {
                column.Width = Math.Max(20, Math.Min(400,
                    settings.GetValue("treeView.Columns." + column.Header + ".Width",
                    column.Width)));
            }

            treeView.RowHeight = Math.Max(treeView.Font.Height + 1, 18); 

            treeModel = new TreeModel();

            showHiddenSettings = new UserOption("hiddenMenuItem", false,
                hiddenMenuItem, settings);
            showHiddenSettings.Changed += delegate(object sender, EventArgs e) 
            {
                treeModel.ForceVisible = showHiddenSettings.Value;
            };

            applyChanges = new UserOption("applyMenuItem", false,
                applyMenuItem, settings);

            forcePowerManager = new UserOption("forceMenuItem", false,
                forceMenuItem, settings);


            root = new ComputerNode(System.Environment.MachineName, settings);
            treeModel.Root = root;
            treeView.Model = treeModel;

            nodeTextBoxACValue.IsVisibleValueNeeded += nodeTextBoxAC_IsVisibleValueNeeded;
            nodeComboBoxACValue.IsVisibleValueNeeded += nodeComboBoxAC_IsVisibleValueNeeded;

            nodeComboBoxACValue.DrawText += nodeTextBoxText_DrawText;
            nodeTextBoxACValue.DataPropertyName = "AcValue";
            nodeComboBoxACValue.DataPropertyName = "AcValue";

            nodeTextBoxDCValue.IsVisibleValueNeeded += nodeTextBoxAC_IsVisibleValueNeeded;
            nodeComboBoxDCValue.IsVisibleValueNeeded += nodeComboBoxAC_IsVisibleValueNeeded;

            nodeComboBoxDCValue.DrawText += nodeTextBoxText_DrawText;
            nodeTextBoxDCValue.DataPropertyName = "DcValue";
            nodeComboBoxDCValue.DataPropertyName = "DcValue";
                
            Show();

            // Create a handle, otherwise calling Close() does not fire FormClosed         
            IntPtr handle = Handle;

            // Make sure the settings are saved when the user logs off
            Microsoft.Win32.SystemEvents.SessionEnded += delegate 
            {
                SaveConfiguration();
            };
        }

        private void nodeTextBoxText_DrawText(object sender, DrawEventArgs e) 
        {             
            Node node = e.Node.Tag as Node;
            if (node != null) 
            {
                if (node.IsVisible) 
                {
                    e.TextColor = Color.Black;
                } 
                else 
                {
                    e.TextColor = Color.DarkGray;
                }
            }
        }

        private void nodeTextBoxText_EditorShowing(object sender,
            CancelEventArgs e) 
        {
            e.Cancel = !(treeView.CurrentNode != null &&
                        (treeView.CurrentNode.Tag is SchemeNode) ||
                        (treeView.CurrentNode.Tag is SubGroupNode));
        }


        private void nodeTextBoxAC_IsVisibleValueNeeded(object sender,
            NodeControlValueEventArgs e)
        {
                e.Value = ( null != e.Node.Tag as SettingNode ) && ((e.Node.Tag as SettingNode ).isRangeSetting) ;
        }

        private void nodeComboBoxAC_IsVisibleValueNeeded(object sender,
                NodeControlValueEventArgs e)
        {
                e.Value = (null != e.Node.Tag as SettingNode) && (!(e.Node.Tag as SettingNode).isRangeSetting);
        }

        private void exitClick(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveConfiguration() 
        {
            foreach (TreeColumn column in treeView.Columns)
                settings.SetValue("treeView.Columns." + column.Header + ".Width",
                    column.Width);

            string fileName = Path.ChangeExtension(
                    System.Windows.Forms.Application.ExecutablePath, ".config");
            try 
            {
                settings.Save(fileName);
            } 
            catch (UnauthorizedAccessException) 
            {
                MessageBox.Show("Access to the path '" + fileName + "' is denied. " +
                    "The current settings could not be saved.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
            catch (IOException) 
            {
                MessageBox.Show("The path '" + fileName + "' is not writeable. " +
                    "The current settings could not be saved.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_Load(object sender, EventArgs e) 
        {
            Rectangle newBounds = new Rectangle {
                X = settings.GetValue("mainForm.Location.X", Location.X),
                Y = settings.GetValue("mainForm.Location.Y", Location.Y),
                Width = settings.GetValue("mainForm.Width", 470),
                Height = settings.GetValue("mainForm.Height", 640)
                };

            Rectangle fullWorkingArea = new Rectangle(int.MaxValue, int.MaxValue,
                int.MinValue, int.MinValue);

            foreach (Screen screen in Screen.AllScreens)
                fullWorkingArea = Rectangle.Union(fullWorkingArea, screen.Bounds);

            Rectangle intersection = Rectangle.Intersect(fullWorkingArea, newBounds);
            if (intersection.Width < 20 || intersection.Height < 20 ||
                !settings.Contains("mainForm.Location.X")) 
            {
                newBounds.X = (Screen.PrimaryScreen.WorkingArea.Width / 2) -
                                            (newBounds.Width/2);

                newBounds.Y = (Screen.PrimaryScreen.WorkingArea.Height / 2) -
                                            (newBounds.Height / 2);
            }

            this.Bounds = newBounds;

            treeView.FindNodeByTag(root).Expand();
        }
        
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) 
        {
            Visible = false;            

            SaveConfiguration();
        }

        
        private void aboutMenuItem_Click(object sender, EventArgs e) 
        {
            new AboutBox().ShowDialog();
        }
        

        private void treeView_Click(object sender, EventArgs e) 
        {
            // TODO: Context menu
            // Display 'Clone' for SchemeNode
        }

        private void MainForm_MoveOrResize(object sender, EventArgs e) 
        {
            if (WindowState != FormWindowState.Minimized) 
            {
                settings.SetValue("mainForm.Location.X", Bounds.X);
                settings.SetValue("mainForm.Location.Y", Bounds.Y);
                settings.SetValue("mainForm.Width", Bounds.Width);
                settings.SetValue("mainForm.Height", Bounds.Height);
            }
        }

        private void treeView_SelectionChanged(object sender, EventArgs e)
        {
            if (null != treeView.SelectedNode)
            {
                Node node = treeView.SelectedNode.Tag as Node;

                if (node != null)
                {
                    txtSettingDesc.Text = node.Description;
                }
                
                SettingNode settingNode = node as SettingNode;

                if ( settingNode != null )
                {
                    if (!settingNode.isRangeSetting)
                    {
                        nodeComboBoxACValue.DropDownItems.Clear();
                        nodeComboBoxDCValue.DropDownItems.Clear();
                        foreach (SettingNode.IndexedSetting s in settingNode.indexedSettings)
                        {
                            nodeComboBoxACValue.DropDownItems.Add(s.name);
                            nodeComboBoxDCValue.DropDownItems.Add(s.name);
                        }
                    }
                }
            }
            else
            {
                txtSettingDesc.Text = "";
            }
        }

        private void shutdownMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Process return value and handle exceptions
            PowerManagement.PowerManager.ShutdownComputer(forcePowerManager.Value);
        }

        private void poweroffMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Process return value and handle exceptions
            PowerManagement.PowerManager.PowerOffComputer(forcePowerManager.Value);
        }


        private void rebootMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Process return value and handle exceptions
            PowerManagement.PowerManager.RebootComputer(forcePowerManager.Value);
        }

        private void logoffMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Process return value and handle exceptions
            PowerManagement.PowerManager.LogOffCurrentUser(forcePowerManager.Value);
        }

        private void lockMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Process return value and handle exceptions
            PowerManagement.PowerManager.LockWorkStation();
        }

        private void hibernateMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Process return value and handle exceptions
            PowerManagement.PowerManager.HibernateComputer(forcePowerManager.Value);
        }

        private void suspendWithEventsMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Process return value and handle exceptions
            PowerManagement.PowerManager.StandbyComputer(forcePowerManager.Value, false);
        }

        private void suspendWithoutEventsMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Process return value and handle exceptions
            PowerManagement.PowerManager.StandbyComputer(forcePowerManager.Value, true);
        }        
    }
}
