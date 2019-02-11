namespace Test1
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label2 = new System.Windows.Forms.Label();
            this.blankRadius = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.massDensity = new System.Windows.Forms.TextBox();
            this.YoungsModulus = new System.Windows.Forms.TextBox();
            this.PoissonsRatio = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.PlasticData = new System.Windows.Forms.DataGridView();
            this.Log = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lastAngle = new System.Windows.Forms.TextBox();
            this.strtAngle = new System.Windows.Forms.TextBox();
            this.angleStep = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.Mesh = new System.Windows.Forms.TextBox();
            this.Cores = new System.Windows.Forms.TextBox();
            this.JobName = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.MainPage = new System.Windows.Forms.TabPage();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.endFriction = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.startFriction = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.MaterialsList = new System.Windows.Forms.ComboBox();
            this.LogPage = new System.Windows.Forms.TabPage();
            this.ExtraPage = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.ExtraDataGridView = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.ExtraTextBox = new System.Windows.Forms.TextBox();
            this.ExtraMaterialsList = new System.Windows.Forms.ComboBox();
            this.ChartPage = new System.Windows.Forms.TabPage();
            this.lineChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.PlasticData)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.MainPage.SuspendLayout();
            this.LogPage.SuspendLayout();
            this.ExtraPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExtraDataGridView)).BeginInit();
            this.ChartPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lineChart)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(338, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Radius of Blank, mm";
            // 
            // blankRadius
            // 
            this.blankRadius.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.blankRadius.Location = new System.Drawing.Point(512, 29);
            this.blankRadius.Name = "blankRadius";
            this.blankRadius.Size = new System.Drawing.Size(100, 29);
            this.blankRadius.TabIndex = 4;
            this.blankRadius.Text = "11";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(298, 475);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 50);
            this.button2.TabIndex = 5;
            this.button2.Text = "Run compute";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(15, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(174, 21);
            this.label5.TabIndex = 10;
            this.label5.Text = "Mass Density, kg/m^3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(15, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(186, 21);
            this.label6.TabIndex = 11;
            this.label6.Text = "Young\'s Modulus, MPa";
            // 
            // massDensity
            // 
            this.massDensity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.massDensity.Location = new System.Drawing.Point(237, 27);
            this.massDensity.Name = "massDensity";
            this.massDensity.Size = new System.Drawing.Size(100, 29);
            this.massDensity.TabIndex = 12;
            this.massDensity.Text = "8.89e-9";
            // 
            // YoungsModulus
            // 
            this.YoungsModulus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.YoungsModulus.Location = new System.Drawing.Point(237, 53);
            this.YoungsModulus.Name = "YoungsModulus";
            this.YoungsModulus.Size = new System.Drawing.Size(100, 29);
            this.YoungsModulus.TabIndex = 13;
            this.YoungsModulus.Text = "125000";
            // 
            // PoissonsRatio
            // 
            this.PoissonsRatio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PoissonsRatio.Location = new System.Drawing.Point(237, 81);
            this.PoissonsRatio.Name = "PoissonsRatio";
            this.PoissonsRatio.Size = new System.Drawing.Size(100, 29);
            this.PoissonsRatio.TabIndex = 14;
            this.PoissonsRatio.Text = "0.35";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(15, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 21);
            this.label7.TabIndex = 15;
            this.label7.Text = "Posisson\'s Ratio";
            // 
            // PlasticData
            // 
            this.PlasticData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.PlasticData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PlasticData.Location = new System.Drawing.Point(17, 179);
            this.PlasticData.Name = "PlasticData";
            this.PlasticData.Size = new System.Drawing.Size(330, 164);
            this.PlasticData.TabIndex = 20;
            // 
            // Log
            // 
            this.Log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Log.Location = new System.Drawing.Point(6, 44);
            this.Log.Name = "Log";
            this.Log.Size = new System.Drawing.Size(713, 459);
            this.Log.TabIndex = 21;
            this.Log.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 21);
            this.label3.TabIndex = 22;
            this.label3.Text = "Log";
            // 
            // lastAngle
            // 
            this.lastAngle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lastAngle.Location = new System.Drawing.Point(538, 412);
            this.lastAngle.Name = "lastAngle";
            this.lastAngle.Size = new System.Drawing.Size(100, 29);
            this.lastAngle.TabIndex = 23;
            this.lastAngle.Text = "5";
            this.lastAngle.Visible = false;
            // 
            // strtAngle
            // 
            this.strtAngle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.strtAngle.Location = new System.Drawing.Point(512, 57);
            this.strtAngle.Name = "strtAngle";
            this.strtAngle.Size = new System.Drawing.Size(100, 29);
            this.strtAngle.TabIndex = 24;
            this.strtAngle.Text = "5";
            // 
            // angleStep
            // 
            this.angleStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.angleStep.Location = new System.Drawing.Point(538, 436);
            this.angleStep.Name = "angleStep";
            this.angleStep.Size = new System.Drawing.Size(100, 29);
            this.angleStep.TabIndex = 25;
            this.angleStep.Text = "5";
            this.angleStep.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(414, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 21);
            this.label4.TabIndex = 26;
            this.label4.Text = "Start Angle";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(445, 418);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 21);
            this.label8.TabIndex = 27;
            this.label8.Text = "End Angle";
            this.label8.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(440, 442);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 21);
            this.label9.TabIndex = 28;
            this.label9.Text = "Angle Step";
            this.label9.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(17, 349);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(90, 36);
            this.button3.TabIndex = 29;
            this.button3.Text = "Extra";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(17, 388);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 21);
            this.label10.TabIndex = 30;
            this.label10.Text = "Mesh";
            this.label10.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(17, 414);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(228, 21);
            this.label11.TabIndex = 31;
            this.label11.Text = "Count of Cores for Compute";
            this.label11.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(17, 442);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(106, 21);
            this.label12.TabIndex = 32;
            this.label12.Text = "Name of Job";
            this.label12.Visible = false;
            // 
            // Mesh
            // 
            this.Mesh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Mesh.Location = new System.Drawing.Point(251, 386);
            this.Mesh.Name = "Mesh";
            this.Mesh.Size = new System.Drawing.Size(100, 29);
            this.Mesh.TabIndex = 34;
            this.Mesh.Text = "2";
            this.Mesh.Visible = false;
            // 
            // Cores
            // 
            this.Cores.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Cores.Location = new System.Drawing.Point(251, 412);
            this.Cores.Name = "Cores";
            this.Cores.Size = new System.Drawing.Size(100, 29);
            this.Cores.TabIndex = 35;
            this.Cores.Text = "4";
            this.Cores.Visible = false;
            // 
            // JobName
            // 
            this.JobName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JobName.Location = new System.Drawing.Point(251, 440);
            this.JobName.Name = "JobName";
            this.JobName.Size = new System.Drawing.Size(100, 29);
            this.JobName.TabIndex = 36;
            this.JobName.Text = "Job-1";
            this.JobName.Visible = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.MainPage);
            this.tabControl1.Controls.Add(this.LogPage);
            this.tabControl1.Controls.Add(this.ExtraPage);
            this.tabControl1.Controls.Add(this.ChartPage);
            this.tabControl1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(677, 565);
            this.tabControl1.TabIndex = 38;
            // 
            // MainPage
            // 
            this.MainPage.Controls.Add(this.label17);
            this.MainPage.Controls.Add(this.label16);
            this.MainPage.Controls.Add(this.endFriction);
            this.MainPage.Controls.Add(this.label15);
            this.MainPage.Controls.Add(this.startFriction);
            this.MainPage.Controls.Add(this.label14);
            this.MainPage.Controls.Add(this.MaterialsList);
            this.MainPage.Controls.Add(this.label4);
            this.MainPage.Controls.Add(this.JobName);
            this.MainPage.Controls.Add(this.Cores);
            this.MainPage.Controls.Add(this.label2);
            this.MainPage.Controls.Add(this.Mesh);
            this.MainPage.Controls.Add(this.label12);
            this.MainPage.Controls.Add(this.blankRadius);
            this.MainPage.Controls.Add(this.label11);
            this.MainPage.Controls.Add(this.button2);
            this.MainPage.Controls.Add(this.label10);
            this.MainPage.Controls.Add(this.label5);
            this.MainPage.Controls.Add(this.button3);
            this.MainPage.Controls.Add(this.label6);
            this.MainPage.Controls.Add(this.label9);
            this.MainPage.Controls.Add(this.massDensity);
            this.MainPage.Controls.Add(this.label8);
            this.MainPage.Controls.Add(this.YoungsModulus);
            this.MainPage.Controls.Add(this.PoissonsRatio);
            this.MainPage.Controls.Add(this.angleStep);
            this.MainPage.Controls.Add(this.label7);
            this.MainPage.Controls.Add(this.strtAngle);
            this.MainPage.Controls.Add(this.PlasticData);
            this.MainPage.Controls.Add(this.lastAngle);
            this.MainPage.Location = new System.Drawing.Point(4, 30);
            this.MainPage.Name = "MainPage";
            this.MainPage.Padding = new System.Windows.Forms.Padding(3);
            this.MainPage.Size = new System.Drawing.Size(669, 531);
            this.MainPage.TabIndex = 0;
            this.MainPage.Text = "Main";
            this.MainPage.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(128, 155);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(98, 21);
            this.label17.TabIndex = 43;
            this.label17.Text = "Plastic Data";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(353, 178);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(97, 21);
            this.label16.TabIndex = 42;
            this.label16.Text = "Pick Plastic";
            // 
            // endFriction
            // 
            this.endFriction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.endFriction.Location = new System.Drawing.Point(512, 110);
            this.endFriction.Name = "endFriction";
            this.endFriction.Size = new System.Drawing.Size(100, 29);
            this.endFriction.TabIndex = 41;
            this.endFriction.Text = "0.4";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(403, 118);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(103, 21);
            this.label15.TabIndex = 40;
            this.label15.Text = "End Friction";
            // 
            // startFriction
            // 
            this.startFriction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.startFriction.Location = new System.Drawing.Point(512, 83);
            this.startFriction.Name = "startFriction";
            this.startFriction.Size = new System.Drawing.Size(100, 29);
            this.startFriction.TabIndex = 39;
            this.startFriction.Text = "0.25";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(398, 92);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(108, 21);
            this.label14.TabIndex = 38;
            this.label14.Text = "Start Friction";
            // 
            // MaterialsList
            // 
            this.MaterialsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MaterialsList.FormattingEnabled = true;
            this.MaterialsList.Location = new System.Drawing.Point(353, 202);
            this.MaterialsList.Name = "MaterialsList";
            this.MaterialsList.Size = new System.Drawing.Size(121, 29);
            this.MaterialsList.TabIndex = 37;
            this.MaterialsList.SelectedIndexChanged += new System.EventHandler(this.MaterialsList_SelectedIndexChanged);
            // 
            // LogPage
            // 
            this.LogPage.Controls.Add(this.Log);
            this.LogPage.Controls.Add(this.label3);
            this.LogPage.Location = new System.Drawing.Point(4, 30);
            this.LogPage.Name = "LogPage";
            this.LogPage.Padding = new System.Windows.Forms.Padding(3);
            this.LogPage.Size = new System.Drawing.Size(669, 531);
            this.LogPage.TabIndex = 1;
            this.LogPage.Text = "Log";
            this.LogPage.UseVisualStyleBackColor = true;
            // 
            // ExtraPage
            // 
            this.ExtraPage.Controls.Add(this.label1);
            this.ExtraPage.Controls.Add(this.label13);
            this.ExtraPage.Controls.Add(this.ExtraDataGridView);
            this.ExtraPage.Controls.Add(this.RemoveButton);
            this.ExtraPage.Controls.Add(this.AddButton);
            this.ExtraPage.Controls.Add(this.ExtraTextBox);
            this.ExtraPage.Controls.Add(this.ExtraMaterialsList);
            this.ExtraPage.Location = new System.Drawing.Point(4, 30);
            this.ExtraPage.Name = "ExtraPage";
            this.ExtraPage.Padding = new System.Windows.Forms.Padding(3);
            this.ExtraPage.Size = new System.Drawing.Size(669, 531);
            this.ExtraPage.TabIndex = 3;
            this.ExtraPage.Text = "Extra";
            this.ExtraPage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(363, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 21);
            this.label1.TabIndex = 6;
            this.label1.Text = "Plasic Data";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 313);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(125, 42);
            this.label13.TabIndex = 5;
            this.label13.Text = "Enter the name \r\nof new metal";
            // 
            // ExtraDataGridView
            // 
            this.ExtraDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.ExtraDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ExtraDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column4});
            this.ExtraDataGridView.Location = new System.Drawing.Point(6, 6);
            this.ExtraDataGridView.Name = "ExtraDataGridView";
            this.ExtraDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.ExtraDataGridView.Size = new System.Drawing.Size(337, 304);
            this.ExtraDataGridView.TabIndex = 4;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Yield Stress, MPa";
            this.Column3.Name = "Column3";
            this.Column3.Width = 154;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Plastic Strain";
            this.Column4.Name = "Column4";
            this.Column4.Width = 121;
            // 
            // RemoveButton
            // 
            this.RemoveButton.Location = new System.Drawing.Point(479, 36);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(98, 30);
            this.RemoveButton.TabIndex = 3;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(106, 358);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(98, 33);
            this.AddButton.TabIndex = 2;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // ExtraTextBox
            // 
            this.ExtraTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ExtraTextBox.Location = new System.Drawing.Point(148, 317);
            this.ExtraTextBox.Name = "ExtraTextBox";
            this.ExtraTextBox.Size = new System.Drawing.Size(152, 29);
            this.ExtraTextBox.TabIndex = 1;
            this.ExtraTextBox.TextChanged += new System.EventHandler(this.ExtraTextBox_TextChanged);
            // 
            // ExtraMaterialsList
            // 
            this.ExtraMaterialsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ExtraMaterialsList.FormattingEnabled = true;
            this.ExtraMaterialsList.Location = new System.Drawing.Point(352, 36);
            this.ExtraMaterialsList.Name = "ExtraMaterialsList";
            this.ExtraMaterialsList.Size = new System.Drawing.Size(121, 29);
            this.ExtraMaterialsList.TabIndex = 0;
            this.ExtraMaterialsList.SelectedIndexChanged += new System.EventHandler(this.ExtraMaterialsList_SelectedIndexChanged);
            // 
            // ChartPage
            // 
            this.ChartPage.Controls.Add(this.lineChart);
            this.ChartPage.Location = new System.Drawing.Point(4, 30);
            this.ChartPage.Name = "ChartPage";
            this.ChartPage.Padding = new System.Windows.Forms.Padding(3);
            this.ChartPage.Size = new System.Drawing.Size(669, 531);
            this.ChartPage.TabIndex = 4;
            this.ChartPage.Text = "Chart";
            this.ChartPage.UseVisualStyleBackColor = true;
            // 
            // lineChart
            // 
            chartArea1.Name = "ChartArea1";
            this.lineChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.lineChart.Legends.Add(legend1);
            this.lineChart.Location = new System.Drawing.Point(6, 6);
            this.lineChart.Name = "lineChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.lineChart.Series.Add(series1);
            this.lineChart.Size = new System.Drawing.Size(657, 372);
            this.lineChart.TabIndex = 0;
            this.lineChart.Text = "chart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 589);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "TopyProject";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PlasticData)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.MainPage.ResumeLayout(false);
            this.MainPage.PerformLayout();
            this.LogPage.ResumeLayout(false);
            this.LogPage.PerformLayout();
            this.ExtraPage.ResumeLayout(false);
            this.ExtraPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExtraDataGridView)).EndInit();
            this.ChartPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lineChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox Log;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        internal System.Windows.Forms.DataGridView PlasticData;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage MainPage;
        private System.Windows.Forms.TabPage LogPage;
        private System.Windows.Forms.TabPage ExtraPage;
        public System.Windows.Forms.DataGridView ExtraDataGridView;
        public System.Windows.Forms.Button RemoveButton;
        public System.Windows.Forms.Button AddButton;
        public System.Windows.Forms.TextBox ExtraTextBox;
        public System.Windows.Forms.ComboBox ExtraMaterialsList;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox MaterialsList;
        public System.Windows.Forms.TextBox blankRadius;
        public System.Windows.Forms.TextBox massDensity;
        public System.Windows.Forms.TextBox YoungsModulus;
        public System.Windows.Forms.TextBox PoissonsRatio;
        public System.Windows.Forms.TextBox lastAngle;
        public System.Windows.Forms.TextBox strtAngle;
        public System.Windows.Forms.TextBox angleStep;
        public System.Windows.Forms.TextBox Mesh;
        public System.Windows.Forms.TextBox Cores;
        public System.Windows.Forms.TextBox JobName;
        public System.Windows.Forms.TextBox endFriction;
        public System.Windows.Forms.TextBox startFriction;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TabPage ChartPage;
        public System.Windows.Forms.DataVisualization.Charting.Chart lineChart;
    }
}

