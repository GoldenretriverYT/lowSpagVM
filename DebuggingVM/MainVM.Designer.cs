namespace DebuggingVM {
    partial class MainVM {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainVM));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.resetCPUOnErrorCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.TitleDisassemblerLabel = new System.Windows.Forms.Label();
            this.disassembledInstructionsListBox = new System.Windows.Forms.ListView();
            this.Type = new System.Windows.Forms.ColumnHeader();
            this.Addr = new System.Windows.Forms.ColumnHeader();
            this.IT = new System.Windows.Forms.ColumnHeader();
            this.Data = new System.Windows.Forms.ColumnHeader();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.outputTextBox = new System.Windows.Forms.RichTextBox();
            this.infoDumpBox = new System.Windows.Forms.RichTextBox();
            this.openSpagBinDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dbgStateLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.onlyUpdateInformationDumpOnSingleStepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(38, 22);
            this.toolStripDropDownButton1.Text = "File";
            this.toolStripDropDownButton1.Click += new System.EventHandler(this.toolStripDropDownButton1_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetCPUOnErrorCheck,
            this.onlyUpdateInformationDumpOnSingleStepToolStripMenuItem});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(62, 22);
            this.toolStripDropDownButton2.Text = "Options";
            // 
            // resetCPUOnErrorCheck
            // 
            this.resetCPUOnErrorCheck.Checked = true;
            this.resetCPUOnErrorCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.resetCPUOnErrorCheck.Name = "resetCPUOnErrorCheck";
            this.resetCPUOnErrorCheck.Size = new System.Drawing.Size(321, 22);
            this.resetCPUOnErrorCheck.Text = "Reset CPU on error?";
            this.resetCPUOnErrorCheck.Click += new System.EventHandler(this.resetCPUOnErrorCheck_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton3,
            this.toolStripButton2});
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(800, 31);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::DebuggingVM.Properties.Resources.Run;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(115, 28);
            this.toolStripButton1.Text = "Run until break";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = global::DebuggingVM.Properties.Resources.RunOne1;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(159, 28);
            this.toolStripButton3.Text = "Execute one instruction";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::DebuggingVM.Properties.Resources.Pause;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(121, 28);
            this.toolStripButton2.Text = "Pause execution";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 56);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(800, 394);
            this.splitContainer1.SplitterDistance = 270;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.TitleDisassemblerLabel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.disassembledInstructionsListBox);
            this.splitContainer2.Size = new System.Drawing.Size(270, 394);
            this.splitContainer2.SplitterDistance = 38;
            this.splitContainer2.SplitterWidth = 2;
            this.splitContainer2.TabIndex = 0;
            // 
            // TitleDisassemblerLabel
            // 
            this.TitleDisassemblerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TitleDisassemblerLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.TitleDisassemblerLabel.Location = new System.Drawing.Point(0, 0);
            this.TitleDisassemblerLabel.Name = "TitleDisassemblerLabel";
            this.TitleDisassemblerLabel.Size = new System.Drawing.Size(270, 38);
            this.TitleDisassemblerLabel.TabIndex = 0;
            this.TitleDisassemblerLabel.Text = "Disassembler";
            this.TitleDisassemblerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // disassembledInstructionsListBox
            // 
            this.disassembledInstructionsListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Type,
            this.Addr,
            this.IT,
            this.Data});
            this.disassembledInstructionsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.disassembledInstructionsListBox.FullRowSelect = true;
            this.disassembledInstructionsListBox.Location = new System.Drawing.Point(0, 0);
            this.disassembledInstructionsListBox.Name = "disassembledInstructionsListBox";
            this.disassembledInstructionsListBox.Size = new System.Drawing.Size(270, 354);
            this.disassembledInstructionsListBox.TabIndex = 0;
            this.disassembledInstructionsListBox.UseCompatibleStateImageBehavior = false;
            this.disassembledInstructionsListBox.View = System.Windows.Forms.View.Details;
            // 
            // Type
            // 
            this.Type.Text = "Type";
            this.Type.Width = 20;
            // 
            // Addr
            // 
            this.Addr.Text = "Addr";
            // 
            // IT
            // 
            this.IT.Text = "IT";
            this.IT.Width = 100;
            // 
            // Data
            // 
            this.Data.Text = "Data";
            this.Data.Width = 80;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.outputTextBox);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.infoDumpBox);
            this.splitContainer3.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer3_Panel2_Paint);
            this.splitContainer3.Size = new System.Drawing.Size(526, 394);
            this.splitContainer3.SplitterDistance = 230;
            this.splitContainer3.TabIndex = 0;
            // 
            // outputTextBox
            // 
            this.outputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputTextBox.Font = new System.Drawing.Font("Cascadia Code", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.outputTextBox.Location = new System.Drawing.Point(0, 0);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(526, 230);
            this.outputTextBox.TabIndex = 2;
            this.outputTextBox.Text = "Click File -> Load; then press \"run until break\" to start execution";
            // 
            // infoDumpBox
            // 
            this.infoDumpBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoDumpBox.Font = new System.Drawing.Font("Cascadia Code", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.infoDumpBox.Location = new System.Drawing.Point(0, 0);
            this.infoDumpBox.Name = "infoDumpBox";
            this.infoDumpBox.Size = new System.Drawing.Size(526, 160);
            this.infoDumpBox.TabIndex = 0;
            this.infoDumpBox.Text = "Dump:";
            // 
            // openSpagBinDialog
            // 
            this.openSpagBinDialog.Filter = "Spaghetto Binaries|*.bin|All Files|*.*";
            this.openSpagBinDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openSpagBinDialog_FileOk);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dbgStateLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // dbgStateLabel
            // 
            this.dbgStateLabel.Name = "dbgStateLabel";
            this.dbgStateLabel.Size = new System.Drawing.Size(110, 17);
            this.dbgStateLabel.Text = "Debugger State: OK";
            // 
            // onlyUpdateInformationDumpOnSingleStepToolStripMenuItem
            // 
            this.onlyUpdateInformationDumpOnSingleStepToolStripMenuItem.Checked = true;
            this.onlyUpdateInformationDumpOnSingleStepToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.onlyUpdateInformationDumpOnSingleStepToolStripMenuItem.Name = "onlyUpdateInformationDumpOnSingleStepToolStripMenuItem";
            this.onlyUpdateInformationDumpOnSingleStepToolStripMenuItem.Size = new System.Drawing.Size(321, 22);
            this.onlyUpdateInformationDumpOnSingleStepToolStripMenuItem.Text = "Only update information dump on single step?";
            this.onlyUpdateInformationDumpOnSingleStepToolStripMenuItem.Click += new System.EventHandler(this.onlyUpdateInformationDumpOnSingleStepToolStripMenuItem_Click);
            // 
            // MainVM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainVM";
            this.Text = "lowSpag DEBUGGER";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStrip toolStrip2;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private Label TitleDisassemblerLabel;
        private OpenFileDialog openSpagBinDialog;
        private ListView disassembledInstructionsListBox;
        private ColumnHeader Addr;
        private ColumnHeader IT;
        private ColumnHeader Data;
        private ColumnHeader Type;
        private ToolStripDropDownButton toolStripDropDownButton2;
        private ToolStripMenuItem resetCPUOnErrorCheck;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel dbgStateLabel;
        private SplitContainer splitContainer3;
        private RichTextBox outputTextBox;
        private RichTextBox infoDumpBox;
        private ToolStripMenuItem onlyUpdateInformationDumpOnSingleStepToolStripMenuItem;
    }
}