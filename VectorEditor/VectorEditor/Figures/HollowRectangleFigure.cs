using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using VectorEditor.Drawing;

namespace VectorEditor.Figures
{
    [Serializable]
    public class HollowRectangleFigure : Figure
    {
        public bool IsSquareHole { get; set; }
        public float HolePercent { get; set; }

        public HollowRectangleFigure()
        {
            IsSquareHole = true;
            HolePercent = 0.5f;
        }

        public HollowRectangleFigure(Rectangle bounds, bool isSquareHole = true)
        {
            Bounds = bounds;
            IsSquareHole = isSquareHole;
            HolePercent = 0.5f;
        }

        private Rectangle GetHoleBounds()
        {
            int holeW = (int)(Bounds.Width * HolePercent);
            int holeH = IsSquareHole ? holeW : (int)(Bounds.Height * HolePercent);
            int x = Bounds.X + (Bounds.Width - holeW) / 2;
            int y = Bounds.Y + (Bounds.Height - holeH) / 2;
            return new Rectangle(x, y, holeW, holeH);
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            using (var pen = Stroke.CreatePen())
            using (var path = new GraphicsPath())
            {
                path.AddRectangle(Bounds);
                path.AddRectangle(GetHoleBounds());

                if (Fill.IsFilled)
                {
                    using (var brush = Fill.CreateBrush())
                    {
                        g.FillPath(brush, path);
                    }
                }
                g.DrawPath(pen, path);
            }
        }

        public override bool HitTest(Point point)
        {
            if (!Bounds.Contains(point)) return false;
            if (GetHoleBounds().Contains(point)) return false;

            using (var pen = Stroke.CreatePen())
            using (var path = new GraphicsPath())
            {
                path.AddRectangle(Bounds);
                path.AddRectangle(GetHoleBounds());
                return path.IsOutlineVisible(point, pen);
            }
        }

        public void ResizeHole(int deltaPercent)
        {
            HolePercent += deltaPercent / 100f;
            HolePercent = Math.Max(0.1f, Math.Min(0.9f, HolePercent));
        }

        public override Figure Clone()
        {
            var clone = new HollowRectangleFigure(Bounds, IsSquareHole);
            clone.Stroke = Stroke.Clone();
            clone.Fill = Fill.Clone();
            clone.HolePercent = this.HolePercent;
            return clone;
        }
    }
}