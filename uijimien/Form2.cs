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
    public partial class Form2 : Form
    {
        bool inputting = false;
        bool charIn = false;
        List<string> varNames = new List<string>();
        List<int> varValues = new List<int>();
        int openVar;
        int PC = 0;
        string[] code = new string[1];
        string currentlyExecCommand = null;
        public Form2(string unformattedCode)
        {
            InitializeComponent();
            timer1.Interval = speed.Value;
            speedIndicator.Text = speed.Value + " ms";
            code = unformattedCode.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            timer1.Interval = speed.Value;
            currentlyExecCommand = code[PC];
            if (currentlyExecCommand == "TERMINE ESSE PROGRAMA")
            {
                timer1.Stop();
                MessageBox.Show("O PROGRAMA TERMINOU!");
                Application.Exit();
            }
            else if (currentlyExecCommand == "OBTENHA ENTRADA E GUARDE NA VARIÁVEL ABERTA COMO UM CARÁTER")
            {
                timer1.Stop();
                inputting = true;
                charIn = true;
                textBox2.Enabled = true;
                button1.Enabled = true;
                textBox2.Focus();
            }
            else if (currentlyExecCommand == "OBTENHA ENTRADA E GUARDE NA VARIÁVEL ABERTA COMO UM NÚMERO")
            {
                timer1.Stop();
                inputting = true;
                charIn = false;
                textBox2.Enabled = true;
                button1.Enabled = true;
                textBox2.Focus();
            }
            else if (currentlyExecCommand == "IMPRIMA O CARÁTER DA VARIÁVEL ABERTA")
            {
                textBox1.Text += (char)varValues[openVar];
            }
            else if (currentlyExecCommand == "IMPRIMA O VALOR DA VARIÁVEL ABERTA")
            {
                textBox1.Text += varValues[openVar].ToString();
            }
            else if (currentlyExecCommand.StartsWith("IMPRIMA O CARÁTER COM O VALOR ASCII "))
            {
                textBox1.Text += (char)Int32.Parse(currentlyExecCommand.Substring(36));
            }
            else if (currentlyExecCommand.StartsWith("DECLARE A NOVA VARIÁVEL "))
            {
                varNames.Add(currentlyExecCommand.Substring(24));
                varValues.Add(0);

            }
            else if (currentlyExecCommand.StartsWith("ABRA A VARIÁVEL "))
            {
                openVar = varNames.IndexOf(currentlyExecCommand.Substring(16));
            }
            else if (currentlyExecCommand.EndsWith(" À VARIÁVEL ABERTA"))
            {
                string[] splCommand = currentlyExecCommand.Split(new char[1] { ' ' });
                if (splCommand[0] == "ATRIBUA")
                {
                    varValues[openVar] = Int32.Parse(splCommand[1]);
                }
                else if (splCommand[0] == "ADICIONE")
                {
                    varValues[openVar] += varValues[varNames.IndexOf(splCommand[1])];
                }
                else if (splCommand[0] == "MULTIPLIQUE")
                {
                    varValues[openVar] *= varValues[varNames.IndexOf(splCommand[1])];
                }
            }
            else if (currentlyExecCommand.StartsWith("PULE A "))
            {
                string[] splCommand = currentlyExecCommand.Split(new char[1] { ' ' });
                if (splCommand[3] == "SE" && splCommand[5] == "É")
                {

                    if (splCommand[6] == "IGUAL" && splCommand[7] == "A")
                    {
                        if (varValues[varNames.IndexOf(splCommand[4])] == varValues[varNames.IndexOf(splCommand[8])]) { PC = Array.IndexOf(code, "DEFINA O NOVO RÓTULO " + splCommand[2]); }
                    }
                    else if (splCommand[6] == "MAIOR" && splCommand[7] == "QUE")
                    {
                        if (varValues[varNames.IndexOf(splCommand[4])] > varValues[varNames.IndexOf(splCommand[8])]) { PC = Array.IndexOf(code, "DEFINA O NOVO RÓTULO " + splCommand[2]); }
                    }
                    else if (splCommand[6] == "MENOR" && splCommand[7] == "QUE")
                    {
                        if (varValues[varNames.IndexOf(splCommand[4])] < varValues[varNames.IndexOf(splCommand[8])]) { PC = Array.IndexOf(code, "DEFINA O NOVO RÓTULO " + splCommand[2]); }
                    }
                }

            }
            PC++;
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void speed_Scroll(object sender, EventArgs e)
        {
            speedIndicator.Text = speed.Value + " ms";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!inputting)
            {
                MessageBox.Show("NÓS NÃO ESTAMOS ACEITANDO ENTRADA!!!!!");
            }
            else
            {
                if (charIn)
                {
                    varValues[openVar] = (int)Convert.ToChar(textBox2.Text);
                }else
                {
                    varValues[openVar] = Int32.Parse(textBox2.Text);
                }
                button1.Enabled = false;
                textBox2.Enabled = false;
                textBox2.Clear();
                timer1.Start();
            }
        }
    }
}
