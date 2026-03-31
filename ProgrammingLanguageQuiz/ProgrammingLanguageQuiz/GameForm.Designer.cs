namespace ProgrammingLanguageQuiz
{
    partial class GameForm
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
            this.pnlQuiz = new System.Windows.Forms.Panel();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblTimer = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblExplanation = new System.Windows.Forms.Label();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.rbOption1 = new System.Windows.Forms.RadioButton();
            this.rbOption2 = new System.Windows.Forms.RadioButton();
            this.rbOption3 = new System.Windows.Forms.RadioButton();
            this.rbOption4 = new System.Windows.Forms.RadioButton();
            this.pnlQuiz.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlQuiz
            // 
            this.pnlQuiz.BackColor = System.Drawing.Color.White;
            this.pnlQuiz.Controls.Add(this.lblQuestion);
            this.pnlQuiz.Controls.Add(this.lblScore);
            this.pnlQuiz.Controls.Add(this.lblTimer);
            this.pnlQuiz.Controls.Add(this.lblLevel);
            this.pnlQuiz.Controls.Add(this.lblExplanation);
            this.pnlQuiz.Controls.Add(this.pbImage);
            this.pnlQuiz.Controls.Add(this.btnSubmit);
            this.pnlQuiz.Controls.Add(this.btnNext);
            this.pnlQuiz.Controls.Add(this.btnExit);
            this.pnlQuiz.Controls.Add(this.rbOption1);
            this.pnlQuiz.Controls.Add(this.rbOption2);
            this.pnlQuiz.Controls.Add(this.rbOption3);
            this.pnlQuiz.Controls.Add(this.rbOption4);
            this.pnlQuiz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlQuiz.Location = new System.Drawing.Point(0, 0);
            this.pnlQuiz.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlQuiz.Name = "pnlQuiz";
            this.pnlQuiz.Size = new System.Drawing.Size(1000, 738);
            this.pnlQuiz.TabIndex = 0;
            // 
            // lblQuestion
            // 
            this.lblQuestion.BackColor = System.Drawing.Color.LightBlue;
            this.lblQuestion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblQuestion.Location = new System.Drawing.Point(27, 25);
            this.lblQuestion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(946, 98);
            this.lblQuestion.TabIndex = 0;
            this.lblQuestion.Text = "Вопрос будет отображен здесь";
            this.lblQuestion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblScore
            // 
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblScore.ForeColor = System.Drawing.Color.Green;
            this.lblScore.Location = new System.Drawing.Point(27, 640);
            this.lblScore.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(200, 37);
            this.lblScore.TabIndex = 1;
            this.lblScore.Text = "Счёт: 0";
            this.lblScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTimer
            // 
            this.lblTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTimer.ForeColor = System.Drawing.Color.Red;
            this.lblTimer.Location = new System.Drawing.Point(773, 640);
            this.lblTimer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(200, 37);
            this.lblTimer.TabIndex = 2;
            this.lblTimer.Text = "Время: 60";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLevel
            // 
            this.lblLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblLevel.ForeColor = System.Drawing.Color.Blue;
            this.lblLevel.Location = new System.Drawing.Point(27, 677);
            this.lblLevel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(333, 37);
            this.lblLevel.TabIndex = 3;
            this.lblLevel.Text = "Уровень: 1 (Начальный)";
            this.lblLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblExplanation
            // 
            this.lblExplanation.BackColor = System.Drawing.Color.LightYellow;
            this.lblExplanation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblExplanation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblExplanation.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblExplanation.Location = new System.Drawing.Point(253, 345);
            this.lblExplanation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExplanation.Name = "lblExplanation";
            this.lblExplanation.Size = new System.Drawing.Size(719, 123);
            this.lblExplanation.TabIndex = 7;
            this.lblExplanation.Text = "Пояснение:";
            this.lblExplanation.Visible = false;
            // 
            // pbImage
            // 
            this.pbImage.BackColor = System.Drawing.Color.LightGray;
            this.pbImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbImage.Location = new System.Drawing.Point(27, 135);
            this.pbImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(213, 196);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 6;
            this.pbImage.TabStop = false;
            this.pbImage.Visible = false;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.LightGreen;
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.Location = new System.Drawing.Point(467, 640);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(200, 49);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "✅ Ответить";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.LightBlue;
            this.btnNext.Enabled = false;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnNext.Location = new System.Drawing.Point(773, 677);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(200, 49);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "➡ Следующий";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.LightCoral;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(27, 689);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(160, 43);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "🏠 Выход";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // rbOption1
            // 
            this.rbOption1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rbOption1.Location = new System.Drawing.Point(253, 135);
            this.rbOption1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbOption1.Name = "rbOption1";
            this.rbOption1.Size = new System.Drawing.Size(720, 37);
            this.rbOption1.TabIndex = 8;
            this.rbOption1.Text = "Вариант ответа 1";
            this.rbOption1.UseVisualStyleBackColor = true;
            // 
            // rbOption2
            // 
            this.rbOption2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rbOption2.Location = new System.Drawing.Point(253, 178);
            this.rbOption2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbOption2.Name = "rbOption2";
            this.rbOption2.Size = new System.Drawing.Size(720, 37);
            this.rbOption2.TabIndex = 9;
            this.rbOption2.Text = "Вариант ответа 2";
            this.rbOption2.UseVisualStyleBackColor = true;
            // 
            // rbOption3
            // 
            this.rbOption3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rbOption3.Location = new System.Drawing.Point(253, 222);
            this.rbOption3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbOption3.Name = "rbOption3";
            this.rbOption3.Size = new System.Drawing.Size(720, 37);
            this.rbOption3.TabIndex = 10;
            this.rbOption3.Text = "Вариант ответа 3";
            this.rbOption3.UseVisualStyleBackColor = true;
            this.rbOption3.CheckedChanged += new System.EventHandler(this.rbOption3_CheckedChanged);
            // 
            // rbOption4
            // 
            this.rbOption4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rbOption4.Location = new System.Drawing.Point(253, 265);
            this.rbOption4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbOption4.Name = "rbOption4";
            this.rbOption4.Size = new System.Drawing.Size(720, 37);
            this.rbOption4.TabIndex = 11;
            this.rbOption4.Text = "Вариант ответа 4";
            this.rbOption4.UseVisualStyleBackColor = true;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 738);
            this.Controls.Add(this.pnlQuiz);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Тестирование по программированию";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_FormClosing);
            this.pnlQuiz.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);

        }

        // Объявление всех элементов управления
        private System.Windows.Forms.Panel pnlQuiz;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblExplanation;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.RadioButton rbOption1;
        private System.Windows.Forms.RadioButton rbOption2;
        private System.Windows.Forms.RadioButton rbOption3;
        private System.Windows.Forms.RadioButton rbOption4;
    }
}