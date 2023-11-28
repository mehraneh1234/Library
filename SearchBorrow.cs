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
    public partial class SearchBorrow : Form
    {
        private List<BookInfo> books = new List<BookInfo>();
        private List<UserInfo> users = new List<UserInfo>();
        private List<LibraryInfo> libs = new List<LibraryInfo>();

       /* private string username;
        private string userRole;*/
       /* public SearchBorrow()
        {
            InitializeComponent();
        }*/
        public SearchBorrow(string userRole)
        {
            InitializeComponent();
            if (userRole == "Staff")
            {
                InitializeListView();
                InitializeListViewUser();
                InitializeStaffFeatures();
                DisplayLvUsersBorrow();
                DisplayLvBorrowedBooks();
            }
            else if (userRole == "Student")
            {
                InitializeListView();
                InitializeStudentFeatures();
                DisplayLvBorrowedBooks();
            }

            // Load users from file and add them to the users list
            List<UserInfo> loadedUsers = LoadUsersFromDataFile();
            users.AddRange(loadedUsers);
            /*DeserializeLibs();*/
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

                statusStripSearchBorrow.Items.Add("Data loaded successfully.");
            }
            catch (Exception ex)
            {
                statusStripSearchBorrow.Items.Add("Error loading data: " + ex.Message);
            }
            return loadedUsers;
        }
        private void InitializeStaffFeatures()
        {
            lvBorrowedBook.Enabled = true;
            lvUsersBorrowedBook.Enabled = true;           
        }
        private void InitializeStudentFeatures()
        {
            lvBorrowedBook.Enabled = true;
            lvUsersBorrowedBook.Enabled = false;
        }

        private void InitializeListView()
        {
            lvBorrowedBook.View = View.Details;
            lvBorrowedBook.Columns.Add("Title");
            lvBorrowedBook.Columns.Add("Due_date");
            lvBorrowedBook.Columns.Add("users");
            /*lstViewBooks.Columns.Add("CopiesNO");
            lstViewBooks.Columns.Add("AvailableNo");*/
        }
        private void InitializeListViewUser()
        {
            lvUsersBorrowedBook.View = View.Details;
            lvUsersBorrowedBook.Columns.Add("Name");
            lvUsersBorrowedBook.Columns.Add("Title");
            lvUsersBorrowedBook.Columns.Add("Due_date");
        }

        private void DisplayLvBorrowedBooks()
        {
            DeserializeLibs();
            lvBorrowedBook.Items.Clear();
            foreach (LibraryInfo lInfo in libs)
            {
                ListViewItem item = new ListViewItem(lInfo._bookTitle);
                item.SubItems.Add(lInfo._dueDate);
                item.SubItems.Add(lInfo._name);      
                lvBorrowedBook.Items.Add(item);
            }
            lvBorrowedBook.Tag = "Borrowed Books";
        }
        private void DisplayLvUsersBorrow()
        {
            DeserializeLibs();
            lvUsersBorrowedBook.Items.Clear();
            foreach (LibraryInfo libInfo in libs)
            {
                ListViewItem item = //new ListViewItem(userInfo.GetUserID());
                new ListViewItem(libInfo._name);
                item.SubItems.Add(libInfo._bookTitle);
                item.SubItems.Add(libInfo._dueDate);                
                lvUsersBorrowedBook.Items.Add(item);
            }
            lvUsersBorrowedBook.Tag = "Users";
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
                }
                else
                {
                    statusStripSearchBorrow.Items.Add("Error: File not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                statusStripSearchBorrow.Items.Add("Error: " + ex.Message);
            }
            return libs;
        }

    }
}
