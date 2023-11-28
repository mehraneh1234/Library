using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static library.Login;
using static System.Net.WebRequestMethods;

namespace library
{
    public partial class Library : Form
    {
        private List<BookInfo> books = new List<BookInfo>();
        private List<UserInfo> users = new List<UserInfo>();
        private List<LibraryInfo> libs = new List<LibraryInfo>();
        private List<LibraryInfo> selectedLibraryInfos = new List<LibraryInfo>();
        private List<ListViewItem> selectedItem = new List<ListViewItem>();
        private List<LibraryInfo> booksToRemove = new List<LibraryInfo>();

        private double totalFine = 0;
        private string username;
        private string userRole;

        public void SetUsers(List<UserInfo> users)
        {
            this.users = users;
        }

        public void SetBooks(List<BookInfo> books)
        {
            this.books = books;
        }

        public void SetLibs(List<LibraryInfo> libs)
        {
            this.libs = libs;
        }

        public Library(string userRole)
        {
            InitializeComponent();
            if (userRole == "Guest")
            {
                InitializeGuestFeatures();
            }

            // Load users from file and add them to the users list
            List<UserInfo> loadedUsers = LoadUsersFromDataFile();
            users.AddRange(loadedUsers);
        }

        public Library(string username, string userRole)
        {
            InitializeComponent();
            this.username = username;
            this.userRole = userRole;

            if (userRole  == "Staff")
            {
                InitializeStaffFeatures();
            }
            else if (userRole == "Student")
            {
                InitializeStudentFeatures();
            }

            // Load users from file and add them to the users list
            List<UserInfo> loadedUsers = LoadUsersFromDataFile();
            users.AddRange(loadedUsers);
        }
        private void InitializeStaffFeatures()
        {
            btnReturn.Enabled = true;
            btnSearchStudent.Enabled = true;
            btnSearchBorrowedBook.Enabled = true;
            btnSearchUsersBorrow.Enabled = true;
            btnLoadStudent.Enabled = true;
            btnFine.Enabled = true;
        }
        private void InitializeStudentFeatures()
        {
            btnBorrow.Enabled = true;
            btnFine.Enabled = false;
            btnSearchStudent.Enabled = false;
            btnSearchBorrowedBook.Enabled = false;
            btnSearchUsersBorrow.Enabled = false;
            btnLoadStudent.Enabled = false;
            btnReturn.Enabled = false;
        }
        private void InitializeGuestFeatures()
        {
            btnReturn.Enabled = false;
            btnBorrow.Enabled = false;
            btnSearchStudent.Enabled = false; 
            btnLoadStudent.Enabled = false;           
            btnFine.Enabled = false;
        }
        private void InitializeListView()
        {
            lstViewBooks.View = View.Details;
            lstViewBooks.Columns.Add("Title");
            lstViewBooks.Columns.Add("Authors");
            lstViewBooks.Columns.Add("Pub_date");
            lstViewBooks.Columns.Add("CopiesNO");
            lstViewBooks.Columns.Add("AvailableNo");

            lstViewSelectedBooks.View = View.Details;
            lstViewSelectedBooks.Columns.Add("Selected_Title");
            lstViewSelectedBooks.Columns.Add("Selected_Authors");
        }
        private void InitializeListViewUser()
        {
            lstViewBooks.View = View.Details;
            lstViewBooks.Columns.Add("Name");
            lstViewBooks.Columns.Add("Email");
            lstViewBooks.Columns.Add("Borrowed_Books");
            /*lstViewBooks.Columns.Add("Type");
            lstViewBooks.Columns.Add("Username");*/

            lstViewSelectedBooks.View = View.Details;
            lstViewSelectedBooks.Columns.Add("Title");
            lstViewSelectedBooks.Columns.Add("Name");
            lstViewSelectedBooks.Columns.Add("DueDate");
            lstViewSelectedBooks.Columns.Add("ReturnDate");
            lstViewSelectedBooks.Columns.Add("BookStatus");
            lstViewSelectedBooks.Columns.Add("Fine");
            //lstViewSelectedBooks.Columns.Add("Selected_Username");
            
        }
        private void DisplayListViewBooks()
        {
            lstViewBooks.Items.Clear();
            foreach (BookInfo bInfo in books)
            {
                ListViewItem item = new ListViewItem(bInfo.GetTitle());
                item.SubItems.Add(bInfo.GetAuthors());
                item.SubItems.Add(bInfo.GetPubDate());
                item.SubItems.Add(bInfo.GetCopies());
                item.SubItems.Add(bInfo.GetAvlCopies());
                lstViewBooks.Items.Add(item);
            }
            lstViewBooks.Tag = "Books";
        }
        private void DisplayListViewUsers()
        {
            lstViewBooks.Items.Clear();
            foreach (UserInfo userInfo in users)
            {
                ListViewItem item = //new ListViewItem(userInfo.GetUserID());
                new ListViewItem(userInfo.GetName());
                item.SubItems.Add(userInfo.GetEmail());
                item.SubItems.Add(userInfo.GetType());
                item.SubItems.Add(userInfo.GetUsername());
                lstViewBooks.Items.Add(item);
            }
            lstViewBooks.Tag = "Users";
        }

