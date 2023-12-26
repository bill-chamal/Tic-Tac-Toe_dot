using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using TicTacToe.Properties;

namespace TicTacToe
{
    public partial class SettingsForm : Form
    {
        string player1 = Settings.Default.playername1, player2 = Settings.Default.playername2;
        private List<PlayerData> playerData;
        public SettingsForm()
        {
            InitializeComponent();
            // check if ID1 and ID2 exist 
            if (Settings.Default.ID1 == Settings.Default.ID2)
            {
                StringCollection listname1 = new StringCollection();
                listname1 = Settings.Default.ListName;
                int i = 0, j=0;
                foreach (string a in listname1)
                {
                    i++;
                    if (a == player1)
                    {
                        Settings.Default.ID1 = i;
                        j++;
                    }
                    else if (a == player2)
                    {
                        Settings.Default.ID2 = i;
                        j++;
                    }
                    if (j == 2)
                        break;
                }
            }

            labelname1.Text = player1 + " " + Settings.Default.sign1.ToString();
            labelname2.Text = player2 + " " + Settings.Default.sign2;
            labelwin1.Text = "Wins: " + Settings.Default.ListWins[Settings.Default.ID1];
            labelwin2.Text = "Wins: " + Settings.Default.ListWins[Settings.Default.ID2];
            labelscore1.Text = "Score: " + Settings.Default.ListScore[Settings.Default.ID1];
            lblscore2.Text = "Score: " + Settings.Default.ListScore[ Settings.Default.ID2];
            lblwinrate1.Text = "Win Rate: " + Convert.ToString(getwinprecent(Settings.Default.ID1)) + "%";
            lblwinrate2.Text = "Win Rate: " + Convert.ToString(getwinprecent(Settings.Default.ID2)) + "%";
            comboBox1.Text = Settings.Default.sign1.ToString();
            comboBox2.Text = Settings.Default.sign2.ToString();
            playerData = GetPlayerData();
            var data = this.playerData;
            dataGridView1.DataSource = data;
        }

        private double getwinprecent(int i)
        {
            return Math.Round( Convert.ToDouble(Convert.ToDouble((int)Settings.Default.ListWins[i]) / Convert.ToDouble(((int)Settings.Default.ListLose[i] + (int)Settings.Default.ListWins[i]))) * 100, 1);
        }
        private List<PlayerData> GetPlayerData()
        {

            var list = new List<PlayerData>();
            for (int i=0; i<= Settings.Default.ListName.Count-1; i++)
            {
                list.Add(new PlayerData()
                {
                    ID = i,
                    Name = Settings.Default.ListName[i],
                    Wins = (int)Settings.Default.ListWins[i],
                    Score = (int)Settings.Default.ListScore[i],
                    WinRate = getwinprecent(i),
                });
            }
            return list;
        }


        public void SaveSettings()
        {
            Settings.Default.playername1 = player1;
            Settings.Default.playername2 = player2; 
            Settings.Default.sign1 = comboBox1.SelectedItem.ToString();
            Settings.Default.sign2 = comboBox2.SelectedItem.ToString();
            Settings.Default.Save();
        }

        Font oldfont;
        Color oldforecolor;
        public void HoverUnderline(object sender, EventArgs e)
        {
            Label label = sender as Label;
            oldfont = label.Font;
            oldforecolor = label.ForeColor;
            label.Font = new Font("Consolas", 24F, System.Drawing.FontStyle.Underline);
            label.ForeColor = Color.FromArgb(16, 52, 166);
        }

