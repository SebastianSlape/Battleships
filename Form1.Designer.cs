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
            this.btnPlaceShip = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
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
            this.dgvPlace.ReadOnly = true;
            this.dgvPlace.RowHeadersWidth = 51;
            this.dgvPlace.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvPlace.RowTemplate.Height = 29;
            this.dgvPlace.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvPlace.Size = new System.Drawing.Size(516, 348);
            this.dgvPlace.TabIndex = 0;
            // 
            // btnPlaceShip
            // 
            this.btnPlaceShip.Location = new System.Drawing.Point(34, 49);
            this.btnPlaceShip.Name = "btnPlaceShip";
            this.btnPlaceShip.Size = new System.Drawing.Size(151, 54);
            this.btnPlaceShip.TabIndex = 1;
            this.btnPlaceShip.Text = "Place Ships";
            this.btnPlaceShip.UseVisualStyleBackColor = true;
            this.btnPlaceShip.Click += new System.EventHandler(this.btnPlaceShip_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(34, 128);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(151, 54);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear Fleet";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(34, 343);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(151, 54);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start Game";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnPlaceShip);
            this.Controls.Add(this.dgvPlace);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlace)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dgvPlace;
        private Button btnPlaceShip;
        private Button btnClear;
        private Button btnStart;
    }
}