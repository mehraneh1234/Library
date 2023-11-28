namespace library
{
    partial class Library
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lstViewBooks = new System.Windows.Forms.ListView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnBorrow = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lstViewSelectedBooks = new System.Windows.Forms.ListView();
            this.statusStripLibrary = new System.Windows.Forms.StatusStrip();
            this.btnSelected = new System.Windows.Forms.Button();
            this.btnUnselected = new System.Windows.Forms.Button();
            this.btnLoadStudent = new System.Windows.Forms.Button();
            this.btnSearchStudent = new System.Windows.Forms.Button();
            this.btnFine = new System.Windows.Forms.Button();
            this.btnSearchBorrowedBook = new System.Windows.Forms.Button();
            this.btnSearchUsersBorrow = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1004, 52);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(135, 40);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Search-Book";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(373, 474);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(126, 40);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Login";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lstViewBooks
            // 
            this.lstViewBooks.HideSelection = false;
            this.lstViewBooks.Location = new System.Drawing.Point(738, 130);
            this.lstViewBooks.Name = "lstViewBooks";
            this.lstViewBooks.Size = new System.Drawing.Size(530, 303);
            this.lstViewBooks.TabIndex = 2;
            this.lstViewBooks.UseCompatibleStateImageBehavior = false;
            this.lstViewBooks.SelectedIndexChanged += new System.EventHandler(this.lstViewBooks_SelectedIndexChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(125, 59);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(409, 26);
            this.txtSearch.TabIndex = 3;
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(247, 474);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(131, 40);
            this.btnReturn.TabIndex = 4;
            this.btnReturn.Text = "Return";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnBorrow
            // 
            this.btnBorrow.Location = new System.Drawing.Point(108, 474);
            this.btnBorrow.Name = "btnBorrow";
            this.btnBorrow.Size = new System.Drawing.Size(141, 40);
            this.btnBorrow.TabIndex = 5;
            this.btnBorrow.Text = "Borrow";
            this.btnBorrow.UseVisualStyleBackColor = true;
            this.btnBorrow.Click += new System.EventHandler(this.btnBorrow_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Search:";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(1135, 52);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(133, 40);
            this.btnLoad.TabIndex = 7;
            this.btnLoad.Text = "Load-Book";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lstViewSelectedBooks
            // 
            this.lstViewSelectedBooks.HideSelection = false;
            this.lstViewSelectedBooks.Location = new System.Drawing.Point(59, 130);
            this.lstViewSelectedBooks.Name = "lstViewSelectedBooks";
            this.lstViewSelectedBooks.Size = new System.Drawing.Size(498, 303);
            this.lstViewSelectedBooks.TabIndex = 9;
            this.lstViewSelectedBooks.UseCompatibleStateImageBehavior = false;
            // 
            // statusStripLibrary
            // 
            this.statusStripLibrary.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStripLibrary.Location = new System.Drawing.Point(0, 607);
            this.statusStripLibrary.Name = "statusStripLibrary";
            this.statusStripLibrary.Size = new System.Drawing.Size(1325, 22);
            this.statusStripLibrary.TabIndex = 10;
            this.statusStripLibrary.Text = "statusStrip1";
            // 
            // btnSelected
            // 
            this.btnSelected.Location = new System.Drawing.Point(604, 214);
            this.btnSelected.Name = "btnSelected";
            this.btnSelected.Size = new System.Drawing.Size(75, 59);
            this.btnSelected.TabIndex = 11;
            this.btnSelected.Text = "<<";
            this.btnSelected.UseVisualStyleBackColor = true;
            this.btnSelected.Click += new System.EventHandler(this.btnSelected_Click);
            // 
            // btnUnselected
            // 
            this.btnUnselected.Location = new System.Drawing.Point(604, 293);
            this.btnUnselected.Name = "btnUnselected";
            this.btnUnselected.Size = new System.Drawing.Size(75, 59);
            this.btnUnselected.TabIndex = 12;
            this.btnUnselected.Text = ">>";
            this.btnUnselected.UseVisualStyleBackColor = true;
            this.btnUnselected.Click += new System.EventHandler(this.btnUnselected_Click);
            // 
            // btnLoadStudent
            // 
            this.btnLoadStudent.Location = new System.Drawing.Point(1133, 477);
            this.btnLoadStudent.Name = "btnLoadStudent";
            this.btnLoadStudent.Size = new System.Drawing.Size(133, 40);
            this.btnLoadStudent.TabIndex = 14;
            this.btnLoadStudent.Text = "Load-User";
            this.btnLoadStudent.UseVisualStyleBackColor = true;
            this.btnLoadStudent.Click += new System.EventHandler(this.btnLoadStudent_Click);
            // 
            // btnSearchStudent
            // 
            this.btnSearchStudent.Location = new System.Drawing.Point(999, 477);
            this.btnSearchStudent.Name = "btnSearchStudent";
            this.btnSearchStudent.Size = new System.Drawing.Size(135, 40);
            this.btnSearchStudent.TabIndex = 13;
            this.btnSearchStudent.Text = "Search-User";
            this.btnSearchStudent.UseVisualStyleBackColor = true;
            this.btnSearchStudent.Click += new System.EventHandler(this.btnSearchStudent_Click);
            // 
            // btnFine
            // 
            this.btnFine.Location = new System.Drawing.Point(600, 477);
            this.btnFine.Name = "btnFine";
            this.btnFine.Size = new System.Drawing.Size(95, 40);
            this.btnFine.TabIndex = 16;
            this.btnFine.Text = "Pay-fine";
            this.btnFine.UseVisualStyleBackColor = true;
            this.btnFine.Click += new System.EventHandler(this.btnFine_Click);
            // 
            // btnSearchBorrowedBook
            // 
            this.btnSearchBorrowedBook.Location = new System.Drawing.Point(738, 52);
            this.btnSearchBorrowedBook.Name = "btnSearchBorrowedBook";
            this.btnSearchBorrowedBook.Size = new System.Drawing.Size(266, 40);
            this.btnSearchBorrowedBook.TabIndex = 17;
            this.btnSearchBorrowedBook.Text = "Search-Users-Borrow";
            this.btnSearchBorrowedBook.UseVisualStyleBackColor = true;
            this.btnSearchBorrowedBook.Click += new System.EventHandler(this.btnSearchUsersBorrowedBook_Click);
            // 
            // btnSearchUsersBorrow
            // 
            this.btnSearchUsersBorrow.Location = new System.Drawing.Point(738, 477);
            this.btnSearchUsersBorrow.Name = "btnSearchUsersBorrow";
            this.btnSearchUsersBorrow.Size = new System.Drawing.Size(261, 40);
            this.btnSearchUsersBorrow.TabIndex = 18;
            this.btnSearchUsersBorrow.Text = "Search-Borrow-Books/Users ";
            this.btnSearchUsersBorrow.UseVisualStyleBackColor = true;
            this.btnSearchUsersBorrow.Click += new System.EventHandler(this.btnSearchBooksAndUsersBorrow_Click);
            // 
            // Library
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1325, 629);
            this.Controls.Add(this.btnSearchUsersBorrow);
            this.Controls.Add(this.btnSearchBorrowedBook);
            this.Controls.Add(this.btnFine);
            this.Controls.Add(this.btnLoadStudent);
            this.Controls.Add(this.btnSearchStudent);
            this.Controls.Add(this.btnUnselected);
            this.Controls.Add(this.btnSelected);
            this.Controls.Add(this.statusStripLibrary);
            this.Controls.Add(this.lstViewSelectedBooks);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBorrow);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lstViewBooks);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSearch);
            this.Name = "Library";
            this.Text = "Library";
            this.Load += new System.EventHandler(this.Library_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListView lstViewBooks;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnBorrow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ListView lstViewSelectedBooks;
        private System.Windows.Forms.StatusStrip statusStripLibrary;
        private System.Windows.Forms.Button btnSelected;
        private System.Windows.Forms.Button btnUnselected;
        private System.Windows.Forms.Button btnLoadStudent;
        private System.Windows.Forms.Button btnSearchStudent;
        private System.Windows.Forms.Button btnFine;
        private System.Windows.Forms.Button btnSearchBorrowedBook;
        private System.Windows.Forms.Button btnSearchUsersBorrow;
    }
}