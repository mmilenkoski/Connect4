using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    public partial class Form1 : Form
    {
        public int N { get; set; }
        public int M { get; set; }
        private int circleWidth;
        private int circleHeight;
        private Graphics g;
        public int Radius { get; }

        Game game;

        public Form1()
        {
            InitializeComponent();
            game = new Game();
            DoubleBuffered = true;

            N = 5;
            M = 4;
            g = panel1.CreateGraphics();
            circleWidth = panel1.Width / M;
            circleHeight = panel1.Height / N;
            Radius = Math.Min(circleWidth, circleHeight) / 2;
            Radius -= 5;
        }

        public void printBoard(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 2);
            g.Clear(Color.Blue);
            for (int i = 1; i <= M - 1; ++i)
            {
                g.DrawLine(pen, new Point(i * circleWidth, 0), new Point(i * circleWidth, panel1.Height));

            }
           for (int i = 0; i < N; ++i)
            {
                for (int j = 0; j < M; ++j)
                {
                    Color color = Color.White;
                    if (game.board[i,j] == 1)
                        color = Color.Yellow;
                    if (game.board[i,j] == 2)
                        color = Color.Red;
                    Brush brush = new SolidBrush(color);
                    int x = (j * circleWidth) + (circleWidth / 2) - Radius;
                    int y = (i * circleHeight) + (circleHeight / 2) - Radius;
                    g.FillEllipse(brush, x, y, 2 * Radius, 2 * Radius);
                    brush.Dispose();
                }
            }
            pen.Dispose();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            printBoard(g);
            Console.WriteLine("INVALIDATED!");
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            int choice = e.X / circleWidth;
            Console.WriteLine("Choice: " + choice); //DEBUGGING!
            Game.playerMove(game.board, choice);
            Game.printBoard(game.board); // DEBUGGING
            Invalidate(true);
            int winner = Game.checkWinner(game.board);
            if (winner != -1)
            {
                if (winner == 1)
                {
                    MessageBox.Show("Player wins!", "Winner!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Console.WriteLine("Player wins!");
                }
                else
                {
                    MessageBox.Show("Computer wins!", "Winner!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Console.WriteLine("Computer wins!");
                }
            }
            Game.computerMove(game.board);
            Game.printBoard(game.board); // DEBUGGING
            Invalidate(true);
            winner = Game.checkWinner(game.board);
            if (winner != -1)
            {
                if (winner == 1)
                {
                    MessageBox.Show("Player wins!", "Winner!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Console.WriteLine("Player wins!");
                }
                else
                {
                    MessageBox.Show("Computer wins!", "Winner!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Console.WriteLine("Computer wins!");
                }
            }
        }
    }
}
