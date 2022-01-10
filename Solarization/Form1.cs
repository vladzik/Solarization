using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Solarization
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFileDialog1.FileName);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap inputImg = new Bitmap(pictureBox1.Image);
                Bitmap outputImg = new Bitmap(inputImg.Width, inputImg.Height);

                double k = (double)trackBar1.Value / 10000;
                label1.Text = k.ToString();
                int maxR = 0;
                int maxG = 0;
                int maxB = 0;

                int maxY = 0;



                for (int y = 0; y < inputImg.Height; y++)
                {
                    for (int x = 0; x < inputImg.Width; x++)
                    {
                        UInt32 pixel = (UInt32)(inputImg.GetPixel(x, y).ToArgb());

                        float R = (float)((pixel & 0x00FF0000) >> 16);
                        float G = (float)((pixel & 0x0000FF00) >> 8);
                        float B = (float)(pixel & 0x000000FF);

                        //if (maxR < R) maxR = (int)R;
                        //if (maxG < G) maxG = (int)G;
                        //if (maxB < B) maxB = (int)B;

                        //float Y = (float)(0.33 * R + 0.5 * G + 0.16 * B);
                        //if (maxY < Y) maxY = (int)Y;

                        if (maxY < R) maxY = (int)R;
                        if (maxY < G) maxY = (int)G;
                        if (maxY < B) maxY = (int)B;
                    }
                }

                for (int y = 0; y < inputImg.Height; y++)
                {
                    for (int x = 0; x < inputImg.Width; x++)
                    {
                        UInt32 pixel = (UInt32)(inputImg.GetPixel(x, y).ToArgb());

                        float R = (float)((pixel & 0x00FF0000) >> 16);
                        float G = (float)((pixel & 0x0000FF00) >> 8);
                        float B = (float)(pixel & 0x000000FF);

                        //R = (float)(k * R * (maxR - R));
                        //G = (float)(k * G * (maxG - G));
                        //B = (float)(k * B * (maxB - B));

                        R = (float)(- k * R * (maxY - R) + maxY * maxY / 4);
                        G = (float)(- k * G * (maxY - G) + maxY * maxY / 4);
                        B = (float)(- k * B * (maxY - B) + maxY * maxY / 4);

                        UInt32 outPixel = 0xFF000000 | ((UInt32)R << 16 | (UInt32)G << 8 | (UInt32)B);

                        outputImg.SetPixel(x, y, Color.FromArgb((int)outPixel));

                    }
                }
                pictureBox2.Image = outputImg;
            }
        }
    }
}
