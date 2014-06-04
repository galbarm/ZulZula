namespace ZulZula
{
    partial class MainForm
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
            this._stocksListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this._fromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._toDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this._algorithmsComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this._arg0TextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this._arg1TextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this._arg2TextBox = new System.Windows.Forms.TextBox();
            this._goButton = new System.Windows.Forms.Button();
            this._logListView = new System.Windows.Forms.ListView();
            this._logColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._clearLogButton = new System.Windows.Forms.Button();
            this._algDescriptionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _stocksListBox
            // 
            this._stocksListBox.FormattingEnabled = true;
            this._stocksListBox.Location = new System.Drawing.Point(15, 25);
            this._stocksListBox.Name = "_stocksListBox";
            this._stocksListBox.Size = new System.Drawing.Size(233, 95);
            this._stocksListBox.TabIndex = 0;
            this._stocksListBox.SelectedIndexChanged += new System.EventHandler(this.OnStockSelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Stock";
            // 
            // _fromDateTimePicker
            // 
            this._fromDateTimePicker.Location = new System.Drawing.Point(48, 139);
            this._fromDateTimePicker.Name = "_fromDateTimePicker";
            this._fromDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this._fromDateTimePicker.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "From";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "To";
            // 
            // _toDateTimePicker
            // 
            this._toDateTimePicker.Location = new System.Drawing.Point(48, 169);
            this._toDateTimePicker.Name = "_toDateTimePicker";
            this._toDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this._toDateTimePicker.TabIndex = 5;
            // 
            // _algorithmsComboBox
            // 
            this._algorithmsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._algorithmsComboBox.FormattingEnabled = true;
            this._algorithmsComboBox.Location = new System.Drawing.Point(297, 25);
            this._algorithmsComboBox.Name = "_algorithmsComboBox";
            this._algorithmsComboBox.Size = new System.Drawing.Size(200, 21);
            this._algorithmsComboBox.TabIndex = 6;
            this._algorithmsComboBox.SelectedIndexChanged += new System.EventHandler(this.OnAlgorithmChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(294, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Algorithm";
            // 
            // _arg0TextBox
            // 
            this._arg0TextBox.Location = new System.Drawing.Point(297, 65);
            this._arg0TextBox.Name = "_arg0TextBox";
            this._arg0TextBox.Size = new System.Drawing.Size(36, 20);
            this._arg0TextBox.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(294, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "arg0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(338, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "arg1";
            // 
            // _arg1TextBox
            // 
            this._arg1TextBox.Location = new System.Drawing.Point(341, 65);
            this._arg1TextBox.Name = "_arg1TextBox";
            this._arg1TextBox.Size = new System.Drawing.Size(36, 20);
            this._arg1TextBox.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(381, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "arg2";
            // 
            // _arg2TextBox
            // 
            this._arg2TextBox.Location = new System.Drawing.Point(384, 65);
            this._arg2TextBox.Name = "_arg2TextBox";
            this._arg2TextBox.Size = new System.Drawing.Size(36, 20);
            this._arg2TextBox.TabIndex = 12;
            // 
            // _goButton
            // 
            this._goButton.Enabled = false;
            this._goButton.Location = new System.Drawing.Point(456, 175);
            this._goButton.Name = "_goButton";
            this._goButton.Size = new System.Drawing.Size(75, 23);
            this._goButton.TabIndex = 15;
            this._goButton.Text = "Go";
            this._goButton.UseVisualStyleBackColor = true;
            this._goButton.Click += new System.EventHandler(this.OnGoClick);
            // 
            // _logListView
            // 
            this._logListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._logColumn});
            this._logListView.FullRowSelect = true;
            this._logListView.GridLines = true;
            this._logListView.Location = new System.Drawing.Point(12, 204);
            this._logListView.MultiSelect = false;
            this._logListView.Name = "_logListView";
            this._logListView.Size = new System.Drawing.Size(519, 354);
            this._logListView.TabIndex = 16;
            this._logListView.UseCompatibleStateImageBehavior = false;
            this._logListView.View = System.Windows.Forms.View.Details;
            // 
            // _logColumn
            // 
            this._logColumn.Text = "Log";
            this._logColumn.Width = 493;
            // 
            // _clearLogButton
            // 
            this._clearLogButton.Location = new System.Drawing.Point(436, 564);
            this._clearLogButton.Name = "_clearLogButton";
            this._clearLogButton.Size = new System.Drawing.Size(75, 23);
            this._clearLogButton.TabIndex = 17;
            this._clearLogButton.Text = "Clear Log";
            this._clearLogButton.UseVisualStyleBackColor = true;
            this._clearLogButton.Click += new System.EventHandler(this.OnClearLogClick);
            // 
            // _algDescriptionButton
            // 
            this._algDescriptionButton.Location = new System.Drawing.Point(503, 25);
            this._algDescriptionButton.Name = "_algDescriptionButton";
            this._algDescriptionButton.Size = new System.Drawing.Size(19, 21);
            this._algDescriptionButton.TabIndex = 18;
            this._algDescriptionButton.Text = "?";
            this._algDescriptionButton.UseVisualStyleBackColor = true;
            this._algDescriptionButton.Click += new System.EventHandler(this.OnAlgDescriptionButtonClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 599);
            this.Controls.Add(this._algDescriptionButton);
            this.Controls.Add(this._clearLogButton);
            this.Controls.Add(this._logListView);
            this.Controls.Add(this._goButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this._arg2TextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this._arg1TextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._arg0TextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._algorithmsComboBox);
            this.Controls.Add(this._toDateTimePicker);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._fromDateTimePicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._stocksListBox);
            this.Name = "MainForm";
            this.Text = "ZulZula";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox _stocksListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker _fromDateTimePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker _toDateTimePicker;
        private System.Windows.Forms.ComboBox _algorithmsComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _arg0TextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox _arg1TextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox _arg2TextBox;
        private System.Windows.Forms.Button _goButton;
        private System.Windows.Forms.ListView _logListView;
        private System.Windows.Forms.ColumnHeader _logColumn;
        private System.Windows.Forms.Button _clearLogButton;
        private System.Windows.Forms.Button _algDescriptionButton;
    }
}