        public void HoverLeaveUnderline(object sender, EventArgs e)
        {
            Label label1 = sender as Label;
            label1.Font = oldfont;
            label1.ForeColor = oldforecolor;
        }
        string notname;
        private void labelname1_Click(object sender, EventArgs e)
        {
            checkBoxAI.Enabled = false;
            notname = player2;
            textBox1.Text = player1;
            textBox1.Enabled = true;
            checkBoxAI.Font = new Font("Microsoft Sans Serif", 13.875F, FontStyle.Strikeout | FontStyle.Italic);
            tableEnterNWName.BringToFront();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void btnCanc_Click(object sender, EventArgs e) => tableEnterNWName.SendToBack();

        private void labelname2_Click(object sender, EventArgs e)
        {
            checkBoxAI.Enabled = true;
            notname = player1;
            textBox1.Text = player2;
            textBox1.Enabled = !checkBoxAI.Checked;
            checkBoxAI.Font = new Font("Microsoft Sans Serif", 13.875F, FontStyle.Regular | FontStyle.Italic);
            tableEnterNWName.BringToFront();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!checkBoxAI.Enabled)
                player1 = textBox1.Text;
            else
            {
                if (checkBoxAI.Checked)
                    player2 = "AI";
                else
                    player2 = textBox1.Text;
            }
            labelname1.Text = player1 + " " + Settings.Default.sign1;
            labelname2.Text = player2 + " " + Settings.Default.sign2;
            StringCollection listname1 = new StringCollection();
            listname1 = Settings.Default.ListName;

            #region Add player database 
            if (player1 != Settings.Default.playername1)
            {
                int i = 0;
                bool flag = false;
                foreach (string a in listname1)
                {
                    if (a == player1)
                    {
                        flag = true;
                        break;
                    }
                    i++;
                }
                if (flag)
                {
                    labelwin1.Text = "Wins: " + Settings.Default.ListWins[i];
                    labelscore1.Text = "Score: " + Settings.Default.ListScore[i];
                    lblwinrate1.Text = "Win Rate: " + Convert.ToString(getwinprecent(i)) + "%";
                    Settings.Default.ID1 = i;
                    player1 = Settings.Default.ListName[i];
                    Settings.Default.playername1 = player1;
                }
                else
                {
                    listname1.Add(player1);
                    Settings.Default.ListName = listname1;
                    Settings.Default.ListWins.Add(0);
                    Settings.Default.ListScore.Add(0);
                    Settings.Default.ListWinRate.Add(0);
                    Settings.Default.ListLose.Add(0);
                    Settings.Default.ID1 = i;
                    labelwin1.Text = "Wins: " + Settings.Default.ListWins[i];
                    labelscore1.Text = "Score: " + Settings.Default.ListScore[i];
                    lblwinrate1.Text = "Win Rate: " + Convert.ToString( getwinprecent(i)) + "%";
                    player1 = Settings.Default.ListName[i];
                    Settings.Default.playername1 = player1;
                }
            }
            else if (player2 != Settings.Default.playername2)
            {
                int i = 0;
                bool flag = false;
                foreach (string a in listname1)
                {
                    if (a == player2)
                    {
                        flag = true;
                        break;
                    }
                    i++;
                }
                if (flag)
                {
                    labelwin2.Text = "Wins: " + Settings.Default.ListWins[i];
                    lblscore2.Text = "Score: " + Settings.Default.ListScore[i];
                    lblwinrate2.Text = "Win Rate: " + Convert.ToString(getwinprecent(i)) + "%";
                    Settings.Default.ID2 = i;
                    player2 = Settings.Default.ListName[i];
                    Settings.Default.playername2 = player2;
                }
                else
                {
                    listname1.Add(player2);
                    Settings.Default.ListName = listname1;
                    Settings.Default.ListWins.Add(0);
                    Settings.Default.ListScore.Add(0);
                    Settings.Default.ListWinRate.Add(0);
                    Settings.Default.ListLose.Add(0);
                    Settings.Default.ID2 = i;
                    labelwin2.Text = "Wins: " + Settings.Default.ListWins[i];
                    lblscore2.Text = "Score: " + Settings.Default.ListScore[i];
                    lblwinrate2.Text = "Win Rate: " + Convert.ToString(getwinprecent(i)) + "%";
                    player2 = Settings.Default.ListName[i];
                    Settings.Default.playername2 = player2;
                }
            }
            #endregion
            Settings.Default.Save();
            tableEnterNWName.SendToBack();

        }

        private void checkBoxAI_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = !(checkBoxAI.Checked);
            if (!checkBoxAI.Checked && textBox1.Text == "AI")
            {
                if (player1 != "Player")
                    textBox1.Text = "Player";
                else
                    textBox1.Text = "Human";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(!checkBoxAI.Checked)
                if (textBox1.Text == notname || textBox1.Text == "AI" || textBox1.Text == "")
                {
                    lblWarning.Visible = true;
                    btnSave.Enabled = false;
                }
            else { 
                lblWarning.Visible = false;
                btnSave.Enabled = true;
            }

        }

    }
}
