using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private Pen mypen;
        private Point clock_p = new Point(200, 50);
        private Point hour_hand_p, minute_hand_p, second_hand_p, middle_point, new_point;
        private Point pivot_second, pivot_minute, pivot_hour;
        private int degree = 6, width = 300, height = 300;

        private int cnt_sec = 0, cnt_min = 0, cnt_hour = 0;
        private bool flag = false, drawn = false;

        private void numMin_ValueChanged(object sender, EventArgs e)
        {
            cnt_sec = (int)numSec.Value;
            cnt_min = (int)numMin.Value;
            cnt_hour = (int)numHour.Value * 5;
            cnt_hour += cnt_min / 12;
        }

        private void numHour_ValueChanged(object sender, EventArgs e)
        {
            cnt_sec = (int)numSec.Value;
            cnt_min = (int)numMin.Value;
            cnt_hour = (int)numHour.Value * 5;
            cnt_hour += cnt_min / 12;
        }

        private void numSec_ValueChanged(object sender, EventArgs e)
        {
            cnt_sec = (int)numSec.Value;
            cnt_min = (int)numMin.Value;
            cnt_hour = (int)numHour.Value * 5;
            cnt_hour += cnt_min / 12;
        }

        public Form1()
        {
            InitializeComponent();
            middle_point = clock_p;
            middle_point.X += 120;
            middle_point.Y += 120;

            second_hand_p = middle_point;
            second_hand_p.Y -= 140;

            minute_hand_p = middle_point;
            minute_hand_p.Y -= 110;

            hour_hand_p = middle_point;
            hour_hand_p.Y -= 100;

            pivot_second = second_hand_p;
            pivot_minute = minute_hand_p;
            pivot_hour = hour_hand_p;

            DateTime dt = DateTime.Now;
            cnt_sec = dt.Second;
            cnt_min = dt.Minute;
            cnt_hour = dt.Hour * 5 + cnt_min / 12;
           
        }

        void drawHand(ref Point clock_hand, Point pivot, int cnt)
        {    

            mypen.Color = this.BackColor;
            g.DrawLine(mypen, middle_point, clock_hand); //Remove old line
            
            double rad = -(Math.PI / 180) * degree * cnt;

            Point temp_p = pivot;     //Pivot point
            temp_p.Y *= -1;
            temp_p.X -= middle_point.X;
            temp_p.Y += middle_point.Y;

            clock_hand.X = (int)Math.Ceiling(temp_p.X * Math.Cos(rad)) - (int)Math.Ceiling(temp_p.Y * Math.Sin(rad));
            clock_hand.Y = (int)Math.Ceiling(temp_p.X * Math.Sin(rad)) + (int)Math.Ceiling(temp_p.Y * Math.Cos(rad));

            clock_hand.X += middle_point.X;
            clock_hand.Y -= middle_point.Y;
            clock_hand.Y *= -1;

            mypen.Color = Color.Black;
            g.DrawLine(mypen, middle_point, clock_hand); 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {         
            ++cnt_sec;
            drawHand(ref second_hand_p, pivot_second, cnt_sec);
            drawHand(ref minute_hand_p, pivot_minute, cnt_min);
            drawHand(ref hour_hand_p, pivot_hour, cnt_hour);
            if (cnt_sec == 60)
            {
                cnt_sec = 0;
                ++cnt_min;
                flag = false;
            }
            if (cnt_min % 12 == 0 && cnt_min != 0 && !flag)
            {
                flag = true;
                if (cnt_min == 60)
                {
                    cnt_min = 0;
                }
                ++cnt_hour;
            }
            if (cnt_hour == 24 * 5)
            {
                cnt_hour = 0;
            }
            if (cnt_hour < 10 * 5) txtTime.Text = "0" + (cnt_hour / 5).ToString();
            else txtTime.Text = (cnt_hour / 5).ToString();
            txtTime.Text += ":";
            if (cnt_min < 10) txtTime.Text += "0" + cnt_min.ToString();
            else txtTime.Text += cnt_min.ToString();
            txtTime.Text += ":";
            if (cnt_sec < 10) txtTime.Text += "0" + cnt_sec.ToString();
            else txtTime.Text += cnt_sec.ToString();
        }

        void drawClock()
        {
            mypen.Width = 3;
            g.DrawEllipse(mypen, middle_point.X - 150, middle_point.Y - 150, width, height);
            mypen.Width = 1;
            Point temp_p = second_hand_p;
            temp_p.Y -= 5;
            Point temp_p_1 = temp_p;
            Point temp_p_2 = temp_p_1;
            temp_p_1.Y += 2;
            temp_p_2.Y += 10;

            temp_p.Y *= -1;
            temp_p.X -= middle_point.X;
            temp_p.Y += middle_point.Y;

            temp_p_1.Y *= -1;
            temp_p_1.X -= middle_point.X;
            temp_p_1.Y += middle_point.Y;

            temp_p_2.Y *= -1;
            temp_p_2.X -= middle_point.X;
            temp_p_2.Y += middle_point.Y;

            Point first_point = new Point();
            Point second_point = new Point();
            Point num_point = new Point();

            for (int i = 0;i < 60;++i)
            {
                double rad = -(Math.PI / 180) * 6 * i;

                first_point.X = (int)Math.Ceiling(temp_p_1.X * Math.Cos(rad)) - (int)Math.Ceiling(temp_p_1.Y * Math.Sin(rad));
                first_point.Y = (int)Math.Ceiling(temp_p_1.X * Math.Sin(rad)) + (int)Math.Ceiling(temp_p_1.Y * Math.Cos(rad));

                second_point.X = (int)Math.Ceiling(temp_p.X * Math.Cos(rad)) - (int)Math.Ceiling(temp_p.Y * Math.Sin(rad));
                second_point.Y = (int)Math.Ceiling(temp_p.X * Math.Sin(rad)) + (int)Math.Ceiling(temp_p.Y * Math.Cos(rad));

                num_point.X = (int)Math.Ceiling(temp_p_2.X * Math.Cos(rad)) - (int)Math.Ceiling(temp_p_2.Y * Math.Sin(rad));
                num_point.Y = (int)Math.Ceiling(temp_p_2.X * Math.Sin(rad)) + (int)Math.Ceiling(temp_p_2.Y * Math.Cos(rad));

                first_point.X += middle_point.X;
                first_point.Y -= middle_point.Y;
                first_point.Y *= -1;

                second_point.X += middle_point.X;
                second_point.Y -= middle_point.Y;
                second_point.Y *= -1;

                num_point.X += middle_point.X;
                num_point.Y -= middle_point.Y;
                num_point.Y *= -1;

                if (i % 5 == 0)
                {
                    int time = i / 5;
                    if (i == 0) time = 12;
                    mypen.Width = 3;
                    g.DrawString(time.ToString(), new Font("Ariel", 10), Brushes.Black, num_point.X - 6, num_point.Y - 7);
                    mypen.Width = 5;
                }

                g.DrawLine(mypen, first_point, second_point);
                mypen.Width = 1;
            } 
            pivot_second.Y += 5;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = this.CreateGraphics();
            mypen = new Pen(Color.Black);
            drawClock();    
        }
    }
}
