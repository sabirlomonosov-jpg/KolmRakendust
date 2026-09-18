using System.Windows.Forms;

namespace KolmRakendust
{
    public class MainForm : Form
    {
        public MainForm()
        {
            Text = "Kolm rakendust";
            Width = 400;
            Height = 300;

            Button btnPicture = new Button();
            btnPicture.Text = "Pildi vaatamine";
            btnPicture.Left = 100;
            btnPicture.Top = 40;
            btnPicture.Width = 180;

            btnPicture.Click += BtnPicture_Click;

            Button btnMath = new Button();
            btnMath.Text = "Matemaatika mäng";
            btnMath.Left = 100;
            btnMath.Top = 100;
            btnMath.Width = 180;

            //btnMath.Click += BtnMath_Click;

            Button btnMatch = new Button();
            btnMatch.Text = "Paaride mäng";
            btnMatch.Left = 100;
            btnMatch.Top = 160;
            btnMatch.Width = 180;

            //btnMatch.Click += BtnMatch_Click;

            Controls.Add(btnPicture);
            Controls.Add(btnMath);
            Controls.Add(btnMatch);
        }

        private void BtnPicture_Click(object sender, System.EventArgs e)
        {
            PictureViewerForm form = new PictureViewerForm();
            form.ShowDialog();
        }

        //private void BtnMath_Click(object sender, System.EventArgs e)
        //{
        //    MathQuizForm form = new MathQuizForm();
        //    form.ShowDialog();
        //}

        //private void BtnMatch_Click(object sender, System.EventArgs e)
        //{
        //    MatchingGameForm form = new MatchingGameForm();
        //    form.ShowDialog();
        //}
    }
}