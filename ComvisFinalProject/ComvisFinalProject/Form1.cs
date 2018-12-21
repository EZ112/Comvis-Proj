using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComvisFinalProject{
	public partial class Form1 : Form{
		public Form1(){
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e){
			Form2 form2 = new Form2 ();
			form2.Show();
			this.Hide();
		}

		private void button2_Click(object sender, EventArgs e){
			Form3 form3 = new Form3();
			form3.Show();
			this.Hide();
		}

		private void button3_Click(object sender, EventArgs e){
			Form4 form4 = new Form4();
			form4.Show();
			this.Hide();
		}

		private void button4_Click(object sender, EventArgs e){
			Form5 form5 = new Form5();
			form5.Show();
			this.Hide();
		}

		private void pictureBox2_Click(object sender, EventArgs e){
			Application.Exit();
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e){
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

        private void button3_MouseEnter(object sender, EventArgs e) {
            button3.ForeColor = Color.Black;
        }

        private void button3_MouseLeave(object sender, EventArgs e) {
            button3.ForeColor = Color.White;
        }

        private void button4_MouseEnter(object sender, EventArgs e) {
            button4.ForeColor = Color.Black;
        }

        private void button4_MouseLeave(object sender, EventArgs e) {
            button4.ForeColor = Color.White;
        }
    }
}