        private List<UserInfo> LoadUsersFromDataFile()
        {
            List<UserInfo> loadedUsers = new List<UserInfo>();

            string fileName = "allusers.bin";
            try
            {
                using (Stream stream = System.IO.File.Open(fileName, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        while (stream.Position < stream.Length)
                        {
                            UserInfo userInfo = new UserInfo();
                            userInfo.SetUserID(reader.ReadString());
                            userInfo.SetName(reader.ReadString());
                            userInfo.SetEmail(reader.ReadString());
                            userInfo.SetType(reader.ReadString());
                            userInfo.SetUsername(reader.ReadString());
                            userInfo.SetPassword(reader.ReadString());
                            loadedUsers.Add(userInfo);
                        }
                    }
                }

                statusStripLibrary.Items.Add("Data loaded successfully.");
            }
            catch (Exception ex)
            {
                statusStripLibrary.Items.Add("Error loading data: " + ex.Message);
            }
            return loadedUsers;
        }
           
        public void BorrowBook(string username, string bookTitle)
        {
            statusStripLibrary.Items.Clear();
            
            UserInfo user = users.FirstOrDefault(u => u.GetUsername().Trim().Equals(username.Trim(), StringComparison.OrdinalIgnoreCase));
            if (user == null)
            {
                statusStripLibrary.Items.Add("User not found.");
                return;
            }
            BookInfo book = books.FirstOrDefault(b => b.GetTitle() == bookTitle);
           
            if (book == null)
            {
                statusStripLibrary.Items.Add("Book not found.");
                return;
            }
            if (Convert.ToInt32(book.GetAvlCopies()) == 0)
            {
                statusStripLibrary.Items.Add("Book " + book.GetTitle() + " is not available.");
                return;
            }
            DeserializeLibs();
            if (libs.Count.ToString() != null)
            {
                if (libs.Count(lib => lib._userID == user.GetUserID() && lib._bookStatus == "Borrowed") >= 3)
                {
                    statusStripLibrary.Items.Add("User has borrowed three books already. Return a book before borrowing another one.");
                    return;
                }

                if (libs.Any(lib => lib._userID == user.GetUserID() && lib._bookTitle == bookTitle && lib._bookStatus == "Borrowed"))
                {
                    statusStripLibrary.Items.Add("User has already borrowed " + bookTitle + " book.");
                    return;
                }
                // Decrease the available copies of the book
                int avlCopies = Convert.ToInt32(book.GetAvlCopies());
                avlCopies--;
                book.SetAvlCopies(avlCopies.ToString());
                // Debug statements
                Debug.WriteLine($"Book before update: {book.GetTitle()}, Available Copies: {book.GetAvlCopies()}");

                // Save changes to the books
                ResaveBook();

                Debug.WriteLine($"Book after update: {book.GetTitle()}, Available Copies: {book.GetAvlCopies()}");

                LibraryInfo libraryInfo = new LibraryInfo
                {
                    _userID = user.GetUserID(),
                    _name = user.GetName(),
                    _bookID = book.GetBookID(),
                    _bookTitle = book.GetTitle(),
                    _borrowedDate = DateTime.Now.ToString(),
                    _dueDate = CalculateDueDate(),
                    _bookStatus = "Borrowed",
                    _returnDate = "00/00/2023",
                    _fine = "0"
                };
                libs.Add(libraryInfo);
                ResaveLibrary();

                statusStripLibrary.Items.Add("Book borrowed successfully.");
            }  
        }
        public string CalculateDueDate()
        {
            return DateTime.Now.AddDays(21).ToString();
        }
        private void btnBorrow_Click(object sender, EventArgs e)
        {
            if (lstViewSelectedBooks.Items.Count ==0)
            {
                statusStripLibrary.Items.Add("No books selected for borrowing.");
                return;
            }
            else if (lstViewSelectedBooks.SelectedItems.Count > 0)
            {               
                foreach (ListViewItem selectedItem in lstViewSelectedBooks.SelectedItems)
                {  
                    if (lstViewSelectedBooks.SelectedItems.Count > 0)
                    {
                        string bookTitle = selectedItem.Text;       
                        BorrowBook(username, bookTitle);
                        return;
                    }
                }
            }
            else
            {
                statusStripLibrary.Items.Add("Please select a book to borrow!");
            }
        }
     
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (lstViewSelectedBooks.Items.Count == 0)
            {
                statusStripLibrary.Items.Add("No book selected for returning.");
                return;
            }

