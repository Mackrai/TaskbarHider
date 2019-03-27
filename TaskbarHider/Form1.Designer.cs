namespace TaskbarHider
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.refreshListBtn = new System.Windows.Forms.Button();
            this.processesListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.curProcLabel = new System.Windows.Forms.Label();
            this.currProcTB = new System.Windows.Forms.TextBox();
            this.applyBtn = new System.Windows.Forms.Button();
            this.StartBtn = new System.Windows.Forms.Button();
            this.fileProcessesListView = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.deleteFromFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 25);
            this.button1.TabIndex = 0;
            this.button1.Text = "Hide taskbar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 43);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 25);
            this.button2.TabIndex = 1;
            this.button2.Text = "Show taskbar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // refreshListBtn
            // 
            this.refreshListBtn.Location = new System.Drawing.Point(12, 332);
            this.refreshListBtn.Name = "refreshListBtn";
            this.refreshListBtn.Size = new System.Drawing.Size(117, 26);
            this.refreshListBtn.TabIndex = 3;
            this.refreshListBtn.Text = "Refresh list";
            this.refreshListBtn.UseVisualStyleBackColor = true;
            this.refreshListBtn.Click += new System.EventHandler(this.refreshListBtn_Click);
            // 
            // processesListView
            // 
            this.processesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.processesListView.Location = new System.Drawing.Point(135, 12);
            this.processesListView.MultiSelect = false;
            this.processesListView.Name = "processesListView";
            this.processesListView.Size = new System.Drawing.Size(159, 346);
            this.processesListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.processesListView.TabIndex = 4;
            this.processesListView.UseCompatibleStateImageBehavior = false;
            this.processesListView.View = System.Windows.Forms.View.Details;
            this.processesListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.processesListView_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "PROCESS NAME";
            this.columnHeader1.Width = 155;
            // 
            // curProcLabel
            // 
            this.curProcLabel.AutoSize = true;
            this.curProcLabel.Location = new System.Drawing.Point(9, 195);
            this.curProcLabel.Name = "curProcLabel";
            this.curProcLabel.Size = new System.Drawing.Size(121, 17);
            this.curProcLabel.TabIndex = 5;
            this.curProcLabel.Text = "Selected process:";
            // 
            // currProcTB
            // 
            this.currProcTB.Location = new System.Drawing.Point(12, 215);
            this.currProcTB.Name = "currProcTB";
            this.currProcTB.ReadOnly = true;
            this.currProcTB.Size = new System.Drawing.Size(114, 22);
            this.currProcTB.TabIndex = 6;
            // 
            // applyBtn
            // 
            this.applyBtn.Location = new System.Drawing.Point(12, 243);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(114, 44);
            this.applyBtn.TabIndex = 7;
            this.applyBtn.Text = "Add to allowed processes";
            this.applyBtn.UseVisualStyleBackColor = true;
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(9, 103);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(117, 59);
            this.StartBtn.TabIndex = 8;
            this.StartBtn.Text = "START";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // fileProcessesListView
            // 
            this.fileProcessesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.fileProcessesListView.Location = new System.Drawing.Point(300, 12);
            this.fileProcessesListView.Name = "fileProcessesListView";
            this.fileProcessesListView.Size = new System.Drawing.Size(155, 314);
            this.fileProcessesListView.TabIndex = 9;
            this.fileProcessesListView.UseCompatibleStateImageBehavior = false;
            this.fileProcessesListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "PROCESSES.TXT";
            this.columnHeader2.Width = 151;
            // 
            // deleteFromFile
            // 
            this.deleteFromFile.Location = new System.Drawing.Point(300, 332);
            this.deleteFromFile.Name = "deleteFromFile";
            this.deleteFromFile.Size = new System.Drawing.Size(154, 26);
            this.deleteFromFile.TabIndex = 10;
            this.deleteFromFile.Text = "Delete from list";
            this.deleteFromFile.UseVisualStyleBackColor = true;
            this.deleteFromFile.Click += new System.EventHandler(this.deleteFromFile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 370);
            this.Controls.Add(this.deleteFromFile);
            this.Controls.Add(this.fileProcessesListView);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.applyBtn);
            this.Controls.Add(this.currProcTB);
            this.Controls.Add(this.curProcLabel);
            this.Controls.Add(this.processesListView);
            this.Controls.Add(this.refreshListBtn);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Taskbar Hider";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button refreshListBtn;
        private System.Windows.Forms.ListView processesListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label curProcLabel;
        private System.Windows.Forms.TextBox currProcTB;
        private System.Windows.Forms.Button applyBtn;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.ListView fileProcessesListView;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button deleteFromFile;
    }
}

