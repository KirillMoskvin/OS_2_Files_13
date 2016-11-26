namespace OS_2_Files_13
{
    partial class Form_Main
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btn_task = new System.Windows.Forms.Button();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.tb_result = new System.Windows.Forms.TextBox();
            this.button_condition = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_task
            // 
            this.btn_task.Location = new System.Drawing.Point(12, 239);
            this.btn_task.Name = "btn_task";
            this.btn_task.Size = new System.Drawing.Size(75, 23);
            this.btn_task.TabIndex = 0;
            this.btn_task.Text = "Выполнить";
            this.btn_task.UseVisualStyleBackColor = true;
            this.btn_task.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_result
            // 
            this.tb_result.Location = new System.Drawing.Point(12, 12);
            this.tb_result.Multiline = true;
            this.tb_result.Name = "tb_result";
            this.tb_result.Size = new System.Drawing.Size(462, 206);
            this.tb_result.TabIndex = 1;
            // 
            // button_condition
            // 
            this.button_condition.Location = new System.Drawing.Point(399, 239);
            this.button_condition.Name = "button_condition";
            this.button_condition.Size = new System.Drawing.Size(75, 23);
            this.button_condition.TabIndex = 2;
            this.button_condition.Text = "Условие";
            this.button_condition.UseVisualStyleBackColor = true;
            this.button_condition.Click += new System.EventHandler(this.button_condition_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 291);
            this.Controls.Add(this.button_condition);
            this.Controls.Add(this.tb_result);
            this.Controls.Add(this.btn_task);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form_Main";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Задача 2.13";
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btn_task;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.TextBox tb_result;
        private System.Windows.Forms.Button button_condition;
    }
}

