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
    public partial class Form1 : Form
    {
        int[,] array2D = new int[3, 3]
        {
            {1,1,1},    // 0 Disabled by the player, 1 Enabled (free), 2 Disabled by the program AI
            {1,1,1},
            {1,1,1},
        };
        string sign1, sign2, player1, player2;
        public Form1()
        {
            InitializeComponent();
            LoadSettings();
            StringCollection list = new StringCollection();

            // Insert data to empty database
            Settings.Default.playername1 = "User1";
            Settings.Default.playername2 = "User2";
            list.Add("User1");
            list.Add("User2");
            System.Collections.ArrayList arraylistwins = new System.Collections.ArrayList();
            arraylistwins.Add(0);
            arraylistwins.Add(0);
            System.Collections.ArrayList arraylistscore = new System.Collections.ArrayList();
            System.Collections.ArrayList arraylistwinrate = new System.Collections.ArrayList();
            System.Collections.ArrayList arraylislose = new System.Collections.ArrayList();
            arraylistscore.Add(0);
            arraylistscore.Add(0);
            arraylistwinrate.Add(0);
            arraylistwinrate.Add(0);
            arraylislose.Add(0);
            arraylislose.Add(0);
            Settings.Default.ListName = list;
            Settings.Default.ListWins = arraylistwins;
            Settings.Default.ListScore = arraylistscore;
            Settings.Default.ListWinRate = arraylistwinrate;
            Settings.Default.ListLose = arraylislose;
            Settings.Default.ID1 = 0;
            Settings.Default.ID2 = 1;
            //MessageBox.Show(list.Count.ToString() + ", " + arraylistwins.Count.ToString() + ", " + arraylistscore.Count.ToString() + ", " + arraylistwinrate.Count.ToString() + "\nDatabase created!", "TicTacToe Statistics Modified", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            // = = = = = = = = = = = == = = = == = = = = == = = =
        }

        public void LoadSettings()
        {
            sign1 = Settings.Default.sign1;
            sign2 = Settings.Default.sign2;
            player1 = Settings.Default.playername1;
            player2 = Settings.Default.playername2;
        }
        int end = 1;

        public void ShowWinner(int x, int o)
        {
            System.Collections.ArrayList lwins = new System.Collections.ArrayList();
            System.Collections.ArrayList llose = new System.Collections.ArrayList();
            lwins = Settings.Default.ListWins;
            llose = Settings.Default.ListLose;
            
            string player;
            if (x > o) // Player X Wins
            {
                int a = (int)lwins[Settings.Default.ID1];
                a++;
                lwins[Settings.Default.ID1] = a;
                a = (int)llose[Settings.Default.ID2];
                a++;
                llose[Settings.Default.ID2] = a;

                player = Settings.Default.playername1;
            }
            else if (x < o) //Player O Wins
            {
                int a = (int)lwins[Settings.Default.ID2];
                a++;
                lwins[Settings.Default.ID2] = a;
                a = (int)llose[Settings.Default.ID1];
                a++;
                llose[Settings.Default.ID1] = a;

                player = Settings.Default.playername2; 
            }
            else
                player = "DRAW";
            new WinMessegeForm(player).ShowDialog(this);
            restartToolStripMenuItem.PerformClick();
            end = 1;
            Settings.Default.ListWins = lwins;
            Settings.Default.ListLose = llose;

        }
        private void CheckX()
        { 
            int countX=0, countO=0,ii=0,jj=2;
            for (int i = 0; i <= 2; i++)
            {
                if (array2D[i, i] == 0)
                    countX++;
                else if (array2D[i, i] == 2)
                    countO++;
            }
            if (countX == 3 || countO == 3)
                ShowWinner(countX, countO);
            else
            {
                countX = 0;
                countO = 0;
                while(ii <= 2 && jj >= 0)
                {
                        if (array2D[ii, jj] == 0)
                            countX++;
                        else if (array2D[ii, jj] == 2)
                            countO++;
                        ii++;
                    jj--;
                }
                if (countX == 3 || countO == 3)
                    ShowWinner(countX, countO);
                // check if all buttons are clicked
                if (end == 9)
                {
                    new WinMessegeForm("DRAW").ShowDialog(this);
                    restartToolStripMenuItem.PerformClick();
                    end = 1;
                }
                else end++;
            }
        }

        private bool CheckColumn()
        {
            bool flag = false;
            int i = 0, countX = 0, countO = 0;
            while (!flag && i <= 2)
            {
                countO = 0;
                countX = 0;
                for (int j = 0; j <= 2; j++)
                {
                    if (array2D[j, i] == 0)
                        countX++;
                    else if (array2D[j, i] == 2)
                        countO++;
                }
                if (countX == 3 || countO == 3)
                    flag = true;
                i++;
            }
            if (flag)
                ShowWinner(countX, countO);
            else CheckX();
            return flag;
        }

        public void CheckWinner()
        {
            bool flag = false;
            int i=0,countX=0, countO=0;
            while(!flag && i<=2)
            {
                countO = 0;
                countX = 0;
                for(int j=0; j<=2; j++)
                {
                    if (array2D[i,j] == 0)
                        countX++;
                    else if (array2D[i, j] == 2)
                        countO++;
                }
                if (countX == 3 || countO == 3)
                    flag = true;
                i++;
            }
            if (flag)
                ShowWinner(countX, countO);
            else CheckColumn();
        }

        public void FindBtn(object sender, int a)
        {
            Button btn = sender as Button;

            #region find btn // 0
            switch (btn.Tag)
            {
                case "btn1":
                    array2D[0, 0] = a;
                    break;
                case "btn2":
                    array2D[0, 1] = a;
                    break;
                case "btn3":
                    array2D[0, 2] = a;
                    break;
                case "btn4":
                    array2D[1, 0] = a;
                    break;
                case "btn5":
                    array2D[1, 1] = a;
                    break;
                case "btn6":
                    array2D[1, 2] = a;
                    break;
                case "btn7":
                    array2D[2, 0] = a;
                    break;
                case "btn8":
                    array2D[2, 1] = a;
                    break;
                case "btn9":
                    array2D[2, 2] = a;
                    break;
            }
            #endregion find btn

        }
        int nextplyr=1;
        private void buttonClicked(object sender, EventArgs e)
        {
            // sender, provides a reference to the object that raised the event
            // https://docs.microsoft.com/en-us/dotnet/desktop/winforms/event-handlers-overview-windows-forms?view=netframeworkdesktop-4.8
            Button btn = sender as Button;
            if (nextplyr > 0)
            {
                FindBtn(sender, 0);
                nextplyr *= -1;
                btn.Text = sign1;
            }
            else if (player2 != "AI")
            {
                FindBtn(sender, 2);
                nextplyr *= -1;
                btn.Text = sign2;
            }
            else
                ;// The AI is here!

            btn.Enabled = false;
            restartToolStripMenuItem.Enabled = true;
            CheckWinner();
        }
        private IEnumerable<Control> GetAllButtons(Control container) // Create an list that will collect all buttons in a contrainer
        {
            List<Control> controlList = new List<Control>();
            foreach (Control c in container.Controls)
            {
                controlList.AddRange(GetAllButtons(c));
                if (c is Button)
                    controlList.Add(c);
            }
            return controlList;
            //https://stackoverflow.com/questions/3419159/how-to-get-all-child-controls-of-a-windows-forms-form-of-a-specific-type-button
        }
        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Control> controlList = new List<Control>(GetAllButtons(tableLayoutPanel2));
            foreach (Control c in controlList)
            {
                c.Enabled = true;
                c.Text = "";
            }
            for(int i=0; i<=2; i++)
                for(int j=0; j<=2; j++)
                    array2D[i, j] = 1; // Reset array2D
            restartToolStripMenuItem.Enabled = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox1 = new AboutBox();
            aboutBox1.ShowDialog(this);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog(this);  
            if(sign1 != Settings.Default.sign1)
            {
                string temp = Settings.Default.sign1;
                List<Control> controlList = new List<Control>(GetAllButtons(tableLayoutPanel2));
                foreach (Control c in controlList)
                {
                    if (c.Text == sign1)
                        c.Text = temp;
                }
            }
            if (sign2 != Settings.Default.sign2)
            {
                string temp = Settings.Default.sign2;
                List<Control> controlList = new List<Control>(GetAllButtons(tableLayoutPanel2));
                foreach (Control c in controlList)
                {
                    if (c.Text == sign2)
                        c.Text = temp;
                }
            }

            LoadSettings();
        }
    }
}
