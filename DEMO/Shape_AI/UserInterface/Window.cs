using System.Diagnostics;

namespace Shape_AI
{
    internal class Window : Form
    {
        public Window()
        {
            this.Width = 600; this.Height = 600;
            this.CenterToScreen();
            this.Text = "Shape AI - Tobias Spilker - MiT LICENSE";

            this.Panel1();
            this.Panel2();
            this.Panel3();
        }

        public Bitmap[] BitmapRetrievers()
        {
            DataPrep dataPrep = new DataPrep();

            Bitmap[] RetrievedBitmaps = new Bitmap[3];

            RetrievedBitmaps[0] = dataPrep.Initializer();
            RetrievedBitmaps[1] = dataPrep.Compressor(RetrievedBitmaps[0], 100, 1);

            return RetrievedBitmaps;
        }

        public void Panel1()
        //Shows the unchanged picture (will later be the user's input)
        {
            //Creates the panel (first one):
            Panel panel1 = new Panel();
            panel1.Size = new Size(150, 150);
            panel1.Location = new Point(30, 150);
            panel1.BorderStyle = BorderStyle.FixedSingle;

            //Retrieves the array of bitmaps to pick the right one for the specific panel:
            Bitmap[] RetrievedBitmaps = BitmapRetrievers();

            //Creates the Picturebox to which the bitmap will be fitted:
            PictureBox PictureBox1 = new PictureBox();
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox1.Image = RetrievedBitmaps[0];
            PictureBox1.Size = new Size(panel1.Width, panel1.Height);
            panel1.Controls.Add(PictureBox1);

            Controls.Add(panel1);
        }

        public void Panel2()
        //Shows the true black and white picture
        {
            //Creates the panel (second one):
            Panel panel2 = new Panel();
            panel2.Size = new Size(150, 150);
            panel2.Location = new Point(210, 150);
            panel2.BorderStyle = BorderStyle.FixedSingle;

            //Retrieves the array of bitmaps to pick the right one for the specific panel:
            Bitmap[] RetrievedBitmaps = BitmapRetrievers();

            //Creates the Picturebox to which the bitmap will be fitted:
            PictureBox PictureBox2 = new PictureBox();
            PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBox2.Image = RetrievedBitmaps[1];
            PictureBox2.Size = new Size(panel2.Width, panel2.Height);
            panel2.Controls.Add(PictureBox2);

            Controls.Add(panel2);
        }

        public void Panel3()
        //Shows (part of) the encoded string
        {
            //Creates the panel (third one):
            Panel panel3 = new Panel();
            panel3.Size = new Size(150, 150);
            panel3.Location = new Point(390, 150);
            panel3.BackColor = Color.White;
            panel3.BorderStyle = BorderStyle.FixedSingle;

            //Creates the streamreader object with file directory:
            string TextFilepath = @"C:\Users\Tobias\Desktop\Shape_AI\Shape_AI\Data\EncodedData\EncodedData.txt";
            StreamReader streamreader = new StreamReader(TextFilepath);

            //Creates the text field:
            Label label = new Label();
            label.Size = new Size(150, 150);
            label.ForeColor = Color.Black;
            label.Text += streamreader.ReadLine() + @"  [\N]" + streamreader.ReadLine();
            streamreader.Close();
            panel3.Controls.Add(label);

            Controls.Add(panel3);
        }

    }
}
