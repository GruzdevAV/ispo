
namespace SimpleHTTPServer
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
            this.StopServerBtn = new System.Windows.Forms.Button();
            this.StartServerBtn = new System.Windows.Forms.Button();
            this.serverPortText = new System.Windows.Forms.TextBox();
            this.serverLogsText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.messageToBrowser = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // StopServerBtn
            // 
            this.StopServerBtn.Location = new System.Drawing.Point(34, 108);
            this.StopServerBtn.Name = "StopServerBtn";
            this.StopServerBtn.Size = new System.Drawing.Size(125, 23);
            this.StopServerBtn.TabIndex = 0;
            this.StopServerBtn.Text = "Stop Server";
            this.StopServerBtn.UseVisualStyleBackColor = true;
            this.StopServerBtn.Click += new System.EventHandler(this.StopServerBtn_Click);
            // 
            // StartServerBtn
            // 
            this.StartServerBtn.Location = new System.Drawing.Point(37, 79);
            this.StartServerBtn.Name = "StartServerBtn";
            this.StartServerBtn.Size = new System.Drawing.Size(121, 23);
            this.StartServerBtn.TabIndex = 1;
            this.StartServerBtn.Text = "Start Server";
            this.StartServerBtn.UseVisualStyleBackColor = true;
            this.StartServerBtn.Click += new System.EventHandler(this.StartServerBtn_Click);
            // 
            // serverPortText
            // 
            this.serverPortText.Location = new System.Drawing.Point(33, 174);
            this.serverPortText.Name = "serverPortText";
            this.serverPortText.Size = new System.Drawing.Size(100, 22);
            this.serverPortText.TabIndex = 2;
            // 
            // serverLogsText
            // 
            this.serverLogsText.Location = new System.Drawing.Point(271, 34);
            this.serverLogsText.Multiline = true;
            this.serverLogsText.Name = "serverLogsText";
            this.serverLogsText.Size = new System.Drawing.Size(517, 404);
            this.serverLogsText.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Server Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(271, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Server Logs:";
            // 
            // messageToBrowser
            // 
            this.messageToBrowser.Location = new System.Drawing.Point(34, 279);
            this.messageToBrowser.Multiline = true;
            this.messageToBrowser.Name = "messageToBrowser";
            this.messageToBrowser.Size = new System.Drawing.Size(131, 126);
            this.messageToBrowser.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.messageToBrowser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.serverLogsText);
            this.Controls.Add(this.serverPortText);
            this.Controls.Add(this.StartServerBtn);
            this.Controls.Add(this.StopServerBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StopServerBtn;
        private System.Windows.Forms.Button StartServerBtn;
        private System.Windows.Forms.TextBox serverPortText;
        private System.Windows.Forms.TextBox serverLogsText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox messageToBrowser;
    }
}

