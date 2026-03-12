using System;
using System.Windows.Forms;

namespace TriangleApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBoxCount;
        private TextBox textBoxMin;
        private TextBox textBoxMax;
        private Button buttonGenerate;
        private DataGridView dataGridView1;
        private Label labelResult;

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
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.textBoxCount = new TextBox();
            this.textBoxMin = new TextBox();
            this.textBoxMax = new TextBox();
            this.buttonGenerate = new Button();
            this.dataGridView1 = new DataGridView();
            this.labelResult = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Количество треугольников:";

            // textBoxCount
            this.textBoxCount.Location = new System.Drawing.Point(190, 17);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(80, 23);
            this.textBoxCount.TabIndex = 1;
            this.textBoxCount.TextChanged += new EventHandler(this.textBox_TextChanged);
            this.textBoxCount.KeyPress += new KeyPressEventHandler(this.textBox_KeyPress);

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Мин. длина стороны:";

            // textBoxMin
            this.textBoxMin.Location = new System.Drawing.Point(190, 47);
            this.textBoxMin.Name = "textBoxMin";
            this.textBoxMin.Size = new System.Drawing.Size(80, 23);
            this.textBoxMin.TabIndex = 3;
            this.textBoxMin.Text = "1";
            this.textBoxMin.TextChanged += new EventHandler(this.textBox_TextChanged);
            this.textBoxMin.KeyPress += new KeyPressEventHandler(this.textBox_KeyPress);

            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(280, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Макс. длина стороны:";

            // textBoxMax
            this.textBoxMax.Location = new System.Drawing.Point(400, 47);
            this.textBoxMax.Name = "textBoxMax";
            this.textBoxMax.Size = new System.Drawing.Size(80, 23);
            this.textBoxMax.TabIndex = 5;
            this.textBoxMax.Text = "20";
            this.textBoxMax.TextChanged += new EventHandler(this.textBox_TextChanged);
            this.textBoxMax.KeyPress += new KeyPressEventHandler(this.textBox_KeyPress);

            // buttonGenerate
            this.buttonGenerate.Location = new System.Drawing.Point(190, 80);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(150, 30);
            this.buttonGenerate.TabIndex = 6;
            this.buttonGenerate.Text = "Сгенерировать";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new EventHandler(this.buttonGenerate_Click);

            // dataGridView1
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.Add("colNumber", "№");
            this.dataGridView1.Columns.Add("colSides", "Стороны");
            this.dataGridView1.Columns.Add("colArea", "Площадь");
            this.dataGridView1.Columns.Add("colAngles", "Углы");
            this.dataGridView1.Columns.Add("colHeights", "Высоты");
            this.dataGridView1.Location = new System.Drawing.Point(20, 120);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(760, 250);
            this.dataGridView1.TabIndex = 7;

            // labelResult
            this.labelResult.AutoSize = true;
            this.labelResult.Location = new System.Drawing.Point(20, 380);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(0, 15);
            this.labelResult.TabIndex = 8;

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 420);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.textBoxMax);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxMin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Генератор треугольников (Вариант 9)";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}