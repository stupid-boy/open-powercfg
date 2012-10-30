/*
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
  
    Copyright (C) 2012 Andrey Mushatov ( openPowerCfg@gmail.com )
*/

namespace OpenPowerCfg.GUI {
  partial class MainForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        this.components = new System.ComponentModel.Container();
        System.Windows.Forms.MenuItem menuItem2;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
        this.fileMenuItem = new System.Windows.Forms.MenuItem();
        this.exitMenuItem = new System.Windows.Forms.MenuItem();
        this.optionsMenuItem = new System.Windows.Forms.MenuItem();
        this.hiddenMenuItem = new System.Windows.Forms.MenuItem();
        this.applyMenuItem = new System.Windows.Forms.MenuItem();
        this.powerMenuItem = new System.Windows.Forms.MenuItem();
        this.shutdownMenuItem = new System.Windows.Forms.MenuItem();
        this.poweroffMenuItem = new System.Windows.Forms.MenuItem();
        this.rebootMenuItem = new System.Windows.Forms.MenuItem();
        this.logoffMenuItem = new System.Windows.Forms.MenuItem();
        this.lockMenuItem = new System.Windows.Forms.MenuItem();
        this.suspendMenuItem = new System.Windows.Forms.MenuItem();
        this.suspendWithEventsMenuItem = new System.Windows.Forms.MenuItem();
        this.suspendWithoutEventsMenuItem = new System.Windows.Forms.MenuItem();
        this.hibernateMenuItem = new System.Windows.Forms.MenuItem();
        this.forceMenuItem = new System.Windows.Forms.MenuItem();
        this.helpMenuItem = new System.Windows.Forms.MenuItem();
        this.menuItem1 = new System.Windows.Forms.MenuItem();
        this.celsiusMenuItem = new System.Windows.Forms.MenuItem();
        this.fahrenheitMenuItem = new System.Windows.Forms.MenuItem();
        this.runWebServerMenuItem = new System.Windows.Forms.MenuItem();
        this.serverPortMenuItem = new System.Windows.Forms.MenuItem();
        this.aboutMenuItem = new System.Windows.Forms.MenuItem();
        this.treeContextMenu = new System.Windows.Forms.ContextMenu();
        this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
        this.plotWindowMenuItem = new System.Windows.Forms.MenuItem();
        this.plotBottomMenuItem = new System.Windows.Forms.MenuItem();
        this.plotRightMenuItem = new System.Windows.Forms.MenuItem();
        this.splitContainer = new OpenPowerCfg.GUI.SplitContainerAdv();
        this.treeView = new Aga.Controls.Tree.TreeViewAdv();
        this.colSetting = new Aga.Controls.Tree.TreeColumn();
        this.colAC = new Aga.Controls.Tree.TreeColumn();
        this.colDC = new Aga.Controls.Tree.TreeColumn();
        this.colUint = new Aga.Controls.Tree.TreeColumn();
        this.nodeImage = new Aga.Controls.Tree.NodeControls.NodeIcon();
        this.nodeTextBoxText = new Aga.Controls.Tree.NodeControls.NodeTextBox();
        this.nodeTextBoxACValue = new Aga.Controls.Tree.NodeControls.NodeTextBox();
        this.nodeTextBoxDCValue = new Aga.Controls.Tree.NodeControls.NodeTextBox();
        this.nodeTextBoxUnit = new Aga.Controls.Tree.NodeControls.NodeTextBox();
        this.nodeComboBoxACValue = new Aga.Controls.Tree.NodeControls.NodeComboBox();
        this.nodeComboBoxDCValue = new Aga.Controls.Tree.NodeControls.NodeComboBox();
        this.txtSettingDesc = new System.Windows.Forms.TextBox();
        menuItem2 = new System.Windows.Forms.MenuItem();
        this.splitContainer.Panel1.SuspendLayout();
        this.splitContainer.Panel2.SuspendLayout();
        this.splitContainer.SuspendLayout();
        this.SuspendLayout();
        // 
        // menuItem2
        // 
        menuItem2.Index = 7;
        menuItem2.Text = "-";
        // 
        // mainMenu
        // 
        this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileMenuItem,
            this.optionsMenuItem,
            this.powerMenuItem,
            this.helpMenuItem});
        // 
        // fileMenuItem
        // 
        this.fileMenuItem.Index = 0;
        this.fileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.exitMenuItem});
        this.fileMenuItem.Text = "File";
        // 
        // exitMenuItem
        // 
        this.exitMenuItem.Index = 0;
        this.exitMenuItem.Text = "Exit";
        this.exitMenuItem.Click += new System.EventHandler(this.exitClick);
        // 
        // optionsMenuItem
        // 
        this.optionsMenuItem.Index = 1;
        this.optionsMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.hiddenMenuItem,
            this.applyMenuItem});
        this.optionsMenuItem.Text = "Options";
        // 
        // hiddenMenuItem
        // 
        this.hiddenMenuItem.Index = 0;
        this.hiddenMenuItem.Text = "Show Hidden Settings";
        // 
        // applyMenuItem
        // 
        this.applyMenuItem.Index = 1;
        this.applyMenuItem.Text = "Apply Changes";
        // 
        // powerMenuItem
        // 
        this.powerMenuItem.Index = 2;
        this.powerMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.shutdownMenuItem,
            this.poweroffMenuItem,
            this.rebootMenuItem,
            this.logoffMenuItem,
            this.lockMenuItem,
            this.suspendMenuItem,
            this.hibernateMenuItem,
            menuItem2,
            this.forceMenuItem});
        this.powerMenuItem.Text = "Power";
        // 
        // shutdownMenuItem
        // 
        this.shutdownMenuItem.Index = 0;
        this.shutdownMenuItem.Text = "Shutdown";
        this.shutdownMenuItem.Click += new System.EventHandler(this.shutdownMenuItem_Click);
        // 
        // poweroffMenuItem
        // 
        this.poweroffMenuItem.Index = 1;
        this.poweroffMenuItem.Text = "PowerOff";
        this.poweroffMenuItem.Click += new System.EventHandler(this.poweroffMenuItem_Click);
        // 
        // rebootMenuItem
        // 
        this.rebootMenuItem.Index = 2;
        this.rebootMenuItem.Text = "Reboot";
        this.rebootMenuItem.Click += new System.EventHandler(this.rebootMenuItem_Click);
        // 
        // logoffMenuItem
        // 
        this.logoffMenuItem.Index = 3;
        this.logoffMenuItem.Text = "Logoff";
        this.logoffMenuItem.Click += new System.EventHandler(this.logoffMenuItem_Click);
        // 
        // lockMenuItem
        // 
        this.lockMenuItem.Index = 4;
        this.lockMenuItem.Text = "Lock Display";
        this.lockMenuItem.Click += new System.EventHandler(this.lockMenuItem_Click);
        // 
        // suspendMenuItem
        // 
        this.suspendMenuItem.Index = 5;
        this.suspendMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.suspendWithEventsMenuItem,
            this.suspendWithoutEventsMenuItem});
        this.suspendMenuItem.Text = "Suspend (sleep) with ...";
        // 
        // suspendWithEventsMenuItem
        // 
        this.suspendWithEventsMenuItem.Index = 0;
        this.suspendWithEventsMenuItem.Text = " Wake Events enabled";
        this.suspendWithEventsMenuItem.Click += new System.EventHandler(this.suspendWithEventsMenuItem_Click);
        // 
        // suspendWithoutEventsMenuItem
        // 
        this.suspendWithoutEventsMenuItem.Index = 1;
        this.suspendWithoutEventsMenuItem.Text = " Wake Events disabled";
        this.suspendWithoutEventsMenuItem.Click += new System.EventHandler(this.suspendWithoutEventsMenuItem_Click);
        // 
        // hibernateMenuItem
        // 
        this.hibernateMenuItem.Index = 6;
        this.hibernateMenuItem.Text = "Hibernate";
        this.hibernateMenuItem.Click += new System.EventHandler(this.hibernateMenuItem_Click);
        // 
        // forceMenuItem
        // 
        this.forceMenuItem.Index = 8;
        this.forceMenuItem.Text = "Force";
        // 
        // helpMenuItem
        // 
        this.helpMenuItem.Index = 3;
        this.helpMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
        this.helpMenuItem.Text = "Help";
        // 
        // menuItem1
        // 
        this.menuItem1.Index = 0;
        this.menuItem1.Text = "About";
        this.menuItem1.Click += new System.EventHandler(this.aboutMenuItem_Click);
        // 
        // celsiusMenuItem
        // 
        this.celsiusMenuItem.Index = -1;
        this.celsiusMenuItem.Text = "";
        // 
        // fahrenheitMenuItem
        // 
        this.fahrenheitMenuItem.Index = -1;
        this.fahrenheitMenuItem.Text = "";
        // 
        // runWebServerMenuItem
        // 
        this.runWebServerMenuItem.Index = -1;
        this.runWebServerMenuItem.Text = "";
        // 
        // serverPortMenuItem
        // 
        this.serverPortMenuItem.Index = -1;
        this.serverPortMenuItem.Text = "";
        // 
        // aboutMenuItem
        // 
        this.aboutMenuItem.Index = -1;
        this.aboutMenuItem.Text = "About";
        // 
        // saveFileDialog
        // 
        this.saveFileDialog.DefaultExt = "txt";
        this.saveFileDialog.FileName = "OpenPowerCfg.Report.txt";
        this.saveFileDialog.Filter = "Text Documents|*.txt|All Files|*.*";
        this.saveFileDialog.RestoreDirectory = true;
        this.saveFileDialog.Title = "Save Report As";
        // 
        // plotWindowMenuItem
        // 
        this.plotWindowMenuItem.Index = -1;
        this.plotWindowMenuItem.Text = "";
        // 
        // plotBottomMenuItem
        // 
        this.plotBottomMenuItem.Index = -1;
        this.plotBottomMenuItem.Text = "";
        // 
        // plotRightMenuItem
        // 
        this.plotRightMenuItem.Index = -1;
        this.plotRightMenuItem.Text = "";
        // 
        // splitContainer
        // 
        this.splitContainer.Border3DStyle = System.Windows.Forms.Border3DStyle.Raised;
        this.splitContainer.Color = System.Drawing.SystemColors.Control;
        this.splitContainer.Cursor = System.Windows.Forms.Cursors.Default;
        this.splitContainer.Location = new System.Drawing.Point(12, 12);
        this.splitContainer.Name = "splitContainer";
        this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
        // 
        // splitContainer.Panel1
        // 
        this.splitContainer.Panel1.Controls.Add(this.treeView);
        // 
        // splitContainer.Panel2
        // 
        this.splitContainer.Panel2.Controls.Add(this.txtSettingDesc);
        this.splitContainer.Panel2.Cursor = System.Windows.Forms.Cursors.Default;
        this.splitContainer.Size = new System.Drawing.Size(386, 483);
        this.splitContainer.SplitterDistance = 354;
        this.splitContainer.SplitterWidth = 5;
        this.splitContainer.TabIndex = 3;
        // 
        // treeView
        // 
        this.treeView.AsyncExpanding = true;
        this.treeView.BackColor = System.Drawing.SystemColors.Window;
        this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.treeView.Columns.Add(this.colSetting);
        this.treeView.Columns.Add(this.colAC);
        this.treeView.Columns.Add(this.colDC);
        this.treeView.Columns.Add(this.colUint);
        this.treeView.DefaultToolTipProvider = null;
        this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
        this.treeView.DragDropMarkColor = System.Drawing.Color.Black;
        this.treeView.FullRowSelect = true;
        this.treeView.GridLineStyle = Aga.Controls.Tree.GridLineStyle.Horizontal;
        this.treeView.LineColor = System.Drawing.SystemColors.ControlDark;
        this.treeView.LoadOnDemand = true;
        this.treeView.Location = new System.Drawing.Point(0, 0);
        this.treeView.Model = null;
        this.treeView.Name = "treeView";
        this.treeView.NodeControls.Add(this.nodeImage);
        this.treeView.NodeControls.Add(this.nodeTextBoxText);
        this.treeView.NodeControls.Add(this.nodeTextBoxACValue);
        this.treeView.NodeControls.Add(this.nodeTextBoxDCValue);
        this.treeView.NodeControls.Add(this.nodeTextBoxUnit);
        this.treeView.NodeControls.Add(this.nodeComboBoxACValue);
        this.treeView.NodeControls.Add(this.nodeComboBoxDCValue);
        this.treeView.SelectedNode = null;
        this.treeView.Size = new System.Drawing.Size(386, 354);
        this.treeView.TabIndex = 0;
        this.treeView.Text = "treeView";
        this.treeView.UseColumns = true;
        this.treeView.SelectionChanged += new System.EventHandler(this.treeView_SelectionChanged);
        this.treeView.Click += new System.EventHandler(this.treeView_Click);
        // 
        // colSetting
        // 
        this.colSetting.Header = "Setting";
        this.colSetting.SortOrder = System.Windows.Forms.SortOrder.Ascending;
        this.colSetting.TooltipText = null;
        this.colSetting.Width = 200;
        // 
        // colAC
        // 
        this.colAC.Header = "AC";
        this.colAC.SortOrder = System.Windows.Forms.SortOrder.None;
        this.colAC.TooltipText = null;
        this.colAC.Width = 60;
        // 
        // colDC
        // 
        this.colDC.Header = "DC";
        this.colDC.SortOrder = System.Windows.Forms.SortOrder.None;
        this.colDC.TooltipText = null;
        this.colDC.Width = 60;
        // 
        // colUint
        // 
        this.colUint.Header = "Unit";
        this.colUint.SortOrder = System.Windows.Forms.SortOrder.None;
        this.colUint.TooltipText = null;
        // 
        // nodeImage
        // 
        this.nodeImage.DataPropertyName = "Image";
        this.nodeImage.LeftMargin = 1;
        this.nodeImage.ParentColumn = this.colSetting;
        this.nodeImage.ScaleMode = Aga.Controls.Tree.ImageScaleMode.Fit;
        // 
        // nodeTextBoxText
        // 
        this.nodeTextBoxText.DataPropertyName = "Text";
        this.nodeTextBoxText.EditEnabled = true;
        this.nodeTextBoxText.IncrementalSearchEnabled = true;
        this.nodeTextBoxText.LeftMargin = 3;
        this.nodeTextBoxText.ParentColumn = this.colSetting;
        this.nodeTextBoxText.Trimming = System.Drawing.StringTrimming.EllipsisCharacter;
        this.nodeTextBoxText.UseCompatibleTextRendering = true;
        // 
        // nodeTextBoxACValue
        // 
        this.nodeTextBoxACValue.DataPropertyName = "AcValue";
        this.nodeTextBoxACValue.EditEnabled = true;
        this.nodeTextBoxACValue.EditOnClick = true;
        this.nodeTextBoxACValue.IncrementalSearchEnabled = true;
        this.nodeTextBoxACValue.LeftMargin = 3;
        this.nodeTextBoxACValue.ParentColumn = this.colAC;
        this.nodeTextBoxACValue.Trimming = System.Drawing.StringTrimming.EllipsisCharacter;
        this.nodeTextBoxACValue.UseCompatibleTextRendering = true;
        // 
        // nodeTextBoxDCValue
        // 
        this.nodeTextBoxDCValue.DataPropertyName = "DcValue";
        this.nodeTextBoxDCValue.EditEnabled = true;
        this.nodeTextBoxDCValue.EditOnClick = true;
        this.nodeTextBoxDCValue.IncrementalSearchEnabled = true;
        this.nodeTextBoxDCValue.LeftMargin = 3;
        this.nodeTextBoxDCValue.ParentColumn = this.colDC;
        this.nodeTextBoxDCValue.Trimming = System.Drawing.StringTrimming.EllipsisCharacter;
        this.nodeTextBoxDCValue.UseCompatibleTextRendering = true;
        // 
        // nodeTextBoxUnit
        // 
        this.nodeTextBoxUnit.DataPropertyName = "Unit";
        this.nodeTextBoxUnit.IncrementalSearchEnabled = true;
        this.nodeTextBoxUnit.LeftMargin = 3;
        this.nodeTextBoxUnit.ParentColumn = this.colUint;
        this.nodeTextBoxUnit.Trimming = System.Drawing.StringTrimming.EllipsisCharacter;
        this.nodeTextBoxUnit.UseCompatibleTextRendering = true;
        // 
        // nodeComboBoxACValue
        // 
        this.nodeComboBoxACValue.EditEnabled = true;
        this.nodeComboBoxACValue.EditOnClick = true;
        this.nodeComboBoxACValue.IncrementalSearchEnabled = true;
        this.nodeComboBoxACValue.LeftMargin = 3;
        this.nodeComboBoxACValue.ParentColumn = this.colAC;
        this.nodeComboBoxACValue.Trimming = System.Drawing.StringTrimming.Character;
        this.nodeComboBoxACValue.UseCompatibleTextRendering = true;
        // 
        // nodeComboBoxDCValue
        // 
        this.nodeComboBoxDCValue.EditEnabled = true;
        this.nodeComboBoxDCValue.EditOnClick = true;
        this.nodeComboBoxDCValue.IncrementalSearchEnabled = true;
        this.nodeComboBoxDCValue.LeftMargin = 3;
        this.nodeComboBoxDCValue.ParentColumn = this.colDC;
        this.nodeComboBoxDCValue.Trimming = System.Drawing.StringTrimming.Character;
        this.nodeComboBoxDCValue.UseCompatibleTextRendering = true;
        // 
        // txtSettingDesc
        // 
        this.txtSettingDesc.Dock = System.Windows.Forms.DockStyle.Fill;
        this.txtSettingDesc.Location = new System.Drawing.Point(0, 0);
        this.txtSettingDesc.Multiline = true;
        this.txtSettingDesc.Name = "txtSettingDesc";
        this.txtSettingDesc.ReadOnly = true;
        this.txtSettingDesc.Size = new System.Drawing.Size(386, 124);
        this.txtSettingDesc.TabIndex = 25;
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(410, 505);
        this.Controls.Add(this.splitContainer);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Menu = this.mainMenu;
        this.Name = "MainForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        this.Text = "Open PowerCfg";
        this.Load += new System.EventHandler(this.MainForm_Load);
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
        this.splitContainer.Panel1.ResumeLayout(false);
        this.splitContainer.Panel2.ResumeLayout(false);
        this.splitContainer.Panel2.PerformLayout();
        this.splitContainer.ResumeLayout(false);
        this.ResumeLayout(false);

    }

    #endregion

    private Aga.Controls.Tree.TreeViewAdv treeView;
    private System.Windows.Forms.MainMenu mainMenu;
    private System.Windows.Forms.MenuItem fileMenuItem;
    private System.Windows.Forms.MenuItem exitMenuItem;
    private Aga.Controls.Tree.TreeColumn colSetting;
    private Aga.Controls.Tree.TreeColumn colAC;
    private Aga.Controls.Tree.TreeColumn colDC;
    private Aga.Controls.Tree.NodeControls.NodeIcon nodeImage;
    private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxText;
    private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxACValue;
    private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxDCValue;
    private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBoxUnit;
    private SplitContainerAdv splitContainer;
    private System.Windows.Forms.MenuItem optionsMenuItem;
    private System.Windows.Forms.MenuItem helpMenuItem;
    private System.Windows.Forms.MenuItem aboutMenuItem;
    private System.Windows.Forms.MenuItem powerMenuItem;
    private System.Windows.Forms.ContextMenu treeContextMenu;
    private System.Windows.Forms.SaveFileDialog saveFileDialog;
    private System.Windows.Forms.MenuItem hiddenMenuItem;
    private System.Windows.Forms.MenuItem celsiusMenuItem;
    private System.Windows.Forms.MenuItem fahrenheitMenuItem;
    private System.Windows.Forms.MenuItem plotWindowMenuItem;
    private System.Windows.Forms.MenuItem plotBottomMenuItem;
    private System.Windows.Forms.MenuItem plotRightMenuItem;
    private System.Windows.Forms.MenuItem runWebServerMenuItem;
    private System.Windows.Forms.MenuItem serverPortMenuItem;
    private System.Windows.Forms.TextBox txtSettingDesc;
    private Aga.Controls.Tree.TreeColumn colUint;
    private Aga.Controls.Tree.NodeControls.NodeComboBox nodeComboBoxACValue;
    private Aga.Controls.Tree.NodeControls.NodeComboBox nodeComboBoxDCValue;
    private System.Windows.Forms.MenuItem menuItem1;
    private System.Windows.Forms.MenuItem shutdownMenuItem;
    private System.Windows.Forms.MenuItem rebootMenuItem;
    private System.Windows.Forms.MenuItem logoffMenuItem;
    private System.Windows.Forms.MenuItem poweroffMenuItem;
    private System.Windows.Forms.MenuItem lockMenuItem;
    private System.Windows.Forms.MenuItem hibernateMenuItem;
    private System.Windows.Forms.MenuItem forceMenuItem;
    private System.Windows.Forms.MenuItem suspendMenuItem;
    private System.Windows.Forms.MenuItem suspendWithEventsMenuItem;
    private System.Windows.Forms.MenuItem suspendWithoutEventsMenuItem;
    private System.Windows.Forms.MenuItem applyMenuItem;
  }
}