            string selectedUser = null;

            if (lstViewBooks.SelectedIndices.Count > 0)
            {
                foreach (ListViewItem selectedItem in lstViewBooks.SelectedItems)
                {
                    if (lstViewBooks.SelectedItems.Count > 0)
                    {
                        selectedUser = selectedItem.Text;   
                    }
                    else
                    {
                        statusStripLibrary.Items.Add("Invalid user selection.");
                        return;
                    }
                }  
            }
            else
            {
                statusStripLibrary.Items.Add("No user selected.");
                return;
            }

            string selectedBook = null;

            if (lstViewSelectedBooks.SelectedIndices.Count > 0)
            {
                foreach (ListViewItem selectedItem in lstViewSelectedBooks.SelectedItems)
                {
                    if (lstViewSelectedBooks.SelectedItems.Count > 0)
                    {
                        selectedBook = selectedItem.Text;               
                    }  
                }
            }
            else
            {
                statusStripLibrary.Items.Add("No Book selected.");
                return;
            }
  
            float fine = 0.5F;
            DateTime returnDate = DateTime.Now;
            double totalFine = 0;

            foreach (LibraryInfo info in selectedLibraryInfos)
            {
                if (returnDate > Convert.ToDateTime(info._dueDate))
                {
                    TimeSpan overdueTime = returnDate - Convert.ToDateTime(info._dueDate);
                    double fineDue = overdueTime.TotalDays * fine;
                    totalFine += fineDue;
                    MessageBox.Show("Your Fine is $" + totalFine + "!");
                }
            }
            if (totalFine == 0)
            {
                LibraryInfo foundRecord = libs.FirstOrDefault(record => record._name == selectedUser &&
                record._bookTitle == selectedBook);

                if (foundRecord != null)
                {
                    booksToRemove.Add(foundRecord);
                }
                else
                {
                    statusStripLibrary.Items.Add("No Book selected.");
                    return;
                }

                foreach (LibraryInfo bookToRemove in booksToRemove)
                {
                    libs.Remove(bookToRemove);
                }

                // Save the updated libs list back to the file
                ResaveLibrary();
                DeserializeBook();

                foreach (ListViewItem selectedItem in lstViewSelectedBooks.SelectedItems)
                {
                    string bookTitle = selectedItem.SubItems[0].Text;

                    // Find the corresponding BookInfo in the 'books' list based on 'bookID'
                    BookInfo book = books.FirstOrDefault(b => b.GetTitle() == bookTitle);
                    // Decrease the available copies of the book
                    int avlCopies = Convert.ToInt32(book.GetAvlCopies());
                    avlCopies++;
                    book.SetAvlCopies(avlCopies.ToString());
                    // Debug statements
                    Debug.WriteLine($"Book before update: {book.GetTitle()}, Available Copies: {book.GetAvlCopies()}");

                    // Save changes to the books
                    ResaveBook();
                    
                }
                statusStripLibrary.Items.Add("Book returned successfully.");
            }

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var LoginForm = new Login();
            LoginForm.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            lstViewBooks.SelectedItems.Clear();
            statusStripLibrary.Items.Clear();
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                string keyword = txtSearch.Text;

