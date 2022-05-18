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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize Location Of Folders
            soundFolder = utility.NavigateParents(Environment.CurrentDirectory, 3) + "\\Sounds";


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
        private void btnPlaceShips_Click(object sender, EventArgs e)
        {
            Interaction.MsgBox(soundFolder);

            wplayer.controls.stop();
            wplayer.URL = soundFolder + "\\" + soundArray[rnd.Next(0,2)];
            wplayer.controls.play();
        }
    }
}