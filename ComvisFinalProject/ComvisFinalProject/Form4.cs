using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace ComvisFinalProject
{
	public partial class Form4 : Form
	{
        Image<Bgr, byte> ori, edit;
        Image<Bgr, byte> gray;
        CascadeClassifier cascade;
        VideoCapture vid;
		public Form4()
		{
			InitializeComponent();
            groupBox1.Hide();
            pictureBox4.Hide();
            pictureBox5.Hide();
		}

		private void pictureBox2_Click(object sender, EventArgs e)
		{
			Form1 form1 = new Form1();
			form1.Show();
			this.Hide();
		}

		private void Form4_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

        private void button1_Click(object sender, EventArgs e) {
            openFileDialog1.Filter = "Image or Video File | *.jpg;*.png;*.mp4;*.avi;*.flv;*.wmv;*.mov";
            if(openFileDialog1.ShowDialog() == DialogResult.OK) {
                if(Path.GetExtension(openFileDialog1.FileName) == ".jpg" || Path.GetExtension(openFileDialog1.FileName) == ".png") {
                    ori = new Image<Bgr, byte>(openFileDialog1.FileName);
                    gray = new Image<Bgr, byte>(ori.Width, ori.Height);

                    CvInvoke.CvtColor(ori, gray, ColorConversion.Bgr2Gray);
                    pictureBox1.Image = ori.ToBitmap();
                    button1.Hide();
                    label2.Hide();
                    getCascade();
                    imgDetect();
                    groupBox1.Show();
                    pictureBox4.Show();
                }
                else {
                    vid = new VideoCapture(openFileDialog1.FileName);
                    label2.Hide();
                    button1.Hide();
                    groupBox1.Show();
                    timer1.Start();
                    pictureBox4.Show();
                    pictureBox5.Show();
                }
            }
        }

        private void getCascade() {
            cascade = new CascadeClassifier("../../haarcascade_frontalface_alt.xml");
        }

        private void imgDetect() {
            edit = ori.Copy();
            var objs = cascade.DetectMultiScale(gray);
            foreach (var obj in objs) {
                edit.Draw(obj, new Bgr(Color.Blue), 3);
            }
            if (objs.Length > 0)
                label1.Text = objs.Length.ToString() + " Face(s)";
            else
                label1.Text = "0 Face(s)";
            pictureBox1.Image = edit.ToBitmap();
        }

        private void vidDetect() {
            ori = vid.QueryFrame().ToImage<Bgr, byte>();
            gray = new Image<Bgr, byte>(ori.Width, ori.Height);
            CvInvoke.CvtColor(ori, gray, ColorConversion.Bgr2Gray);
            edit = ori.Copy();
            var objs = cascade.DetectMultiScale(gray);
            foreach (var obj in objs) {
                edit.Draw(obj, new Bgr(Color.Blue), 3);
            }
            if (objs.Length > 0) 
                label1.Text = objs.Length.ToString() + " Face(s)";
            else
                label1.Text = "0 Face(s)";
            pictureBox1.Image = edit.ToBitmap();
            gray.Dispose();
        }

        private void pictureBox4_Click(object sender, EventArgs e) {
            timer1.Stop();
            pictureBox1.Image = null;
            label2.Show();
            button1.Show();
            groupBox1.Hide();
            pictureBox4.Hide();
            pictureBox5.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e) {
            if (timer1.Enabled == true) {
                timer1.Stop();
                pictureBox5.Image = ComvisFinalProject.Properties.Resources.play;
            }
            else {
                timer1.Start();
                pictureBox5.Image = ComvisFinalProject.Properties.Resources.pause;
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            getCascade();
            vidDetect();
        }
    }
}
