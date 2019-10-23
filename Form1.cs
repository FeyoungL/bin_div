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
        FileStream readStream = null;
        BinaryReader binReader = null;
        FileStream writerStream = null;
        BinaryWriter binWriter = null;

        String fileName = String.Empty;
        String richText = String.Empty;

        private void errorText(String str)
        {
            richTextBox1.ForeColor = Color.Red;
            richTextBox1.Text = "ERROR: " + str + System.Environment.NewLine;
        }

        private void infoText(String str)
        {
            richTextBox1.ForeColor = Color.Black;
            richTextBox1.Text = str + System.Environment.NewLine;
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            dialog.Multiselect = false; //不能多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "bin文件(*.bin)|*.bin|所有文件(*.*)|*.*";
            try
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = dialog.FileName;
                    using (readStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        using (binReader = new BinaryReader(readStream))
                        {
                        }
                    }
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
            catch (Exception e1)
            {
                throw e1;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            long addrStart = 0;
            long addrEnd = 0;

            if (String.IsNullOrEmpty(fileName))
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

            try
            {
                SaveFileDialog saveFDialog = new SaveFileDialog();
                saveFDialog.Title = "保存bin文件";
                saveFDialog.Filter = "bin文件|*.bin";
                saveFDialog.RestoreDirectory = true;

                if (saveFDialog.ShowDialog() == DialogResult.OK)
                {
                    String saveFileName = saveFDialog.FileName;
                    infoText(saveFDialog.FileName);

                    using (writerStream = new FileStream(saveFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using (binWriter = new BinaryWriter(writerStream))
                        {
                            //binWriter.Write(binReader.ReadChars(200));
                        }
                    }

                   // errorDeal();
                }
            }catch (Exception e1)
            {
                Console.WriteLine(e1.Message);
                errorDeal();
                throw e1;

            }

            richTextBox1.ForeColor = Color.Black;
            richTextBox1.Text += "ok."+ System.Environment.NewLine;

            errorDeal();
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
            errorDeal();
        }

        private void errorDeal()
        {
            if (binWriter != null)
            {
                binWriter.Close();
            }
            if (writerStream != null)
            {
                writerStream.Close();
            }
            if (binReader != null)
            {
                binReader.Close();
            }
            if (readStream != null)
            {
                readStream.Close();
            }
        }

    }
}
