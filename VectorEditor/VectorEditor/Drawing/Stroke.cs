using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace VectorEditor.Drawing
{
    [Serializable]
    public class Stroke
    {
        public Color Color { get; set; }
        public float Width { get; set; }
        public DashStyle DashStyle { get; set; }

        public Stroke()
        {
            Color = Color.Black;
            Width = 1f;
            DashStyle = DashStyle.Solid;
        }

        public Pen CreatePen()
        {
            return new Pen(Color, Width) { DashStyle = DashStyle };
        }

        public Stroke Clone()
        {
            return new Stroke { Color = this.Color, Width = this.Width, DashStyle = this.DashStyle };
        }
    }
}