using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace visualsort
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer beepTimer = new System.Windows.Forms.Timer();
        public Form1()
        {
            InitializeComponent();
            timer.Interval = 1;
            timer.Enabled = false;
            timer.Tick += new EventHandler(timer_Tick);
            beepTimer.Interval = 1;
            beepTimer.Enabled = false;
            beepTimer.Tick += new EventHandler(beepTimer_Tick);
            pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);
            colors = new SolidBrush[253];
            ints = new int[253];
            getGradient();
        }
        public int beepFr = 1000;
        private void beepTimer_Tick(object sender, EventArgs e)
        {
            Console.Beep(beepFr, 1);
            beepFr += 18;
            beepTimer.Enabled = false;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (mode==0)
            {
                Random rand = new Random();
                int newPlace = rand.Next(colors.Length);
                SolidBrush tempColor = colors[newPlace];
                int tempInt = ints[newPlace];
                colors[newPlace] = colors[iterator];
                ints[newPlace] = ints[iterator];
                colors[iterator] = tempColor;
                ints[iterator] = tempInt;
                if (iterator < colors.Length - 1)
                    iterator++;
                else
                {
                    timer.Enabled = false;
                }
                pictureBox1.Invalidate();
            }
            else if (mode == 1)
            {
                int i = 1;
                int j = ints.Length - 2;
                while (i < ints.Length)
                {
                    if (ints[i - 1] > ints[i])
                    {
                        int tempInt = ints[i];
                        ints[i] = ints[i - 1];
                        ints[i - 1] = tempInt;
                        SolidBrush tempColor = colors[i];
                        colors[i] = colors[i - 1];
                        colors[i - 1] = tempColor;
                    }
                    if (ints[j + 1] < ints[j])
                    {
                        int tempInt = ints[j];
                        ints[j] = ints[j + 1];
                        ints[j + 1] = tempInt;
                        SolidBrush tempColor = colors[j];
                        colors[j] = colors[j + 1];
                        colors[j + 1] = tempColor;
                    }
                    i++;
                    j--;
                    beepTimer.Enabled = true;
                    pictureBox1.Invalidate();
                }
                int m = 0;
                for (m = 0; m < ints.Length; m++)
                    if (m < ints.Length - 1 && ints[m] > ints[m + 1])
                        break;
                if (m == ints.Length)
                {
                    timer.Enabled = false;
                    beepFr = 1000;
                }
            }
            else
            {
                for (iterator = 1; iterator < ints.Length; iterator++)
                {
                    if (ints[iterator - 1] > ints[iterator])
                    {
                        SolidBrush tempColor = colors[iterator];
                        int tempInt = ints[iterator];

                        colors[iterator] = colors[iterator - 1];
                        ints[iterator] = ints[iterator - 1];

                        colors[iterator - 1] = tempColor;
                        ints[iterator - 1] = tempInt;
                    }
                    beepTimer.Enabled = true;
                    pictureBox1.Invalidate();
                }
                int m = 0;
                for (m = 0; m < ints.Length; m++)
                    if (m < ints.Length - 1 && ints[m] > ints[m + 1])
                        break;
                if (m == ints.Length)
                {
                    timer.Enabled = false;
                    beepFr = 1000;
                }
            }
        }

        public void getGradient()
        {
            char[] activeColors = new char[2];
            int r = 252;
            int g = 0;
            int b = 0;
            for (int i = 0; i <= 42; i++)
            {
                colors[i] = new SolidBrush(Color.FromArgb(r, g, b));
                g += 6;
            }
            g = 252;
            for (int i = 43; i <= 84; i++)
            {
                colors[i] = new SolidBrush(Color.FromArgb(r, g, b));
                r -= 6;
            }
            r = 0;
            for (int i = 85; i <= 126; i++)
            {
                colors[i] = new SolidBrush(Color.FromArgb(r, g, b));
                b += 6;
            }
            b = 252;
            for (int i = 127; i <= 168; i++)
            {
                colors[i] = new SolidBrush(Color.FromArgb(r, g, b));
                g -= 6;
            }
            g = 0;
            for (int i = 169; i <= 210; i++)
            {
                colors[i] = new SolidBrush(Color.FromArgb(r, g, b));
                r += 6;
            }
            r = 252;
            for (int i = 211; i <= 252; i++)
            {
                colors[i] = new SolidBrush(Color.FromArgb(r, g, b));
                b -= 6;
            }
            for (int i = 0; i < ints.Length; i++)
                ints[i] = i;
        }

        public static int count = 253;
        public float angleStep = 360f / count;
        public Random rand = new Random();
        public SolidBrush[] colors;
        public int[] ints;
        public int iterator = 0;
        public int mode = 0;

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int zoom = 200;
            e.Graphics.Clear(Color.Black);
            for (int i = 0; i < colors.Length; i++)
            {
                double angle = i * angleStep;
                double angle2;
                if (i < count - 1)
                    angle2 = (i + 1) * angleStep;
                else
                    angle2 = 0;

                int stepy = -35;
                double radian = Deg2Rad(angle);
                double radian2 = Deg2Rad(angle2);
                Point center = new Point(Width / 2, Height / 2 + stepy);
                Point direction = new Point(Width / 2 + (int)Math.Round(Math.Cos(radian) * zoom), Height / 2 + (int)Math.Round(Math.Sin(radian) * zoom) + stepy);
                Point direction2 = new Point(Width / 2 + (int)Math.Round(Math.Cos(radian2) * zoom), Height / 2 + (int)Math.Round(Math.Sin(radian2) * zoom) + stepy);
                //e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Black), 1f), center, direction);
                e.Graphics.FillPolygon(colors[i],
                    new PointF[3] {
                        center,
                        direction,
                        direction2
                    });
            }
        }

        private double Deg2Rad(double angle)
        {
            return Math.PI * angle / 180.0;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!timer.Enabled)
            {
                mode = 0;
                timer.Interval = 1;
                iterator = 0;
                timer.Enabled = true;
            }   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!timer.Enabled)
            {
                mode = 1;
                timer.Interval = 25;
                iterator = 1;
                timer.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!timer.Enabled)
            {
                mode = 2;
                timer.Interval = 25;
                iterator = 1;
                timer.Enabled = true;
            }
        }
    }
}
