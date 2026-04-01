using System.Drawing;

namespace VectorEditor.Drawing
{
    public static class SelectionBorder
    {
        private const int MarkerSize = 7;

        public enum MarkerType
        {
            None, TopLeft, TopCenter, TopRight,
            MiddleLeft, MiddleRight,
            BottomLeft, BottomCenter, BottomRight
        }

        public static void Draw(System.Drawing.Graphics g, Rectangle bounds)
        {
            using (var pen = new Pen(Color.Blue, 2) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
            {
                g.DrawRectangle(pen, bounds);
            }

            // Рисуем маркеры
            DrawMarker(g, bounds.X - 3, bounds.Y - 3);
            DrawMarker(g, bounds.X + bounds.Width / 2 - 3, bounds.Y - 3);
            DrawMarker(g, bounds.X + bounds.Width - 3, bounds.Y - 3);
            DrawMarker(g, bounds.X - 3, bounds.Y + bounds.Height / 2 - 3);
            DrawMarker(g, bounds.X + bounds.Width - 3, bounds.Y + bounds.Height / 2 - 3);
            DrawMarker(g, bounds.X - 3, bounds.Y + bounds.Height - 3);
            DrawMarker(g, bounds.X + bounds.Width / 2 - 3, bounds.Y + bounds.Height - 3);
            DrawMarker(g, bounds.X + bounds.Width - 3, bounds.Y + bounds.Height - 3);
        }

        private static void DrawMarker(System.Drawing.Graphics g, int x, int y)
        {
            g.FillRectangle(Brushes.White, x, y, MarkerSize, MarkerSize);
            g.DrawRectangle(Pens.Blue, x, y, MarkerSize, MarkerSize);
        }

        public static MarkerType GetMarkerAtPoint(Rectangle bounds, Point point)
        {
            if (IsPointInRect(point, bounds.X - 3, bounds.Y - 3)) return MarkerType.TopLeft;
            if (IsPointInRect(point, bounds.X + bounds.Width / 2 - 3, bounds.Y - 3)) return MarkerType.TopCenter;
            if (IsPointInRect(point, bounds.X + bounds.Width - 3, bounds.Y - 3)) return MarkerType.TopRight;
            if (IsPointInRect(point, bounds.X - 3, bounds.Y + bounds.Height / 2 - 3)) return MarkerType.MiddleLeft;
            if (IsPointInRect(point, bounds.X + bounds.Width - 3, bounds.Y + bounds.Height / 2 - 3)) return MarkerType.MiddleRight;
            if (IsPointInRect(point, bounds.X - 3, bounds.Y + bounds.Height - 3)) return MarkerType.BottomLeft;
            if (IsPointInRect(point, bounds.X + bounds.Width / 2 - 3, bounds.Y + bounds.Height - 3)) return MarkerType.BottomCenter;
            if (IsPointInRect(point, bounds.X + bounds.Width - 3, bounds.Y + bounds.Height - 3)) return MarkerType.BottomRight;
            return MarkerType.None;
        }

        private static bool IsPointInRect(Point point, int x, int y)
        {
            return point.X >= x && point.X <= x + MarkerSize && point.Y >= y && point.Y <= y + MarkerSize;
        }

        public static Rectangle ApplyResize(Rectangle bounds, MarkerType marker, int dx, int dy)
        {
            var r = bounds;
            switch (marker)
            {
                case MarkerType.TopLeft:
                    r = new Rectangle(r.X + dx, r.Y + dy, r.Width - dx, r.Height - dy);
                    break;
                case MarkerType.TopCenter:
                    r = new Rectangle(r.X, r.Y + dy, r.Width, r.Height - dy);
                    break;
                case MarkerType.TopRight:
                    r = new Rectangle(r.X, r.Y + dy, r.Width + dx, r.Height - dy);
                    break;
                case MarkerType.MiddleLeft:
                    r = new Rectangle(r.X + dx, r.Y, r.Width - dx, r.Height);
                    break;
                case MarkerType.MiddleRight:
                    r = new Rectangle(r.X, r.Y, r.Width + dx, r.Height);
                    break;
                case MarkerType.BottomLeft:
                    r = new Rectangle(r.X + dx, r.Y, r.Width - dx, r.Height + dy);
                    break;
                case MarkerType.BottomCenter:
                    r = new Rectangle(r.X, r.Y, r.Width, r.Height + dy);
                    break;
                case MarkerType.BottomRight:
                    r = new Rectangle(r.X, r.Y, r.Width + dx, r.Height + dy);
                    break;
            }
            if (r.Width < 10) r.Width = 10;
            if (r.Height < 10) r.Height = 10;
            return r;
        }
    }
}