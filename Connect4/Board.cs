using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    class Board
    {
        public int N { get; set; }
        public int M { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Panel Panel { get; set; }
        private Circle[,] circles;
        private int circleWidth;
        private int circleHeight;
        private Graphics g;

        public Board(int n, int m, Panel panel)
        {
            N = n;
            M = m;
            Width = panel.Width;
            Height = panel.Height;
            this.Panel = panel;
            g = panel.CreateGraphics();
            circleWidth = panel.Width / M;
            circleHeight = panel.Height / N;
            circles = new Circle[N, M];
            for (int i = 0; i < N; ++i)
            {
                for (int j = 0; j < M; ++j)
                {
                    circles[i, j] = new Circle(i, j, circleWidth, circleHeight);
                }
            }
        }

        public int this[int i, int j]
        {
            get
            {
                return circles[i, j].State;
            }
            set
            {
                circles[i, j].State = value;
            }
        }

        public void printBoard()
        {
            Pen pen = new Pen(Color.Black, 2);
            g.Clear(Color.Blue);
            for (int i = 1; i <= M - 1; ++i)
            {
                g.DrawLine(pen, new Point(i * circleWidth, 0), new Point(i * circleWidth, Panel.Height));

            }
            for (int i = 0; i < N; ++i)
            {
                for (int j = 0; j < M; ++j)
                {
                    circles[i, j].Draw(g);
                }
            }
            pen.Dispose();
        }

        public void playerMove(Point point)
        {
            int choice = point.X / circleWidth;
            Console.WriteLine("Choice: " + choice); //DEBUGGING!
            for (int i = N - 1; i >= 0; i--)
            {
                if (circles[i, choice].State == 0)
                {
                    circles[i, choice].State = 1;
                    break;
                }
            }
        }

    }
}
