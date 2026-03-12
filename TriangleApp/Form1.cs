using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TriangleApp;  // ДОБАВЛЕНО

namespace TriangleApp  // ИСПРАВЛЕНО
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            buttonGenerate.Enabled = false;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                // Чтение данных
                int count = int.Parse(textBoxCount.Text);
                int minSide = int.Parse(textBoxMin.Text);
                int maxSide = int.Parse(textBoxMax.Text);

                // Проверка данных
                if (count <= 0 || count > 100)
                {
                    MessageBox.Show("Количество треугольников должно быть от 1 до 100", "Ошибка");
                    return;
                }

                if (minSide <= 0 || maxSide <= 0 || minSide > maxSide)
                {
                    MessageBox.Show("Некорректный диапазон сторон", "Ошибка");
                    return;
                }

                // Генерация треугольников
                List<int[]> triangles = TriangleCalculator.GenerateRandomTriangles(count, minSide, maxSide);

                if (triangles.Count == 0)
                {
                    MessageBox.Show("Не удалось сгенерировать ни одного треугольника", "Ошибка");
                    return;
                }

                // Вывод в DataGridView
                dataGridView1.Rows.Clear();
                for (int i = 0; i < triangles.Count; i++)
                {
                    int a = triangles[i][0];
                    int b = triangles[i][1];
                    int c = triangles[i][2];

                    double area = TriangleCalculator.GetArea(a, b, c);
                    double[] angles = TriangleCalculator.GetAngles(a, b, c);
                    double[] heights = TriangleCalculator.GetHeights(a, b, c);

                    dataGridView1.Rows.Add(
                        i + 1,
                        $"{a}, {b}, {c}",
                        area.ToString("F4"),
                        $"{angles[0]:F1}°, {angles[1]:F1}°, {angles[2]:F1}°",
                        $"h_a={heights[0]:F2}, h_b={heights[1]:F2}, h_c={heights[2]:F2}"
                    );
                }

                // Сохранение в файл
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                saveDialog.DefaultExt = "txt";
                saveDialog.FileName = $"triangles_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    TriangleCalculator.SaveResultsToFile(saveDialog.FileName, triangles);
                    MessageBox.Show($"Результаты сохранены в файл:\n{saveDialog.FileName}", "Успешно");
                }

                labelResult.Text = $"Сгенерировано треугольников: {triangles.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            buttonGenerate.Enabled = !string.IsNullOrEmpty(textBoxCount.Text) &&
                                     !string.IsNullOrEmpty(textBoxMin.Text) &&
                                     !string.IsNullOrEmpty(textBoxMax.Text);
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}