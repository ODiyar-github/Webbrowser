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
    public partial class DiWeb_16 : Form
    {
        public DiWeb_16()
        {
            InitializeComponent();
        }

        WebBrowser web = new WebBrowser();
        int i = 0;
        string home="";
        String dirPath = AppDomain.CurrentDomain.BaseDirectory;
        public void tabLoader()
        {
            web.ScriptErrorsSuppressed = true;
            web.Dock = DockStyle.Fill;
            web.Visible = true;
            web.DocumentCompleted += Web_DocumentCompleted;
            web.ProgressChanged += Web_ProgressChanged;
            tabControl1.TabPages.Add("New Tab");
            tabControl1.SelectTab(i);
            tabControl1.SelectedTab.Controls.Add(web);
            i += 1;
            ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate("www.google.de");
        }
        public void HistoryWrite()
        {
            StreamWriter writeHisto = new StreamWriter("History.txt",false);
            for(int i=0; i<comboBox2.Items.Count; i++)
            {
                writeHisto.WriteLine(comboBox2.Items[i].ToString());
            }
            writeHisto.Close();

        }
        public void HomepageTxT()
        {
            if (Directory.GetFiles(dirPath, "HomePage.txt").Length == 0)
            {
                StreamWriter writeHome = new StreamWriter(dirPath + "HomePage.txt", false);
                writeHome.Write("www.google.de");
                home = "www.google.de";
                writeHome.Close();
            }
            else
            {
                StreamReader readHome = new StreamReader(dirPath + "HomePage.txt");
                String readerH = home = readHome.ReadLine();
                readHome.Close();
                ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(readerH);
            }
        }
        public void HistoryTxT()
        {
            if(Directory.GetFiles(dirPath, "History.txt").Length == 0)
            {
                StreamWriter verlauf = new StreamWriter(dirPath + "History.txt", false);
                verlauf.Write("");
                verlauf.Close();
            }
            else
            {
                StreamReader ReaderHist = new StreamReader(dirPath + "History.txt");
                while (!ReaderHist.EndOfStream)
                {
                    comboBox2.Items.Add(ReaderHist.ReadLine());
                }
                ReaderHist.Close();
            }
        }
        public void LanguagTxt()
        {
            if (Directory.GetFiles(dirPath, "Language.txt").Length == 0)
            {
                StreamWriter writer = new StreamWriter(dirPath + "Language.txt", false);
                writer.Write("English");
                writer.Close();
            }
            else
            {
                StreamReader Reader = new StreamReader(dirPath + "Language.txt");
                String reader = Reader.ReadLine();
                Reader.Close();
                if (reader == "English")
                {
                    english();
                }
                else if (reader == "German")
                {
                    german();
                }
            }
        }
        public void favoriteTxT()
        {
            if (Directory.GetFiles(dirPath, "Favorite.txt").Length == 0)
            {
                StreamWriter Writer = new StreamWriter(dirPath + "Favorite.txt", false);
                Writer.Write("");
                Writer.Close();
            }
            else
            {
                StreamReader Readerf = new StreamReader(dirPath + "Favorite.txt");
                while (!Readerf.EndOfStream)
                {
                    comboBox1.Items.Add(Readerf.ReadLine());
                }
                Readerf.Close();
            }
        }
        public void labelForeColor()
        {
            if (this.BackColor.GetBrightness() > 0.4)
            {
                label1.ForeColor = Color.Black;
            }
            else
            {
                label1.ForeColor = Color.White;
            }
        }
        public void colorTxt()
        {
            if (Directory.GetFiles(dirPath, "color.txt").Length == 0)
            {
                StreamWriter Writer = new StreamWriter(dirPath + "/color.txt", false);
                Writer.Write("Black");
                Writer.Close();
            }
            else
            {
                StreamReader Reader = new StreamReader(dirPath + "/color.txt");
                int lines = File.ReadAllLines("color.txt").Length;
                if (lines == 1)
                {
                    CColor c2rColor = CColor.FromName(Reader.ReadToEnd());
                    this.BackColor = c2rColor;
                    Reader.Close();
                }
                else
                {
                    String x = Reader.ReadLine();
                    int y = Int32.Parse(x);
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
                labelForeColor();
            }
        }
        private void DiWeb_10_Load(object sender, EventArgs e)
        {
            tabLoader();
            HomepageTxT();
            LanguagTxt();
            favoriteTxT();
            colorTxt();
            HistoryTxT();
        }

        private void Web_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            StreamReader Reader = new StreamReader("Language.txt");
            String reader = Reader.ReadLine();
            Reader.Close();
            if (reader == "English")
            {
                toolStripProgressBar1.Maximum = (int)e.MaximumProgress;
                if (e.CurrentProgress > 0 || e.CurrentProgress < 100)
                {
                    statusStrip1.Visible = true;
                    toolStripStatusLabel1.Text = "Loading...";
                    toolStripProgressBar1.Value = (int)e.MaximumProgress;
                }
                if (e.CurrentProgress==0)
                {
                    statusStrip1.Visible = false;
                }
            }
            else if (reader == "German")
            {
                toolStripProgressBar1.Maximum = (int)e.MaximumProgress;
                if (e.CurrentProgress > 0 || e.CurrentProgress < 100)
                {
                    statusStrip1.Visible = true;
                    toolStripStatusLabel1.Text = "Lädt...";
                    toolStripProgressBar1.Value = (int)e.MaximumProgress;
                }
                if (e.CurrentProgress == 0)
                {
                    statusStrip1.Visible = false;
                }
            }
        }

        private void Web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            tabControl1.SelectedTab.Text = ((WebBrowser)tabControl1.SelectedTab.Controls[0]).DocumentTitle;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ((WebBrowser)tabControl1.SelectedTab.Controls[0]).GoBack();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(home);
            if (home == "")
            {
                ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate("www.google.de");
            }
            else
            {
                ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(home);
            }
            StreamReader Reader = new StreamReader(dirPath + "Language.txt");
            String reader = Reader.ReadLine();
            Reader.Close();
            if (reader == "English")
            {
                english();
            }
            else if (reader == "German")
            {
                german();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HomePage2();
            Boolean a = false;
            if (comboBox2.Items.Count != 0)
            {
                for (int i = 0; i < comboBox2.Items.Count; i++)
                {
                    if (comboBox2.Items[i].ToString() == comboBox2.Text)
                    {
                        a = true;
                    }
                }
                if ((!a && comboBox2.Text != "") || (!a && comboBox2.Text != " "))
                {
                    comboBox2.Items.Add(comboBox2.Text);
                    HistoryWrite();
                }
            }
            else
            {
                comboBox2.Items.Add(comboBox2.Text);
                HistoryWrite();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ((WebBrowser)tabControl1.SelectedTab.Controls[0]).GoForward();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Refresh();
            ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(home);
        }

        public void HomePage2()
        {
            StreamReader readerH = new StreamReader("HomePage.txt");
            string HomeP = readerH.ReadLine();
            readerH.Close();

            if (HomeP == "www.google.de" || HomeP == "www.google.com" || HomeP == "www.google.net" || HomeP == "google.de" || HomeP == "google.com" || HomeP == "google.net")
            {
                if (comboBox2.Text.Contains("ä"))
                {
                    string google = " https://www.google.de/search?client=opera&q=" + comboBox2.Text.Replace("ä","ae") + "&sourceid=opera&ie=UTF-8&oe=UTF-8 ";
                    HomePage(google);
                }
                else if (comboBox2.Text.Contains("ü"))
                {
                    string google = " https://www.google.de/search?client=opera&q=" + comboBox2.Text.Replace("ü","ue") + "&sourceid=opera&ie=UTF-8&oe=UTF-8 ";
                    HomePage(google);
                }
                else if (comboBox2.Text.Contains("ö"))
                {
                    string google = " https://www.google.de/search?client=opera&q=" + comboBox2.Text.Replace("ö","oe") + "&sourceid=opera&ie=UTF-8&oe=UTF-8 ";
                    HomePage(google);
                }
                else
                {
                    string google = " https://www.google.de/search?client=opera&q=" + comboBox2.Text + "&sourceid=opera&ie=UTF-8&oe=UTF-8 ";
                    HomePage(google);
                }
            }
            else if (HomeP == "www.bing.de" || HomeP == "www.bing.com" || HomeP == "www.bing.net" || HomeP == "bing.de" || HomeP == "bing.com" || HomeP == "bing.net")
            {
                string bing = "https://www.bing.com/search?q=" + comboBox2.Text + "&go=Senden&qs=n&form=QBLH&pq=" + comboBox2.Text + "&sc=0-0&sp=-1&sk=&cvid=56E0D4DBD826462594FD505DC0C46E22";
                HomePage(bing);
            }
            else if (HomeP == "www.yahoo.de" || HomeP == "www.yahoo.com" || HomeP == "www.yahoo.net" || HomeP == "yahoo.de" || HomeP == "yahoo.com" || HomeP == "yahoo.net")
            {
                if (comboBox2.Text.Contains("ä"))
                {
                    string yahoo = " https://de.search.yahoo.com/search?p=" + comboBox2.Text.Replace("ä","ae") + "&fr=yfp-t-402";
                    HomePage(yahoo);
                }
                else if (comboBox2.Text.Contains("ü"))
                {
                    string yahoo = " https://de.search.yahoo.com/search?p=" + comboBox2.Text.Replace("ü","ue") + "&fr=yfp-t-402";
                    HomePage(yahoo);
                }
                else if (comboBox2.Text.Contains("ö"))
                {
                    string yahoo = " https://de.search.yahoo.com/search?p=" + comboBox2.Text.Replace("ö","oe") + "&fr=yfp-t-402";
                    HomePage(yahoo);
                }
                else
                {
                    string yahoo = " https://de.search.yahoo.com/search?p=" + comboBox2.Text + "&fr=yfp-t-402";
                    HomePage(yahoo);
                }
            }
            else
            {
                string google = " https://www.google.de/search?client=opera&q=" + comboBox2.Text + "&sourceid=opera&ie=UTF-8&oe=UTF-8 ";
                HomePage(google);
            }
        }
        public void HomePage(string x)
        {
            if (comboBox2.Text.Contains("."))
            {
                if (comboBox2.Text.Contains("ä") || comboBox2.Text.Contains("ö") || comboBox2.Text.Contains("ü"))
                {
                    acent();
                }
                else
                {
                    ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(comboBox2.Text);
                }
            }
            else
            {
                ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(x);
            }
        }
        public void acent()
        {
            if (comboBox2.Text.Contains("ä") || comboBox2.Text.Contains("ö") || comboBox2.Text.Contains("ü"))
            {
                string ae = comboBox2.Text.Replace("ä", "ae");
                string ue = comboBox2.Text.Replace("ü", "ue");
                string oe = comboBox2.Text.Replace("ö", "oe");
                if (comboBox2.Text.Contains("ä"))
                {
                    ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(ae);
                }
                else if (comboBox2.Text.Contains("ü"))
                {
                    ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(ue);
                }
                else if (comboBox2.Text.Contains("ö"))
                {
                    ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(oe);
                }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            WebBrowser web = new WebBrowser();
            web.ScriptErrorsSuppressed = true;
            web.Dock = DockStyle.Fill;
            web.Visible = true;
            web.DocumentCompleted += Web_DocumentCompleted;
            web.ProgressChanged += Web_ProgressChanged;
            tabControl1.TabPages.Add("New Tab");
            tabControl1.SelectTab(tabControl1.SelectedIndex+1);
            //  tabControl1.SelectedTab.Controls.Add(web);
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                tabControl1.TabPages[i].Controls.Add(web);
            }
            ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(home);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(tabControl1.TabPages.Count - 1 > 0)
            {
          //      tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                tabControl1.SelectTab(tabControl1.TabPages.Count - 1);
          //      tabControl1.SelectTab(tabControl1.SelectedIndex);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebBrowser web = new WebBrowser();
            web.ScriptErrorsSuppressed = true;
            web.Dock = DockStyle.Fill;
            web.Visible = true;
            web.DocumentCompleted += Web_DocumentCompleted;
            web.ProgressChanged += Web_ProgressChanged;
            tabControl1.TabPages.Add("New Tab");
            tabControl1.SelectTab(tabControl1.SelectedIndex + 1);
            //  tabControl1.SelectedTab.Controls.Add(web);
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                tabControl1.TabPages[i].Controls.Add(web);
            }
            ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(home);
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("Black");
            Writer.Close();
            labelForeColor();
        }

        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("White");
            Writer.Close();
            labelForeColor();
        }

        private void xXXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(255, 255, 128);
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("255 \r\n255 \r\n128");
            Writer.Close();
            labelForeColor();
        }

        private void xXXToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Yellow;
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("Yellow");
            Writer.Close();
            labelForeColor();
        }

        private void xXXToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(192, 192, 0);
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("192 \r\n192 \r\n0");
            Writer.Close();
            labelForeColor();
        }

        private void xXXToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(224, 224, 224);
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("224 \r\n224 \r\n224");
            Writer.Close();
            labelForeColor();
        }

        private void xXXToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Silver;
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("Silver");
            Writer.Close();
            labelForeColor();
        }

        private void xXXToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Gray;
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("Gray");
            Writer.Close();
            labelForeColor();
        }

        private void xXXToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(64, 64, 64);
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("64 \r\n64 \r\n64");
            Writer.Close();
            labelForeColor();
        }

        private void xXXToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(255, 192, 128);
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("255 \r\n192 \r\n128");
            Writer.Close();
            labelForeColor();
        }

        private void xXXToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Orange;
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("Orange");
            Writer.Close();
            labelForeColor();
        }

        private void xXXToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.DarkOrange;
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("DarkOrange");
            Writer.Close();
            labelForeColor();
        }

        private void xXXToolStripMenuItem16_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(192, 0, 0);
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("192 \r\n0 \r\n0");
            Writer.Close();
            labelForeColor();
        }

        private void xXXToolStripMenuItem10_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(128, 255, 128);
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("128 \r\n255 \r\n128");
            Writer.Close();
            labelForeColor();
        }

        private void xXXToolStripMenuItem11_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Lime;
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("Lime");
            Writer.Close();
            labelForeColor();
        }

        private void xXXToolStripMenuItem12_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(0, 192, 0);
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("0 \r\n192 \r\n0");
            Writer.Close();
            labelForeColor();
        }

        private void xXXToolStripMenuItem13_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Green;
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("Green");
            Writer.Close();
            labelForeColor();
        }

        private void xXXToolStripMenuItem14_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.OrangeRed;
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("OrangeRed");
            Writer.Close();
            labelForeColor();
        }

        private void xXXToolStripMenuItem15_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Red;
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("Red");
            Writer.Close();
            labelForeColor();
        }

        private void xXXToolStripMenuItem17_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromName("Highlight");
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("Highlight");
            Writer.Close();
            labelForeColor();
        }

        private void xXXToolStripMenuItem18_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Blue;
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("Blue");
            Writer.Close();
            labelForeColor();
        }

        private void xXXToolStripMenuItem19_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(0, 0, 192);
            StreamWriter Writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/color.txt", false);
            Writer.Write("0 \r\n0 \r\n192");
            Writer.Close();
            labelForeColor();
        }

        private void aboutDiWeb11ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamReader a = new StreamReader("language.txt");
            String x = a.ReadLine();
            a.Close();
            if (x == "English")
            {
                MessageBox.Show("Greetings, Thank you for using DiWeb 1.6. If you have some issues or found some bugs, send it to the email belowe! But befor Check out of Update's \r\n \r\nEmail: Diyar.Omar@gmx.net", "About DiWeb 1.6", MessageBoxButtons.OK);
            }
            else if (x == "German")
            {
                MessageBox.Show("Hallo, Danke das du DiWeb 1.6 benutzt. wenn du Probleme oder Bugs Gefunden hast, dann sende mir bitte eine Email in der Unten gegebene Email addresse! Aber bitte Prüfe vorher auf Update's \r\n \r\nEmail: Diyar.Omar@gmx.net", "Über DiWeb 1.6", MessageBoxButtons.OK);
            }
        }

        private void eXITToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            String link = ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Url.ToString(); //link
            String linkinfo = ((WebBrowser)tabControl1.SelectedTab.Controls[0]).DocumentTitle; //linkinfo

            if (!comboBox1.Items.Contains(link))
            {
                comboBox1.Items.Add(link);
                StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "favorite.txt", true);
                writer.WriteLine(link);
                writer.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Contains("."))
            {
                WebBrowser web = new WebBrowser();
                web.ScriptErrorsSuppressed = true;
                web.Dock = DockStyle.Fill;
                web.Visible = true;
                web.DocumentCompleted += Web_DocumentCompleted;
                web.ProgressChanged += Web_ProgressChanged;
                tabControl1.TabPages.Add("New Tab");
                tabControl1.SelectTab(tabControl1.SelectedIndex + 1);
                tabControl1.SelectedTab.Controls.Add(web);
                comboBox2.Text = comboBox1.Text;
                ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(comboBox1.Text);
            }
            else
            {
                WebBrowser web = new WebBrowser();
                web.ScriptErrorsSuppressed = true;
                web.Dock = DockStyle.Fill;
                web.Visible = true;
                web.DocumentCompleted += Web_DocumentCompleted;
                web.ProgressChanged += Web_ProgressChanged;
                tabControl1.TabPages.Add("New Tab");
                tabControl1.SelectTab(tabControl1.SelectedIndex + 1);
                tabControl1.SelectedTab.Controls.Add(web);
                comboBox2.Text = comboBox1.Text;
                ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(comboBox1.Text+" ");
            }
        }
  
        private void timer1_Tick(object sender, EventArgs e)
        {
            timerToolStripMenuItem.Text = DateTime.Now.ToString();
        }

        private void tabCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count - 1 > 0)
            {
                tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
                tabControl1.SelectTab(tabControl1.TabPages.Count - 1);
            }
        }

        private void newBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DiWeb_16 diWeb = new DiWeb_16();
            diWeb.Show();
            
        }
        public void english()
        {
          //  deleteHistoryBut.Text = "Delete History";
          //  deleteHistoryBut.ToolTipText = "Delete the History List";
            manualDeleteFavorite.Text = "Manual Deleting";
            manualDeleteFavorite.ToolTipText = "Delete manually the favorites list";
            changeHomePage.Text = "Change HomePage";
            changeHomePage.ToolTipText = "Changer the HomePage to a new one";
            deleteAllTabs.Text = "Delete all Tabs";
            deleteAllTabs.ToolTipText = "Delete all Tabs and the last one goes back to the Main Homepage";
            optionsToolStripMenuItem.Text = "File"; //<- ist "File"
            newBrowserBut.Text = "New Browser";
            newBrowserBut.ToolTipText = "Open a New DiWeb";
            newTabBut.Text = "Add Tab";
            newTabBut.ToolTipText = "Open a New Tab";
            tabCloseBut.Text = "Close Tab";
            tabCloseBut.ToolTipText = "Close a current Tab";
            fileToolStripMenuItem.Text = "Option"; //<- ist "Option"
            colorBut.Text = "Background Color";
            colorBut.ToolTipText = "Change the Background color";
            blackToolStripMenuItem.Text = "Black";
            whiteToolStripMenuItem.Text = "White";
            closeProgramm.Text = "Close Application";
            closeProgramm.ToolTipText = "Close the Application";
            grayToolStripMenuItem.Text = "Gray";
            yellowToolStripMenuItem.Text = "Yellow";
            orangeToolStripMenuItem.Text = "Orange";
            greenToolStripMenuItem.Text = "Green";
            redToolStripMenuItem.Text = "Red";
            blueToolStripMenuItem.Text = "Blue";
            deleteFavoriteToolStripMenuItem.Text = "Delete/Change Favorites";
            deleteFavoriteToolStripMenuItem.ToolTipText = "Delete or Change the favorite-list";
            deleteAllFavorite.Text = "Delete favorite-list";
            deleteAllFavorite.ToolTipText = "Delete the whole favorite-List";
            editFavorite.Text = "Edit";
            editFavorite.ToolTipText = "Manual processing of the favorite-list";
            changeLanguageBut.Text = "Change Language";
            germanBut.Text = "German";
            englishBut.Text = "English";
            aboutToolStripMenuItem.Text = "Help";
            aboutDiWebBut.Text = "About DiWeb1.6";
            comboBox2.Text = "Put The WebSite-Address In";
            Refresh.Text = "Refreshh";
            toolTip1.SetToolTip(Refresh, "Refresh the current Website");
            HomePageBut.Text = "Home";
            toolTip1.SetToolTip(HomePageBut, "Go to your Main WebSite");
            GoBut.Text = "GO!";
            toolTip1.SetToolTip(GoBut, "Load WebSite");
            ADDFavorite.Text = "Add Favorite";
            toolTip1.SetToolTip(ADDFavorite, "Will Add the current WebSite to your FavoriteList");
            toolTip1.SetToolTip(BackTab, "Go Back");
            toolTip1.SetToolTip(ForwardTab, "Go Forward");
            toolTip1.SetToolTip(Tabadd, "Add Tab");
            toolTip1.SetToolTip(TabDelete, "Delete current Tab");
            toolTip1.SetToolTip(WebLinkBut, "Show the current tab-link in the textbox");
            StreamWriter writerL = new StreamWriter("Language.txt");
            writerL.Write("English");
            writerL.Close();
        }
        public void german()
        {
           // deleteHistoryBut.Text = "Verlauf Löschen";
           // deleteHistoryBut.ToolTipText = "Löscht den gesamten Verlauf";
            manualDeleteFavorite.Text = "Manuelles Leeren";
            manualDeleteFavorite.ToolTipText = "Manuelles löschen der Favoriten-Liste";
            changeHomePage.Text = "HomePage-seite ändern";
            changeHomePage.ToolTipText = "Setzt eine Neue Hauptseite";
            deleteAllTabs.Text = "Lösche alle Tabs";
            deleteAllTabs.ToolTipText = "Löscht alle Tabs und setzt einen auf die Hauptseite zurück";
            editFavorite.Text = "Bearbeiten";
            editFavorite.ToolTipText = "Manuelle bearbeitung der Favoriten Liste";
            deleteAllFavorite.Text = "Liste Leeren";
            deleteAllFavorite.ToolTipText = "Löscht die Liste der Gespeicherten favoriten";
            optionsToolStripMenuItem.Text = "Datei"; //<- ist "File"
            newBrowserBut.Text = "Neues Fenster";
            newBrowserBut.ToolTipText = "Öffnet ein Neues DIWeb-Fenster";
            newTabBut.Text = "Tab hinzufügen";
            newTabBut.ToolTipText = "Öffnet eine neues Tab";
            tabCloseBut.Text = "Tab Entfernen";
            tabCloseBut.ToolTipText = "Schließt das momentant geöffnete Tab";
            fileToolStripMenuItem.Text = "Einstellungen"; //<- ist "Option"
            colorBut.Text = "Farben";
            colorBut.ToolTipText = "Ändert die Hintergrund Farbe";
            blackToolStripMenuItem.Text = "Schwarz";
            whiteToolStripMenuItem.Text = "Weiß";
            closeProgramm.Text = "Programm Schließen";
            closeProgramm.ToolTipText = "Schließt den DiWeb-Browser";
            grayToolStripMenuItem.Text = "Grau";
            yellowToolStripMenuItem.Text = "Gelb";
            orangeToolStripMenuItem.Text = "Orange";
            greenToolStripMenuItem.Text = "Grün";
            redToolStripMenuItem.Text = "Rot";
            blueToolStripMenuItem.Text = "Blau";
            deleteFavoriteToolStripMenuItem.Text = "Favoriten Löschen/Bearbeiten";
            deleteFavoriteToolStripMenuItem.ToolTipText = "Ändern oder löschen der favoriten liste";
            changeLanguageBut.Text = "Sprache ändern";
            germanBut.Text = "Deutsch";
            englishBut.Text = "Englisch";
            aboutToolStripMenuItem.Text = "Hilfe";
            aboutDiWebBut.Text = "Über DiWeb1.6";
            comboBox2.Text = "Internet-Seite eingeben";
            Refresh.Text = "Aktualiesieren";
            toolTip1.SetToolTip(Refresh, "Aktualiesiert die Momentan geöffnete WebSeite");
            HomePageBut.Text = "HauptSeite";
            toolTip1.SetToolTip(HomePageBut, "Öffnet Die HauptSeite");
            GoBut.Text = "LOS!";
            toolTip1.SetToolTip(GoBut, "Lade die WebSeite");
            ADDFavorite.Text = "Favorite Hinzufügen";
            toolTip1.SetToolTip(ADDFavorite, "Fügt die Aktuelle WebSeite in die FavoritenListe");
            toolTip1.SetToolTip(BackTab, "Zurück");
            toolTip1.SetToolTip(ForwardTab, "Vorwärts");
            toolTip1.SetToolTip(Tabadd, "Füge Tab hinzu");
            toolTip1.SetToolTip(TabDelete, "Löscht den aktuellen Tab der geöffnet ist");
            toolTip1.SetToolTip(WebLinkBut, "Zeigt den Aktuellen geöffneten Tab-Link in die TextBox");
            StreamWriter writerL = new StreamWriter("Language.txt");
            writerL.Write("German");
            writerL.Close();
        }
        private void germanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamReader a = new StreamReader("language.txt");
            String x = a.ReadLine();
            a.Close();
            if (x == "English")
            {
                german();
            }
            else if (x == "German")
            {
                german();
            }
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamReader a = new StreamReader("language.txt");
            String x = a.ReadLine();
            a.Close();
            if (x == "German")
            {
                english();
            }
            else if (x == "English")
            {
                english();
            }
        }

        private void resetToDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String dirPath = AppDomain.CurrentDomain.BaseDirectory;
            StreamReader reader = new StreamReader("Language.txt");
            String a = reader.ReadLine();
            reader.Close();
            if (a == "English")
            {
                DialogResult result = MessageBox.Show("would you like to reset all to the default settings?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    english();

                    comboBox1.Items.Clear();
                    comboBox1.Text = "";

                    StreamWriter Writer = new StreamWriter("favorite.txt", false);
                    Writer.Write("");
                    Writer.Close();

                    StreamWriter Writerr = new StreamWriter(dirPath + "/color.txt", false);
                    Writerr.Write("Black");
                    Writerr.Close();
                    this.BackColor = Color.Black;
                    labelForeColor();

                    StreamWriter writerH = new StreamWriter(dirPath + "HomePage.txt", false);
                    writerH.Write("www.google.de");
                    home = "www.google.de";
                    writerH.Close();

                    tabControl1.TabPages.Clear();
                    WebBrowser web3 = new WebBrowser();
                    web3.ScriptErrorsSuppressed = true;
                    web3.Dock = DockStyle.Fill;
                    web3.Visible = true;
                    web3.DocumentCompleted += Web_DocumentCompleted;
                    web3.ProgressChanged += Web_ProgressChanged;
                    tabControl1.TabPages.Add("New Tab");
                    tabControl1.SelectedTab.Controls.Add(web3);
                    ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(home);
                    web = web3;

                    comboBox2.Items.Clear();
                    StreamWriter writerHisto = new StreamWriter(dirPath + "History.txt");
                    writerHisto.Write("");
                    writerHisto.Close();
                }
            }
            else if(a == "German")
            {
                DialogResult result = MessageBox.Show("Wollen sie wirklich all ihre Einstellung in die Werkeinstellung zurücksetzten?", "Warnung", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    english();
                    comboBox1.Items.Clear();
                    comboBox1.Text = "";

                    StreamWriter Writer = new StreamWriter("favorite.txt", false);
                    Writer.Write("");
                    Writer.Close();

                    StreamWriter Writerr = new StreamWriter(dirPath + "/color.txt", false);
                    Writerr.Write("Black");
                    Writerr.Close();
                    this.BackColor = Color.Black;
                    labelForeColor();

                    StreamWriter writerH = new StreamWriter(dirPath + "HomePage.txt", false);
                    writerH.Write("www.google.de");
                    home = "www.google.de";
                    writerH.Close();

                    tabControl1.TabPages.Clear();
                    WebBrowser web3 = new WebBrowser();
                    web3.ScriptErrorsSuppressed = true;
                    web3.Dock = DockStyle.Fill;
                    web3.Visible = true;
                    web3.DocumentCompleted += Web_DocumentCompleted;
                    web3.ProgressChanged += Web_ProgressChanged;
                    tabControl1.TabPages.Add("New Tab");
                    tabControl1.SelectedTab.Controls.Add(web3);
                    ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(home);
                    web = web3;
                    comboBox2.Items.Clear();

                    StreamWriter writerHisto = new StreamWriter(dirPath + "History.txt");
                    writerHisto.Write("");
                    writerHisto.Close();
                }
            }
        }

        private void deleteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {

            StreamReader reader = new StreamReader("Language.txt");
            String a = reader.ReadLine();
            reader.Close();
            if (a == "English")
            {
                DialogResult result = MessageBox.Show("would you like to delete your favorite-List?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    comboBox1.Items.Clear();
                    comboBox1.Text = "";
                    StreamWriter Writer = new StreamWriter("favorite.txt", false);
                    Writer.Write("");
                    Writer.Close();
                }
            }
            else if (a == "German")
            {
                DialogResult result = MessageBox.Show("Wollen sie wirklich ihre Favoriten-Liste löschen?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    comboBox1.Items.Clear();
                    comboBox1.Text = "";
                    StreamWriter Writer = new StreamWriter("favorite.txt", false);
                    Writer.Write("");
                    Writer.Close();
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FavoritenListe favolist = new FavoritenListe();
            favolist.FormClosed += Favolist_FormClosed;
            favolist.Show();

        }

        private void Favolist_FormClosed(object sender, FormClosedEventArgs e)
        {
            string dirPath = AppDomain.CurrentDomain.BaseDirectory;
            comboBox1.Items.Clear();
            StreamReader Reader = new StreamReader(dirPath + "favorite.txt");
            while (!Reader.EndOfStream)
            {
                comboBox1.Items.Add(Reader.ReadLine());
            }
            Reader.Close();
        }

        private void deleteAllTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamReader reader = new StreamReader("Language.txt");
            String a = reader.ReadLine();
            reader.Close();
            if (a == "English")
            {
                DialogResult result = MessageBox.Show("would you like to delete all tabs?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    tabControl1.TabPages.Clear();
                    comboBox2.Text = "Put The WebSite-Address In";

                    WebBrowser web2 = new WebBrowser();
                    web2.ScriptErrorsSuppressed = true;
                    web2.Dock = DockStyle.Fill;
                    web2.Visible = true;
                    web2.DocumentCompleted += Web_DocumentCompleted;
                    web2.ProgressChanged += Web_ProgressChanged;
                    tabControl1.TabPages.Add("New Tab");
                    tabControl1.SelectedTab.Controls.Add(web2);
                    ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(home);
                    web = web2;
                }
            }
            else if (a == "German")
            {
                DialogResult result = MessageBox.Show("Wollen Sie wirklich alle ihre Tabs Schließen?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    tabControl1.TabPages.Clear();
                    comboBox2.Text = "Internet-Seite eingeben";
                    WebBrowser web2 = new WebBrowser();
                    web2.ScriptErrorsSuppressed = true;
                    web2.Dock = DockStyle.Fill;
                    web2.Visible = true;
                    web2.DocumentCompleted += Web_DocumentCompleted;
                    web2.ProgressChanged += Web_ProgressChanged;
                    tabControl1.TabPages.Add("New Tab");
                    tabControl1.SelectedTab.Controls.Add(web2);
                    ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(home);
                    web = web2;
                }
            }
            
        }

        private void colorDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            BackColor = colorDialog1.Color;
            StreamWriter writerColor = new StreamWriter("Color.txt");
            writerColor.WriteLine(colorDialog1.Color.R);
            writerColor.WriteLine(colorDialog1.Color.G);
            writerColor.WriteLine(colorDialog1.Color.B);
            writerColor.Close();
            labelForeColor();
        }

        private void changeHomePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HomePage_Set Homepage = new HomePage_Set();
            Homepage.FormClosed += Homepage_FormClosed;
            Homepage.Show();
        }

        private void Homepage_FormClosed(object sender, FormClosedEventArgs e)
        {
            StreamReader readerHome = new StreamReader("HomePage.txt");
            home = readerHome.ReadLine();
            readerHome.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            comboBox2.Text = web.Url.ToString();
            Boolean a = false;
            if (comboBox2.Items.Count != 0)
            {
                for (int i = 0; i < comboBox2.Items.Count; i++)
                {
                    if (comboBox2.Items[i].ToString() == comboBox2.Text)
                    {
                        a = true;
                    }
                }
                if (!a)
                {
                    comboBox2.Items.Add(comboBox2.Text);
                    HistoryWrite();
                }
            }
            else
            {
                comboBox2.Items.Add(comboBox2.Text);
                HistoryWrite();
            }

        }

        private void manualDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedDeletefavo selectFavo = new SelectedDeletefavo();
            selectFavo.FormClosed += SelectFavo_FormClosed;
            selectFavo.textReader("favorite.txt");
            selectFavo.Show();
        }

        private void SelectFavo_FormClosed(object sender, FormClosedEventArgs e)
        {
            string dirPath = AppDomain.CurrentDomain.BaseDirectory;
            comboBox1.Items.Clear();
            StreamReader Reader = new StreamReader(dirPath + "favorite.txt");
            while (!Reader.EndOfStream)
            {
                comboBox1.Items.Add(Reader.ReadLine());
            }
            Reader.Close();
        }

        private void closeProgrammToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (comboBox2.Text == "Put The WebSite-Address In" || comboBox2.Text == "Internet-Seite eingeben" || comboBox2.Text == "Website")
            {
                comboBox2.Text = "";
            }
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                HomePage2();
                e.Handled = true;
                e.SuppressKeyPress = true;
                Boolean a = false;
                if (comboBox2.Items.Count != 0)
                {
                    for (int i = 0; i < comboBox2.Items.Count; i++)
                    {
                        if (comboBox2.Items[i].ToString() == comboBox2.Text)
                        {
                            a = true;
                        }
                    }
                    if ( (!a&& comboBox2.Text != "") || (!a&& comboBox2.Text != " ") )
                    {
                        comboBox2.Items.Add(comboBox2.Text);
                        HistoryWrite();
                    }
                }
                else
                {
                    comboBox2.Items.Add(comboBox2.Text);
                    HistoryWrite();
                }
            }
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(comboBox2.Text);
        }
        public void helpAnzeige()
        {
            helpToolStripMenuItem.Enabled = false;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DiWeb_BrowserImage DiwebImage = new DiWeb_BrowserImage();
            DiwebImage.FormClosed += DiwebImage_FormClosed;
            helpToolStripMenuItem.Enabled = false;
            DiwebImage.MainMenuStrip.Items[1].Enabled = false;
            DiwebImage.Show();
        }

        private void DiwebImage_FormClosed(object sender, FormClosedEventArgs e)
        {
            helpToolStripMenuItem.Enabled = true;
        }

        private void deleteAllToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            StreamReader a = new StreamReader("language.txt");
            String x = a.ReadLine();
            a.Close();
            if (x == "German")
            {
                DialogResult result = MessageBox.Show("Möchten Sie ihren Web-Verlauf löschen?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    german();
                    comboBox2.Items.Clear();
                    StreamWriter writeHisto = new StreamWriter("History.txt");
                    writeHisto.Write("");
                    writeHisto.Close();
                }
            }
            else if (x == "English")
            {
                DialogResult result = MessageBox.Show("would you like to delete the Web-History?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    english();
                    comboBox2.Items.Clear();
                    StreamWriter writeHisto = new StreamWriter("History.txt");
                    writeHisto.Write("");
                    writeHisto.Close();
                }
            }
        }

        private void manualDeletingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedDeletefavo selectFavo = new SelectedDeletefavo();
            selectFavo.FormClosed += SelectFavo_FormClosed;
            selectFavo.textReader("History.txt");
            selectFavo.Show();
        }
    }
}