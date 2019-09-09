using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uijimien
{
    
    public partial class MainProgram : Form
    {
        Random r = new Random();
        double maxTime = 10000;
        double currentTime = 10000;
        public MainProgram()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("NÃO SEU BURRO É PRA USAR OS BOTÕES TONGOOOOOO");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            codeBox.Text += "á";
            codeBox.Focus();
            codeBox.DeselectAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            codeBox.Text += "à";
            codeBox.Focus();
            codeBox.DeselectAll();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            codeBox.Text += "é";
            codeBox.Focus();
            codeBox.DeselectAll();


        }

        private void button5_Click(object sender, EventArgs e)
        {
            codeBox.Text += "ó";
            codeBox.Focus();
            codeBox.DeselectAll();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timerTimer.Stop();
            var interpreter = new Form2(textBox1.Text);
            interpreter.Show();
            this.Hide();
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            codeBox.Text += "ú";
            codeBox.Focus();
            codeBox.DeselectAll();
        }

        private void timerTimer_Tick(object sender, EventArgs e)
        {
            currentTime -= 10;
            timelabel.Text = (currentTime / 1000).ToString();
            if(currentTime < 0)
            {
                timerTimer.Stop();
                if(codeBox.Lines.Length == 0 || codeBox.Lines[0] == "")
                {
                    var interpreter = new Form2(textBox1.Text);
                    interpreter.Show();
                    this.Hide();
                }else
                {
                    var oldSel = codeBox.SelectionStart;
                    var delLen = codeBox.Lines.First().Length;
                    textBox1.Text += codeBox.Lines.First() + Environment.NewLine;
                    codeBox.Lines = codeBox.Lines.Skip(1).ToArray();
                        codeBox.SelectionStart = oldSel - delLen;
                    codeBox.SelectionLength = 0;
                    if (r.Next(1,10) == 5)
                    {
                        maxTime -= maxTime / 8;
                    }
                    currentTime = maxTime;
                    timerTimer.Start();

                }
            }
            
        }

        private void codeBox_TextChanged(object sender, EventArgs e)
        {
            if (timerTimer.Enabled == false)
            {
                timerTimer.Start();
            }
        }

       
    }
}
