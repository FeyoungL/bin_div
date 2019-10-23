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

        OpenFileDialog dialog = new OpenFileDialog();
        FileStream fs = null;
        BinaryReader binReader = null;

        String fileName = String.Empty;
        String richText = String.Empty;

        private void errorText(String str)
        {
            richTextBox1.ForeColor = Color.Red;
            richTextBox1.Text = "ERROR: " + str + System.Environment.NewLine;
        }

        private void infoText(String str)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            dialog.Multiselect = false; //不能多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "bin文件(*.bin)|*.bin|所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //textBox1.Text = dialog.FileName;    //显示名字
                
                fileName = dialog.FileName;
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                binReader = new BinaryReader(fs);
                //textBox2.Text = fs.Length.ToString();     //获取长度

                //foreach (byte j in binReader.ReadChars(200))
                //{
                //    richText += j.ToString("X2");
                //    richText += " ";
                //}
                //richTextBox1.Text = richText;   //打印

                richTextBox1.ForeColor = Color.Black;
                richTextBox1.Text += "Load file: " + fileName + " success." + System.Environment.NewLine;


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            long addrStart = 0;
            long addrEnd = 0;
            if(String.IsNullOrEmpty(fileName))
            {
                errorText("Please open bin file to split operation.");
                return;
            }
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text))
            {
                errorText("addr is null or Empty.");
                return;
            }
            try
            {
                addrStart = long.Parse(textBox1.Text);
                addrEnd = long.Parse(textBox2.Text);
                if(addrEnd<=addrStart)
                {
                    errorText("The start of Address >= The end of Address.");
                    return;
                }
            }
            catch
            {
                errorText("addr is not number.");
                return;
            }
            richTextBox1.ForeColor = Color.Black;
            richTextBox1.Text = "ok."+ System.Environment.NewLine;
            if (binReader != null)
            {
                binReader.Close();
            }
            if(fs != null)
            { 
                fs.Close();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Closing(object sender, EventArgs e)
        {
            if(binReader != null)
            {
                binReader.Close();
            }
            if(fs != null)
            {
                fs.Close();
            }
        }

    }
}
