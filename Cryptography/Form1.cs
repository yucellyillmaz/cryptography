using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cryptography
{
    public partial class Form1 : Form
    {
        string text,finalText,binaryTotal;
        byte binaryFirst,binarySecond,notBinaryFirst;
        char[] charList;
        char[] addList = {'Y','U','C','E','L'};
        char[] mod;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            encryptText();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            decryptText();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void encryptText()
        {
            text = textBox1.Text;
            finalText = "";
            charList = text.ToCharArray();
            mod = charList;
            for (int i = 0; i < charList.Length; i++)
            {
                if (Convert.ToInt16(charList[i]) == 32)
                {
                    finalText += " ";
                    mod[i] = ' ';
                }
                else
                {
                    binaryFirst = Convert.ToByte(Convert.ToInt16(charList[i]));
                    notBinaryFirst = unchecked((byte)(~binaryFirst)); //~binaryFirst;
                    binarySecond = Convert.ToByte(Convert.ToInt16(addList[i % addList.Length]));
                    binaryTotal = calcXOR(Convert.ToByte(notBinaryFirst), binarySecond);
                    finalText += (char)((Convert.ToInt32(binaryTotal, 2) % 26) + 65);
                    mod[i] = (char)(Convert.ToInt32(binaryTotal, 2) / 26 + 48);
                }
            }
            textBox2.Text = finalText;
        }

        private void decryptText()
        {   
            text = textBox3.Text;
            int temp;
            finalText = "";
            charList = text.ToCharArray();
            for (int i = 0; i < charList.Length; i++)
            {
                if (Convert.ToInt32(mod[i]) == 32)
                {
                    finalText += " ";
                }
                else
                {
                    binaryFirst = Convert.ToByte(Convert.ToInt16(charList[i]));
                    binarySecond = Convert.ToByte(Convert.ToInt16(addList[i % addList.Length]));
                    temp = ((Convert.ToInt16(charList[i]) - 65) % 26) + ((Convert.ToInt32(mod[i]) - 48) * 26);
                    binaryTotal = calcXOR(Convert.ToByte(temp), binarySecond);
                    byte temp2 = unchecked((byte)(~Convert.ToByte(Convert.ToInt32(binaryTotal, 2))));
                    finalText += (char)temp2;
                }
            }
            textBox4.Text = finalText;
        }

        private void clearAll()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        public string calcXOR(byte a, byte b)
        {
            char[] charAArray = Convert.ToString(a,2).PadLeft(8,'0').ToCharArray();
            char[] charBArray = Convert.ToString(b, 2).PadLeft(8, '0').ToCharArray();
            char[] result = new char[8];
            string control = "";
            for (int i = 0; i < 8; i++)
            {
                control = charAArray[i].ToString();
                control += charBArray[i].ToString();
                if ( control == "01" || control == "10")
                    result[i] = '1';
                else
                    result[i] = '0';
            }
            return new string(result);
        }

        public string calcBackXOR(byte a, byte b)
        {
            char[] charAArray = Convert.ToString(a, 2).PadLeft(8, '0').ToCharArray();
            char[] charBArray = Convert.ToString(b, 2).PadLeft(8, '0').ToCharArray();
            char[] result = new char[8];
            string control = "";
            for (int i = 0; i < 8; i++)
            {
                control = charAArray[i].ToString();
                control += charBArray[i].ToString();
                if (control == "00" ||control == "11")
                    result[i] = '0';
                else
                    result[i] = '1';
            }
            return new string(result);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.CharacterCasing = CharacterCasing.Upper;
            textBox2.CharacterCasing = CharacterCasing.Upper;
            textBox3.CharacterCasing = CharacterCasing.Upper;
            textBox4.CharacterCasing = CharacterCasing.Upper;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            textBox3.Text = textBox2.Text;
            textBox4.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
            textBox1.Text = textBox4.Text;
            textBox2.Text = "";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back ||  char.IsWhiteSpace(e.KeyChar));
        }
   } 
}
