using Microsoft.VisualStudio.TestTools.UnitTesting;
using TriangleApp;
using System;

namespace TriangleTests
{
    [TestClass]
    public class UnitTest1
    {
        // ============ ТЕСТЫ НА СУЩЕСТВОВАНИЕ ТРЕУГОЛЬНИКА ============

        [TestMethod]
        public void CanExist_ValidTriangle_ReturnsTrue()
        {
            // Допустимый класс: правильный треугольник
            bool result = TriangleCalculator.CanExist(3, 4, 5);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanExist_ZeroSide_ReturnsFalse()
        {
            // Недопустимый класс: сторона = 0
            bool result = TriangleCalculator.CanExist(0, 4, 5);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CanExist_NegativeSide_ReturnsFalse()
        {
            // Недопустимый класс: отрицательная сторона
            bool result = TriangleCalculator.CanExist(-3, 4, 5);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CanExist_TooLongSide_ReturnsFalse()
        {
            // Недопустимый класс: нарушение неравенства треугольника
            bool result = TriangleCalculator.CanExist(10, 4, 5);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CanExist_EqualSides_ReturnsTrue()
        {
            // Допустимый класс: равносторонний треугольник
            bool result = TriangleCalculator.CanExist(5, 5, 5);
            Assert.IsTrue(result);
        }

        // ============ ТЕСТЫ НА ПЛОЩАДЬ ============

        [TestMethod]
        public void GetArea_ValidTriangle_ReturnsCorrectArea()
        {
            // Допустимый класс: треугольник 3-4-5 (площадь = 6)
            double area = TriangleCalculator.GetArea(3, 4, 5);
            Assert.AreEqual(6.0, area, 0.0001);
        }

        [TestMethod]
        public void GetArea_EquilateralTriangle_ReturnsCorrectArea()
        {
            // Допустимый класс: равносторонний треугольник со стороной 6
            // Формула: (√3/4) * a² = (1.732/4)*36 = 15.588
            double area = TriangleCalculator.GetArea(6, 6, 6);
            double expected = 15.58845727;
            Assert.AreEqual(expected, area, 0.0001);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetArea_InvalidTriangle_ThrowsException()
        {
            // Недопустимый класс: несуществующий треугольник
            TriangleCalculator.GetArea(10, 4, 5);
        }

        // ============ ТЕСТЫ НА УГЛЫ ============

        [TestMethod]
        public void GetAngles_ValidTriangle_SumIs180()
        {
            // Проверка суммы углов
            double[] angles = TriangleCalculator.GetAngles(3, 4, 5);
            double sum = angles[0] + angles[1] + angles[2];
            Assert.AreEqual(180.0, sum, 0.001);
        }

        [TestMethod]
        public void GetAngles_RightTriangle_ReturnsCorrectAngles()
        {
            // Прямоугольный треугольник 3-4-5: углы ≈ 36.87°, 53.13°, 90°
            double[] angles = TriangleCalculator.GetAngles(3, 4, 5);
            Assert.AreEqual(36.87, angles[0], 0.01);
            Assert.AreEqual(53.13, angles[1], 0.01);
            Assert.AreEqual(90.0, angles[2], 0.01);
        }

        [TestMethod]
        public void GetAngles_EquilateralTriangle_AllAngles60()
        {
            double[] angles = TriangleCalculator.GetAngles(5, 5, 5);
            Assert.AreEqual(60.0, angles[0], 0.001);
            Assert.AreEqual(60.0, angles[1], 0.001);
            Assert.AreEqual(60.0, angles[2], 0.001);
        }

        // ============ ТЕСТЫ НА ВЫСОТЫ ============

        [TestMethod]
        public void GetHeights_ValidTriangle_ReturnsCorrectHeights()
        {
            // Треугольник 3-4-5: высоты = 4, 3, 2.4
            double[] heights = TriangleCalculator.GetHeights(3, 4, 5);
            Assert.AreEqual(4.0, heights[0], 0.0001); // h_a
            Assert.AreEqual(3.0, heights[1], 0.0001); // h_b
            Assert.AreEqual(2.4, heights[2], 0.0001); // h_c
        }

        [TestMethod]
        public void GetHeights_AreaFormula_Consistent()
        {
            // Проверка связи: S = (a * h_a)/2
            double a = 3, b = 4, c = 5;
            double area = TriangleCalculator.GetArea(a, b, c);
            double[] heights = TriangleCalculator.GetHeights(a, b, c);
            
            Assert.AreEqual(area, (a * heights[0]) / 2, 0.0001);
            Assert.AreEqual(area, (b * heights[1]) / 2, 0.0001);
            Assert.AreEqual(area, (c * heights[2]) / 2, 0.0001);
        }

        // ============ ТЕСТЫ НА ГЕНЕРАЦИЮ ============

        [TestMethod]
        public void GenerateRandomTriangles_Count_ReturnsCorrectNumber()
        {
            var triangles = TriangleCalculator.GenerateRandomTriangles(5, 1, 10);И
            Assert.AreEqual(5, triangles.Count);
        }

        [TestMethod]
        public void GenerateRandomTriangles_AllTrianglesAreValid()
        {
            var triangles = TriangleCalculator.GenerateRandomTriangles(10, 1, 10);
            foreach (var t in triangles)
            {
                Assert.IsTrue(TriangleCalculator.CanExist(t[0], t[1], t[2]));
            }
        }
    }
}