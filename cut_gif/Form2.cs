using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using GifMotion;


namespace cut_gif
{
  
    public partial class Form2 : Form
    {
        
        bool screen,rec;
        Form1 start;
        public Form2(Form1 start1, bool screen1)
        {
            InitializeComponent();
            start = start1;
            screen = screen1;
            this.WindowState = FormWindowState.Maximized;
            timer1.Interval = 25;
            timer3.Interval = 4;
        }

        
        private void Form2_Load(object sender, EventArgs e)
        {
            button1.Location = new Point(-1000, -1000);
           
        }
        Graphics area;
        Point startpoint = new Point(0, 0);
        int Rect_width = 0;
        int Rect_height = 0;

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            area = CreateGraphics();
           
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            button1.Location = new Point(-1000, -1000);
            button2.Location = new Point(-1000, -1000);
            area.Clear(Color.Black);
            startpoint.X = Control.MousePosition.X;
            startpoint.Y = Control.MousePosition.Y;
            timer1.Start();
           
        }
        
        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {

            
            timer1.Stop();


            button1.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
            if (!screen)
            {
                button2.Location = new Point(Control.MousePosition.X + button2.Width, Control.MousePosition.Y);
            }
        }
       
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            Rect_width = Control.MousePosition.X - startpoint.X;
            Rect_height = Control.MousePosition.Y - startpoint.Y;
            area.Clear(Color.Black);
           
            area.FillRectangle(Brushes.White, startpoint.X+5, startpoint.Y-20, Rect_width, Rect_height);
        }

        public static Bitmap BM = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        public static Bitmap[] BM_mass = new Bitmap[1000];
        private void button1_Click(object sender, EventArgs e)
        {
            if (screen)
            {
                BM = new Bitmap(Rect_width, Rect_height);
                Graphics GH = Graphics.FromImage(BM as Image);
                GH.CopyFromScreen(startpoint.X + 5, startpoint.Y + 3, 0, 0, BM.Size);

                SaveFileDialog SFD = new SaveFileDialog();
                SFD.Filter = "PNG|*.png|JPEG|*.jpg|GIF|*.gif|BMP|*.bmp";
                if (SFD.ShowDialog() == DialogResult.OK)
                {
                    Form2.BM.Save(SFD.FileName);
                }
                this.Close();
            }
            else
            {
                if (!rec)
                {
                    button1.BackColor = Color.Red;
                    rec = true;
                    BM = new Bitmap(Rect_width, Rect_height);
                    timer3.Start();
                }
                else
                {
                    rec = false;
                    timer3.Stop();
                    SaveGif();
                }
               
            }
            //ScreenShot SI = new ScreenShot();
            //SI.ShowDialog();
        }

        void SaveGif()
        {
            
         /*   Image[] imageFilePaths = new Image[frames] ;
          
            for (int i = 1; i < frames; i++)
            {
                // imageFilePaths[i] = BM_mass[i];
                imageFilePaths[i] = Image.FromFile("test" + i.ToString() + ".png");
                //BM_mass[frames].Save(imageFilePaths[i], ImageFormat.Png);
            }
            GifCreator gifCreator = AnimatedGif.Create("awesomegif.gif", 33);
            for(int i = 1; i < frames; i++)
            {
               
                    // Add the image to gifEncoder with default Quality
                    gifCreator.AddFrame(imageFilePaths[i], 33, GIFQuality.Default);
               
            }
            */

            start.frames = frames;
            start.const_frames = frames;
            frames = 0;
            
            start.set_pic(Image.FromFile("frames\\test1.png"));
            start.Show();
            this.Close();
        }
        int frames = 0;
     
    
        private void timer3_Tick(object sender, EventArgs e)
        {
            frames++;
            Graphics GH = Graphics.FromImage(BM as Image);
            GH.CopyFromScreen(startpoint.X + 5, startpoint.Y + 3, 0, 0, BM.Size);

            //BM_mass[frames].Save(ss, ImageFormat.Png);
            try
            {
                BM.Save("frames\\test" + frames.ToString() + ".png", ImageFormat.Png);
            }
            catch
            {

            }
            if (frames > 1000)
            {
                timer3.Stop();
                SaveGif();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
