using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false; // Кнопка заблокирована до ввода данных
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Чтение данных
            double x = double.Parse(textBox1.Text);
            double eps = double.Parse(textBox2.Text);

            // Проверка области определения (x > 0)
            if (x <= 0)
            {
                MessageBox.Show("x должен быть > 0", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Левая часть - математическая функция ln(x)
            double left = Math.Log(x);

            // Вычисление суммы ряда
            double sum = 0;
            double term;
            int n = 0;          // n в формуле (начинается с 0)
            int memberCount = 0; // счётчик просуммированных членов

            // Вычисляем a = (x-1)/(x+1)
            double a = (x - 1) / (x + 1);
            double a2 = a * a;   // a² для рекуррентной формулы

            // Первый член при n=0: (x-1)/(x+1)
            term = a;

            // Суммируем, пока текущий член больше точности
            while (Math.Abs(term) > eps)
            {
                sum += term;           // добавляем член к сумме
                memberCount++;          // увеличиваем счётчик членов
                n++;                    // переходим к следующему n

                // Рекуррентная формула для следующего члена:
                // term(n) = term(n-1) * a² * (2n-1)/(2n+1)
                term = term * a2 * (2 * n - 1) / (2 * n + 1);
            }

            // Умножаем всю сумму на 2 (по формуле)
            sum = 2 * sum;

            // Вывод результатов
            textBox3.Text = left.ToString("F10");
            textBox4.Text = sum.ToString("F10");
            textBox5.Text = memberCount.ToString();
        }

        // Проверка ввода - только цифры, запятая и минус
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                e.KeyChar != ',' && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // Разрешаем только один минус в начале
            if (e.KeyChar == '-' && ((TextBox)sender).Text.Length > 0)
                e.Handled = true;

            // Разрешаем только одну запятую
            if (e.KeyChar == ',' && ((TextBox)sender).Text.Contains(","))
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            if (e.KeyChar == ',' && ((TextBox)sender).Text.Contains(","))
                e.Handled = true;
        }

        // Активация кнопки при заполнении полей
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = !string.IsNullOrEmpty(textBox1.Text) &&
                             !string.IsNullOrEmpty(textBox2.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = !string.IsNullOrEmpty(textBox1.Text) &&
                             !string.IsNullOrEmpty(textBox2.Text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}