using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    public partial class Form1 : Form
    {
        System.Media.SoundPlayer startSoundPlayer = new System.Media.SoundPlayer(@"C:\Users\Martin\Music\pesna.wav");
        private Graphics g;
        Game game;
        public string FileName { get; set; }
        int time;

        public Form1(int N , int M, Color p1, Color p2)
        {
            InitializeComponent();
            game = new Game(N, M, 6, panel1.Height, panel1.Width, p1, p2);
            DoubleBuffered = true;
            g = panel1.CreateGraphics();
            FileName = "Untitled";
           // startSoundPlayer.Play();
            time = 30;
            timer1.Start();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            game.printBoard(g);
            Console.WriteLine("INVALIDATED!");
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (game.playerMove(e.Location)) {
                game.printBoard(game.board); // DEBUGGING
                Invalidate(true);
                int winner = game.checkWinner();
                if (winner == Game.PLAYER)
                {
                    timer1.Stop();
                    MessageBox.Show("Player wins!", "Winner!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                    Console.WriteLine("Player wins!");
                }
                else if (winner == Game.COMPUTER)
                {
                    timer1.Stop();
                    MessageBox.Show("Computer wins!", "Winner!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                    Console.WriteLine("Computer wins!");
                }
                game.computerMove();
                game.printBoard(game.board); // DEBUGGING
                Invalidate(true);
                winner = game.checkWinner();
                if (winner == Game.PLAYER)
                {
                    timer1.Stop();
                    MessageBox.Show("Player wins!", "Winner!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                    Console.WriteLine("Player wins!");
                }
                else if (winner == Game.COMPUTER)
                {
                    timer1.Stop();
                    MessageBox.Show("Computer wins!", "Winner!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                    Console.WriteLine("Computer wins!");
                } else if (winner == Game.DRAW)
                {
                    MessageBox.Show("The board is full!", "Full!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                    Console.WriteLine("Computer wins!");
                }
                time = 30;
                textBox1.Text = time.ToString();
            }
            else
            {
                MessageBox.Show("Invalid move! Make another choice!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void saveFile()
        {
            if (FileName == "Untitled")
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Circles doc file (*.crl)|*.crl";
                saveFileDialog.Title = "Save circles doc";
                saveFileDialog.FileName = FileName;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileName = saveFileDialog.FileName;
                }
            }
            if (FileName != null)
            {
                using (FileStream fileStream = new FileStream(FileName, FileMode.Create))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, game);
                }
            }
        }
        private void openFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Circles doc file (*.crl)|*.crl";
            openFileDialog.Title = "Open Circles doc file";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = openFileDialog.FileName;
                try
                {
                    using (FileStream fileStream = new FileStream(FileName, FileMode.Open))
                    {
                        IFormatter formater = new BinaryFormatter();
                        game = (Game)formater.Deserialize(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not read file: " + FileName);
                    FileName = null;
                    return;
                }
                Invalidate(true);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.undoMove();
            Invalidate(true);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (game != null)
            {
                panel1.Size = new Size(this.Size.Width - 36, this.Size.Height - 70);
                g = panel1.CreateGraphics();
                game.resize(panel1.Height, panel1.Width);
                Invalidate(true);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            time--;
            textBox1.Text = time.ToString();
            if (time == 0)
            {
                MessageBox.Show("Game Over!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rules rules = new Rules();
            this.Visible = false;
            DialogResult dr = rules.ShowDialog();
            if (dr == DialogResult.Cancel)
            {
                this.Visible = true;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartForm startForm = new StartForm();
            this.Visible = false;
            DialogResult dr = startForm.ShowDialog();
            if (dr == DialogResult.Cancel)
            {
                this.Visible = true;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileName = "Untitled";
            saveFile();
        }
    }
}
