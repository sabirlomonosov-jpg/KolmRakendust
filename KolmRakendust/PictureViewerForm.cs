using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace KolmRakendust
{
    public class PictureViewerForm : Form
    {
        private PictureBox pictureBox;

        private Button openButton;
        private Button clearButton;
        private Button colorButton;
        private Button rotateButton;
        private Button saveButton;

        public PictureViewerForm()
        {
            Text = "Pildi vaatamise programm";

            Width = 900;
            Height = 700;

            CreateControls();
        }

        private void CreateControls()
        {
            pictureBox = new PictureBox();

            pictureBox.Left = 10;
            pictureBox.Top = 60;

            pictureBox.Width = 850;
            pictureBox.Height = 550;

            pictureBox.BorderStyle =
                BorderStyle.FixedSingle;

            pictureBox.SizeMode =
                PictureBoxSizeMode.StretchImage;

            Controls.Add(pictureBox);

            openButton = new Button();
            openButton.Text = "Ava pilt";
            openButton.Left = 10;
            openButton.Top = 10;
            openButton.Click += OpenButton_Click;

            Controls.Add(openButton);

            clearButton = new Button();
            clearButton.Text = "Puhasta";
            clearButton.Left = 100;
            clearButton.Top = 10;
            clearButton.Click += ClearButton_Click;

            Controls.Add(clearButton);

            colorButton = new Button();
            colorButton.Text = "Taust";
            colorButton.Left = 190;
            colorButton.Top = 10;
            colorButton.Click += ColorButton_Click;

            Controls.Add(colorButton);

            rotateButton = new Button();
            rotateButton.Text = "Pööra";
            rotateButton.Left = 280;
            rotateButton.Top = 10;
            rotateButton.Click += RotateButton_Click;

            Controls.Add(rotateButton);

            saveButton = new Button();
            saveButton.Text = "Salvesta";
            saveButton.Left = 370;
            saveButton.Top = 10;
            saveButton.Click += SaveButton_Click;

            Controls.Add(saveButton);
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog =
                new OpenFileDialog();

            dialog.Filter =
                "Images|*.jpg;*.jpeg;*.png;*.bmp";

            if (dialog.ShowDialog() ==
                DialogResult.OK)
            {
                pictureBox.Image =
                    Image.FromFile(
                        dialog.FileName);
            }
        }

        private void ClearButton_Click(
            object sender,
            EventArgs e)
        {
            pictureBox.Image = null;
        }

        private void ColorButton_Click(
            object sender,
            EventArgs e)
        {
            ColorDialog dialog =
                new ColorDialog();

            if (dialog.ShowDialog() ==
                DialogResult.OK)
            {
                pictureBox.BackColor =
                    dialog.Color;
            }
        }

        private void RotateButton_Click(
            object sender,
            EventArgs e)
        {
            if (pictureBox.Image == null)
                return;

            pictureBox.Image.RotateFlip(
                RotateFlipType.Rotate90FlipNone);

            pictureBox.Refresh();
        }

        private void SaveButton_Click(
            object sender,
            EventArgs e)
        {
            if (pictureBox.Image == null)
            {
                MessageBox.Show(
                    "Pilt puudub!");
                return;
            }

            SaveFileDialog dialog =
                new SaveFileDialog();

            dialog.Filter =
                "JPEG|*.jpg|PNG|*.png|BMP|*.bmp";

            if (dialog.ShowDialog() ==
                DialogResult.OK)
            {
                string ext =
                    System.IO.Path.GetExtension(
                        dialog.FileName)
                        .ToLower();

                if (ext == ".jpg")
                {
                    pictureBox.Image.Save(
                        dialog.FileName,
                        ImageFormat.Jpeg);
                }
                else if (ext == ".png")
                {
                    pictureBox.Image.Save(
                        dialog.FileName,
                        ImageFormat.Png);
                }
                else if (ext == ".bmp")
                {
                    pictureBox.Image.Save(
                        dialog.FileName,
                        ImageFormat.Bmp);
                }

                MessageBox.Show(
                    "Pilt salvestatud!");
            }
        }
    }
}