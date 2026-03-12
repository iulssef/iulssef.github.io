using System;
using System.Collections.Generic;
using System.IO;


namespace TriangleApp
{
    /// <summary>
    /// Класс для работы с треугольниками
    /// Все методы статические - вызываются через имя класса
    /// </summary>
    public static class TriangleCalculator
    {
        /// <summary>
        /// Проверяет, можно ли построить треугольник со сторонами a, b, c
        /// </summary>
        /// <param name="a">первая сторона</param>
        /// <param name="b">вторая сторона</param>
        /// <param name="c">третья сторона</param>
        /// <returns>true - треугольник существует, false - не существует</returns>
        public static bool CanExist(double a, double b, double c)
        {
            // Проверка на положительность сторон
            if (a <= 0 || b <= 0 || c <= 0)
                return false;

            // Неравенство треугольника: каждая сторона меньше суммы двух других
            return a < b + c && b < a + c && c < a + b;
        }

        /// <summary>
        /// Вычисляет площадь треугольника по формуле Герона
        /// </summary>
        /// <param name="a">первая сторона</param>
        /// <param name="b">вторая сторона</param>
        /// <param name="c">третья сторона</param>
        /// <returns>площадь треугольника</returns>
        /// <exception cref="ArgumentException">Если треугольник не существует</exception>
        public static double GetArea(double a, double b, double c)
        {
            if (!CanExist(a, b, c))
                throw new ArgumentException("Треугольник с такими сторонами не существует");

            double p = (a + b + c) / 2; // полупериметр
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        /// <summary>
        /// Вычисляет углы треугольника в градусах
        /// </summary>
        /// <param name="a">первая сторона</param>
        /// <param name="b">вторая сторона</param>
        /// <param name="c">третья сторона</param>
        /// <returns>массив из трех углов [угол A, угол B, угол C] в градусах</returns>
        /// <exception cref="ArgumentException">Если треугольник не существует</exception>
        public static double[] GetAngles(double a, double b, double c)
        {
            if (!CanExist(a, b, c))
                throw new ArgumentException("Треугольник с такими сторонами не существует");

            double[] angles = new double[3];

            // Теорема косинусов: cos(A) = (b² + c² - a²) / (2bc)
            angles[0] = Math.Acos((b * b + c * c - a * a) / (2 * b * c)) * 180 / Math.PI;
            angles[1] = Math.Acos((a * a + c * c - b * b) / (2 * a * c)) * 180 / Math.PI;
            angles[2] = 180 - angles[0] - angles[1]; // сумма углов = 180°

            return angles;
        }

        /// <summary>
        /// Вычисляет высоты треугольника
        /// </summary>
        /// <param name="a">первая сторона</param>
        /// <param name="b">вторая сторона</param>
        /// <param name="c">третья сторона</param>
        /// <returns>массив из трех высот [h_a, h_b, h_c]</returns>
        /// <exception cref="ArgumentException">Если треугольник не существует</exception>
        public static double[] GetHeights(double a, double b, double c)
        {
            if (!CanExist(a, b, c))
                throw new ArgumentException("Треугольник с такими сторонами не существует");

            double area = GetArea(a, b, c);
            double[] heights = new double[3];

            // h = 2S / a
            heights[0] = 2 * area / a;
            heights[1] = 2 * area / b;
            heights[2] = 2 * area / c;

            return heights;
        }

        /// <summary>
        /// Генерирует N случайных треугольников с целыми сторонами
        /// </summary>
        /// <param name="count">количество треугольников</param>
        /// <param name="minSide">минимальная длина стороны</param>
        /// <param name="maxSide">максимальная длина стороны</param>
        /// <returns>список треугольников (каждый треугольник - массив из трех сторон)</returns>
        public static List<int[]> GenerateRandomTriangles(int count, int minSide = 1, int maxSide = 20)
        {
            Random rnd = new Random();
            List<int[]> triangles = new List<int[]>();
            int attempts = 0;

            while (triangles.Count < count && attempts < count * 100)
            {
                int a = rnd.Next(minSide, maxSide + 1);
                int b = rnd.Next(minSide, maxSide + 1);
                int c = rnd.Next(minSide, maxSide + 1);

                if (CanExist(a, b, c))
                {
                    triangles.Add(new int[] { a, b, c });
                }
                attempts++;
            }

            return triangles;
        }

        /// <summary>
        /// Сохраняет результаты в файл
        /// </summary>
        /// <param name="filename">имя файла</param>
        /// <param name="triangles">список треугольников</param>
        public static void SaveResultsToFile(string filename, List<int[]> triangles)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine("Треугольники и их характеристики");
                writer.WriteLine("=".PadRight(60, '='));
                writer.WriteLine();

                for (int i = 0; i < triangles.Count; i++)
                {
                    int a = triangles[i][0];
                    int b = triangles[i][1];
                    int c = triangles[i][2];

                    double area = GetArea(a, b, c);
                    double[] angles = GetAngles(a, b, c);
                    double[] heights = GetHeights(a, b, c);

                    writer.WriteLine($"Треугольник #{i + 1}: стороны {a}, {b}, {c}");
                    writer.WriteLine($"  Площадь: {area:F4}");
                    writer.WriteLine($"  Углы: A = {angles[0]:F2}°, B = {angles[1]:F2}°, C = {angles[2]:F2}°");
                    writer.WriteLine($"  Высоты: h_a = {heights[0]:F4}, h_b = {heights[1]:F4}, h_c = {heights[2]:F4}");
                    writer.WriteLine();
                }
            }
        }
    }
}