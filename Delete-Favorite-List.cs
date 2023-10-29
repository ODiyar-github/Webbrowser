using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CColor = System.Drawing.Color;

namespace DiCode
{
    public partial class SelectedDeletefavo : Form
    {
        public SelectedDeletefavo()
        {
            InitializeComponent();
        }
        string Reader = "";
        string NameReader = "";
        public void colorTxT()
        {
            StreamReader Reader = new StreamReader("color.txt");
            int lines = File.ReadAllLines("color.txt").Length;
            if (lines == 1)
            {
                CColor c2rColor = CColor.FromName(Reader.ReadToEnd());
                this.BackColor = c2rColor;
                Reader.Close();
            }
            else
            {
                String z = Reader.ReadLine();
                int y = Int32.Parse(z);
                int a = y;
                String bb = Reader.ReadLine();
                int bbb = Int32.Parse(bb);
                int b = bbb;
                String cc = Reader.ReadLine();
                int ccc = Int32.Parse(cc);
                int c = ccc;
                CColor c1rColor = CColor.FromArgb(a, b, c);
                this.BackColor = c1rColor;
                Reader.Close();
            }

            if (this.BackColor.GetBrightness() > 0.4)
            {
                label1.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
                label3.ForeColor = Color.Black;
            }
            else
            {
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label3.ForeColor = Color.White;
            }
        }
        public void LanguagTxT()
        {
            StreamReader readerF = new StreamReader("Language.txt");
            string reader = readerF.ReadLine();
            readerF.Close();
            if (reader == "German")
            {
                German();
            }
            else if (reader == "English")
            {
                English();
            }
        }
        public void textRead()
        {
            StreamReader readFavo = new StreamReader(Reader);
            while (!readFavo.EndOfStream)
            {
                checkedListBox1.Items.Add(readFavo.ReadLine());
            }
            readFavo.Close();
        }
        public void textReader(string TxTReader)
        {
            Reader = TxTReader;
            string nameReader = "";
            if (TxTReader.Contains("."))
            {
                for(int i=0; i < TxTReader.Length; i++)
                {
                    if (TxTReader[i].ToString() ==".")
                    {
                        break;
                    }
                    nameReader = nameReader + TxTReader[i].ToString();
                }
            }
            NameReader = nameReader;
        }
        public void fontSize()
        {
            string textBK = checkedListBox1.Text;
            checkedListBox1.Font = new Font("Arial", 12.0f);
            checkedListBox1.Text = textBK;
            string textLB = label1.Text;
            label1.Font = new Font("Arial", 10.0f);
            label1.Text = textLB;
        }
        private void SelectedDeletefavo_Load(object sender, EventArgs e)
        {
            colorTxT();
            LanguagTxT();
            fontSize();
            textRead();
            button1.Enabled = false;
            button6.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter(Reader, false);
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                writer.WriteLine(checkedListBox1.Items[i].ToString());
            }
            writer.Close();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            while (checkedListBox1.CheckedItems.Count > 0)
            {
                checkedListBox1.Items.RemoveAt(checkedListBox1.CheckedIndices[0]);
                button1.Enabled = true;
                button6.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for(int i=0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "8")
            {
                string textBK = checkedListBox1.Text;
                checkedListBox1.Font = new Font("Arial", 8.0f);
                checkedListBox1.Text = textBK;
            }
            if (comboBox1.Text == "12")
            {
                string textBK = checkedListBox1.Text;
                checkedListBox1.Font = new Font("Arial", 12.0f);
                checkedListBox1.Text = textBK;
            }
            if (comboBox1.Text == "16")
            {
                string textBK = checkedListBox1.Text;
                checkedListBox1.Font = new Font("Arial", 16.0f);
                checkedListBox1.Text = textBK;
            }
            if (comboBox1.Text == "20")
            {
                string textBK = checkedListBox1.Text;
                checkedListBox1.Font = new Font("Arial", 20.0f);
                checkedListBox1.Text = textBK;
            }
            if (comboBox1.Text == "24")
            {
                string textBK = checkedListBox1.Text;
                checkedListBox1.Font = new Font("Arial", 24.0f);
                checkedListBox1.Text = textBK;
            }
        }
        public void German()
        {
            this.Text = "Manuelles Löschen der " + NameReader + "-Liste";
            label2.Text = "Schriftgröße";
            button1.Text = "Speichern";
            toolTip1.SetToolTip(button1, "Speichern die neue "+NameReader+"-liste ab");
            button2.Text = "Abbrechen";
            toolTip1.SetToolTip(button2, "Bricht die manuel " + NameReader + "-liste ab");
            button3.Text = "Alle Auswählen";
            toolTip1.SetToolTip(button3, "Wählt alle Links aus");
            button4.Text = "Löschen";
            toolTip1.SetToolTip(button4, "Löscht die Markierten links aus der Liste");
            button5.Text = "Alle Abwählen";
            toolTip1.SetToolTip(button5, "Wählt alle Links ab");
            button6.Text = "Rückgängig";
            toolTip1.SetToolTip(button6, "Setzt die Checkbox zurück zu der Aktuellen " + NameReader + "-Liste");
            label1.Text = "INFO: \nIn diesem Fenster können sie ihre \n" + NameReader + "-liste manuel Löschen. \nMarkieren sie dazu alle gewünschte \nMarkierungen die Sie löschen möchten und \nmit 'Löschen' werden die \nMarkierten Listen gelöscht.\nAnschließend mit 'Speichern' wird \ndie neue Liste abgespeichert \n\nMarkieren gilt nur für das Löschen!\nNicht Markierte bleiben weiterhin in \nder Liste.";
        }
        public void English()
        {
            this.Text = "Delete-" + NameReader + "-List";
            label2.Text = "Font Size";
            button1.Text = "Save";
            toolTip1.SetToolTip(button1, "Save the " + NameReader + "-list");
            button2.Text = "Cancel";
            toolTip1.SetToolTip(button2, "Cancel and go back to DiWeb-Browser");
            button3.Text = "Select All";
            toolTip1.SetToolTip(button3, "Select all your links");
            button4.Text = "Delete";
            toolTip1.SetToolTip(button4, "Delete the marked links on you " + NameReader + "-list");
            button5.Text = "Deselect All";
            toolTip1.SetToolTip(button5, "Deselect all your links");
            button6.Text = "Reset";
            toolTip1.SetToolTip(button6, "Reset the Checkbox to the current " + NameReader + "-list");
            label1.Text = "INFO: \nHere can you manual delete your \n" + NameReader + "-list. Mark the links you want \nto delete and press the button 'Delete'\nto delete the marked links.\nIf your done, press the button 'Save' \nto save your new favorite-list.\n\nThe Mark ones are only for deleting!\nNon Marked one will still be on the list.";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            button1.Enabled = false;
            StreamReader readFavo = new StreamReader("favorite.txt");
            while (!readFavo.EndOfStream)
            {
                checkedListBox1.Items.Add(readFavo.ReadLine());
            }
            readFavo.Close();
            StreamWriter writer = new StreamWriter("favorite.txt", false);
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                writer.WriteLine(checkedListBox1.Items[i].ToString());
            }
            writer.Close();
            button6.Enabled = false;
        }
    }
}
