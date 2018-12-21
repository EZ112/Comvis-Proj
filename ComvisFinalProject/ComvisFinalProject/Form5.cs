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

namespace ComvisFinalProject
{
	public partial class Form5 : Form
	{
		Image<Bgr, byte> ori;
		Image<Bgr, byte> edit1;
		Image<Gray, byte> edit2;
		Image<Gray, byte> gray;

		public Form5()
		{
			InitializeComponent();
			label4.Hide();
			label5.Hide();
			label6.Hide();
			numericUpDown2.Hide();
			numericUpDown3.Hide();
			numericUpDown1.Hide();
			groupBox1.Hide();
			button2.Hide();
		}

		private void pictureBox4_Click(object sender, EventArgs e)
		{
			Form1 form1 = new Form1();
			form1.Show();
			this.Hide();
		}

		private void button1_Click(object sender, EventArgs e)
		{
            if (button1.Text == "Browse") {
                if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                    int level1 = Convert.ToInt32(numericUpDown1.Value);
                    int level2 = Convert.ToInt32(numericUpDown2.Value);
                    int level3 = Convert.ToInt32(numericUpDown3.Value);

                    openFileDialog1.Filter = "Image Files | *.jpg;*.png";
                    ori = new Image<Bgr, byte>(new Bitmap(openFileDialog1.FileName));
                    gray = new Image<Gray, byte>(ori.Width, ori.Height);
                    edit1 = new Image<Bgr, byte>(ori.Width, ori.Height);
                    edit2 = new Image<Gray, byte>(ori.Width, ori.Height);

                    CvInvoke.CvtColor(ori, gray, ColorConversion.Bgr2Gray);
                    CvInvoke.MedianBlur(ori, edit1, 31);
                    CvInvoke.Threshold(gray, edit2, 60, 255, ThresholdType.Binary);
                    pictureBox1.Image = ori.ToBitmap();
                    groupBox1.Show();
                    pictureBox5.Image = gray.ToBitmap();
                    pictureBox6.Image = edit1.ToBitmap();
                    pictureBox7.Image = edit2.ToBitmap();
                    button1.Text = "Clear";
                }
            }
            else {
                pictureBox1.Image = ComvisFinalProject.Properties.Resources.imgPlaceholder;
                pictureBox2.Image = ComvisFinalProject.Properties.Resources.imgPlaceholder;
                button1.Text = "Browse";
                label4.Hide();
                label5.Hide();
                label6.Hide();
                numericUpDown2.Hide();
                numericUpDown3.Hide();
                numericUpDown1.Hide();
                groupBox1.Hide();
                button2.Hide();
                picUncheck(0);
            }
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			int level1 = Convert.ToInt32(numericUpDown1.Value);
            if(level1%2 !=0) {
                CvInvoke.MedianBlur(ori, edit1, level1);

                pictureBox6.Image = edit1.ToBitmap();
                pictureBox2.Image = edit1.ToBitmap();
            }
            else {
                MessageBox.Show("Smooth level must be odd number");
                numericUpDown1.Value = 31;
            }
			
		}

        private void picUncheck(int num) {
            if (num != 1) label1.ForeColor = Color.White;
            if (num != 2) label2.ForeColor = Color.White;
            if (num != 3) label3.ForeColor = Color.White;
            button2.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
		{
			pictureBox2.Image = gray.ToBitmap();
			label1.ForeColor = Color.Aqua;
            picUncheck(1);
        }

		private void pictureBox6_Click(object sender, EventArgs e)
		{
            numericUpDown1.Value = 31;
			pictureBox2.Image = edit1.ToBitmap();
			label2.ForeColor = Color.Aqua;
            picUncheck(2);
            numericUpDown2.Hide();
			numericUpDown3.Hide();
			numericUpDown1.Show();
			label6.Show();
			label4.Hide();
			label5.Hide();

		}

		private void pictureBox7_Click(object sender, EventArgs e)
		{
            numericUpDown2.Value = 60;
            numericUpDown3.Value = 255;
			pictureBox2.Image = edit2.ToBitmap();
			label3.ForeColor = Color.Aqua;
            picUncheck(3);
            numericUpDown2.Show();
			numericUpDown3.Show();
			numericUpDown1.Hide();
			label6.Hide();
			label4.Show();
			label5.Show();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			saveFileDialog1.Filter = "Image Files | *.jpg;*.png";
			saveFileDialog1.ShowDialog();
			if (saveFileDialog1.FileName != "")
				pictureBox2.Image.Save(saveFileDialog1.FileName);
		}

		private void numericUpDown2_ValueChanged(object sender, EventArgs e)
		{
			int level2 = Convert.ToInt32(numericUpDown2.Value);
			int level3 = Convert.ToInt32(numericUpDown3.Value);
			CvInvoke.Threshold(gray, edit2, level2, level3, ThresholdType.Binary);
			pictureBox2.Image = edit2.ToBitmap();
		}

        private void numericUpDown3_ValueChanged(object sender, EventArgs e) {
            int level2 = Convert.ToInt32(numericUpDown2.Value);
            int level3 = Convert.ToInt32(numericUpDown3.Value);
            CvInvoke.Threshold(gray, edit2, level2, level3, ThresholdType.Binary);
            pictureBox2.Image = edit2.ToBitmap();
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
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
