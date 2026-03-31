namespace ProgrammingLanguageQuiz
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblSelectTopic = new System.Windows.Forms.Label();
            this.cmbTopics = new System.Windows.Forms.ComboBox();
            this.btnStartGame = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();

            // pnlMain
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Controls.Add(this.lblDescription);
            this.pnlMain.Controls.Add(this.lblSelectTopic);
            this.pnlMain.Controls.Add(this.cmbTopics);
            this.pnlMain.Controls.Add(this.btnStartGame);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(550, 500);
            this.pnlMain.TabIndex = 0;

            // lblTitle
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(50, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(450, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quiz: Изучение языков программирования";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblDescription
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblDescription.ForeColor = System.Drawing.Color.Gray;
            this.lblDescription.Location = new System.Drawing.Point(50, 100);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(450, 60);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Проверьте свои знания в языке программирования C#.\r\nВыберите тему и проходите тесты по уровням сложности.";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblSelectTopic
            this.lblSelectTopic.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblSelectTopic.Location = new System.Drawing.Point(150, 180);
            this.lblSelectTopic.Name = "lblSelectTopic";
            this.lblSelectTopic.Size = new System.Drawing.Size(250, 30);
            this.lblSelectTopic.TabIndex = 2;
            this.lblSelectTopic.Text = "Выберите тему для тестирования:";
            this.lblSelectTopic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // cmbTopics
            this.cmbTopics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTopics.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cmbTopics.Location = new System.Drawing.Point(150, 220);
            this.cmbTopics.Name = "cmbTopics";
            this.cmbTopics.Size = new System.Drawing.Size(250, 28);
            this.cmbTopics.TabIndex = 3;

            // btnStartGame
            this.btnStartGame.BackColor = System.Drawing.Color.LightGreen;
            this.btnStartGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnStartGame.Location = new System.Drawing.Point(150, 280);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(250, 45);
            this.btnStartGame.TabIndex = 4;
            this.btnStartGame.Text = "▶ Начать тестирование";
            this.btnStartGame.UseVisualStyleBackColor = false;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);

            // btnExit
            this.btnExit.BackColor = System.Drawing.Color.LightCoral;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(150, 350);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(250, 45);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "✖ Выход";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 500);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quiz: Изучение языков программирования";
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblSelectTopic;
        private System.Windows.Forms.ComboBox cmbTopics;
        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.Button btnExit;
    }
}