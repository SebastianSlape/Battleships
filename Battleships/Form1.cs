using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using UtilityFunctions;
using SoundFunctions;
using ShipFunctions;
using CPUFunctions;

namespace Battleships
{
    public partial class frmBattleship : Form
    {
        Utility utility = new Utility();
        Sound sound = new Sound();
        Random rnd = new Random();
        Ship ship = new Ship();
        CPU cpu = new CPU();

        bool noahMode = false;
        string state = "placing";

        char[] letters = new char[10] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        
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

        bool[,] ColumnCheck = new bool[10, 10];
        bool[,] RowCheck = new bool[10, 10];

        bool shoot = true;
        bool bedtime = false;

        public frmBattleship()
        {
            InitializeComponent();
        }
        public bool BedtimeCheck() // Check if it is bedtime for you know who
        {
            DateTime localDate = DateTime.Now;
            if (noahMode)
            {
                return false;
            }
            if (Convert.ToInt32(localDate.ToString("HH")) < 6 || Convert.ToInt32(localDate.ToString("HH")) > 20)
            {
                Interaction.MsgBox("Go to bed");
                if (bedtime == false)
                {
                    bedtime = true;
                    sound.SoundEffect("bedtime", btnMute);
                }
                return true;
            }
            return false;
        }
        private void frmBattleship_Load(object sender, EventArgs e)
        {
            string name = string.Join(' ', Regex.Split(utility.GetNameOfUser(), @"(?<!^)(?=[A-Z])")); // Regex to Seperate Name by Capitalization ("JohnDoe" => "John Doe")

            if (name == "Noah Casey" || name == "noahc") // Check if it is you know who
            {
                noahMode = true;
            }

            if (noahMode)
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                utility.CreateFile(desktopPath + "\\OPENME.txt", "I know where you are " + name);
                Interaction.MsgBox("Open your desktop.");
            } else
            {
                Interaction.MsgBox("To place your ships, drag your cursor over your grid with the length of the ship you want to create.", Microsoft.VisualBasic.MsgBoxStyle.Information, "Help");
            }

            // Set row and column counts
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
            string shipsToPlace = "Ships to place:\n";

            if (shipsPlaced[0] == false)
            {
                shipsToPlace += "Carrier - 5 long ship\n";
            }
            if (shipsPlaced[1] == false)
            {
                shipsToPlace += "Battleship - 4 long ship\n";
            }
            if (shipsPlaced[2] == false)
            {
                shipsToPlace += "Cruiser - 3 long ship\n";
            }
            if (shipsPlaced[3] == false)
            {
                shipsToPlace += "Submarine - 3 long ship\n";
            }
            if (shipsPlaced[4] == false)
            {
                shipsToPlace += "Destroyer - 2 long ship\n";
            }
            rtbShipsToPlace.Text = shipsToPlace;
            BedtimeCheck();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (BedtimeCheck())
            {
                return;
            }

            // Reset values
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

            shoot = true;
            rtbShipsToPlace.Clear();

            for (int i = 0; i < shipsPlaced.Length; i++) // Check if all ships are placed or not
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
            state = "playing";
            dgvShips.ClearSelection();

            // Place all ships
            ship.PlaceCPUShip(Carrier, CPUShip, 5);
            ship.PlaceCPUShip(Battleship, CPUShip, 4);
            ship.PlaceCPUShip(Cruiser, CPUShip, 3);
            ship.PlaceCPUShip(Submarine, CPUShip, 3);
            ship.PlaceCPUShip(Destroyer, CPUShip, 2);
            if (!noahMode)
            {
                Interaction.MsgBox("To fire, double click on the cell that you want to shoot. If the cell becomes black, you hit a ship. If the cell becomes grey, you didn't hit a ship", Microsoft.VisualBasic.MsgBoxStyle.Information, "Help");
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (BedtimeCheck())
            {
                return;
            }
            // Clear ships placed
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
            string shipsToPlace = "Ships to place:\n";

            if (shipsPlaced[0] == false)
            {
                shipsToPlace += "Carrier - 5 long ship\n";
            }
            if (shipsPlaced[1] == false)
            {
                shipsToPlace += "Battleship - 4 long ship\n";
            }
            if (shipsPlaced[2] == false)
            {
                shipsToPlace += "Cruiser - 3 long ship\n";
            }
            if (shipsPlaced[3] == false)
            {
                shipsToPlace += "Submarine - 3 long ship\n";
            }
            if (shipsPlaced[4] == false)
            {
                shipsToPlace += "Destroyer - 2 long ship\n";
            }
            rtbShipsToPlace.Text = shipsToPlace;
        }
        private void dgvShips_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (BedtimeCheck())
            {
                return;
            }
            string shipsToPlace = "Ships to place:\n";
            