                // Set the keyword in each book before sorting
                foreach (var book in books)
                {
                    book.Keyword = keyword;
                }

                // Sort the books using the CompareTo method that considers the keyword
                books.Sort();
                

                int indexTitle = books.FindIndex(ds =>
                        ds.GetTitle().Equals(txtSearch.Text, StringComparison.OrdinalIgnoreCase));
                int indexAuthor = books.FindIndex(ds =>
                        ds.GetAuthors().Equals(txtSearch.Text, StringComparison.OrdinalIgnoreCase));
                if (indexTitle != -1)
                {                    
                    string title = books[indexTitle].GetTitle();                                      
                    lstViewBooks.Items[indexTitle].Selected = true;                
                    statusStripLibrary.Items.Add("Target found.");                   
                }
                else if (indexAuthor != -1)
                {
                    string author = books[indexAuthor].GetAuthors();
                    lstViewBooks.Items[indexAuthor].Selected = true;
                    statusStripLibrary.Items.Add("Target found.");                   
                }
                else
                {
                    statusStripLibrary.Items.Add("Target not found.");
                }
                txtSearch.Clear();
                txtSearch.Focus();
            }
            else
            {
                statusStripLibrary.Items.Add("Please write the target to search.");
            }
        }
        
        private void btnLoad_Click(object sender, EventArgs e)
        {
            statusStripLibrary.Items.Clear();
            lstViewBooks.Items.Clear();
            lstViewBooks.View = View.Details;
            lstViewBooks.Columns.Clear(); // Clear existing columns
            lstViewSelectedBooks.View = View.Details;
            lstViewSelectedBooks.Columns.Clear(); // Clear existing columns
            InitializeListView();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Binary Files|*.bin|All Files|*.*";
            openFileDialog.FileName = "books.bin";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                try
                {
                    using (Stream stream = System.IO.File.Open(fileName, FileMode.Open))
                    {
                        using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                        {
                            while (stream.Position < stream.Length)
                            {
                                BookInfo bookInfo = new BookInfo();
                                bookInfo.SetBookID(reader.ReadString());
                                bookInfo.SetTitle(reader.ReadString());
                                bookInfo.SetAuthors(reader.ReadString());
                                bookInfo.SetPubDate(reader.ReadString());
                                bookInfo.SetCopies(reader.ReadString());
                                bookInfo.SetAvlCopies(reader.ReadString());
                                books.Add(bookInfo);
                            }
                        }
                    }
                    DisplayListViewBooks();
                    statusStripLibrary.Items.Add("Data loaded successfully.");
                }
                catch (Exception ex)
                {
                    statusStripLibrary.Items.Add("Error loading data: " + ex.Message);
                }
            }
        }

        public void ResaveBook()
        {
            string filename = "books.bin";
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
                   // statusStripLibrary.Items.Add("Data saved successfully.");
                }
            }
            catch (Exception ex)
            {
                statusStripLibrary.Items.Add("Error saving data: " + ex.Message);
            }

        }

        public void ResaveLibrary()
        {
            string filename = "library.bin";
            try
            {
                using (FileStream stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                    {
                        foreach (var infoLibs in libs)
                        {
                            writer.Write(infoLibs._userID);
                            writer.Write(infoLibs._name);
                            writer.Write(infoLibs._bookID);
                            writer.Write(infoLibs._bookTitle);
                            writer.Write(infoLibs._bookStatus);
                            writer.Write(infoLibs._borrowedDate);
                            writer.Write(infoLibs._dueDate);
                            writer.Write(infoLibs._returnDate);
                            writer.Write(infoLibs._fine);
                        }
                    }
                   // statusStripLibrary.Items.Add("Data saved succesfully.");
                }
            }
            catch (Exception ex)
            {
                statusStripLibrary.Items.Add("Error saving data: " + ex.Message);
            }
        }

        private void Library_Load(object sender, EventArgs e)
        {
            if (userRole == "Student")
            {
                InitializeListView();
            }
            else
            {
                InitializeListViewUser();
            }
            //DisplayListViewBooks();
        }

        private void btnSelected_Click(object sender, EventArgs e)
        {
            if (lstViewBooks.SelectedItems.Count > 0)
            {
                ListViewItem selectedBook = lstViewBooks.SelectedItems[0];
                if (lstViewSelectedBooks.Items.Count < 3)
                {
                    lstViewSelectedBooks.Items.Add((ListViewItem)selectedBook.Clone());
                    //lstViewBooks.Items.Remove(selectedBook);
                }
                else
                {
                    statusStripLibrary.Items.Add("You can only select up to 3 books.");
                }
            }
            else
            {
                statusStripLibrary.Items.Add("Please select a book to add.");
            }
        }

        private void btnUnselected_Click(object sender, EventArgs e)
        {
            if (lstViewSelectedBooks.SelectedItems.Count > 0)
            {
                ListViewItem selectedBook = lstViewSelectedBooks.SelectedItems[0];
               // lstViewBooks.Items.Add((ListViewItem)selectedBook.Clone());
                lstViewSelectedBooks.Items.Remove(selectedBook);
            }
            else
            {
                statusStripLibrary.Items.Add("Please select a book to remove.");
            }
        }

        private void btnSearchStudent_Click(object sender, EventArgs e)
        {
            lstViewBooks.SelectedItems.Clear();
            statusStripLibrary.Items.Clear();
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                books.Sort();
                UserInfo result = users.FirstOrDefault(ds =>
                    ds.GetName().Equals(txtSearch.Text, StringComparison.OrdinalIgnoreCase));
                if (result != null)
                {
                    int index = users.FindIndex(ds =>
                        ds.GetName().Equals(txtSearch.Text, StringComparison.OrdinalIgnoreCase));
                    if (index != -1)
                    {
                        foreach (ListViewItem item in lstViewBooks.Items)
                        {
                            if (item.Text == result.GetName())
                            {
                                item.Selected = true;
                                break;
                            }
                        }
                        statusStripLibrary.Items.Add("Target found.");
                        txtSearch.Clear();
                        txtSearch.Focus();
                    }
                    else
                    {
                        statusStripLibrary.Items.Add("Please write the correct target!");
                    }
                }
                else
                {
                    statusStripLibrary.Items.Add("Target not found.");
                }
            }
            else
            {
                statusStripLibrary.Items.Add("Please write the target to search.");
            }
        }

        private void btnLoadStudent_Click(object sender, EventArgs e)
        {
            statusStripLibrary.Items.Clear();
            lstViewBooks.Items.Clear();
            lstViewBooks.View = View.Details;
            lstViewBooks.Columns.Clear(); // Clear existing columns
            lstViewSelectedBooks.View = View.Details;
            lstViewSelectedBooks.Columns.Clear(); // Clear existing columns
            InitializeListViewUser();
            users.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Binary Files|*.bin|All Files|*.*";
            openFileDialog.FileName = "allusers.bin";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                try
                {
                    using (Stream stream = System.IO.File.Open(fileName, FileMode.Open))
                    {
                        using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                        {
                            while (stream.Position < stream.Length)
                            {
                                UserInfo userInfo = new UserInfo();
                                userInfo.SetUserID(reader.ReadString());
                                userInfo.SetName(reader.ReadString());
                                userInfo.SetEmail(reader.ReadString());
                                userInfo.SetType(reader.ReadString());
                                userInfo.SetUsername(reader.ReadString());
                                userInfo.SetPassword(reader.ReadString());
                                users.Add(userInfo);
                            }
                        }
                    }
                    DisplayListViewUsers();

                    statusStripLibrary.Items.Add("Data loaded successfully.");
                }
                catch (Exception ex)
                {
                    statusStripLibrary.Items.Add("Error loading data: " + ex.Message);
                }
            }
        }

        private List<BookInfo> DeserializeBook()
        {
            try
            {
                books.Clear();
                //lstViewSelectedBooks.Clear();
                //string filePath = "D://tafe//semester2 - 2023//wednesday//OO design & Testing - 433, 441 - Rupan//AT2-2-0//library//bin//Debug//library.bin";
                string filePath = @"D:\tafe\semester2 - 2023\wednesday\OO design & Testing - 433, 441 - Rupan\AT2-2-0\library\bin\Debug\books.bin";

                if (System.IO.File.Exists(filePath))
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        using (var reader = new BinaryReader(fs, Encoding.UTF8, false))
                        {
                            while (fs.Position < fs.Length)
                            {
                                BookInfo infoBooks = new BookInfo();
                                
                                infoBooks.SetBookID(reader.ReadString());
                                infoBooks.SetTitle(reader.ReadString());
                                infoBooks.SetAuthors(reader.ReadString());
                                infoBooks.SetPubDate(reader.ReadString());
                                infoBooks.SetCopies(reader.ReadString());
                                infoBooks.SetAvlCopies(reader.ReadString());

                                books.Add(infoBooks);
                            }
                        }
                    }
                }
                else
                {
                    statusStripLibrary.Items.Add("Error: File not found.");
                    return null;
                }

            }
            catch (Exception ex)
            {
                statusStripLibrary.Items.Add("Error: " + ex.Message);
            }
            return books;
        }

        private List<LibraryInfo> DeserializeLibs()
        {
            try
            {
                libs.Clear();
                //lstViewSelectedBooks.Clear();
                //string filePath = "D://tafe//semester2 - 2023//wednesday//OO design & Testing - 433, 441 - Rupan//AT2-2-0//library//bin//Debug//library.bin";
                string filePath = @"D:\tafe\semester2 - 2023\wednesday\OO design & Testing - 433, 441 - Rupan\AT2-2-0\library\bin\Debug\library.bin";

                if (System.IO.File.Exists(filePath))
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        using (var reader = new BinaryReader(fs, Encoding.UTF8, false))
                        {
                            while (fs.Position < fs.Length)
                            {
                                LibraryInfo infoLibs = new LibraryInfo();
                                infoLibs._userID = reader.ReadString();
                                infoLibs._name = reader.ReadString();
                                infoLibs._bookID = reader.ReadString();
                                infoLibs._bookTitle = reader.ReadString();
                                infoLibs._bookStatus = reader.ReadString();
                                infoLibs._borrowedDate = reader.ReadString();
                                infoLibs._dueDate = reader.ReadString();
                                infoLibs._returnDate = reader.ReadString();
                                infoLibs._fine = reader.ReadString();

                                libs.Add(infoLibs);
                            }
                        }
                    }
                } else
                {
                    statusStripLibrary.Items.Add("Error: File not found.");
                    return null; 
                }
            
            }
            catch (Exception ex)
            {
                statusStripLibrary.Items.Add("Error: " + ex.Message);
            }
            return libs;
        }

        private void lstViewBooks_SelectedIndexChanged(object sender, EventArgs e)
        {

           
            statusStripLibrary.Items.Clear();
            
            if (lstViewBooks.Tag.ToString() == "Users")
            {
                lstViewSelectedBooks.Items.Clear();
                DeserializeLibs();
                if (lstViewBooks.SelectedItems.Count > 0)
                {
                    int selectedIndex = lstViewBooks.SelectedIndices[0];
                   
                    //int index = selectedIndex;
                    if (libs != null && selectedIndex >= 0 && selectedIndex < users.Count)
                    {
                        UserInfo selectedUser = users[selectedIndex];
                        foreach (LibraryInfo info in libs)
                        {
                            if (info._name == selectedUser.GetName())
                            {
                                ListViewItem item = //new ListViewItem(info._userID.ToString());
                                                    //item.SubItems.Add(info._bookID.ToString());
                                new ListViewItem(info._bookTitle);
                                item.SubItems.Add(info._name);
                                item.SubItems.Add(info._dueDate);
                                item.SubItems.Add(info._returnDate);
                                item.SubItems.Add(info._bookStatus);
                                item.SubItems.Add(info._fine.ToString());
                                lstViewSelectedBooks.Items.Add(item);
                                selectedItem.Add(item);
                            }
                        }
                    }
                    else
                    {
                        statusStripLibrary.Items.Add("library couldn't load.");
                    }
                      
                }
            }
            else if (lstViewBooks.Tag.ToString() == "Books")
            {
                lstViewSelectedBooks.View = View.Details;
                lstViewSelectedBooks.Columns.Clear();
                InitializeListView();
                if (lstViewBooks.SelectedItems.Count > 0)
                {
                    BookInfo selectedBook = books[lstViewBooks.SelectedIndices[0]];
                    //ListViewItem item = new ListViewItem(selectedBook.GetBookID());
                    ListViewItem item = new ListViewItem(selectedBook.GetTitle());
                    //item.SubItems.Add(selectedBook.GetTitle());
                    item.SubItems.Add(selectedBook.GetAuthors());
                    item.SubItems.Add(selectedBook.GetPubDate());
                    item.SubItems.Add(selectedBook.GetCopies());
                    item.SubItems.Add(selectedBook.GetAvlCopies());
                    lstViewSelectedBooks.Items.Add(item);
                    selectedItem.Add(item);
                }
                if (selectedItem.Count > 3)
                {
                    ListViewItem removeItem = selectedItem[0];
                    selectedItem.RemoveAt(0);
                    lstViewSelectedBooks.Items.Remove(removeItem);
                }
            }
            
        }

        /*private void btnSaveStudent_Click(object sender, EventArgs e)
        {
            try
            {
                using (FileStream fs = new FileStream("library.bin", FileMode.Create, FileAccess.Write))
                using (var writer = new BinaryWriter(fs, Encoding.UTF8, false))
                {
                    foreach (var infoLibs in selectedLibraryInfos)
                    {
                        writer.Write(infoLibs._userID);
                        writer.Write(infoLibs._name);
                        writer.Write(infoLibs._bookID);
                        writer.Write(infoLibs._bookTitle); 
                        writer.Write(infoLibs._borrowedDate);
                        writer.Write(infoLibs._dueDate);
                        writer.Write(infoLibs._bookStatus);
                        writer.Write(infoLibs._returnDate);
                        writer.Write(infoLibs._fine);
                    }
                }
                statusStripLibrary.Items.Add("Data saved succesfully.");
            }
            catch (Exception ex)
            {
                statusStripLibrary.Items.Add("Error saving data: " + ex.Message);
            }
        }*/

        private void btnFine_Click(object sender, EventArgs e)
        {
            totalFine = 0;
            if (lstViewSelectedBooks.Items.Count == 0)
            {
                statusStripLibrary.Items.Add("No book selected for returning.");
                return;
            }

            string selectedUser = null;

            if (lstViewBooks.SelectedIndices.Count > 0)
            {
                foreach (ListViewItem selectedItem in lstViewBooks.SelectedItems)
                {
                    if (lstViewBooks.SelectedItems.Count > 0)
                    {
                        selectedUser = selectedItem.Text;
                    }
                    else
                    {
                        statusStripLibrary.Items.Add("Invalid user selection.");
                        return;
                    }
                }
            }
            else
            {
                statusStripLibrary.Items.Add("No user selected.");
                return;
            }

            string selectedBook = null;

            if (lstViewSelectedBooks.SelectedIndices.Count > 0)
            {
                foreach (ListViewItem selectedItem in lstViewSelectedBooks.SelectedItems)
                {
                    if (lstViewSelectedBooks.SelectedItems.Count > 0)
                    {
                        selectedBook = selectedItem.Text;
                    }
                }
            }
            else
            {
                statusStripLibrary.Items.Add("No Book selected.");
                return;
            }

            LibraryInfo foundRecord = libs.FirstOrDefault(record => record._name == selectedUser &&
                record._bookTitle == selectedBook);

            if (foundRecord != null)
            {
                booksToRemove.Add(foundRecord);
            }
            else
            {
                statusStripLibrary.Items.Add("No Book selected.");
                return;
            }

            foreach (LibraryInfo bookToRemove in booksToRemove)
            {
                libs.Remove(bookToRemove);
            }

            // Save the updated libs list back to the file
            ResaveLibrary();
            DeserializeBook();

            foreach (ListViewItem selectedItem in lstViewSelectedBooks.SelectedItems)
            {
                string bookTitle = selectedItem.SubItems[0].Text;

                // Find the corresponding BookInfo in the 'books' list based on 'bookID'
                BookInfo book = books.FirstOrDefault(b => b.GetTitle() == bookTitle);
                // Decrease the available copies of the book
                int avlCopies = Convert.ToInt32(book.GetAvlCopies());
                avlCopies++;
                book.SetAvlCopies(avlCopies.ToString());
                // Debug statements
                Debug.WriteLine($"Book before update: {book.GetTitle()}, Available Copies: {book.GetAvlCopies()}");

                // Save changes to the books
                ResaveBook();

            }
            statusStripLibrary.Items.Add("Book returned successfully.");
           
        }

        private void btnSearchUsersBorrowedBook_Click(object sender, EventArgs e)
        {
            statusStripLibrary.Items.Clear();
            lstViewBooks.Items.Clear();
            lstViewBooks.View = View.Details;
            lstViewBooks.Columns.Clear(); // Clear existing columns
            lstViewSelectedBooks.View = View.Details;
            lstViewSelectedBooks.Columns.Clear(); // Clear existing columns
            InitializeListViewUser();
            //users.Clear();

            DeserializeLibs();
            // Combine data using LINQ
            var combinedData = from user in users
                               join lib in libs on user.GetName() equals lib._name into userLibs
                               from userLib in userLibs.DefaultIfEmpty() // Left outer join
                               select new
                               {
                                   UserName = user.GetName(),
                                   Email = user.GetEmail(),
                                   BookTitle = userLib != null ? userLib._bookTitle : "No book borrowed"
                               };
            foreach (var data in combinedData)
            {
                
                ListViewItem item = //new ListViewItem(userInfo.GetUserID());
                new ListViewItem(data.UserName);
                item.SubItems.Add(data.Email);
                item.SubItems.Add(data.BookTitle);
                lstViewBooks.Items.Add(item);
            }
            lstViewBooks.Tag = "Users";
        }

        private void btnSearchBooksAndUsersBorrow_Click(object sender, EventArgs e)
        {
            var searchBorrowForm = new SearchBorrow(Convert.ToString(userRole));
            searchBorrowForm.Show();
        }

    }
}
