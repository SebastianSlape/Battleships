namespace Battleships
{
    partial class Form1
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
            this.dgvPlace = new System.Windows.Forms.DataGridView();
            this.btnPlaceShips = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlace)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPlace
            // 
            this.dgvPlace.AllowUserToAddRows = false;
            this.dgvPlace.AllowUserToDeleteRows = false;
            this.dgvPlace.AllowUserToResizeColumns = false;
            this.dgvPlace.AllowUserToResizeRows = false;
            this.dgvPlace.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPlace.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPlace.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlace.Location = new System.Drawing.Point(232, 49);
            this.dgvPlace.Name = "dgvPlace";
            this.dgvPlace.RowHeadersWidth = 51;
            this.dgvPlace.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvPlace.RowTemplate.Height = 29;
            this.dgvPlace.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvPlace.Size = new System.Drawing.Size(516, 348);
            this.dgvPlace.TabIndex = 0;
            // 
            // btnPlaceShips
            // 
            this.btnPlaceShips.Location = new System.Drawing.Point(34, 49);
            this.btnPlaceShips.Name = "btnPlaceShips";
            this.btnPlaceShips.Size = new System.Drawing.Size(151, 54);
            this.btnPlaceShips.TabIndex = 1;
            this.btnPlaceShips.Text = "Place Ships";
            this.btnPlaceShips.UseVisualStyleBackColor = true;
            this.btnPlaceShips.Click += new System.EventHandler(this.btnPlaceShips_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnPlaceShips);
            this.Controls.Add(this.dgvPlace);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlace)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dgvPlace;
        private Button btnPlaceShips;
    }
}