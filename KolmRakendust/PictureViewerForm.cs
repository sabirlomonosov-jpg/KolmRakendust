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

            Button btnRotate = new Button();
            btnRotate.Text = "Pööra";
            btnRotate.Left = 230;
            btnRotate.Top = 10;
            btnRotate.Width = 100;
            btnRotate.Click += BtnRotate_Click;

            Button btnClear = new Button();
            btnClear.Text = "Puhasta";
            btnClear.Left = 340;
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

            Controls.Add(btnOpen);
            Controls.Add(btnColor);
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