using System.Drawing;
using VectorEditor.Figures;

namespace VectorEditor.Helpers
{
    public static class FigureFactory
    {
        public enum FigureType
        {
            Rectangle,
            Square,
            HollowRectangle,
            HollowSquare
        }

        public static Figure CreateFigure(FigureType type, Rectangle bounds)
        {
            switch (type)
            {
                case FigureType.Rectangle:
                    return new RectangleFigure(bounds);
                case FigureType.Square:
                    return new SquareFigure(bounds);
                case FigureType.HollowRectangle:
                    return new HollowRectangleFigure(bounds, false);
                case FigureType.HollowSquare:
                    return new HollowRectangleFigure(bounds, true);
                default:
                    return new RectangleFigure(bounds);
            }
        }

        public static string GetFigureTypeName(FigureType type)
        {
            switch (type)
            {
                case FigureType.Rectangle: return "Прямоугольник";
                case FigureType.Square: return "Квадрат";
                case FigureType.HollowRectangle: return "Прямоугольник с отверстием";
                case FigureType.HollowSquare: return "Квадрат с отверстием";
                default: return "Фигура";
            }
        }
    }
}