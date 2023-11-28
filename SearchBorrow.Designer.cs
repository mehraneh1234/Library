namespace library
{
    partial class SearchBorrow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvUsersBorrowedBook = new System.Windows.Forms.ListView();
            this.lvBorrowedBook = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStripSearchBorrow = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            // 
            // lvUsersBorrowedBook
            // 
            this.lvUsersBorrowedBook.HideSelection = false;
            this.lvUsersBorrowedBook.Location = new System.Drawing.Point(72, 98);
            this.lvUsersBorrowedBook.Name = "lvUsersBorrowedBook";
            this.lvUsersBorrowedBook.Size = new System.Drawing.Size(472, 524);
            this.lvUsersBorrowedBook.TabIndex = 0;
            this.lvUsersBorrowedBook.UseCompatibleStateImageBehavior = false;
            // 
            // lvBorrowedBook
            // 
            this.lvBorrowedBook.HideSelection = false;
            this.lvBorrowedBook.Location = new System.Drawing.Point(589, 98);
            this.lvBorrowedBook.Name = "lvBorrowedBook";
            this.lvBorrowedBook.Size = new System.Drawing.Size(469, 524);
            this.lvBorrowedBook.TabIndex = 1;
            this.lvBorrowedBook.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(72, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(338, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "All users who borrowed books";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label2.Location = new System.Drawing.Point(584, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(222, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "All borrowed books";
            // 
            // statusStripSearchBorrow
            // 
            this.statusStripSearchBorrow.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStripSearchBorrow.Location = new System.Drawing.Point(0, 682);
            this.statusStripSearchBorrow.Name = "statusStripSearchBorrow";
            this.statusStripSearchBorrow.Size = new System.Drawing.Size(1145, 22);
            this.statusStripSearchBorrow.TabIndex = 4;
            this.statusStripSearchBorrow.Text = "statusStrip1";
            // 
            // SearchBorrow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 704);
            this.Controls.Add(this.statusStripSearchBorrow);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvBorrowedBook);
            this.Controls.Add(this.lvUsersBorrowedBook);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SearchBorrow";
            this.Text = "SearchBorrow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvUsersBorrowedBook;
        private System.Windows.Forms.ListView lvBorrowedBook;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStripSearchBorrow;
    }
}