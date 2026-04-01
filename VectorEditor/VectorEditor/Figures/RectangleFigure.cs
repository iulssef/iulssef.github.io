using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using VectorEditor.Drawing;

namespace VectorEditor.Figures
{
    [Serializable]
    public class RectangleFigure : Figure
    {
        public RectangleFigure() { }

        public RectangleFigure(Rectangle bounds)
        {
            Bounds = bounds;
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

        public override Figure Clone()
        {
            var clone = new RectangleFigure(Bounds);
            clone.Stroke = Stroke.Clone();
            clone.Fill = Fill.Clone();
            return clone;
        }
    }
}