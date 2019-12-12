namespace sulatetun_jarjestelman_projekti
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.uiUpdater = new System.ComponentModel.BackgroundWorker();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.movementPanel = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.textBoxX = new System.Windows.Forms.TextBox();
            this.textBoxY = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.JsonTextbox = new System.Windows.Forms.TextBox();
            this.ClientUpdater = new System.ComponentModel.BackgroundWorker();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.counttextBox = new System.Windows.Forms.TextBox();
            this.speedtextBox = new System.Windows.Forms.TextBox();
            this.degtextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.recLabel = new System.Windows.Forms.Label();
            this.playBoxRed = new System.Windows.Forms.PictureBox();
            this.playLabel = new System.Windows.Forms.Label();
            this.RecordBoxRed = new System.Windows.Forms.PictureBox();
            this.RecordBoxGreen = new System.Windows.Forms.PictureBox();
            this.playBoxGreen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.movementPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playBoxRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecordBoxRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecordBoxGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playBoxGreen)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(374, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(201, 157);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(185, 78);
            this.textBox1.TabIndex = 2;
            // 
            // uiUpdater
            // 
            this.uiUpdater.WorkerReportsProgress = true;
            this.uiUpdater.DoWork += new System.ComponentModel.DoWorkEventHandler(this.uiUpdater_DoWork);
            this.uiUpdater.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.uiUpdater_ProgressChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(55, 157);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 22);
            this.textBox2.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(55, 185);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 22);
            this.textBox3.TabIndex = 4;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(55, 213);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 22);
            this.textBox4.TabIndex = 5;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(349, 399);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 22);
            this.textBox5.TabIndex = 6;
            // 
            // movementPanel
            // 
            chartArea1.Name = "ChartArea1";
            this.movementPanel.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.movementPanel.Legends.Add(legend1);
            this.movementPanel.Location = new System.Drawing.Point(473, 12);
            this.movementPanel.Name = "movementPanel";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Legend = "Legend1";
            series1.Name = "move";
            this.movementPanel.Series.Add(series1);
            this.movementPanel.Size = new System.Drawing.Size(1055, 822);
            this.movementPanel.TabIndex = 7;
            this.movementPanel.Text = "chart1";
            // 
            // textBoxX
            // 
            this.textBoxX.Location = new System.Drawing.Point(1405, 75);
            this.textBoxX.Name = "textBoxX";
            this.textBoxX.Size = new System.Drawing.Size(100, 22);
            this.textBoxX.TabIndex = 8;
            // 
            // textBoxY
            // 
            this.textBoxY.Location = new System.Drawing.Point(1405, 103);
            this.textBoxY.Name = "textBoxY";
            this.textBoxY.Size = new System.Drawing.Size(100, 22);
            this.textBoxY.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1369, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1369, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Y";
            // 
            // JsonTextbox
            // 
            this.JsonTextbox.Location = new System.Drawing.Point(12, 471);
            this.JsonTextbox.Multiline = true;
            this.JsonTextbox.Name = "JsonTextbox";
            this.JsonTextbox.Size = new System.Drawing.Size(387, 195);
            this.JsonTextbox.TabIndex = 13;
            // 
            // ClientUpdater
            // 
            this.ClientUpdater.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ClientUpdater_DoWork);
            this.ClientUpdater.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ClientUpdater_ProgressChanged);
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(12, 324);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 22);
            this.textBox6.TabIndex = 14;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(12, 361);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(100, 22);
            this.textBox7.TabIndex = 15;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(12, 399);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(100, 22);
            this.textBox8.TabIndex = 16;
            // 
            // counttextBox
            // 
            this.counttextBox.Location = new System.Drawing.Point(233, 324);
            this.counttextBox.Name = "counttextBox";
            this.counttextBox.Size = new System.Drawing.Size(100, 22);
            this.counttextBox.TabIndex = 17;
            // 
            // speedtextBox
            // 
            this.speedtextBox.Location = new System.Drawing.Point(233, 361);
            this.speedtextBox.Name = "speedtextBox";
            this.speedtextBox.Size = new System.Drawing.Size(100, 22);
            this.speedtextBox.TabIndex = 18;
            // 
            // degtextBox
            // 
            this.degtextBox.Location = new System.Drawing.Point(233, 399);
            this.degtextBox.Name = "degtextBox";
            this.degtextBox.Size = new System.Drawing.Size(100, 22);
            this.degtextBox.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(169, 324);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 20;
            this.label3.Text = "Counter";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // recLabel
            // 
            this.recLabel.AutoSize = true;
            this.recLabel.Location = new System.Drawing.Point(86, 27);
            this.recLabel.Name = "recLabel";
            this.recLabel.Size = new System.Drawing.Size(36, 17);
            this.recLabel.TabIndex = 23;
            this.recLabel.Text = "REC";
            // 
            // playBoxRed
            // 
            this.playBoxRed.Image = global::sulatetun_jarjestelman_projekti.Properties.Resources.red_led_on_th;
            this.playBoxRed.Location = new System.Drawing.Point(201, 47);
            this.playBoxRed.Name = "playBoxRed";
            this.playBoxRed.Size = new System.Drawing.Size(100, 100);
            this.playBoxRed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.playBoxRed.TabIndex = 24;
            this.playBoxRed.TabStop = false;
            // 
            // playLabel
            // 
            this.playLabel.AutoSize = true;
            this.playLabel.Location = new System.Drawing.Point(230, 27);
            this.playLabel.Name = "playLabel";
            this.playLabel.Size = new System.Drawing.Size(43, 17);
            this.playLabel.TabIndex = 28;
            this.playLabel.Text = "PLAY";
            // 
            // RecordBoxRed
            // 
            this.RecordBoxRed.Image = global::sulatetun_jarjestelman_projekti.Properties.Resources.red_led_on_th;
            this.RecordBoxRed.Location = new System.Drawing.Point(55, 47);
            this.RecordBoxRed.Name = "RecordBoxRed";
            this.RecordBoxRed.Size = new System.Drawing.Size(100, 100);
            this.RecordBoxRed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.RecordBoxRed.TabIndex = 29;
            this.RecordBoxRed.TabStop = false;
            // 
            // RecordBoxGreen
            // 
            this.RecordBoxGreen.Image = global::sulatetun_jarjestelman_projekti.Properties.Resources.green_led_on_th;
            this.RecordBoxGreen.Location = new System.Drawing.Point(55, 47);
            this.RecordBoxGreen.Name = "RecordBoxGreen";
            this.RecordBoxGreen.Size = new System.Drawing.Size(100, 100);
            this.RecordBoxGreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.RecordBoxGreen.TabIndex = 30;
            this.RecordBoxGreen.TabStop = false;
            // 
            // playBoxGreen
            // 
            this.playBoxGreen.Image = global::sulatetun_jarjestelman_projekti.Properties.Resources.green_led_on_th;
            this.playBoxGreen.Location = new System.Drawing.Point(201, 47);
            this.playBoxGreen.Name = "playBoxGreen";
            this.playBoxGreen.Size = new System.Drawing.Size(100, 100);
            this.playBoxGreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.playBoxGreen.TabIndex = 31;
            this.playBoxGreen.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1540, 846);
            this.Controls.Add(this.playBoxGreen);
            this.Controls.Add(this.RecordBoxGreen);
            this.Controls.Add(this.RecordBoxRed);
            this.Controls.Add(this.playLabel);
            this.Controls.Add(this.playBoxRed);
            this.Controls.Add(this.recLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.degtextBox);
            this.Controls.Add(this.speedtextBox);
            this.Controls.Add(this.counttextBox);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.JsonTextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxY);
            this.Controls.Add(this.textBoxX);
            this.Controls.Add(this.movementPanel);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.movementPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playBoxRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecordBoxRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecordBoxGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playBoxGreen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.ComponentModel.BackgroundWorker uiUpdater;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.DataVisualization.Charting.Chart movementPanel;
        private System.Windows.Forms.TextBox textBoxX;
        private System.Windows.Forms.TextBox textBoxY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox JsonTextbox;
        private System.ComponentModel.BackgroundWorker ClientUpdater;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox counttextBox;
        private System.Windows.Forms.TextBox speedtextBox;
        private System.Windows.Forms.TextBox degtextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label recLabel;
        private System.Windows.Forms.PictureBox playBoxRed;
        private System.Windows.Forms.Label playLabel;
        private System.Windows.Forms.PictureBox RecordBoxRed;
        private System.Windows.Forms.PictureBox RecordBoxGreen;
        private System.Windows.Forms.PictureBox playBoxGreen;
    }
}

