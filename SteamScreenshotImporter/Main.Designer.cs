namespace SteamScreenshotImporter
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.steamPathDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.userBox = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnScan = new System.Windows.Forms.LinkLabel();
            this.gameBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listBox = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnAddFolder = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAddImage = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.outputBox = new System.Windows.Forms.TextBox();
            this.dataSet = new System.Data.DataSet();
            this.games = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataTable2 = new System.Data.DataTable();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.games)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).BeginInit();
            this.SuspendLayout();
            // 
            // steamPathDialog
            // 
            this.steamPathDialog.Description = "选择Steam目录";
            this.steamPathDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.steamPathDialog.ShowNewFolderButton = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择用户";
            // 
            // userBox
            // 
            this.userBox.DataSource = this.dataSet;
            this.userBox.DisplayMember = "Users.name";
            this.userBox.FormattingEnabled = true;
            this.userBox.Location = new System.Drawing.Point(91, 21);
            this.userBox.Name = "userBox";
            this.userBox.Size = new System.Drawing.Size(187, 23);
            this.userBox.TabIndex = 1;
            this.userBox.ValueMember = "Users.id";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnScan);
            this.panel1.Controls.Add(this.gameBox);
            this.panel1.Controls.Add(this.userBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(483, 93);
            this.panel1.TabIndex = 2;
            // 
            // btnScan
            // 
            this.btnScan.AutoSize = true;
            this.btnScan.Location = new System.Drawing.Point(318, 24);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(107, 15);
            this.btnScan.TabIndex = 2;
            this.btnScan.TabStop = true;
            this.btnScan.Text = "重新扫描Steam";
            this.btnScan.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnScan_LinkClicked);
            // 
            // gameBox
            // 
            this.gameBox.DisplayMember = "name";
            this.gameBox.FormattingEnabled = true;
            this.gameBox.Location = new System.Drawing.Point(91, 58);
            this.gameBox.Name = "gameBox";
            this.gameBox.Size = new System.Drawing.Size(375, 23);
            this.gameBox.TabIndex = 1;
            this.gameBox.ValueMember = "appid";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "选择游戏";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listBox);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 93);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10);
            this.panel2.Size = new System.Drawing.Size(483, 174);
            this.panel2.TabIndex = 3;
            // 
            // listBox
            // 
            this.listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 15;
            this.listBox.Location = new System.Drawing.Point(10, 29);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(463, 135);
            this.listBox.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnAddFolder);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.btnAddImage);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 10);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(2);
            this.panel3.Size = new System.Drawing.Size(463, 19);
            this.panel3.TabIndex = 0;
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.AutoSize = true;
            this.btnAddFolder.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAddFolder.Location = new System.Drawing.Point(235, 2);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Size = new System.Drawing.Size(91, 15);
            this.btnAddFolder.TabIndex = 3;
            this.btnAddFolder.TabStop = true;
            this.btnAddFolder.Text = "添加目录...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(220, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "/";
            // 
            // btnAddImage
            // 
            this.btnAddImage.AutoSize = true;
            this.btnAddImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAddImage.Location = new System.Drawing.Point(129, 2);
            this.btnAddImage.Name = "btnAddImage";
            this.btnAddImage.Size = new System.Drawing.Size(91, 15);
            this.btnAddImage.TabIndex = 1;
            this.btnAddImage.TabStop = true;
            this.btnAddImage.Text = "添加图像...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(2, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "拖动图像至此处或";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(367, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 44);
            this.button1.TabIndex = 4;
            this.button1.Text = "导入";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.outputBox);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 267);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.panel4.Size = new System.Drawing.Size(483, 66);
            this.panel4.TabIndex = 4;
            // 
            // outputBox
            // 
            this.outputBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.outputBox.Location = new System.Drawing.Point(10, 0);
            this.outputBox.Multiline = true;
            this.outputBox.Name = "outputBox";
            this.outputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputBox.Size = new System.Drawing.Size(341, 56);
            this.outputBox.TabIndex = 5;
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "NewDataSet";
            this.dataSet.Relations.AddRange(new System.Data.DataRelation[] {
            new System.Data.DataRelation("id", "Users", "UserGame", new string[] {
                        "id"}, new string[] {
                        "id"}, false),
            new System.Data.DataRelation("appid", "Games", "UserGame", new string[] {
                        "appid"}, new string[] {
                        "appid"}, false)});
            this.dataSet.Tables.AddRange(new System.Data.DataTable[] {
            this.games,
            this.dataTable1,
            this.dataTable2});
            // 
            // games
            // 
            this.games.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2});
            this.games.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.UniqueConstraint("Constraint1", new string[] {
                        "appid"}, false)});
            this.games.TableName = "Games";
            // 
            // dataColumn1
            // 
            this.dataColumn1.AllowDBNull = false;
            this.dataColumn1.ColumnName = "appid";
            this.dataColumn1.DataType = typeof(int);
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "name";
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn3,
            this.dataColumn4});
            this.dataTable1.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.UniqueConstraint("Constraint1", new string[] {
                        "id"}, true)});
            this.dataTable1.PrimaryKey = new System.Data.DataColumn[] {
        this.dataColumn3};
            this.dataTable1.TableName = "Users";
            // 
            // dataColumn3
            // 
            this.dataColumn3.AllowDBNull = false;
            this.dataColumn3.ColumnName = "id";
            this.dataColumn3.DataType = typeof(int);
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "name";
            // 
            // dataTable2
            // 
            this.dataTable2.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7});
            this.dataTable2.Constraints.AddRange(new System.Data.Constraint[] {
            new System.Data.ForeignKeyConstraint("appid", "Games", new string[] {
                        "appid"}, new string[] {
                        "appid"}, System.Data.AcceptRejectRule.None, System.Data.Rule.Cascade, System.Data.Rule.Cascade),
            new System.Data.ForeignKeyConstraint("id", "Users", new string[] {
                        "id"}, new string[] {
                        "id"}, System.Data.AcceptRejectRule.None, System.Data.Rule.Cascade, System.Data.Rule.Cascade),
            new System.Data.UniqueConstraint("pkey", new string[] {
                        "appid",
                        "id"}, true)});
            this.dataTable2.PrimaryKey = new System.Data.DataColumn[] {
        this.dataColumn6,
        this.dataColumn5};
            this.dataTable2.TableName = "UserGame";
            // 
            // dataColumn5
            // 
            this.dataColumn5.AllowDBNull = false;
            this.dataColumn5.ColumnName = "id";
            this.dataColumn5.DataType = typeof(int);
            // 
            // dataColumn6
            // 
            this.dataColumn6.AllowDBNull = false;
            this.dataColumn6.ColumnName = "appid";
            this.dataColumn6.DataType = typeof(int);
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "name";
            this.dataColumn7.ReadOnly = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(483, 333);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Main";
            this.Text = "Steam Screenshot Importer";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.games)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog steamPathDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox userBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox gameBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel btnScan;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel btnAddFolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel btnAddImage;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox outputBox;
        private System.Data.DataSet dataSet;
        private System.Data.DataTable games;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataTable dataTable2;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn6;
        private System.Data.DataColumn dataColumn7;
    }
}

