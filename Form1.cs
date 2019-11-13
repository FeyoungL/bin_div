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
        Byte[] zkDataCopyBuffer = null;

        int fileLen = 0;
        int addrLen = 0;


        private void errorText(String str)
        {
            richTextBox1.ForeColor = Color.Red;
            richTextBox1.Text = "ERROR: " + str + System.Environment.NewLine;
        }

        private void infoText(String str)
        {
            richTextBox1.ForeColor = Color.Black;
            richTextBox1.Text += str + System.Environment.NewLine;
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
                            richTextBox1.ForeColor = Color.Black;
                            richTextBox1.Text = "";
                            richTextBox1.Text += "Load file: " + fileName + " success." + System.Environment.NewLine;
                            //把整个文件获取到zkDataCopyBuffer里
                            fileLen = (int)readStream.Length;
                            zkDataCopyBuffer = binReader.ReadBytes(fileLen);
                            infoText("file Length: " + fileLen.ToString() + "(0x" + fileLen.ToString("X") + ")" + " Byte");

                            if (binReader != null)
                            {
                                binReader.Dispose();
                                binReader.Close();
                            }
                        }
                        if (readStream != null)
                        {
                            readStream.Dispose();
                            readStream.Close();
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                throw e1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int addrStart = 0;
            int addrEnd = 0;

            try
            {
                SaveFileDialog saveFDialog = new SaveFileDialog();
                saveFDialog.Title = "保存bin文件";
                saveFDialog.Filter = "bin文件|*.bin";
                saveFDialog.RestoreDirectory = true;

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
                    addrStart = Convert.ToInt32(textBox1.Text, 16);
                    addrEnd = Convert.ToInt32(textBox2.Text, 16);
                    if (addrEnd <= addrStart)
                    {
                        errorText("The start of Address >= The end of Address.");
                        return;
                    }

                    if (addrStart>=fileLen || addrEnd>fileLen)
                    {
                        errorText("addrStart or addrEnd >= The lenght of file.");
                        return;
                    }
                }
                catch
                {
                    errorText("addr is not number.");
                    return;
                }

                addrLen = addrEnd - addrStart;

                if (saveFDialog.ShowDialog() == DialogResult.OK)
                {
                    //检查
                    if (saveFDialog.CheckFileExists)
                        return;

                    String saveFileName = saveFDialog.FileName;

                    using (writerStream = new FileStream(saveFileName, FileMode.Create, FileAccess.Write))
                    {
                        using (binWriter = new BinaryWriter(writerStream))
                        {
                            binWriter.Write(zkDataCopyBuffer, addrStart, addrLen);

                            richTextBox1.ForeColor = Color.Black;
                            richTextBox1.Text += "Save " + saveFileName + " success." + System.Environment.NewLine;
                        }
                    }

                }
            }catch (Exception e1)
            {
                Console.WriteLine(e1.Message);
                throw e1;
            }

            errorDeal();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
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
                binWriter.Dispose();
                binWriter.Close();
            }
            if (writerStream != null)
            {
                writerStream.Dispose();
                writerStream.Close();
            }
            if (binReader != null)
            {
                binReader.Dispose();
                binReader.Close();
            }
            if (readStream != null)
            {
                readStream.Dispose();
                readStream.Close();
            }
        }

   
    }
}
