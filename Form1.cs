using System;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using UtilityFunctions;

namespace Battleships
{
    public partial class frmBattleship : Form
    {
        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        Utility utility = new Utility();
        Random rnd = new Random();

        bool noahMode = false;
        bool placing = true;
        bool end = false;

        string soundFolder = "";

        char[] letters = new char[10] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };

        string[] soundArray = new string[2] { "lessgo.mp3", "yeahbaby.mp3" };
        
        string[] shipNames = new string[5] { "Carrier", "Battleship", "Cruiser", "Submarine", "Destroyer" };
        int[] shipSize = new int[5] { 5, 4, 3, 3, 2 };
        bool[] shipsPlaced = new bool[5] { false, false, false, false, false };

        bool[,] CPUShip = new bool[10,10];

        bool[,] Carrier = new bool[10, 10];
        bool CarrierAlive = false;

        bool[,] Battleship = new bool[10, 10];
        bool BattleshipAlive = false;

        bool[,] Cruiser = new bool[10, 10];
        bool CruiserAlive = false;

        bool[,] Submarine = new bool[10, 10];
        bool SubmarineAlive = false;

        bool[,] Destroyer = new bool[10, 10];
        bool DestroyerAlive = false;

        bool muted = false;

        public frmBattleship()
        {
            InitializeComponent();
        }

        public void PlayGoodSound()
        {
            if (muted)
            {
                return;
            }

            // Initialize Location Of Folders
            soundFolder = utility.NavigateParents(Environment.CurrentDirectory, 3) + "\\Sounds";

            wplayer.controls.stop();
            wplayer.URL = soundFolder + "\\" + soundArray[rnd.Next(0, soundArray.Length)];
            wplayer.controls.play();
        }
        public void PlayBadSound()
        {
            if (muted)
            {
                return;
            }

            // Initialize Location Of Folders
            soundFolder = utility.NavigateParents(Environment.CurrentDirectory, 3) + "\\Sounds";

            wplayer.controls.stop();
            wplayer.URL = soundFolder + "\\" + soundArray[rnd.Next(0, soundArray.Length)];
            wplayer.controls.play();
        }

        public void PlayPlaceSound()
        {
            if (muted)
            {
                return;
            }

            // Initialize Location Of Folders
            soundFolder = utility.NavigateParents(Environment.CurrentDirectory, 3) + "\\Sounds";

            wplayer.controls.stop();
            wplayer.URL = soundFolder + "\\" + soundArray[rnd.Next(0, soundArray.Length)];
            wplayer.controls.play();
        }

        private void frmBattleship_Load(object sender, EventArgs e)
        {
            string name = string.Join(' ', Regex.Split(utility.GetNameOfUser(), @"(?<!^)(?=[A-Z])")); // Regex to Seperate Name by Capitalization ("JohnDoe" => "John Doe")

            if (name != "Noah Casey") // Check if it is you know who
            {
                noahMode = true;
            }

            if (noahMode)
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                utility.CreateFile(desktopPath + "\\OPENME.txt", "I know where you live " + name);
                Interaction.MsgBox("Open your desktop.");
            }

            dgvShips.RowCount = 10;
            dgvShips.ColumnCount = 10;
            dgvGrid.RowCount = 10;
            dgvGrid.ColumnCount = 10;

