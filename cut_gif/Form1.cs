using GifMotion;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace cut_gif
{
    public partial class Form1 : Form
    {

        public int frames,const_frames;
        public int speed = 4;
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = speed;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 SI = new Form2(this,true);

            SI.ShowDialog();
            
            /* SaveFileDialog SFD = new SaveFileDialog();
             SFD.Filter = "PNG|*.png|JPEG|*.jpg|GIF|*.gif|BMP|*.bmp";
             if (SFD.ShowDialog() == DialogResult.OK)
             {
                 Form1.BM.Save(SFD.FileName);
             }*/
            //ScreenShot SI = new ScreenShot();

            //SI.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 SI = new Form2(this, false);

            SI.ShowDialog();
           
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public void set_pic(Image ss)
        {
            //pictureBox1.Image = ss;
            trackBar1.Maximum = frames-1;
            trackBar1.Minimum = 1;
            trackBar2.Maximum = frames;
            trackBar2.Minimum =  2;
            trackBar2.Value= frames;
            textBox1.Text = speed.ToString();
            timer1.Start();
        }
        int i = 1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = i.ToString();
            Thread.Sleep(20);
            pictureBox1.Image = Image.FromFile("frames\\test" + i.ToString() + ".png");
            i++;
            if(i > frames)
            {
                i = trackBar1.Value;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Value < trackBar2.Value)
            {
                i = trackBar1.Value;
            }
            else
            {
                trackBar1.Value = 1;
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if(trackBar2.Value > trackBar1.Value)
            {
                frames = trackBar2.Value;
            }
            else
            {
                trackBar1.Value = 1;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && int.Parse(textBox1.Text) > 0)
                {
               
                    speed = int.Parse(textBox1.Text);
                    timer1.Interval = speed;
               
                 }
            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
                Image[] imageFilePaths = new Image[frames];

                for (int i = trackBar1.Value; i < frames; i++)
                {
                    // imageFilePaths[i] = BM_mass[i];
                    imageFilePaths[i] = Image.FromFile("frames\\test" + i.ToString() + ".png");
                    //BM_mass[frames].Save(imageFilePaths[i], ImageFormat.Png);
                }
                GifCreator gifCreator = AnimatedGif.Create("awesomegif.gif", 33);
                for (int i = trackBar1.Value; i < frames; i++)
                {

                    // Add the image to gifEncoder with default Quality
                    gifCreator.AddFrame(imageFilePaths[i], 33, GIFQuality.Default);

                }
            StringCollection paths = new StringCollection();
            paths.Add(Environment.CurrentDirectory+"\\awesomegif.gif");
            Clipboard.SetFileDropList(paths);
            this.Close();

            }

            private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
