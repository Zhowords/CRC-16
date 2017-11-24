using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRC_16
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
         
        //储存数据
        char[] char_M;
        //储存（数据+冗余码）
        char[] char_m;
        //储存除数
        char[] char_P;
        //储存余数
        char[] char_R;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string M = txtBox_M.Text;
            string P = txtBox_P.Text;
            char_M = M.ToCharArray();
            char_P = P.ToCharArray();
            string _2nM = "被除数2^nM=" + M + "000";
            //label3.Text = _2nM;

            char_R = new char[char_P.Length - 1];
            CRC();
            FCS();
        }
        void CRC()
        {
            //储存被除数是 2nM  数组长度是k+n
            char_m = new char[(char_M.Length + char_P.Length - 1)];

            for (int i = 0; i < char_M.Length; i++)
            {
                char_m[i] = char_M[i];
            }
            //补零
            for (int i = char_M.Length; i < (char_M.Length+char_P.Length-1); i++)
            {
                char_m[i] = '0';
            }
            
            string m = null; ;
            for (int i = 0; i < (char_M.Length+char_P.Length-1); i++)
            {
                m += char_m[i];//将数组变成string类型 方便输出（被除数是 2nM）
               
            }
            listBox1.Items.Add("CRC:");
            listBox1.Items.Add(m);
            Mode2();
        }
        void FCS()
        {
            for (int i = 0; i < char_M.Length; i++)
            {
                char_m[i] = char_M[i];
                
            }
            for (int i = char_M.Length; i < char_m.Length; i++)
            {
                char_m[i] = char_R[i - char_M.Length];
            }

            string m = null;
            for (int i = 0; i < char_m.Length; i++)
            {
                m += char_m[i];
            }
            listBox1.Items.Add("FCS:");
            listBox1.Items.Add(m);
            Mode2();
        }
        void Mode2()
        {
            //循坏 被除数
            for (int i = (char_P.Length-1); i < (char_M.Length+char_P.Length-1); i++)
            {
                //储存被除数
                char[] f = new char[char_P.Length];

                //当所取被除数的前四位的第一位为零时，除数取零
                char[] q = new char[char_P.Length];
                //第一次取被除数的前四位
                if (i<char_P.Length)
                {
                    for (int j = 0; j < char_P.Length; j++)
                    {
                        f[j] = char_m[j];
                    }
                }
                else
                {   
                    // 第二次以后是 取余数加被除数的5位 构成新被除数的前四位
                    for (int j = 0; j < char_P.Length-1; j++)
                    {
                        f[j] = char_R[j];
                    }
                    f[char_P.Length-1] = char_m[i];
                }

                listBox1.Items.Add("char_m[" + i + "]:" + char_m[i]);


                string r=null;
                for (int j = 0; j < char_P.Length; j++)
                {
                    r += f[j];
                }
                listBox1.Items.Add("此时被除数f[]:" + r);
                //除数 若当所取被除数的前四位的第一位不为零时，除数取输入值  
                if (f[0]!='0')
                {
                    for (int j = 0; j < char_P.Length; j++)
                    {
                        q[j] = char_P[j];
                    }
                    
                }
                else
                {
                    for (int j = 0; j < char_P.Length; j++)
                    {
                        q[j] = '0';
                    }
                }
                //输出除数（strq）
                string strq=null;
                for (int j = 0; j < char_P.Length; j++)
                {
                    strq += q[j];
                }
                listBox1.Items.Add("此时除数q[]:" + strq);
                //获取余数
                for (int j =1 ; j < char_P.Length; j++)
                {
                    if ((int)f[j] + (int)q[j]-96 == 0 || (int)f[j] + (int)q[j]-96 == 2)
                    {
                        char_R[j - 1] = '0';
                    }
                    else {
                        char_R[j - 1] = '1';
                    }
                }

                //输出余数
                string strR = null;
                for (int j = 0; j < char_P.Length-1; j++)
                {
                    strR += char_R[j];
                }
                listBox1.Items.Add("此时余数char_R[]:"+strR);
                listBox1.Items.Add("");
         }

        }

        private void button2_Click(object sender, EventArgs e)
        {
           listBox1.Items.Clear();
        }
             
    }
}
