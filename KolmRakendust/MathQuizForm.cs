using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KolmRakendust
{
    public class MathQuizForm : Form
    {
        private Label lblTime;
        private Label lblScore;

        private Label plusLeft;
        private Label plusRight;

        private Label minusLeft;
        private Label minusRight;

        private Label multiplyLeft;
        private Label multiplyRight;

        private Label divideLeft;
        private Label divideRight;

        private NumericUpDown sumAnswer;
        private NumericUpDown minusAnswer;
        private NumericUpDown multiplyAnswer;
        private NumericUpDown divideAnswer;

        private Button startButton;

        private ComboBox difficultyBox;

        private Timer timer;

        private Random random = new Random();

        private int timeLeft;
        private int score = 0;
        private int maxNumber = 50;

        public MathQuizForm()
        {
            Text = "Matemaatiline mäng";
            Width = 450;
            Height = 380;
            StartPosition = FormStartPosition.CenterScreen;

            CreateControls();
        }

        private void CreateControls()
        {
            Label difficultyLabel = new Label();
            difficultyLabel.Text = "Raskus:";
            difficultyLabel.Left = 20;
            difficultyLabel.Top = 20;
            difficultyLabel.Width = 60;

            difficultyBox = new ComboBox();
            difficultyBox.Left = 90;
            difficultyBox.Top = 18;
            difficultyBox.Width = 120;

            difficultyBox.Items.Add("Kerge");
            difficultyBox.Items.Add("Keskmine");
            difficultyBox.Items.Add("Raske");

            difficultyBox.SelectedIndex = 1;

            lblScore = new Label();
            lblScore.Text = "Punktid: 0";
            lblScore.Left = 250;
            lblScore.Top = 20;
            lblScore.Width = 120;

            Controls.Add(difficultyLabel);
            Controls.Add(difficultyBox);
            Controls.Add(lblScore);

            Label title = new Label();
            title.Text = "Järelejäänud aeg:";
            title.Left = 110;
            title.Top = 55;
            title.Width = 100;

            lblTime = new Label();
            lblTime.Text = "0";
            lblTime.Left = 210;
            lblTime.Top = 55;
            lblTime.Width = 100;
            lblTime.BorderStyle = BorderStyle.FixedSingle;

            Controls.Add(title);
            Controls.Add(lblTime);

            plusLeft = CreateNumberLabel(50, 100);
            plusRight = CreateNumberLabel(150, 100);

            Controls.Add(CreateSign("+", 105, 100));
            Controls.Add(CreateSign("=", 220, 100));

            sumAnswer = CreateAnswerBox(270, 100);

            minusLeft = CreateNumberLabel(50, 145);
            minusRight = CreateNumberLabel(150, 145);

            Controls.Add(CreateSign("-", 105, 145));
            Controls.Add(CreateSign("=", 220, 145));

            minusAnswer = CreateAnswerBox(270, 145);

            multiplyLeft = CreateNumberLabel(50, 190);
            multiplyRight = CreateNumberLabel(150, 190);

            Controls.Add(CreateSign("×", 105, 190));
            Controls.Add(CreateSign("=", 220, 190));

            multiplyAnswer = CreateAnswerBox(270, 190);

            divideLeft = CreateNumberLabel(50, 235);
            divideRight = CreateNumberLabel(150, 235);

            Controls.Add(CreateSign("÷", 105, 235));
            Controls.Add(CreateSign("=", 220, 235));

            divideAnswer = CreateAnswerBox(270, 235);

            Controls.Add(plusLeft);
            Controls.Add(plusRight);

            Controls.Add(minusLeft);
            Controls.Add(minusRight);

            Controls.Add(multiplyLeft);
            Controls.Add(multiplyRight);

            Controls.Add(divideLeft);
            Controls.Add(divideRight);

            Controls.Add(sumAnswer);
            Controls.Add(minusAnswer);
            Controls.Add(multiplyAnswer);
            Controls.Add(divideAnswer);

            startButton = new Button();
            startButton.Text = "Algus";
            startButton.Left = 150;
            startButton.Top = 290;
            startButton.Width = 120;
            startButton.Height = 35;
            startButton.Click += StartButton_Click;

            Controls.Add(startButton);

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
        }

        private Label CreateNumberLabel(int x, int y)
        {
            Label lbl = new Label();

            lbl.Left = x;
            lbl.Top = y;
            lbl.Width = 60;

            lbl.Font =
                new Font(
                    "Arial",
                    18,
                    FontStyle.Bold);

            return lbl;
        }

        private Label CreateSign(string text, int x, int y)
        {
            Label lbl = new Label();

            lbl.Text = text;
            lbl.Left = x;
            lbl.Top = y;
            lbl.Width = 40;

            lbl.Font =
                new Font(
                    "Arial",
                    18,
                    FontStyle.Bold);

            return lbl;
        }

        private NumericUpDown CreateAnswerBox(int x, int y)
        {
            NumericUpDown box =
                new NumericUpDown();

            box.Left = x;
            box.Top = y;
            box.Width = 80;
            box.Maximum = 10000;

            return box;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (difficultyBox.Text == "Kerge")
            {
                maxNumber = 20;
            }
            else if (difficultyBox.Text == "Keskmine")
            {
                maxNumber = 50;
            }
            else
            {
                maxNumber = 100;
            }

            GenerateQuestions();

            sumAnswer.Value = 0;
            minusAnswer.Value = 0;
            multiplyAnswer.Value = 0;
            divideAnswer.Value = 0;

            timeLeft = 30;
            lblTime.Text = "30 sekundid";

            timer.Start();
        }

        private void GenerateQuestions()
        {
            int a;
            int b;

            a = random.Next(1, maxNumber);
            b = random.Next(1, maxNumber);

            plusLeft.Text = a.ToString();
            plusRight.Text = b.ToString();

            a = random.Next(1, maxNumber);
            b = random.Next(1, a);

            minusLeft.Text = a.ToString();
            minusRight.Text = b.ToString();

            a = random.Next(2, 11);
            b = random.Next(2, 11);

            multiplyLeft.Text = a.ToString();
            multiplyRight.Text = b.ToString();

            b = random.Next(2, 11);

            int result =
                random.Next(2, 11);

            divideLeft.Text =
                (b * result).ToString();

            divideRight.Text =
                b.ToString();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (CheckAnswers())
            {
                timer.Stop();

                score += 10;

                lblScore.Text =
                    "Punktid: " + score;

                SaveResult();

                MessageBox.Show(
                    "Õige! Punktid: " + score);

                return;
            }

            if (timeLeft > 0)
            {
                timeLeft--;

                lblTime.Text =
                    timeLeft + " sekundid";
            }
            else
            {
                timer.Stop();

                lblTime.Text =
                    "Aeg sai otsa!";

                MessageBox.Show(
                    "Aeg sai otsa!");
            }
        }

        private bool CheckAnswers()
        {
            return
                (int)sumAnswer.Value ==
                int.Parse(plusLeft.Text) +
                int.Parse(plusRight.Text)

                &&

                (int)minusAnswer.Value ==
                int.Parse(minusLeft.Text) -
                int.Parse(minusRight.Text)

                &&

                (int)multiplyAnswer.Value ==
                int.Parse(multiplyLeft.Text) *
                int.Parse(multiplyRight.Text)

                &&

                (int)divideAnswer.Value ==
                int.Parse(divideLeft.Text) /
                int.Parse(divideRight.Text);
        }

        private void SaveResult()
        {
            File.AppendAllText(
                "results.txt",
                DateTime.Now +
                " Punktid: " +
                score +
                Environment.NewLine);
        }
    }
}