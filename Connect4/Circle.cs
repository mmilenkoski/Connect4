using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{

    class Circle
    {
        public int State { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Radius { get; }
        public int x { get; set; }
        public int y { get; set; }
        private Color color;

        public Circle(int i, int j, int width, int height)
        {
            State = 0;
            Width = width;
            Height = height;
            Radius = Math.Min(Width, Height) / 2;
            Radius -= 5;
            x = (j * width) + (width / 2) - Radius;
            y = (i * height) + (height / 2) - Radius;
            color = Color.White;
        }

        public void Draw(Graphics g)
        {
            validateColor();
            Brush brush = new SolidBrush(color);
            g.FillEllipse(brush, x, y, 2 * Radius, 2 * Radius);
            brush.Dispose();
        }

        private void validateColor()
        {
            if (State == 1)
                color = Color.Yellow;
            if (State == 2)
                color = Color.Red;
        }
    }
}
