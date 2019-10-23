using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bin_div
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String fileName = String.Empty;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false; //不能多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "bin文件(*.bin)|*.bin|所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
                System.IO.Stream fileStream = dialog.OpenFile();
                using (StreamReader sr = new StreamReader(fileStream))
                {
                    textBox2.Text = sr.ReadLine();
                }
            }
        }
    }
}
