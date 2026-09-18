using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KolmRakendust
{
    public class PictureViewerForm : Form
    {
        private PictureBox pictureBox;

        private Timer slideTimer;

        private string[] imageFiles;

        private int currentImage = 0;

        public PictureViewerForm()
        {
            Text = "Pildi vaatamise programm";
            Width = 900;
            Height = 700;

            Button btnOpen = new Button();
            btnOpen.Text = "Ava pilt";
            btnOpen.Left = 10;
            btnOpen.Top = 10;
            btnOpen.Width = 100;
            btnOpen.Click += BtnOpen_Click;

            Button btnColor = new Button();
            btnColor.Text = "Taustavärv";
            btnColor.Left = 120;
            btnColor.Top = 10;
            btnColor.Width = 100;
            btnColor.Click += BtnColor_Click;

            Button btnSlideShow = new Button();
            btnSlideShow.Text = "Slideshow";
            btnSlideShow.Left = 230;
            btnSlideShow.Top = 10;
            btnSlideShow.Width = 100;
            btnSlideShow.Click += BtnSlideShow_Click;

            Button btnRotate = new Button();
            btnRotate.Text = "Pööra";
            btnRotate.Left = 340;
            btnRotate.Top = 10;
            btnRotate.Width = 100;
            btnRotate.Click += BtnRotate_Click;

            Button btnClear = new Button();
            btnClear.Text = "Puhasta";
            btnClear.Left = 450;
            btnClear.Top = 10;
            btnClear.Width = 100;
            btnClear.Click += BtnClear_Click;

            pictureBox = new PictureBox();
            pictureBox.Left = 10;
            pictureBox.Top = 50;
            pictureBox.Width = 850;
            pictureBox.Height = 580;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            slideTimer = new Timer();
            slideTimer.Interval = 2000;
            slideTimer.Tick += SlideTimer_Tick;

            Controls.Add(btnOpen);
            Controls.Add(btnColor);
            Controls.Add(btnSlideShow);
            Controls.Add(btnRotate);
            Controls.Add(btnClear);
            Controls.Add(pictureBox);
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter =
                "Images|*.jpg;*.jpeg;*.png;*.bmp";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Image =
                    Image.FromFile(dialog.FileName);
            }
        }

        private void BtnColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox.BackColor =
                    colorDialog.Color;
            }
        }

        private void BtnSlideShow_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder =
                new FolderBrowserDialog();

            if (folder.ShowDialog() == DialogResult.OK)
            {
                imageFiles = Directory.GetFiles(
                    folder.SelectedPath,
                    "*.jpg");

                if (imageFiles.Length > 0)
                {
                    currentImage = 0;

                    pictureBox.Image =
                        Image.FromFile(
                            imageFiles[currentImage]);

                    slideTimer.Start();

                    MessageBox.Show(
                        "Slideshow käivitatud!");
                }
                else
                {
                    MessageBox.Show(
                        "Kaustas ei ole JPG pilte!");
                }
            }
        }

        private void SlideTimer_Tick(
            object sender,
            EventArgs e)
        {
            if (imageFiles == null)
                return;

            if (imageFiles.Length == 0)
                return;

            currentImage++;

            if (currentImage >= imageFiles.Length)
            {
                currentImage = 0;
            }

            pictureBox.Image =
                Image.FromFile(
                    imageFiles[currentImage]);
        }

        private void BtnRotate_Click(
            object sender,
            EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.RotateFlip(
                    RotateFlipType.Rotate90FlipNone);

                pictureBox.Refresh();
            }
        }

        private void BtnClear_Click(
            object sender,
            EventArgs e)
        {
            pictureBox.Image = null;
        }
    }
}