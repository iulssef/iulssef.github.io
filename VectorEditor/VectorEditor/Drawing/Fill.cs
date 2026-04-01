using System;
using System.Drawing;

namespace VectorEditor.Drawing
{
    [Serializable]
    public class Fill
    {
        public Color Color { get; set; }
        public bool IsFilled { get; set; }

        public Fill()
        {
            Color = Color.White;
            IsFilled = false;
        }

        public Brush CreateBrush()
        {
            if (!IsFilled) return null;
            return new SolidBrush(Color);
        }

        public Fill Clone()
        {
            return new Fill { Color = this.Color, IsFilled = this.IsFilled };
        }
    }
}