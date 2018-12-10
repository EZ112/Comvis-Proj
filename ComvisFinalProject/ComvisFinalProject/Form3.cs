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
	public partial class Form3 : Form
	{
		Image<Bgr, byte> ori;
		Image<Gray, float> convertGray1;
		Image<Gray, float> convertGray2;
		Image<Gray, float> convertGray3;
		Image<Gray, byte> gray;

		public Form3()
		{
			InitializeComponent();
			button2.Hide();
			groupBox1.Hide();
		}

		private void pictureBox4_Click(object sender, EventArgs e)
		{
			Form1 form1 = new Form1();
			form1.Show();
			this.Hide();
		}

		private void pictureBox3_Click(object sender, EventArgs e)
		{

		}

		private void groupBox1_Enter(object sender, EventArgs e)
		{

		}

		private void button2_Click(object sender, EventArgs e)
		{
			saveFileDialog1.Filter = "Image Files | *.jpg;*.png";
			saveFileDialog1.ShowDialog();
			if(saveFileDialog1.FileName != "")
				pictureBox2.Image.Save(saveFileDialog1.FileName);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			openFileDialog1.Filter = "Image Files | *.jpg;*.png";
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				if (button1.Text == "Browse")
				{
					ori = new Image<Bgr, byte>(new Bitmap(openFileDialog1.FileName));
					gray = new Image<Gray, byte>(ori.Width, ori.Height);
					CvInvoke.CvtColor(ori, gray, ColorConversion.Bgr2Gray);
					convertGray1 = gray.Convert<Gray, float>();
					convertGray2 = gray.Convert<Gray, float>();
					convertGray3 = gray.Convert<Gray, float>();
					

					pictureBox1.Image = ori.ToBitmap();
					button1.Text = "Clear";
					groupBox1.Show();
					CvInvoke.Canny(gray, convertGray1, 60.0, 120.0);
					convertGray2 = convertGray2.Laplace(7);
					CvInvoke.Sobel(gray, convertGray3, DepthType.Default, 1, 0);

					pictureBox5.Image = convertGray1.ToBitmap();
					pictureBox6.Image = convertGray2.ToBitmap();
					pictureBox7.Image = convertGray3.ToBitmap();
				}
				else
				{

					button1.Text = "Browse";
				}


				
			}

		
		}

		private void pictureBox2_Click(object sender, EventArgs e)
		{

		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{
			

		}

		private void pictureBox5_Click(object sender, EventArgs e)
		{
			pictureBox2.Image = convertGray1.ToBitmap();
			label1.ForeColor = Color.Aqua;
			button2.Show();
		}

		private void pictureBox6_Click(object sender, EventArgs e)
		{
			pictureBox2.Image = convertGray2.ToBitmap();
			label2.ForeColor = Color.Aqua;
			button2.Show();
		}

		private void pictureBox7_Click(object sender, EventArgs e)
		{
			pictureBox2.Image = convertGray3.ToBitmap();
			label3.ForeColor = Color.Aqua;
			button2.Show();
		}

		private void Form3_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}
	}
}
