using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUFunctions
{
    public class CPU
    {
        Random rnd = new Random();
        public void Shoot(bool[,] ColumnCheck, bool[,] RowCheck, DataGridView dgvShips, bool noahMode)
        {
            if (noahMode) // If it is you know who, never miss
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (dgvShips[j, i].Style.BackColor == Color.Red)
                        {
                            dgvShips[j, i].Style.BackColor = Color.Black;
                        }
                    }
                }
                return;
            }
            for (int i = 0; i < 10; i++) // Loop through the array and check if there is a checking square
            {
                for (int j = 0; j < 10; j++)
                {
                    if (ColumnCheck[i, j] == true)
                    {
                        ColumnCheck[i, j] = false;
                        if (dgvShips[i, j].Style.BackColor == Color.Black || dgvShips[i, j].Style.BackColor == Color.Gray)
                        {
                            continue;
                        }
                        if (dgvShips[i, j].Style.BackColor == Color.Red)
                        {
                            for (int a = 0; a < 10; a++) // Clear row check as ship is on a column
                            {
                                for (int b = 0; b < 10; b++)
                                {
                                    RowCheck[a, b] = false;
                                }
                            }
                            dgvShips[i, j].Style.BackColor = Color.Black;
                            if (j <= 8 && j >= 0) // Add new squares to check
                            {
                                ColumnCheck[i, j + 1] = true;
                            }
                            if (j <= 9 && j >= 1)
                            {
                                ColumnCheck[i, j - 1] = true;
                            }
                        }
                        else
                        {
                            dgvShips[i, j].Style.BackColor = Color.Gray;
                        }
                        return;
                    }
                    if (RowCheck[i, j] == true)
                    {
                        RowCheck[i, j] = false;
                        if (dgvShips[i, j].Style.BackColor == Color.Black || dgvShips[i, j].Style.BackColor == Color.Gray)
                        {
                            continue;
                        }
                        if (dgvShips[i, j].Style.BackColor == Color.Red)
                        {
                            for (int a = 0; a < 10; a++) // Clear column check as ship is on a row
                            {
                                for (int b = 0; b < 10; b++)
                                {
                                    ColumnCheck[a, b] = false;
                                }
                            }
                            dgvShips[i, j].Style.BackColor = Color.Black;
                            if (i <= 8 && i >= 0) // Add new squares to check
                            {
                                RowCheck[i + 1, j] = true;
                            }
                            if (i <= 9 && i >= 1)
                            {
                                RowCheck[i - 1, j] = true;
                            }
                        }
                        else
                        {
                            dgvShips[i, j].Style.BackColor = Color.Gray;
                        }
                        return;
                    }
                }
            }
            while (true) // Randomly choose a square and if it is not already hit, hit it
            {
                int x2 = rnd.Next(0, 10);
                int y2 = rnd.Next(0, 10);
                if (dgvShips[x2, y2].Style.BackColor != Color.Black && dgvShips[x2, y2].Style.BackColor != Color.Gray)
                {
                    if (dgvShips[x2, y2].Style.BackColor == Color.Red)
                    {
                        dgvShips[x2, y2].Style.BackColor = Color.Black;
                        if (y2 <= 8 && y2 >= 0)
                        {
                            ColumnCheck[x2, y2 + 1] = true;
                        }
                        if (y2 <= 9 && y2 >= 1)
                        {
                            ColumnCheck[x2, y2 - 1] = true;
                        }
                        if (x2 <= 8 && x2 >= 0)
                        {
                            RowCheck[x2 + 1, y2] = true;
                        }
                        if (x2 <= 9 && x2 >= 1)
                        {
                            RowCheck[x2 - 1, y2] = true;
                        }
                        break;
                    }
                    else
                    {
                        dgvShips[x2, y2].Style.BackColor = Color.Gray;
                        break;
                    }
                }
            }
        }
    }
}
