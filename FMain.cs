using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HashIt
{
    public partial class FMain : Form
    {
        public FMain()
        {
            InitializeComponent();

            try { textBox2.Text = File.ReadAllText(_pathHashes); }
            catch { }

            try { textBox3.Text = File.ReadAllText("ConstantSalt.txt"); }
            catch { }


        }

        SHA256Managed _sha = new SHA256Managed();
        private string _pathHashes = "Hashes.txt";
        private readonly string NL = Environment.NewLine;

        private void button1_Click(object sender, EventArgs e)
        {

            var t = textBox1.Text;
            var salt = textBox3.Text.Trim();

            if (checkBox1.Checked)
                t = t.Trim();

            if (t.Length == 0)
                return;

            t += salt;

            byte[] ba  = Encoding.UTF8.GetBytes(t);
            var bb =  _sha.ComputeHash(ba);
            bb = _sha.ComputeHash(bb);
            string output = ToHexByteStr(bb);

            string s = $"Input: {t}{NL}Bytes: {output}{NL}Base64: {Convert.ToBase64String(bb)}{NL}{NL}";

            s = DT() + s;
            textBox2.Text += s  ;

            File.AppendAllText(_pathHashes, s  );

            textBox2.Select(textBox2.Text.Length - 1, 1);
            textBox2.ScrollToCaret();

            textBox1.Text = "";


        }

        private string DT()
        {
            return DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ");
        }

        static string ToHexByteStr(byte[] data)
        {
            return string.Join("", data.Select(bin => bin.ToString("X2")).ToArray());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var pi = "https://github.com/1spb-org/hash-it";
            textBox2.Text += $"See {pi}" + NL;
            Process.Start(pi);
        }
    }
}
