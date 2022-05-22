namespace Battleships
{
    partial class frmBattleship
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvShips = new System.Windows.Forms.DataGridView();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.dgvGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShips)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvShips
            // 
            this.dgvShips.AllowUserToAddRows = false;
            this.dgvShips.AllowUserToDeleteRows = false;
            this.dgvShips.AllowUserToResizeColumns = false;
            this.dgvShips.AllowUserToResizeRows = false;
            this.dgvShips.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvShips.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvShips.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShips.Location = new System.Drawing.Point(12, 63);
            this.dgvShips.Name = "dgvShips";
            this.dgvShips.ReadOnly = true;
            this.dgvShips.RowHeadersWidth = 51;
            this.dgvShips.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvShips.RowTemplate.Height = 29;
            this.dgvShips.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvShips.Size = new System.Drawing.Size(472, 312);
            this.dgvShips.TabIndex = 0;
            this.dgvShips.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvShips_CellMouseUp);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(181, 435);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(151, 54);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear Fleet";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 435);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(151, 54);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start Game";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // dgvGrid
            // 
            this.dgvGrid.AllowUserToAddRows = false;
            this.dgvGrid.AllowUserToDeleteRows = false;
            this.dgvGrid.AllowUserToResizeColumns = false;
            this.dgvGrid.AllowUserToResizeRows = false;
            this.dgvGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrid.Location = new System.Drawing.Point(514, 63);
            this.dgvGrid.Name = "dgvGrid";
            this.dgvGrid.ReadOnly = true;
            this.dgvGrid.RowHeadersWidth = 51;
            this.dgvGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvGrid.RowTemplate.Height = 29;
            this.dgvGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvGrid.Size = new System.Drawing.Size(472, 312);
            this.dgvGrid.TabIndex = 4;
            // 
            // frmBattleship
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 505);
            this.Controls.Add(this.dgvGrid);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dgvShips);
            this.Name = "frmBattleship";
            this.Text = "Battleship";
            this.Load += new System.EventHandler(this.frmBattleship_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShips)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dgvShips;
        private Button btnClear;
        private Button btnStart;
        private DataGridView dgvGrid;
    }
}