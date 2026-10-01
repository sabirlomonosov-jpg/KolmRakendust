using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KolmRakendust
{
    public class MatchingGameForm : Form
    {
        private PictureBox firstBox = null;
        private PictureBox secondBox = null;

        private Timer hideTimer;
        private Timer gameTimer;

        private Label timeLabel;
        private ComboBox sizeBox;

        private int seconds = 0;

        public MatchingGameForm()
        {
            Text = "Sarnaste piltide mäng";
            Width = 900;
            Height = 850;

            CreateControls();
        }

        private void CreateControls()
        {
            Label sizeLabel = new Label();
            sizeLabel.Text = "Suurus:";
            sizeLabel.Left = 10;
            sizeLabel.Top = 15;
            sizeLabel.Width = 60;

            sizeBox = new ComboBox();
            sizeBox.Left = 80;
            sizeBox.Top = 10;
            sizeBox.Width = 100;

            sizeBox.Items.Add("4x4");
            sizeBox.Items.Add("6x6");
            sizeBox.Items.Add("8x8");

            sizeBox.SelectedIndex = 0;

            Button startButton = new Button();
            startButton.Text = "Alusta";
            startButton.Left = 200;
            startButton.Top = 10;
            startButton.Width = 100;
            startButton.Click += StartButton_Click;

            timeLabel = new Label();
            timeLabel.Text = "Aeg: 0 s";
            timeLabel.Left = 330;
            timeLabel.Top = 15;
            timeLabel.Width = 120;

            Controls.Add(sizeLabel);
            Controls.Add(sizeBox);
            Controls.Add(startButton);
            Controls.Add(timeLabel);

            hideTimer = new Timer();
            hideTimer.Interval = 800;
            hideTimer.Tick += HideTimer_Tick;

            gameTimer = new Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            List<Control> removeList = new List<Control>();

            foreach (Control c in Controls)
            {
                if (c is PictureBox)
                {
                    removeList.Add(c);
                }
            }

            foreach (Control c in removeList)
            {
                Controls.Remove(c);
            }

            firstBox = null;
            secondBox = null;

            int size = 4;

            if (sizeBox.Text == "6x6")
                size = 6;

            if (sizeBox.Text == "8x8")
                size = 8;

            CreateBoard(size);

            seconds = 0;
            timeLabel.Text = "Aeg: 0 s";

            gameTimer.Start();
        }

        private void CreateBoard(int size)
        {
            List<string> images = new List<string>();

            int pairs = (size * size) / 2;

            for (int i = 1; i <= pairs; i++)
            {
                string file = "../../Pictures/" + i + ".png";

                images.Add(file);
                images.Add(file);
            }

            Random rnd = new Random();

            int cardSize = 80;

            if (size == 6)
                cardSize = 70;

            if (size == 8)
                cardSize = 60;

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    int index = rnd.Next(images.Count);

                    PictureBox box = new PictureBox();

                    box.Width = cardSize;
                    box.Height = cardSize;

                    box.Left = 20 + col * (cardSize + 5);
                    box.Top = 60 + row * (cardSize + 5);

                    box.BorderStyle = BorderStyle.FixedSingle;
                    box.SizeMode = PictureBoxSizeMode.StretchImage;
                    box.BackColor = Color.LightGray;

                    box.Tag = images[index];

                    images.RemoveAt(index);

                    box.Click += Picture_Click;

                    Controls.Add(box);
                }
            }
        }

        private void Picture_Click(object sender, EventArgs e)
        {
            if (hideTimer.Enabled)
                return;

            PictureBox clicked = sender as PictureBox;

            if (clicked == null)
                return;

            if (clicked.Image != null)
                return;

            if (clicked == firstBox)
                return;

            string file = clicked.Tag.ToString();

            if (!File.Exists(file))
            {
                MessageBox.Show("Pilti ei leitud!");
                return;
            }

            clicked.Image = new Bitmap(file);

            if (firstBox == null)
            {
                firstBox = clicked;
                return;
            }

            secondBox = clicked;

            if (firstBox.Tag.ToString() ==
                secondBox.Tag.ToString())
            {
                firstBox = null;
                secondBox = null;

                CheckWin();
            }
            else
            {
                hideTimer.Start();
            }
        }

        private void HideTimer_Tick(object sender, EventArgs e)
        {
            hideTimer.Stop();

            if (firstBox != null)
            {
                if (firstBox.Image != null)
                {
                    firstBox.Image.Dispose();
                }

                firstBox.Image = null;
            }

            if (secondBox != null)
            {
                if (secondBox.Image != null)
                {
                    secondBox.Image.Dispose();
                }

                secondBox.Image = null;
            }

            firstBox = null;
            secondBox = null;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            seconds++;

            timeLabel.Text = "Aeg: " + seconds + " s";
        }

        private void CheckWin()
        {
            foreach (Control c in Controls)
            {
                PictureBox box = c as PictureBox;

                if (box != null)
                {
                    if (box.Image == null)
                        return;
                }
            }

            gameTimer.Stop();

            MessageBox.Show(
                "Võitsid!\nAeg: " +
                seconds +
                " sekundit");
        }
    }
}