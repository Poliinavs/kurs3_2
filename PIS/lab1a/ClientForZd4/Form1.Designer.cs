namespace ClientForZd4
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TextX = new System.Windows.Forms.TextBox();
            this.TextY = new System.Windows.Forms.TextBox();
            this.result = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(198, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "x:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "y:";
            // 
            // TextX
            // 
            this.TextX.Location = new System.Drawing.Point(60, 26);
            this.TextX.Name = "TextX";
            this.TextX.Size = new System.Drawing.Size(100, 22);
            this.TextX.TabIndex = 3;
            // 
            // TextY
            // 
            this.TextY.Location = new System.Drawing.Point(60, 82);
            this.TextY.Name = "TextY";
            this.TextY.Size = new System.Drawing.Size(100, 22);
            this.TextY.TabIndex = 4;
            // 
            // result
            // 
            this.result.AutoSize = true;
            this.result.Location = new System.Drawing.Point(195, 112);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(0, 16);
            this.result.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 161);
            this.Controls.Add(this.result);
            this.Controls.Add(this.TextY);
            this.Controls.Add(this.TextX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextX;
        private System.Windows.Forms.TextBox TextY;
        private System.Windows.Forms.Label result;
    }
}

