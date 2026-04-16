namespace VbeAddin.UI
{
    partial class AiAssistantControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.txtQuestion = new System.Windows.Forms.RichTextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtAnswer = new System.Windows.Forms.RichTextBox();
            this.tableLayout.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();

            // tableLayout
            this.tableLayout.ColumnCount = 1;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Controls.Add(this.txtQuestion, 0, 0);
            this.tableLayout.Controls.Add(this.panelButtons, 0, 1);
            this.tableLayout.Controls.Add(this.txtAnswer, 0, 2);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.RowCount = 3;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));

            // txtQuestion
            this.txtQuestion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuestion.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtQuestion.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;

            // panelButtons
            this.panelButtons.Controls.Add(this.btnSend);
            this.panelButtons.Controls.Add(this.lblStatus);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Padding = new System.Windows.Forms.Padding(2);

            // btnSend
            this.btnSend.Text = "Send";
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSend.Width = 70;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);

            // lblStatus
            this.lblStatus.Text = "Ready";
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8F);

            // txtAnswer
            this.txtAnswer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAnswer.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtAnswer.ReadOnly = true;
            this.txtAnswer.BackColor = System.Drawing.SystemColors.Window;
            this.txtAnswer.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;

            // AiAssistantControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayout);
            this.Name = "AiAssistantControl";
            this.Size = new System.Drawing.Size(280, 500);
            this.tableLayout.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.RichTextBox txtQuestion;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.RichTextBox txtAnswer;
    }
}