            for (int colHeader = 0; colHeader < 10; colHeader++)
            {
                dgvShips.Columns[colHeader].HeaderCell.Value = (colHeader + 1).ToString();
                dgvGrid.Columns[colHeader].HeaderCell.Value = (colHeader + 1).ToString();
            }
            char[] letters = new char[10] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
            for (int rowHeader = 0; rowHeader < 10; rowHeader++)
            {
                dgvShips.Rows[rowHeader].HeaderCell.Value = letters[rowHeader].ToString();
                dgvGrid.Rows[rowHeader].HeaderCell.Value = letters[rowHeader].ToString();
            }

        }
        public void ColourShip(int count)
        {
            for (int j = 0; j < count; j++)
            {
                dgvShips.SelectedCells[j].Style.BackColor = Color.Red;
            }
            dgvShips.ClearSelection();
            PlayPlaceSound();
        }
        public void PlaceShip()
        {
            if (!placing || end)
            {
                return;
            }

            int columnNum = dgvShips.SelectedCells[0].ColumnIndex;
            int rowNum = dgvShips.SelectedCells[0].RowIndex;
            bool columnValid = true;
            bool rowValid = true;

            for (int i = 0; i < dgvShips.SelectedCells.Count; i++)
            {
                if (dgvShips.SelectedCells[i].ColumnIndex != columnNum)
                {
                    columnValid = false;
                }
                if (dgvShips.SelectedCells[i].RowIndex != rowNum)
                {
                    rowValid = false;
                }
            }
            if (!columnValid && !rowValid)
            {
                Interaction.MsgBox("Ship Invalid. Too Wide.", Microsoft.VisualBasic.MsgBoxStyle.Exclamation,"Error");
                dgvShips.ClearSelection();
                return;
            }
            int count = dgvShips.SelectedCells.Count;
            if (count <= 1)
            {
                Interaction.MsgBox("Ship Invalid. Not Enough Cells Selected.", Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Error");
                dgvShips.ClearSelection();
                return;
            }
            else if (count >= 6)
            {
                Interaction.MsgBox("Ship Invalid. Too Many Cells Selected.", Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Error");
                dgvShips.ClearSelection();
                return;
            }

            for (int j = 0; j < count; j++)
            {
                if (dgvShips.SelectedCells[j].Style.BackColor == Color.Red)
                {
                    Interaction.MsgBox("You already placed a ship there", Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Error");
                    dgvShips.ClearSelection();
                    return;
                }
            }

            for (int i = 0; i < shipSize.Length; i++)
            {
                if (count == shipSize[i] && count != 3)
                {
                    if (shipsPlaced[i] == true)
                    {
                        Interaction.MsgBox("You already placed a " + shipNames[i], Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Error");
                        dgvShips.ClearSelection();
                        return;
                    }
                    shipsPlaced[i] = true;
                    ColourShip(count);
                    return;
                }
                else if (count == 3) // Edge Case
                {
                    if (shipsPlaced[2] != true)
                    {
                        shipsPlaced[2] = true;
                        ColourShip(count);
                        return;
                    }
                    if (shipsPlaced[3] != true)
                    {
                        shipsPlaced[3] = true;
                        ColourShip(count);
                        return;
                    }
                    Interaction.MsgBox("You already placed a " + shipNames[2] + "/" + shipNames[3], Microsoft.VisualBasic.MsgBoxStyle.Exclamation,"Error");
                    return;
                }
            }
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            CPUShip = new bool[10, 10];
            Carrier = new bool[10, 10];
            CarrierAlive = true;

            Battleship = new bool[10, 10];
            BattleshipAlive = true;

            Cruiser = new bool[10, 10];
            CruiserAlive = true;

            Submarine = new bool[10, 10];
            SubmarineAlive = true;

            Destroyer = new bool[10, 10];
            DestroyerAlive = true;

            for (int i = 0; i < shipsPlaced.Length; i++)
            {
                if (!shipsPlaced[i])
                {
                    Interaction.MsgBox("You need to place all of your ships", Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Error");
                    return;
                }
            }

            btnClear.Enabled = false;
            btnStart.Enabled = false;
            btnReset.Enabled = true;
            placing = false;
            dgvShips.ClearSelection();
            for (int i = 0; i < 5; i++)
            {
                int randX = rnd.Next(0, 10);
                int randY = rnd.Next(0, 10);
                if (CPUShip[randX, randY] == false)
                {
                    CPUShip[randX, randY] = true;
                    Carrier[randX, randY] = true;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                int randX = rnd.Next(0, 10);
                int randY = rnd.Next(0, 10);
                if (CPUShip[randX, randY] == false)
                {
                    CPUShip[randX, randY] = true;
                    Battleship[randX, randY] = true;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                int randX = rnd.Next(0, 10);
                int randY = rnd.Next(0, 10);
                if (CPUShip[randX, randY] == false)
                {
                    CPUShip[randX, randY] = true;
                    Cruiser[randX, randY] = true;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                int randX = rnd.Next(0, 10);
                int randY = rnd.Next(0, 10);
                if (CPUShip[randX, randY] == false)
                {
                    CPUShip[randX, randY] = true;
                    Submarine[randX, randY] = true;
                }
            }
            for (int i = 0; i < 2; i++)
            {
                int randX = rnd.Next(0, 10);
                int randY = rnd.Next(0, 10);
                if (CPUShip[randX, randY] == false)
                {
                    CPUShip[randX, randY] = true;
                    Destroyer[randX, randY] = true;
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvShips.ClearSelection();
            for (int i = 0; i < shipsPlaced.Length; i++)
            {
                shipsPlaced[i] = false;
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    dgvShips[i, j].Style.BackColor = Color.White;
                }
            }
        }
        private void dgvShips_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            PlaceShip();
        }

        private void btnMute_Click(object sender, EventArgs e)
        {
            btnMute.Enabled = false;
            btnUnmute.Enabled = true;
            wplayer.controls.stop();
            muted = true;
        }

        private void btnUnmute_Click(object sender, EventArgs e)
        {
            btnUnmute.Enabled = false;
            btnMute.Enabled = true;
            muted = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            btnReset.Enabled = false;
            btnClear.Enabled = true;
            btnStart.Enabled = true;
            placing = true;
            end = false;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    dgvShips[i, j].Style.BackColor = Color.White;
                    dgvGrid[i, j].Style.BackColor = Color.White;
                }
            }
            for (int i = 0; i < shipsPlaced.Length; i++)
            {
                shipsPlaced[i] = false;
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            if (noahMode)
            {
                Interaction.MsgBox("You don't deserve help.", Microsoft.VisualBasic.MsgBoxStyle.Information, "Help");
                return;
            }
            if (placing)
            {
                Interaction.MsgBox("To place your ships, drag your cursor over the grid with the length of the ship you want to create.", Microsoft.VisualBasic.MsgBoxStyle.Information, "Help");
                return;
            } 
            if (end)
            {
                Interaction.MsgBox("To reset the game, press 'Reset Game'.", Microsoft.VisualBasic.MsgBoxStyle.Information, "Help");
                return;
            }
            Interaction.MsgBox("To fire, double click on the cell that you want to shoot.", Microsoft.VisualBasic.MsgBoxStyle.Information, "Help");
        }
        public void CheckShips()
        {
            bool check = false;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Carrier[i, j] == true)
                    {
                        check = true;
                    }
                }
            }
            if (!check && CarrierAlive)
            {
                Interaction.MsgBox("You have destroyed the Carrier!", Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Carrier");
                CarrierAlive = false;
                return;
            }

            check = false;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Battleship[i, j] == true)
                    {
                        check = true;
                    }
                }
            }
            if (!check && BattleshipAlive)
            {
                Interaction.MsgBox("You have destroyed the Carrier!", Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Carrier");
                BattleshipAlive = false;
                return;
            }

            check = false;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Cruiser[i, j] == true)
                    {
                        check = true;
                    }
                }
            }
            if (!check && CruiserAlive)
            {
                Interaction.MsgBox("You have destroyed the Cruiser!", Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Carrier");
                CruiserAlive = false;
                return;
            }

            check = false;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Submarine[i, j] == true)
                    {
                        check = true;
                    }
                }
            }
            if (!check && SubmarineAlive)
            {
                Interaction.MsgBox("You have destroyed the Submarine!", Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Carrier");
                SubmarineAlive = false;
                return;
            }

            check = false;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Destroyer[i, j] == true)
                    {
                        check = true;
                    }
                }
            }
            if (!check && DestroyerAlive)
            {
                Interaction.MsgBox("You have destroyed the Destroyer!", Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Carrier");
                DestroyerAlive = false;
                return;
            }
        }
        private void dgvGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvGrid.ClearSelection();
            if (placing || end)
            {
                return;
            }
            int x = e.ColumnIndex;
            int y = e.RowIndex;
            bool shoot = true;
            if (x < 0 || y < 0)
            {
                return;
            }
            if (dgvGrid[x,y].Style.BackColor == Color.Gray || dgvGrid[x, y].Style.BackColor == Color.Black)
            {
                Interaction.MsgBox("You have already hit there.", Microsoft.VisualBasic.MsgBoxStyle.Information, "Error");
                return;
            }
            if (noahMode && rnd.NextDouble() > 0.6)
            {
                shoot = false;
                Interaction.MsgBox("The gun malfunctioned and got stuck.", Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Error");
            }
            if (shoot)
            {
                if (CPUShip[x, y] == true)
                {
                    dgvGrid[x, y].Style.BackColor = Color.Black;
                    CPUShip[x, y] = false;

                    Carrier[x, y] = false;
                    Battleship[x, y] = false;
                    Cruiser[x, y] = false;
                    Submarine[x, y] = false;
                    Destroyer[x, y] = false;
                }
                else
                {
                    dgvGrid[x, y].Style.BackColor = Color.Gray;
                }
            }
            CheckShips();
            if (!CarrierAlive && !BattleshipAlive && !CruiserAlive && !SubmarineAlive && !DestroyerAlive)
            {
                end = true;
                PlayGoodSound();
                Interaction.MsgBox("You Won!");
                return;
            }
            while (true)
            {
                int x2 = rnd.Next(0, 10);
                int y2 = rnd.Next(0, 10);
                if (dgvShips[x2, y2].Style.BackColor != Color.Black && dgvShips[x2, y2].Style.BackColor != Color.Gray)
                {
                    if (dgvShips[x2, y2].Style.BackColor == Color.Red)
                    {
                        dgvShips[x2, y2].Style.BackColor = Color.Black;
                        break;
                    } else
                    {
                        dgvShips[x2, y2].Style.BackColor = Color.Gray;
                        break;
                    }
                }
            }
            bool hasLost = true;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (dgvShips[i, j].Style.BackColor == Color.Red)
                    {
                        hasLost = false;
                    }
                }
            }
            if (hasLost)
            {
                end = true;
                PlayBadSound();
                Interaction.MsgBox("You Lost!");
                return;
            }
        }
    }
}