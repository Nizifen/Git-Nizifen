using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
namespace FileRW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Write_Click(object sender, EventArgs e)
        {
            #region 方式1
            StreamWriter sw = new StreamWriter("c:\\MyTextFile.txt", true);
            sw.WriteLine("写入第一行内容，"); //带换行符
            sw.Write("写入第二行内容。"); //无换行符
            string nextLine = "Groovy Line";
            sw.Write(nextLine);

            sw.Flush();//从缓冲区写入基础流
            sw.Close();//使用完后，要关闭StreamWriter
            #endregion


            //#region 方式2
            //StreamWriter sw = File.CreateText("c:\\MyTextFile.txt");
            //sw.WriteLine("这是第一行内容，"); //带换行符
            //sw.WriteLine("这是第二行内容。");
            //sw.Flush();//从缓冲区写入基础流（文件）
            //sw.Close();//使用完后，要关闭StreamWriter
            //#endregion

            //#region 方式3

            //FileStream fs = new FileStream("c:\\MyTextFile.txt", FileMode.OpenOrCreate, FileAccess.Write);
            //StreamWriter sw = new StreamWriter(fs);
            //sw.WriteLine("这是第一行内容，");
            //sw.Flush();//从缓冲区写入基础流（文件）
            //sw.Close();//使用完后，要关闭StreamWriter

            //#endregion

            //#region 方式4

            //FileInfo myFile = new FileInfo("c:\\MyTextFile.txt ");
            //StreamWriter sw = myFile.CreateText();
            //sw.WriteLine("这是第一行内容，");
            //sw.Flush();//从缓冲区写入基础流（文件）
            //sw.Close();//使用完后，要关闭StreamWriter

            //#endregion

            MessageBox.Show("文件写入成功!");
        }

        private void btn_Read_Click(object sender, EventArgs e)
        {
            if (File.Exists("c:\\MyTextFile.txt"))
            {


                ////方式1
                StreamReader sr = new StreamReader("c:\\MyTextFile.txt", Encoding.Default);
                StringBuilder sbContent = new StringBuilder();
                while (sr.Peek() > -1)  //Check EOF
                {
                    sbContent.Append(sr.ReadLine());
                }
                sr.Close();

                ////方式2
                //FileStream fs = new FileStream("c:\\MyTextFile.txt", FileMode.Open, FileAccess.Read, FileShare.None);
                //StreamReader sr = new StreamReader(fs);
                //StringBuilder sbContent = new StringBuilder();
                //while (sr.Peek() > -1)  //Check EOF
                //{
                //    sbContent.Append(sr.ReadLine());
                //}
                //sr.Close();


                //方式3
                //StreamReader sr = File.OpenText("c:\\MyTextFile.txt");                
                //StringBuilder sbContent = new StringBuilder();
                //while (sr.Peek() > -1)  //Check EOF
                //{                    
                //    sbContent.Append(sr.ReadLine());
                //}
                //sr.Close();

                ////方式4
                //FileInfo myFile = new FileInfo("c:\\MyTextFile.txt");
                //StreamReader sr = myFile.OpenText();
                //StringBuilder sbContent = new StringBuilder();
                //while (sr.Peek() > -1)  //Check EOF
                //{                    
                //    sbContent.Append(sr.ReadLine());
                //}
                //sr.Close();


                this.richTextBox1.Text = sbContent.ToString();

                ////方法3
                //this.richTextBox1.LoadFile("c:\\MyTextFile.txt", RichTextBoxStreamType.PlainText);
            }


        }
        private void btn_AppendText_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = File.AppendText("c:\\MyTextFile.txt"))
            {
                sw.WriteLine("这是追加的内容");
            }
            MessageBox.Show("文件追加成功!");
        }

        private void btn_Stream_Click(object sender, EventArgs e)
        {
            #region 方法1
            //FileStream fs = new FileStream("c:\\MyTextFile.dat", FileMode.Open, FileAccess.Read);
            //BinaryReader br = new BinaryReader(fs);
            //StringBuilder str = new StringBuilder();
            //for (int i = 0; i < br.BaseStream.Length; i++)
            //{
            //    str.Append(Convert.ToChar(br.Read()));
            //}
            //richTextBox1.Text = str.ToString();
            //br.Close();
            //fs.Close();



            ////声明并使用File的OpenRead实例化一个文件流对象
            //FileStream fs = File.OpenRead("c:\\MyTextFile.dat");

            ////准备一个存放文件内容的字节数组，fs.Length将得到文件的实际大小
            //byte[] data = new byte[fs.Length];

            ////调用一个文件流的一个方法读取数据到data数组中
            //fs.Read(data, 0, data.Length);            
            //richTextBox1.Text = Encoding.Default.GetString(data);  
            #endregion


            #region  方法2

            ////创建一个文件流，用以写入或者创建一个StreamWriter
            //FileStream fs = new FileStream("c:\\MyTextFile.dat", FileMode.OpenOrCreate, FileAccess.Write);
            //StreamWriter sw = new StreamWriter(fs);

            ////使用StreamWriter来往文件中写入内容
            //sw.BaseStream.Seek(0, SeekOrigin.Begin);

            ////把richTextBox1中的内容写入文件
            //sw.Write(richTextBox1.Text);

            ////关闭此文件
            //sw.Flush();
            //sw.Close();


            //FileStream fs = new FileStream("c:\\MyTextFile.dat", FileMode.Create);
            //BinaryWriter bw = new BinaryWriter(fs);            
            //bw.Write("abcd");
            //bw.Close();
            //fs.Close();
            #endregion





        }
        public static void SafeRead(Stream stream, byte[] data)
        {
            int offset = 0;
            int remaining = data.Length;
            // 只要有剩余的字节就不停的读
            while (remaining > 0)
            {
                int read = stream.Read(data, offset, remaining);
                if (read <= 0)
                    throw new EndOfStreamException("文件读取到" + read.ToString() + "失败！");

                // 减少剩余的字节数
                remaining -= read;

                // 增加偏移量
                offset += read;
            }
        }

        //复制文件
        private void btn_FileCopy_Click(object sender, EventArgs e)
        {
            string OrignFile, NewFile;
            OrignFile = "c:\\MyTextFile.txt";
            NewFile = "d:\\MyTextFile.txt";
            File.Copy(OrignFile, NewFile, true);
        }
        //移动文件
        private void btn_FileMove_Click(object sender, EventArgs e)
        {
            string OrignFile, NewFile;
            OrignFile = "c:\\MyTextFile.txt";
            NewFile = "d:\\MyTextFile.txt";
            File.Move(OrignFile, NewFile);
        }
        //删除文件
        private void btn_DelFile_Click(object sender, EventArgs e)
        {
            string OrignFile = "c:\\MyTextFile.txt";
            File.Delete(OrignFile);
        }


    }
}
