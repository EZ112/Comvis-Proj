using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;

namespace ComvisFinalProject{
	public partial class Form2 : Form{

        Image<Bgr, byte> ori, edit;
        Image<Gray, byte> gray;
		public Form2(){
			InitializeComponent();
            button2.Hide();
            groupBox1.Hide();
		}

		private void pictureBox4_Click(object sender, EventArgs e){
			Form1 form1 = new Form1();
			form1.Show();
			this.Hide();
		}

		private void button1_Click(object sender, EventArgs e){
            if (button1.Text == "Browse") {
                openFileDialog1.Filter = "Image Files | *.jpg;*.png";
                if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                    ori = new Image<Bgr, byte>(openFileDialog1.FileName);
                    gray = new Image<Gray, byte>(ori.Width, ori.Height);

                    CvInvoke.CvtColor(ori, gray, ColorConversion.Bgr2Gray);
                    pictureBox1.Image = ori.ToBitmap();
                    groupBox1.Show();
                    button1.Text = "Clear";

                }
            }
            else {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                pictureBox1.Image = ComvisFinalProject.Properties.Resources.imgPlaceholder;
                pictureBox2.Image = ComvisFinalProject.Properties.Resources.imgPlaceholder;
                button1.Text = "Browse";
                button2.Hide();
                groupBox1.Hide();
            }
        
		}

		private void Form2_FormClosed(object sender, FormClosedEventArgs e){
			Application.Exit();
		}

        private void detect() {
            edit = ori.Copy();
            if (checkBox1.Checked == true) {
                LineSegment2D[] lines = gray.HoughLines(127, 127, 5, Math.PI/45, 10, 20, 0)[0];
                foreach (var line in lines) {
                    edit.Draw(line, new Bgr(Color.Blue), 3);
                }
            }
            if(checkBox2.Checked == true) {
                CircleF[] circles = gray.HoughCircles(new Gray(127), new Gray(127), 5, 400, 10, 0)[0];
                foreach (var circle in circles) {
                    edit.Draw(circle, new Bgr(Color.Red), 3);
                }
            }
            if(checkBox3.Checked == true) {
                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                CvInvoke.FindContours(gray, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
                for (int i = 0; i < contours.Size; i++) {
                    VectorOfPoint contour = contours[i];
                    VectorOfPoint approxCurve = new VectorOfPoint();
                    CvInvoke.ApproxPolyDP(contour, approxCurve, CvInvoke.ArcLength(contour,true) * 0.1, true);
                    if(approxCurve.Size == 3) {
                        Point[] points = approxCurve.ToArray();
                        edit.Draw(new Triangle2DF(points[0], points[1], points[2]), new Bgr(Color.Green), 3);
                    }
                }
            }
            pictureBox2.Image = edit.ToBitmap();
            button2.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            detect();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) {
            detect();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e) {
            detect();
        }

        private void button2_Click(object sender, EventArgs e) {
            saveFileDialog1.Filter = "Image Files | *.jpg;*.png";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
                pictureBox2.Image.Save(saveFileDialog1.FileName);
        }

        private void button1_MouseEnter(object sender, EventArgs e) {
            button1.ForeColor = Color.Black;
        }

        private void button1_MouseLeave(object sender, EventArgs e) {
            button1.ForeColor = Color.White;
        }

        private void button2_MouseEnter(object sender, EventArgs e) {
            button2.ForeColor = Color.Black;
        }

        private void button2_MouseLeave(object sender, EventArgs e) {
            button2.ForeColor = Color.White;
        }
    }
}
