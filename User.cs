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
    public partial class User : Form
    {
        List<UserInfo> users = new List<UserInfo>();
        private int lastUserID = 0;
        
        public User()
        {
            InitializeComponent();
            InitializeListView();
        }

        private void InitializeListView()
        {
            listViewUsers.View = View.Details;
            listViewUsers.Columns.Add("ID");
            listViewUsers.Columns.Add("Name");
            listViewUsers.Columns.Add("Email");
            listViewUsers.Columns.Add("Type");
          /*  listViewUsers.Columns.Add("Username");
            listViewUsers.Columns.Add("Password");*/
        }


        private void DisplayistViewUser()
        {
            listViewUsers.Items.Clear();
            foreach (UserInfo info in users)
            {
                if (!string.IsNullOrEmpty(info.GetName()) &&
                    !string.IsNullOrEmpty(info.GetEmail()) &&
                    !string.IsNullOrEmpty(info.GetType()) &&
                    !string.IsNullOrEmpty(info.GetUsername()) &&
                    !string.IsNullOrEmpty(info.GetPassword()))
                {
                    ListViewItem item = new ListViewItem(info.GetUserID());
                    item.SubItems.Add(info.GetName());
                    item.SubItems.Add(info.GetEmail());
                    item.SubItems.Add(info.GetType());
                    item.SubItems.Add(info.GetUsername());
                    item.SubItems.Add(info.GetPassword());

                    listViewUsers.Items.Add(item);
                }
            }
        }

        private void ClearTextBox()
        {
            txtName.Clear();
            txtEmail.Clear();
            rbStaff.Checked = false;
            rbStudent.Checked = false;
            txtUsername.Clear();
            txtPassword.Clear();
            txtName.Focus();
        }
        private int GenerateUserID()
        {
            return ++lastUserID;
        }

        private string GetRadioButton()
        {
            string radioText = "";
            if (rbStaff.Checked)
                radioText = rbStaff.Text;
            else if (rbStudent.Checked)
                radioText = rbStudent.Text;
            return radioText;
        }

        private void SetRadioButton(int index)
        {
            if (users[index].GetType() == "Staff")
                rbStaff.Checked = true;
            else if (users[index].GetType() == "Student")
                rbStudent.Checked = true;
        }
        private void listViewUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            statusStripInfo.Items.Clear();
            if (listViewUsers.SelectedItems.Count > 0)
            {
                int index = listViewUsers.SelectedIndices[0];
                txtSearch.Text = users[index].GetUserID();
                txtName.Text = users[index].GetName();
                txtEmail.Text = users[index].GetEmail();
                SetRadioButton(index);
                txtUsername.Text = users[index].GetUsername();
                txtPassword.Text = users[index].GetPassword();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            statusStripInfo.Items.Clear();
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string type = GetRadioButton();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(type)
                 || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                statusStripInfo.Items.Add("Please fill in all fields.");
                return;
            }

            if (users.Any(info => info.GetUsername() == username))
            {
                statusStripInfo.Items.Add("Duplicate username. Please enter a unique username.");
            }

            UserInfo newInfo = new UserInfo();
            newInfo.SetUserID(Convert.ToString(GenerateUserID()));
            newInfo.SetName(name);
            newInfo.SetEmail(email);
            newInfo.SetType(type);
            newInfo.SetUsername(username);
            newInfo.SetPassword(password);
            users.Add(newInfo);

            ClearTextBox();
            DisplayistViewUser();
            statusStripInfo.Items.Add("The new user is added.");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            statusStripInfo.Items.Clear();
            UserInfo newInfo = new UserInfo();
            if (listViewUsers.SelectedItems.Count > 0)
            {
                int selectedRowIndex = listViewUsers.SelectedIndices[0];
                string id = txtSearch.Text.Trim();
                string name = txtName.Text.Trim();
                string email = txtEmail.Text.Trim();
                string type = GetRadioButton();
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(type)
                    && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    users[selectedRowIndex].SetUserID(id);
                    users[selectedRowIndex].SetName(name);
                    users[selectedRowIndex].SetEmail(email);
                    users[selectedRowIndex].SetType(type);
                    users[selectedRowIndex].SetUsername(username);
                    users[selectedRowIndex].SetPassword(password);
                }
            }
            else
            {
                statusStripInfo.Items.Add("Please select a row to edit.");
            }
            statusStripInfo.Items.Add("The selected row is edited.");
            ClearTextBox();
            DisplayistViewUser();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            statusStripInfo.Items.Clear();
            if (listViewUsers.SelectedItems.Count >0)
            {
                DialogResult result = MessageBox.Show("Are you sure to delete this data?", "Confirmation",

                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    int selectedIndex = listViewUsers.SelectedItems[0].Index;
                    users.RemoveAt(selectedIndex);
                    DisplayistViewUser();
                }
            }
            else
            {
                statusStripInfo.Items.Add("Please select a row to delete.");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ClearTextBox();
            listViewUsers.SelectedItems.Clear();
            statusStripInfo.Items.Clear();
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                users.Sort();
                UserInfo result = users.FirstOrDefault(ds =>
                    ds.GetName().Equals(txtSearch.Text, StringComparison.OrdinalIgnoreCase));
                if (result != null)
                {
                    int index = users.FindIndex(ds =>
                       ds.GetName().Equals(txtSearch.Text, StringComparison.OrdinalIgnoreCase)); 
                    if (index != -1)
                    {
                        SetRadioButton(index);
                        txtName.Text = result.GetName();
                        txtEmail.Text = result.GetEmail();
                        txtUsername.Text = result.GetUsername();
                        txtPassword.Text = result.GetPassword();
                        foreach (ListViewItem item in listViewUsers.Items)
                        {
                            if (item.Text == result.GetName())
                            {
                                item.Selected = true;
                                break;
                            }
                        }
                        statusStripInfo.Items.Add("Target found.");
                        txtSearch.Clear();
                        txtSearch.Focus();
                    }
                    else
                    {
                        statusStripInfo.Items.Add("Please write the correct target.");
                    }
                }
                else
                {
                    statusStripInfo.Items.Add("Target not found");
                }
            }
            else
            {
                statusStripInfo.Items.Add("Please write the target to search.");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            statusStripInfo.Items.Clear();
            listViewUsers.Items.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Binary Files|*.bin|All Files|*.*";
            openFileDialog.FileName = "allusers.bin";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                try
                {
                    users.Clear();
                    using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                        {
                            while (stream.Position < stream.Length)
                            {
                                UserInfo newInfo = new UserInfo();
                                newInfo.SetUserID(reader.ReadString());
                                newInfo.SetName(reader.ReadString());
                                newInfo.SetEmail(reader.ReadString());
                                newInfo.SetType(reader.ReadString());
                                newInfo.SetUsername(reader.ReadString());
                                newInfo.SetPassword(reader.ReadString());

                                users.Add(newInfo);
                            }
                        }
                    }
                    
                    
                    DisplayistViewUser();
                    statusStripInfo.Items.Add("Data loaded successfully.");
                }
                catch (Exception ex)
                {
                    statusStripInfo.Items.Add("Error loading data: " + ex.Message);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            statusStripInfo.Items.Clear();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Binary Files|*.bin|All Files|*.*";
            saveFileDialog.FileName = "allusers.bin";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog.FileName;
                try
                {
                    using (FileStream filestream = new FileStream(filename, FileMode.Create, FileAccess.Write))
                    using (var writer = new BinaryWriter(filestream, Encoding.UTF8, false))
                        {
                            foreach (var user in users)
                            {
                                writer.Write(user.GetUserID());
                                writer.Write(user.GetName());
                                writer.Write(user.GetEmail());
                                writer.Write(user.GetType());
                                writer.Write(user.GetUsername());
                                writer.Write(user.GetPassword());
                            }
                        }
                        statusStripInfo.Items.Add("Data saved successfully.");   
                }
                catch (Exception ex)
                {
                    statusStripInfo.Items.Add("Error saving data: " + ex.Message);
                }
            }
        }

        private void User_FormClosed(object sender, FormClosedEventArgs e)
        {
            btnSave_Click(this, e);
        }

        private void User_Load(object sender, EventArgs e)
        {
           // InitializeListView();
            txtCourse.Enabled = false;
            txtYear.Enabled = false;
            txtDepartment.Enabled = false;
        }
    }
}
