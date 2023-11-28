using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace library
{
    public partial class Login : Form
    {
        
        public Login()
        {
            InitializeComponent();
        }
        public enum UserRole
        {
            Staff,
            Student,
            Guest
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            UserRole userRole;
            UserInfo foundUser;
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (IsValidUser(username, password, out userRole, out foundUser))
            {
                if (userRole == UserRole.Student)
                {
                    var libraryForm = new Library(username, Convert.ToString(userRole));
                    libraryForm.Show();
                }
                else if (userRole == UserRole.Staff)
                {
                    var bookForm = new Book();
                    bookForm.Show();
                    var userForm = new User();
                    userForm.Show();
                    var libraryForm = new Library(username, Convert.ToString(userRole));
                    libraryForm.Show();
                }
            }
            else
            {
                // Handle the case where the user is not found without displaying an error
                var libraryform = new Library(Convert.ToString(UserRole.Guest));
                libraryform.Show();
            }
        }

        private bool IsValidUser(string username, string password, out UserRole userRole, out UserInfo foundUser)
        {
            userRole = UserRole.Guest;
            foundUser = null;
            statusStripLogin.Items.Clear();
            try
            {
                using (Stream stream = File.Open("D:\\tafe\\semester2 - 2023\\wednesday\\OO design & Testing - 433, 441 - Rupan\\AT2-2-0\\library\\bin\\Debug\\allusers.bin", FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        while (stream.Position < stream.Length)
                        {
                            UserInfo userInfo = new UserInfo();
                            userInfo.SetUserID(reader.ReadString());
                            userInfo.SetUsername(reader.ReadString());
                            userInfo.SetEmail(reader.ReadString());
                            userInfo.SetType(reader.ReadString());
                            userInfo.SetUsername(reader.ReadString());
                            userInfo.SetPassword(reader.ReadString());
                         
                            if (userInfo.GetUsername() == username && userInfo.GetPassword() == password)
                            {
                                if (Enum.TryParse(userInfo.GetType(), out UserRole role))
                                {
                                    userRole = role;
                                    foundUser = userInfo; //Set the found user
                                }
                                else
                                {
                                    statusStripLogin.Items.Add("Invalid userType.");
                                }

                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                statusStripLogin.Items.Add("Error: " + ex.Message);
            }
            return false;
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            var userForm = new User();
            userForm.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var libraryForm = new Library(Convert.ToString(UserRole.Guest));
            libraryForm.Show();
        }
    }
}
