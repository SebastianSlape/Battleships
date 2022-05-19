using System;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using UtilityFunctions;

namespace Battleships
{
    public partial class Form1 : Form
    {
        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        Utility utility = new Utility();
        Random rnd = new Random();

        bool noahMode = false;
        string soundFolder = "";

        string[] soundArray = new string[2] { "lessgo.mp3", "yeahbaby.mp3" };
        
        string[] shipNames = new string[5] { "Carrier", "Battleship", "Cruiser", "Submarine", "Destroyer" };
        int[] shipSize = new int[5] { 5, 4, 3, 3, 2 };
        bool[] shipsPlaced = new bool[5] { false, false, false, false, false };

        public Form1()
        {
            InitializeComponent();
        }
        public void PlayBadSound()
        {
            // Initialize Location Of Folders
            soundFolder = utility.NavigateParents(Environment.CurrentDirectory, 3) + "\\Sounds";

            wplayer.controls.stop();
            wplayer.URL = soundFolder + "\\" + soundArray[rnd.Next(0, soundArray.Length)];
            wplayer.controls.play();
        }

        public void PlayGoodSound()
        {
            // Initialize Location Of Folders
            soundFolder = utility.NavigateParents(Environment.CurrentDirectory, 3) + "\\Sounds";

            wplayer.controls.stop();
            wplayer.URL = soundFolder + "\\" + soundArray[rnd.Next(0, soundArray.Length)];
            wplayer.controls.play();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string name = string.Join(' ', Regex.Split(utility.GetNameOfUser(), @"(?<!^)(?=[A-Z])")); // Regex to Seperate Name by Capitalization ("JohnDoe" => "John Doe")
            
            if (name == "Noah Casey") // Check if it is you know who
            {
                noahMode = true;
            }

            Interaction.MsgBox("Hello " + name);

            if (noahMode)
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                utility.CreateFile(desktopPath + "\\OPENME.txt", "I know where you live " + name);
                Interaction.MsgBox("Open your desktop.");
            }

            dgvPlace.RowCount = 10;
            dgvPlace.ColumnCount = 10;
            for (int colHeader = 0; colHeader < 10; colHeader++)
            {
                dgvPlace.Columns[colHeader].HeaderCell.Value = (colHeader + 1).ToString();
            }
            char[] letters = new char[10] {'A','B','C','D','E','F','G','H','I','J'};
            for (int rowHeader = 0; rowHeader < 10; rowHeader++)
            {
                dgvPlace.Rows[rowHeader].HeaderCell.Value = letters[rowHeader].ToString();
            }
        }
        private void btnPlaceShip_Click(object sender, EventArgs e)
        {   
            int columnNum = dgvPlace.SelectedCells[0].ColumnIndex;
            int rowNum = dgvPlace.SelectedCells[0].RowIndex;
            bool columnValid = true;
            bool rowValid = true;

            for (int i = 0; i < dgvPlace.SelectedCells.Count; i++)
            {
                if (dgvPlace.SelectedCells[i].ColumnIndex != columnNum)
                {
                    columnValid = false;
                }
                if (dgvPlace.SelectedCells[i].RowIndex != rowNum)
                {
                    rowValid = false;
                }
            }
            if (!columnValid && !rowValid)
            {
                Interaction.MsgBox("Ship Invalid. Too Wide.");
                return;
            }
            int count = dgvPlace.SelectedCells.Count;
            if (count <= 1)
            {
                Interaction.MsgBox("Ship Invalid. Not Enough Cells Selected.");
                return;
            } 
            else if (count >= 6)
            {
                Interaction.MsgBox("Ship Invalid. Too Many Cells Selected.");
                return;
            }

            for (int j = 0; j < count; j++)
            {
                if (dgvPlace.SelectedCells[j].Style.BackColor == Color.Red)
                {
                    Interaction.MsgBox("You already placed a ship there");
                    return;
                }
            }

            for (int i = 0; i < shipSize.Length; i++)
            {
                if (count == shipSize[i] && count != 3)
                {
                    if (shipsPlaced[i] == true)
                    {
                        Interaction.MsgBox("You already placed a " + shipNames[i]);
                        return;
                    }
                    shipsPlaced[i] = true;
                    for (int j = 0; j < count; j++)
                    {
                        dgvPlace.SelectedCells[j].Style.BackColor = Color.Red;
                    }
                    PlayGoodSound();
                    return;
                } else if (count == 3) { // Edge Case
                    if (shipsPlaced[2] != true)
                    {
                        shipsPlaced[2] = true;
                        for (int j = 0; j < count; j++)
                        {
                            dgvPlace.SelectedCells[j].Style.BackColor = Color.Red;
                        }
                        PlayGoodSound();
                        return;
                    }
                    if (shipsPlaced[3] != true)
                    {
                        shipsPlaced[3] = true;
                        for (int j = 0; j < count; j++)
                        {
                            dgvPlace.SelectedCells[j].Style.BackColor = Color.Red;
                        }
                        PlayGoodSound();
                        return;
                    }
                    Interaction.MsgBox("You already placed a " + shipNames[2] + "/" + shipNames[3]);
                    return;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < shipsPlaced.Length; i++)
            {
                shipsPlaced[i] = false;
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    dgvPlace[i,j].Style.BackColor = Color.White;
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnPlaceShip.Enabled = false;
            btnClear.Enabled = false;
        }
    }
}