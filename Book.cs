using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace library
{
    public partial class Book : Form
    {
        List<BookInfo> books = new List<BookInfo>();
        private int lastBookID = 0;
        public Book()
        {
            InitializeComponent();
        }
        private int GenerateBookID()
        {
            return ++lastBookID;
        }

        private void InitializeListView()
        {
            listViewBooks.View = View.Details;
            listViewBooks.Columns.Add("Title");
            listViewBooks.Columns.Add("Authors");
            listViewBooks.Columns.Add("Avl Copies");
        }
        private void Book_Load(object sender, EventArgs e)
        {
            InitializeListView();
        }

        private void DisplayListViewBooks()
        {
            listViewBooks.Items.Clear();
            foreach (BookInfo book in books)
            {
                if
                    (!string.IsNullOrEmpty(book.GetTitle()) &&
                     !string.IsNullOrEmpty(book.GetAuthors()) &&
                     !string.IsNullOrEmpty(book.GetPubDate()) &&
                     !string.IsNullOrEmpty(book.GetCopies()) &&
                     !string.IsNullOrEmpty(book.GetAvlCopies()))
                {
                    ListViewItem item = new ListViewItem(book.GetTitle());
                    item.SubItems.Add(book.GetAuthors());
                    item.SubItems.Add(book.GetAvlCopies());
                    item.SubItems.Add(book.GetPubDate());
                    item.SubItems.Add(book.GetCopies());
                    

                    listViewBooks.Items.Add(item);
                }
            }
        }

        private void clearTextBox()
        {
            txtTitle.Clear();
            txtAuthors.Clear();
            txtPubDate.Clear();
            txtCopies.Clear();
            txtAvailableCopies.Clear();
            txtTitle.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            statusStripBook.Items.Clear();
            string title = txtTitle.Text.Trim();
            string authors = txtAuthors.Text.Trim();
            string pubDate = txtPubDate.Text.Trim();
            string copies = txtCopies.Text.Trim();
            string avlCopies = txtAvailableCopies.Text.Trim();

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(authors) ||
                string.IsNullOrEmpty(pubDate) || string.IsNullOrEmpty(copies) || string.IsNullOrEmpty(avlCopies))
            {
                statusStripBook.Items.Add("Please fill in all fields.");
                return;
            }

            if (books.Any(book => book.GetTitle() == title))
            {
                statusStripBook.Items.Add("Duplicate Title. Please enter a unique name.");
            }

            BookInfo newBook = new BookInfo();
            newBook.SetBookID(Convert.ToString(GenerateBookID()));
            newBook.SetTitle(title);
            newBook.SetAuthors(authors);
            newBook.SetPubDate(pubDate);
            newBook.SetCopies(copies);
            newBook.SetAvlCopies(avlCopies);
            books.Add(newBook);

            clearTextBox();
            DisplayListViewBooks();
            statusStripBook.Items.Add("The new book is added.");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            statusStripBook.Items.Clear();
            BookInfo newBook = new BookInfo();
            if (listViewBooks.SelectedItems.Count > 0)
            {
                int selectedRowIndex = listViewBooks.SelectedIndices[0];
                string title = txtTitle.Text.Trim();
                string authors = txtAuthors.Text.Trim();
                string pubDate = txtPubDate.Text.Trim();
                string copies = txtCopies.Text.Trim();
                string avlCopies = txtAvailableCopies.Text.Trim();

                if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(authors) &&
                !string.IsNullOrEmpty(pubDate) && !string.IsNullOrEmpty(copies) && !string.IsNullOrEmpty(avlCopies))
                {
                    books[selectedRowIndex].SetTitle(title);
                    books[selectedRowIndex].SetAuthors(authors);
                    books[selectedRowIndex].SetPubDate(pubDate);
                    books[selectedRowIndex].SetCopies(copies);
                    books[selectedRowIndex].SetAvlCopies(avlCopies);
                }
            }
            else
            {
                statusStripBook.Items.Add("Please select a row to edit.");
            }
            statusStripBook.Items.Add("The selected row is edited.");
            clearTextBox();
            DisplayListViewBooks();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            statusStripBook.Items.Clear();
            if (listViewBooks.SelectedItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure to delete this data?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    int selectIndex = listViewBooks.SelectedItems[0].Index;
                    books.RemoveAt(selectIndex);
                    DisplayListViewBooks();
                }
            }
            else
            {
                statusStripBook.Items.Add("Please select a row to delete.");
            }
        }

        /* private void btnSearch_Click(object sender, EventArgs e)
         {
             clearTextBox();
             listViewBooks.SelectedItems.Clear();
             statusStripBook.Items.Clear();
             if (!String.IsNullOrEmpty(txtSearch.Text))
             {
                 books.Sort();
                 BookInfo result = books.FirstOrDefault(ds =>
                     ds.GetTitle().Equals(txtSearch.Text, StringComparison.OrdinalIgnoreCase));
                 if (result != null)
                 {
                     int index = books.FindIndex(ds =>
                         ds.GetTitle().Equals(txtSearch.Text, StringComparison.OrdinalIgnoreCase));
                     if (index != -1)
                     {
                         txtTitle.Text = result.GetTitle();
                         txtAuthors.Text = result.GetAuthors();
                         txtPubDate.Text = result.GetPubDate();
                         txtCopies.Text = result.GetCopies();
                         txtAvailableCopies.Text = result.GetAvlCopies();
                         foreach (ListViewItem item in listViewBooks.Items)
                         {
                             if (item.Text == result.GetTitle())
                             {
                                 item.Selected = true;
                                 break;
                             }
                             statusStripBook.Items.Add("The book found.");
                             txtSearch.Clear();
                             txtSearch.Focus();
                         }
                     }
                     else
                     {
                         statusStripBook.Items.Add("Please search another word!");
                     }
                 }
                 else
                 {
                     statusStripBook.Items.Add("The book not found!");
                 }
             }
             else
             {
                 statusStripBook.Items.Add("Please write the title to search!");
             }
         }*/

        private void btnSearch_Click(object sender, EventArgs e)
        {
            clearTextBox();
            listViewBooks.SelectedItems.Clear();
            statusStripBook.Items.Clear();

            if (!String.IsNullOrEmpty(txtSearch.Text))
            {
                string keyword = txtSearch.Text;

                // Set the keyword in each book before sorting
                foreach (var book in books)
                {
                    book.Keyword = keyword;
                }

                // Sort the books using the CompareTo method that considers the keyword
                books.Sort();

               /* // Sort the books using the CompareTo method that considers the keyword
                books.Sort((b1, b2) => b1.CompareTo(b2, keyword));*/

                // Find the first book that matches the search criteria
                BookInfo result = books.FirstOrDefault();

                if (result != null)
                {
                    // Update the UI with the details of the found book
                    txtTitle.Text = result.GetTitle();
                    txtAuthors.Text = result.GetAuthors();
                    txtPubDate.Text = result.GetPubDate();
                    txtCopies.Text = result.GetCopies();
                    txtAvailableCopies.Text = result.GetAvlCopies();

                    // Select the corresponding item in the ListView
                    foreach (ListViewItem item in listViewBooks.Items)
                    {
                        if (item.Text == result.GetTitle())
                        {
                            item.Selected = true;
                            break;
                        }
                    }

                    statusStripBook.Items.Add("The book found.");
                    txtSearch.Clear();
                    txtSearch.Focus();
                }
                else
                {
                    statusStripBook.Items.Add("Please search another word!");
                }
            }
            else
            {
                statusStripBook.Items.Add("Please write the title to search!");
            }
        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
            statusStripBook.Items.Clear();
            listViewBooks.Items.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Binary Files|*.bin|All Files|*.*";
            openFileDialog.FileName = "books.bin";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                try
                {
                    books.Clear();
                    using (Stream stream = File.Open(fileName, FileMode.Open))
                    {
                        using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                        {
                            while (stream.Position < stream.Length)
                            {
                                BookInfo newBook = new BookInfo();
                                newBook.SetBookID(reader.ReadString());
                                newBook.SetTitle(reader.ReadString());
                                newBook.SetAuthors(reader.ReadString());
                                newBook.SetPubDate(reader.ReadString());
                                newBook.SetCopies(reader.ReadString());
                                newBook.SetAvlCopies(reader.ReadString());

                                books.Add(newBook);
                            }
                        }
                    }
                    DisplayListViewBooks();
                    statusStripBook.Items.Add("Data loaded successfully.");
                }
                catch (Exception ex)
                {
                    statusStripBook.Items.Add("Error loading data: " + ex.Message);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            statusStripBook.Items.Clear();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Binary Files|*.bin|All Files|*.*";
            saveFileDialog.FileName = "books.bin";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog.FileName;
                try
                {
                    using (FileStream stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
                    {
                        using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                        {
                            foreach (var newBook in books)
                            {
                                writer.Write(newBook.GetBookID());
                                writer.Write(newBook.GetTitle());
                                writer.Write(newBook.GetAuthors());
                                writer.Write(newBook.GetPubDate());
                                writer.Write(newBook.GetCopies());
                                writer.Write(newBook.GetAvlCopies());
                            }
                        }
                        statusStripBook.Items.Add("Data saved successfully.");
                    }
                }
                catch (Exception ex)
                {
                    statusStripBook.Items.Add("Error saving data: " + ex.Message);
                }
            }
        }

       
        private void Book_FormClosed(object sender, FormClosedEventArgs e)
        {
            btnSave_Click(this, e);
        }

        private void listViewBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            statusStripBook.Items.Clear();
            if (listViewBooks.SelectedItems.Count > 0)
            {
                int index = listViewBooks.SelectedIndices[0];
                txtTitle.Text = books[index].GetTitle();
                txtAuthors.Text = books[index].GetAuthors();
                txtPubDate.Text = books[index].GetPubDate();
                txtCopies.Text = books[index].GetCopies();
                txtAvailableCopies.Text = books[index].GetAvlCopies();
            }
        }
    }
}
