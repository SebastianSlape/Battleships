using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using SoundFunctions;

namespace ShipFunctions
{
    public class Ship
    {
        Random rnd = new Random();
        public void PlaceCPUShip(bool[,] shipGrid, bool[,] CPUShip, int shipLength)
        {
            bool shipPlaced = false;

            while (!shipPlaced)
            {
                bool shipValid = true;
                int randX = rnd.Next(0, 10); // Get random x value
                int randY = rnd.Next(0, 10); // Get random y value
                int randDirection = rnd.Next(0, 2); // Get random ship direction
                if (randDirection == 0)
                {
                    if (randX + shipLength < 10) // If the x position + the ship length goes outside of the array, the ship is invalid
                    {
                        for (int i = 0; i < shipLength; i++) // Loop through the length of the ship
                        {
                            if (CPUShip[randX + i, randY] == true) // If there is a ship found, the ship is invalid
                            {
                                shipValid = false;
                            }
                        }
                    }
                    else
                    {
                        shipValid = false;
                    }
                }
                else if (randDirection == 1)
                {
                    if (randY + shipLength < 10) // If the y position + the ship length goes outside of the array, the ship is invalid
                    {
                        for (int i = 0; i < shipLength; i++) // Loop through the length of the ship
                        {
                            if (CPUShip[randX, randY + i] == true) // If there is a ship found, the ship is invalid
                            {
                                shipValid = false;
                            }
                        }
                    }
                    else
                    {
                        shipValid = false;
                    }
                }
                if (shipValid) // If the ship is valid, loop through the ship and set the ship piece positions to true
                {
                    for (int i = 0; i < shipLength; i++)
                    {
                        if (randDirection == 0)
                        {
                            CPUShip[randX + i, randY] = true;
                            shipGrid[randX + i, randY] = true;
                        }
                        else if (randDirection == 1)
                        {
                            CPUShip[randX, randY + i] = true;
                            shipGrid[randX, randY + i] = true;
                        }
                    }
                    shipPlaced = true;
                }
            }
        }
        public void ColourShip(int count, DataGridView dgvShips, Button btnMute, Sound sound)
        {
            for (int j = 0; j < count; j++) // Loop through the selected cells and colour them red
            {
                dgvShips.SelectedCells[j].Style.BackColor = Color.Red;
            }
            dgvShips.ClearSelection();
            sound.SoundEffect("placing", btnMute);
        }
        public void PlaceShip(DataGridView dgvShips, string[] shipNames, int[] shipSize, bool[] shipsPlaced, string state, Button btnMute, Sound sound)
        {
            if (state != "placing")
            {
                return;
            }
            if (dgvShips.SelectedCells.Count <= 0)
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
                Interaction.MsgBox("Ship Invalid. Too Wide.", Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Error");
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
                    ColourShip(count, dgvShips, btnMute, sound);
                    return;
                }
                else if (count == 3) // Edge Case
                {
                    if (shipsPlaced[2] != true)
                    {
                        shipsPlaced[2] = true;
                        ColourShip(count, dgvShips, btnMute, sound);
                        return;
                    }
                    if (shipsPlaced[3] != true)
                    {
                        shipsPlaced[3] = true;
                        ColourShip(count, dgvShips, btnMute, sound);
                        return;
                    }
                    Interaction.MsgBox("You already placed a " + shipNames[2] + "/" + shipNames[3], Microsoft.VisualBasic.MsgBoxStyle.Exclamation, "Error");
                    return;
                }
            }
        }
    }
}
