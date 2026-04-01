using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using VectorEditor.Drawing;

namespace VectorEditor.Figures
{
    [Serializable]
    public class SquareFigure : Figure
    {
        public SquareFigure() { }

        public SquareFigure(Rectangle bounds)
        {
            int size = Math.Min(bounds.Width, bounds.Height);
            Bounds = new Rectangle(bounds.X, bounds.Y, size, size);
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            using (var pen = Stroke.CreatePen())
            {
                if (Fill.IsFilled)
                {
                    using (var brush = Fill.CreateBrush())
                    {
                        g.FillRectangle(brush, Bounds);
                    }
                }
                g.DrawRectangle(pen, Bounds);
            }
        }

        public override bool HitTest(Point point)
        {
            if (!Bounds.Contains(point)) return false;
            using (var pen = Stroke.CreatePen())
            using (var path = new GraphicsPath())
            {
                path.AddRectangle(Bounds);
                return path.IsOutlineVisible(point, pen);
            }
        }

        public override void Resize(int deltaWidth, int deltaHeight)
        {
            int delta = Math.Max(deltaWidth, deltaHeight);
            int newSize = Math.Max(10, Bounds.Width + delta);
            Bounds = new Rectangle(Bounds.X, Bounds.Y, newSize, newSize);
        }

        public override Figure Clone()
        {
            var clone = new SquareFigure(Bounds);
            clone.Stroke = Stroke.Clone();
            clone.Fill = Fill.Clone();
            return clone;
        }
    }
}