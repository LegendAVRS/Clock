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
        private Point hour_hand_p1, minute_hand_p1, second_hand_p1;
        private Point hour_hand_p2, minute_hand_p2, second_hand_p2;
        public Form1()
        {
            InitializeComponent();
        }

        void drawClock()
        {
            g.DrawEllipse(mypen, clock_p.X, clock_p.Y, 200, 200);
            g.DrawString("12", new Font("Ariel", 14), Brushes.Black, clock_p.X + 85, clock_p.Y + 5);
            g.DrawString("3", new Font("Ariel", 14), Brushes.Black, clock_p.X + 180, clock_p.Y + 85);
            g.DrawString("6", new Font("Ariel", 14), Brushes.Black, clock_p.X + 90, clock_p.Y + 175);
            g.DrawString("9", new Font("Ariel", 14), Brushes.Black, clock_p.X + 5, clock_p.Y + 85);

            

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = this.CreateGraphics();
            mypen = new Pen(Color.Black);

            drawClock();
            
        }
    }
}
