using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AIPSAapp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var comments = File.ReadLines(@"C:\Users\Bogac\Desktop\Project\bitirme\kod\AIPSAapp\strings.txt", Encoding.GetEncoding("iso-8859-9"));
            if (listBox1.Items.Count == 0)
            {
                int myIndex = 0;
                foreach (var comment in comments)
                {
                    string ss = (myIndex + ". " + comment);
                    listBox1.Items.Add(ss);
                    myIndex++;
                }
            }
            else
            {
                MessageBox.Show("File Already Read");
            }

            string img_path1 = @"C:\Users\Bogac\Desktop\Project\bitirme\kod\AIPSAapp\img\pie1.png";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = new Bitmap(img_path1);

            string img_path2 = @"C:\Users\Bogac\Desktop\Project\bitirme\kod\AIPSAapp\img\bar2.png";
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.Image = new Bitmap(img_path2);

            string img_path3 = @"C:\Users\Bogac\Desktop\Project\bitirme\kod\AIPSAapp\img\line1.png";
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = new Bitmap(img_path3);


            var labels = File.ReadLines(@"C:\Users\Bogac\Desktop\Project\bitirme\kod\AIPSAapp\labels.txt");
            String[] s = new String[100];
            s = labels.ToArray();
            int[] l = new int[100];
            for(int i = 0; i < 100; i++)
            {
                l[i] = Int32.Parse(s[i]);
            }
            List<int> positives = new List<int>();
            List<int> negatives = new List<int>();
            for(int i = 0; i < 100; i++)
            {
                if(l[i] == 1)
                {
                    positives.Add(l[i]);
                }
                else 
                {
                    negatives.Add(l[i]);
                }
            }
            label1.Text = ("Positive:" + positives.Count);
            label5.Text = ("Negavite:" + negatives.Count);
            
            comboBox1.SelectedIndex = 0;

            var predicts = File.ReadLines(@"C:\Users\Bogac\Desktop\Project\bitirme\kod\AIPSAapp\predicts.txt");
            String[] p = new String[100];
            p = predicts.ToArray();

            List<int> diff = new List<int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i][0] != p[i][0])
                {
                    diff.Add(i);
                }
                
            }
            
            List<String> p_positive = new List<String>();
            List<String> p_negative = new List<String>();
            for(int i = 0; i < 100; i++)
            {
                if(p[i] == "0")
                {
                    p_negative.Add(p[i]);
                }
                else
                {
                    p_positive.Add(p[i]);
                }
            }
            label2.Text = (p.Length + " data analyzed \nPositive: " + p_positive.Count + "\nNegative: " + p_negative.Count);
            for(int i = 0; i < diff.Count; i++)
            {
                listBox2.Items.Add(diff[i]);
            }
            var feaccs = File.ReadLines(@"C:\Users\Bogac\Desktop\Project\bitirme\kod\AIPSAapp\eaccs.txt");
            String[] eaccs = new String[100];
            eaccs = feaccs.ToArray();
            var feloss = File.ReadLines(@"C:\Users\Bogac\Desktop\Project\bitirme\kod\AIPSAapp\eloss.txt");
            String[] eloss = new String[100];
            eloss = feloss.ToArray();

            label4.Text = ("Accuracy:\n" + eaccs[0] + "\n" + eaccs[1] + "\n" + eaccs[2] + "\n" + eaccs[3] + "\n" + eaccs[4] + "\n\n" + "Loss:\n" + eloss[0] + "\n" + eloss[1] + "\n" + eloss[2] + "\n" + eloss[3] + "\n" + eloss[4]);

            var ftaccs = File.ReadLines(@"C:\Users\Bogac\Desktop\Project\bitirme\kod\AIPSAapp\taccs.txt");
            String[] taccs = new String[100];
            taccs = ftaccs.ToArray();
            var ftloss = File.ReadLines(@"C:\Users\Bogac\Desktop\Project\bitirme\kod\AIPSAapp\tloss.txt");
            String[] tloss = new String[100];
            tloss = ftloss.ToArray();

            label6.Text = ("Accuracy:\n" + taccs[0] + "\nLoss:\n" + tloss[0]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var labels = File.ReadLines(@"C:\Users\Bogac\Desktop\Project\bitirme\kod\AIPSAapp\labels.txt");
            String[] s = new String[100];
            s = labels.ToArray();
            var predicts = File.ReadLines(@"C:\Users\Bogac\Desktop\Project\bitirme\kod\AIPSAapp\predicts.txt");
            String[] p = new String[100];
            p = predicts.ToArray();

            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Select An Item");
            }
            else
            {
                int x = listBox1.SelectedIndex;
                string text = listBox1.GetItemText(listBox1.SelectedItem);
                MessageBox.Show("String: " + text + "\n" + "Label:" + s[x] + "\n" + "Predict:" + p[x]);
            }   
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text == "Pie")
            {
                string img_path1 = @"C:\Users\Bogac\Desktop\Project\bitirme\kod\AIPSAapp\img\pie1.png";
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = new Bitmap(img_path1);
            }
            else
            {
                string img_path1 = @"C:\Users\Bogac\Desktop\Project\bitirme\kod\AIPSAapp\img\bar1.png";
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = new Bitmap(img_path1);
            }
        }
    }
}
