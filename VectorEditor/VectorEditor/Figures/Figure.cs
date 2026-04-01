using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using VectorEditor.Drawing;

namespace VectorEditor.Figures
{
    [Serializable]
    public abstract class Figure
    {
        public Rectangle Bounds { get; set; }
        public Stroke Stroke { get; set; }
        public Fill Fill { get; set; }
        public bool IsSelected { get; set; }

        public Figure()
        {
            Stroke = new Stroke();
            Fill = new Fill();
            IsSelected = false;
        }

        public abstract void Draw(System.Drawing.Graphics g);
        public abstract bool HitTest(Point point);
        public abstract Figure Clone();

        public virtual void MoveHorizontal(int delta)
        {
            Bounds = new Rectangle(Bounds.X + delta, Bounds.Y, Bounds.Width, Bounds.Height);
        }

        public virtual void MoveVertical(int delta)
        {
            Bounds = new Rectangle(Bounds.X, Bounds.Y + delta, Bounds.Width, Bounds.Height);
        }

        public virtual void Resize(int deltaWidth, int deltaHeight)
        {
            int w = Math.Max(10, Bounds.Width + deltaWidth);
            int h = Math.Max(10, Bounds.Height + deltaHeight);
            Bounds = new Rectangle(Bounds.X, Bounds.Y, w, h);
        }
    }
}