            ship.PlaceShip(dgvShips, shipNames, shipSize, shipsPlaced, state, btnMute, sound);

            if (shipsPlaced[0] == false)
            {
                shipsToPlace += "Carrier - 5 long ship\n";
            }
            if (shipsPlaced[1] == false)
            {
                shipsToPlace += "Battleship - 4 long ship\n";
            }
            if (shipsPlaced[2] == false)
            {
                shipsToPlace += "Cruiser - 3 long ship\n";
            }
            if (shipsPlaced[3] == false)
            {
                shipsToPlace += "Submarine - 3 long ship\n";
            }
            if (shipsPlaced[4] == false)
            {
                shipsToPlace += "Destroyer - 2 long ship\n";
            }
            rtbShipsToPlace.Text = shipsToPlace;
        }

        private void btnMute_Click(object sender, EventArgs e)
        {
            if (BedtimeCheck())
            {
                return;
            }
            if (noahMode)
            {
                sound.SoundEffect("troll", btnMute);
                return;
            }
            btnMute.Enabled = false;
            btnUnmute.Enabled = true;
            sound.SoundEffect("mute", btnMute);
            return;
        }

        private void btnUnmute_Click(object sender, EventArgs e)
        {
            if (BedtimeCheck())
            {
                return;
            }
            btnUnmute.Enabled = false;
            btnMute.Enabled = true;
            sound.SoundEffect("mute", btnMute);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (BedtimeCheck())
            {
                return;
            }
            // Reset variables, text and grid colours
            rtbShipsLeft.Clear();
            btnReset.Enabled = false;
            btnClear.Enabled = true;
            btnStart.Enabled = true;
            state = "placing";
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
            if (BedtimeCheck())
            {
                return;
            }
            if (noahMode)
            {
                Interaction.MsgBox("You don't deserve help.", Microsoft.VisualBasic.MsgBoxStyle.Information, "Help");
                return;
            }
            if (state == "placing")
            {
                Interaction.MsgBox("To place your ships, drag your cursor over your grid with the length of the ship you want to create.", Microsoft.VisualBasic.MsgBoxStyle.Information, "Help");
                return;
            } 
            if (state == "end")
            {
                Interaction.MsgBox("To reset the game, press the 'Reset Game' button located on the ribbon.", Microsoft.VisualBasic.MsgBoxStyle.Information, "Help");
                return;
            }
            Interaction.MsgBox("To fire, double click on the cell that you want to shoot. If the cell becomes black, you hit a ship. If the cell becomes grey, you didn't hit a ship", Microsoft.VisualBasic.MsgBoxStyle.Information, "Help");
        }
        public void CheckShips()
        {
            string shipString = "Ships Alive:\n";
            bool check = false;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Carrier[i, j] == true)
                    {
                        check = true; // Check if there is still a piece of the carrier left
                    }
                }
            }
            if (!check && CarrierAlive)
            {
                Interaction.MsgBox("You have destroyed the Carrier!", Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Carrier");
                CarrierAlive = false;
            }
            if (CarrierAlive)
            {
                shipString += "Carrier - 5 long ship\n";
            }

            check = false;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Battleship[i, j] == true)
                    {
                        check = true; // Check if there is still a piece of the battleship left
                    }
                }
            }
            if (!check && BattleshipAlive)
            {
                Interaction.MsgBox("You have destroyed the Battleship!", Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Carrier");
                BattleshipAlive = false;
            }
            if (BattleshipAlive)
            {
                shipString += "Battleship - 4 long ship\n";
            }

            check = false;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Cruiser[i, j] == true)
                    {
                        check = true; // Check if there is still a piece of the cruiser left
                    }
                }
            }
            if (!check && CruiserAlive)
            {
                Interaction.MsgBox("You have destroyed the Cruiser!", Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Carrier");
                CruiserAlive = false;
            }
            if (CruiserAlive)
            {
                shipString += "Cruiser - 3 long ship\n";
            }

            check = false;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Submarine[i, j] == true)
                    {
                        check = true; // Check if there is still a piece of the submarine left
                    }
                }
            }
            if (!check && SubmarineAlive)
            {
                Interaction.MsgBox("You have destroyed the Submarine!", Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Carrier");
                SubmarineAlive = false;
            }
            if (SubmarineAlive)
            {
                shipString += "Submarine - 3 long ship\n";
            }

            check = false;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Destroyer[i, j] == true)
                    {
                        check = true; // Check if there is still a piece of the destroyer left
                    }
                }
            }
            if (!check && DestroyerAlive)
            {
                Interaction.MsgBox("You have destroyed the Destroyer!", Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Carrier");
                DestroyerAlive = false;
            }
            if (DestroyerAlive)
            {
                shipString += "Destroyer - 2 long ship\n";
            }
            rtbShipsLeft.Text = shipString;
        }
        private void CPUShoot()
        {
            // Wait a second before cpu shoots
            var t = Task.Run(async delegate
            {
                await Task.Delay(1000);
                return 42;
            });
            t.Wait();

            cpu.Shoot(ColumnCheck, RowCheck, dgvShips, noahMode);

            sound.SoundEffect("shoot", btnMute);
            bool hasLost = true;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (dgvShips[i, j].Style.BackColor == Color.Red) // If player ship still exists, set "hasLost" to false
                    {
                        hasLost = false;
                    }
                }
            }
            shoot = true;
            if (hasLost)
            {
                state = "end";
                if (noahMode)
                {
                    sound.SoundEffect("good", btnMute);
                    Interaction.MsgBox("You lost the game LOL!", Microsoft.VisualBasic.MsgBoxStyle.Information, "Game state");
                    return;
                }
                sound.SoundEffect("bad", btnMute);
                Interaction.MsgBox("You lost the game!", Microsoft.VisualBasic.MsgBoxStyle.Information, "Game state");
                Interaction.MsgBox("To reset the game, press the 'Reset Game' button located on the ribbon.", Microsoft.VisualBasic.MsgBoxStyle.Information, "Help");
                return;
            }
        }
        private void dgvGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (BedtimeCheck())
            {
                return;
            }
            dgvGrid.ClearSelection();
            if (state != "playing")
            {
                return;
            }
            // Get x and y of where the player clicked on the grid
            int x = e.ColumnIndex;
            int y = e.RowIndex;
            
            if (x < 0 || y < 0)
            {
                return;
            }
            if (dgvGrid[x,y].Style.BackColor == Color.Gray || dgvGrid[x, y].Style.BackColor == Color.Black)
            {
                Interaction.MsgBox("You have already hit there.", Microsoft.VisualBasic.MsgBoxStyle.Information, "Error");
                return;
            }
            if (noahMode && rnd.NextDouble() > 0.7) // If you know who is playing and he gets unlucky, jam his gun
            {
                shoot = false;
                Interaction.MsgBox("The gun malfunctioned and got jammed.", Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Error");
                CheckShips();
                if (!CarrierAlive && !BattleshipAlive && !CruiserAlive && !SubmarineAlive && !DestroyerAlive)
                {
                    state = "end";
                    if (noahMode)
                    {
                        sound.SoundEffect("bad", btnMute);
                        Interaction.MsgBox("You won the game... CHEATER!", Microsoft.VisualBasic.MsgBoxStyle.Information, "Game state");
                        return;
                    }
                    sound.SoundEffect("good", btnMute);
                    Interaction.MsgBox("You won the game!", Microsoft.VisualBasic.MsgBoxStyle.Information, "Game state");
                    Interaction.MsgBox("To reset the game, press the 'Reset Game' button located on the ribbon.", Microsoft.VisualBasic.MsgBoxStyle.Information, "Help");
                    return;
                }
                Thread thread = new Thread(CPUShoot);
                thread.Start();
            }
            if (shoot)
            {
                shoot = false;
                sound.SoundEffect("shoot", btnMute);
                if (CPUShip[x, y] == true) // If there is a ship there, set the position of that to false in all ship arrays
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
                CheckShips();
                if (!CarrierAlive && !BattleshipAlive && !CruiserAlive && !SubmarineAlive && !DestroyerAlive)
                {
                    state = "end";
                    if (noahMode)
                    {
                        sound.SoundEffect("bad", btnMute);
                        Interaction.MsgBox("You won the game... CHEATER!", Microsoft.VisualBasic.MsgBoxStyle.Information, "Game state");
                        return;
                    }
                    sound.SoundEffect("good", btnMute);
                    Interaction.MsgBox("You won the game!", Microsoft.VisualBasic.MsgBoxStyle.Information, "Game state");
                    return;
                }
                Thread thread = new Thread(CPUShoot);
                thread.Start();
            }
        }
    }
}