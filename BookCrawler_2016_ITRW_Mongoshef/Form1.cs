using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess;
//using LiveCharts; //Core of the library
//using LiveCharts.WinForms; 
using HtmlAgilityPack;


namespace BookCrawler_2016_ITRW_Mongoshef
{
    public partial class Form1 : Form
    {
        //Instance Variables
        bool DashboardClicked = false;
        bool SystemAdminClicked = false;
        bool CrawlerClicked = false;

        //Library Class instances
        HtmlWeb crawler = new HtmlWeb();


        //User instantiated classes
        DataProcessor.Mongo mongo;

        public Form1()
        {
            InitializeComponent();

            mongo = new DataProcessor.Mongo();
        }


        private async  void links()
        {
            string link = "https://www.goodreads.com/genres/new_releases/science-fiction";

            var page = await Task.Factory.StartNew(() => crawler.Load(link));

            var bookNodes = page.DocumentNode.SelectNodes("/html/body/div[1]/div[2]/div[1]/div[2]/div[2]/div[4]/div[2]/div/div/div/div/a");

            var booksList = bookNodes.Select(book => book.GetAttributeValue("href", string.Empty));

            foreach(var book in booksList)
            {
                listBox1.Items.Add(book.ToString());

                var bookPage = await Task.Factory.StartNew(() => crawler.Load("https://www.goodreads.com" + book.ToString()));

                var bookNames = bookPage.DocumentNode.SelectNodes("/html/body/div[1]/div[2]/div[3]/div[1]/div[4]/div[1]/div[2]/h1/text()");

                var authorNames = bookPage.DocumentNode.SelectNodes("/html/body/div[1]/div[2]/div[3]/div[1]/div[4]/div[1]/div[2]/div[1]/span[2]/a");

                var bookStars = bookPage.DocumentNode.SelectNodes("/html/body/div[1]/div[2]/div[3]/div[1]/div[4]/div[1]/div[2]/div[2]/span[3]/span");

                var bookRatings = bookPage.DocumentNode.SelectNodes("/html/body/div[1]/div[2]/div[3]/div[1]/div[4]/div[1]/div[2]/div[2]/a[2]/span");

                var bookReviews = bookPage.DocumentNode.SelectNodes("/html/body/div[1]/div[2]/div[3]/div[1]/div[4]/div[1]/div[2]/div[2]/a[3]/span/span");

                var bookISBNS = bookPage.DocumentNode.SelectNodes("/html/body/div[1]/div[2]/div[3]/div[1]/div[4]/div[1]/div[2]/div[4]/div[3]/div[1]/div[2]/div[2]/text()");

                var bookISBNSCollection = bookISBNS.Select(coll => coll.InnerText);

                var pageNums = bookPage.DocumentNode.SelectNodes("/html/body/div[1]/div[2]/div[3]/div[1]/div[4]/div[1]/div[2]/div[4]/div[1]/span[2]");

                var publishedDates = bookPage.DocumentNode.SelectNodes("/html/body/div[1]/div[2]/div[3]/div[1]/div[4]/div[1]/div[2]/div[4]/div[2]/text()");

                var bookLanguages = bookPage.DocumentNode.SelectNodes("/html/body/div[1]/div[2]/div[3]/div[1]/div[4]/div[1]/div[2]/div[4]/div[3]/div[1]/div[3]/div[2]");

                var bookSynopsis = bookPage.DocumentNode.SelectNodes("/html/body/div[1]/div[2]/div[3]/div[1]/div[4]/div[1]/div[2]/div[3]/div/span[2]/text()");

                var synopsisStrings = bookSynopsis.Select(coll => coll.InnerText);

                BookMetaData data = new BookMetaData
                {

                };
                
           }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            links();
        }

        private void btnSignInOption_MouseMove(object sender, MouseEventArgs e)
        {
            btnSignInOption.Size = new Size(70, 80);
            btnSignInOption.Location = new Point(49, 78);

            lblSignInOption.Visible = true;           
        }

        private void btnSignInOption_MouseLeave(object sender, EventArgs e)
        {
            btnSignInOption.Size = new Size(60, 70);
            btnSignInOption.Location = new Point(51, 80);

            lblSignInOption.Visible = false;
        }

        private void btnAddNewUserOption_MouseMove(object sender, MouseEventArgs e)
        {
            btnAddNewUserOption.Size = new Size(70, 80);
            btnAddNewUserOption.Location = new Point(49, 193);

            lblAddNewUserOption.Visible = true;
        }

        private void btnAddNewUserOption_MouseLeave(object sender, EventArgs e)
        {
            btnAddNewUserOption.Size = new Size(60, 70);
            btnAddNewUserOption.Location = new Point(51, 195);

            lblAddNewUserOption.Visible = false;
        }

        private void btnSignInOption_Click(object sender, EventArgs e)
        {
            pnlSignIn.Visible = true;
            pnlAddNewUser.Visible = false;

            pnlHomeOption.Location = new Point(442, 139);
            btnLockScreen.Visible = true;
            lblLock.Visible = false;
        }

        private void btnAddNewUserOption_Click(object sender, EventArgs e)
        {
            pnlSignIn.Visible = false;
            pnlAddNewUser.Visible = true;

            pnlHomeOption.Location = new Point(402, 139);
                        
            btnLockScreen.Visible = true;
            lblLock.Visible = false;
        }

        private void btnLockScreen_Click(object sender, EventArgs e)
        {
            btnLockScreen.Visible = false;
            lblLock.Visible = false;

            pnlHomeOption.Location = new Point(412, 139);
                       
            pnlSignIn.Visible = false;
            pnlAddNewUser.Visible = false;
        }

        private void btnLockScreen_MouseMove(object sender, MouseEventArgs e)
        {
            lblLock.Visible = true;

            btnLockScreen.Size = new Size(64, 50);
            btnLockScreen.Location = new Point(88, 305);
        }

        private void btnLockScreen_MouseLeave(object sender, EventArgs e)
        {
            lblLock.Visible = false;

            btnLockScreen.Size = new Size(54, 40);
            btnLockScreen.Location = new Point(90, 307);
        }

        private void btnSignIn_MouseMove(object sender, MouseEventArgs e)
        {
            btnSignIn.Size = new Size(105, 79);
            btnSignIn.Location = new Point(100, 261);

            lblSignIn.Visible = true;
        }

        private void btnSignIn_MouseLeave(object sender, EventArgs e)
        {
            btnSignIn.Size = new Size(95, 69);
            btnSignIn.Location = new Point(102, 263);

            lblSignIn.Visible = false;
        }

        private void btnAccept_MouseMove(object sender, MouseEventArgs e)
        {
            lblAccept.Visible = true;

            btnAccept.Size = new Size(105, 79);
            btnAccept.Location = new Point(119, 358);
        }

        private void btnAccept_MouseLeave(object sender, EventArgs e)
        {
            lblAccept.Visible = false;

            btnAccept.Size = new Size(95, 69);
            btnAccept.Location = new Point(121, 360);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtLoginUserName.ForeColor = Color.Gray;
            txtLoginPassword.ForeColor = Color.Gray;

            txtLoginUserName.Text = "Username";
            txtLoginPassword.Text = "Password";            

            ////////////////////////////////////////////////////////////////////

            txtRegFirstName.ForeColor = Color.Gray;
            txtRegLastName.ForeColor = Color.Gray;
            txtRegUserName.ForeColor = Color.Gray;
            txtRegPassword.ForeColor = Color.Gray;
            txtRegConfirmPass.ForeColor = Color.Gray;

            txtRegFirstName.Text = "First name";
            txtRegLastName.Text = "Last name";
            txtRegUserName.Text = "User name";

            txtRegPassword.UseSystemPasswordChar = false;
            txtRegConfirmPass.UseSystemPasswordChar = false;

            txtRegPassword.Text = "Password";
            txtRegConfirmPass.Text = "Confirm password";
        }

        private void txtLoginUserName_Enter(object sender, EventArgs e)
        {
            if(txtLoginUserName.Text == "Username")
            {
                txtLoginUserName.Text = "";
            }
            else
            {
                if(txtLoginUserName.Text == "")
                {
                    txtLoginUserName.Text = "Username";
                }              
            }
        }

        private void txtLoginUserName_Leave(object sender, EventArgs e)
        {
            if (txtLoginUserName.Text == "Username")
            {
                
            }
            else
            {
                if (txtLoginUserName.Text == "")
                {
                    txtLoginUserName.Text = "Username";
                }
                else
                {
                    txtLoginUserName.ForeColor = Color.Black;
                }
            }            
        }

        private void txtLoginPassword_Enter(object sender, EventArgs e)
        {            
            if (txtLoginPassword.Text == "Password")
            {
                txtLoginPassword.UseSystemPasswordChar = true;
                txtLoginPassword.Text = "";
                txtLoginPassword.ForeColor = Color.Black;
            }
            else
            {
                if (txtLoginPassword.Text == "")
                {
                    txtLoginPassword.Text = "Password";
                    txtLoginPassword.UseSystemPasswordChar = false;
                }
            }
        }

        private void txtLoginPassword_Leave(object sender, EventArgs e)
        {
            if (txtLoginPassword.Text == "Password")
            {                
                txtLoginPassword.UseSystemPasswordChar = false;
                txtLoginPassword.ForeColor = Color.Gray;
            }
            else
            {
                if (txtLoginPassword.Text == "")
                {
                    txtLoginPassword.Text = "Password";
                    txtLoginPassword.UseSystemPasswordChar = false;
                    txtLoginPassword.ForeColor = Color.Gray;
                }
                else
                {
                    txtLoginPassword.ForeColor = Color.Black;
                }
            }
        }

        private void txtRegFirstName_Enter(object sender, EventArgs e)
        {
            if (txtRegFirstName.Text == "First name")
            {
                txtRegFirstName.Text = "";
            }
            else
            {
                if (txtRegFirstName.Text == "")
                {
                    txtRegFirstName.Text = "First name";
                }
            }
        }

        private void txtRegFirstName_Leave(object sender, EventArgs e)
        {
            if (txtRegFirstName.Text == "First name")
            {

            }
            else
            {
                if (txtRegFirstName.Text == "")
                {
                    txtRegFirstName.Text = "First name";
                    txtRegFirstName.ForeColor = Color.Gray;
                }
                else
                {
                    txtRegFirstName.ForeColor = Color.Black;
                }
            }
        }

        private void txtRegLastName_Enter(object sender, EventArgs e)
        {
            if (txtRegLastName.Text == "Last name")
            {
                txtRegLastName.Text = "";
            }
            else
            {
                if (txtRegLastName.Text == "")
                {
                    txtRegLastName.Text = "Last name";
                }
            }
        }

        private void txtRegLastName_Leave(object sender, EventArgs e)
        {
            if (txtRegLastName.Text == "Last name")
            {

            }
            else
            {
                if (txtRegLastName.Text == "")
                {
                    txtRegLastName.Text = "Last name";
                    txtRegLastName.ForeColor = Color.Gray;
                }
                else
                {
                    txtRegLastName.ForeColor = Color.Black;
                }
            }
        }

        private void txtRegUserName_Enter(object sender, EventArgs e)
        {
            if (txtRegUserName.Text == "User name")
            {
                txtRegUserName.Text = "";
            }
            else
            {
                if (txtRegUserName.Text == "")
                {
                    txtRegUserName.Text = "User name";
                }
            }
        }

        private void txtRegUserName_Leave(object sender, EventArgs e)
        {
            if (txtRegUserName.Text == "User name")
            {

            }
            else
            {
                if (txtRegUserName.Text == "")
                {
                    txtRegUserName.Text = "User name";
                    txtRegUserName.ForeColor = Color.Gray;
                }
                else
                {
                    txtRegUserName.ForeColor = Color.Black;
                }
            }
        }

        private void txtRegPassword_Enter(object sender, EventArgs e)
        {
            if (txtRegPassword.Text == "Password")
            {
                txtRegPassword.UseSystemPasswordChar = true;
                txtRegPassword.Text = "";
                txtRegPassword.ForeColor = Color.Black;
            }
            else
            {
                if (txtRegPassword.Text == "")
                {
                    txtRegPassword.Text = "Password";
                    txtRegPassword.UseSystemPasswordChar = false;
                }
            }
        }

        private void txtRegPassword_Leave(object sender, EventArgs e)
        {
            if (txtRegPassword.Text == "Password")
            {
                txtRegPassword.UseSystemPasswordChar = false;
                txtRegPassword.ForeColor = Color.Gray;
            }
            else
            {
                if (txtRegPassword.Text == "")
                {
                    txtRegPassword.Text = "Password";
                    txtRegPassword.UseSystemPasswordChar = false;
                    txtRegPassword.ForeColor = Color.Gray;
                }
                else
                {
                    txtRegPassword.ForeColor = Color.Black;
                }
            }
        }

        private void txtRegConfirmPass_Enter(object sender, EventArgs e)
        {
            if (txtRegConfirmPass.Text == "Confirm password")
            {
                txtRegConfirmPass.UseSystemPasswordChar = true;
                txtRegConfirmPass.Text = "";
                txtRegConfirmPass.ForeColor = Color.Black;
            }
            else
            {
                if (txtRegConfirmPass.Text == "")
                {
                    txtRegConfirmPass.Text = "Confirm password";
                    txtRegConfirmPass.UseSystemPasswordChar = false;
                }
            }
        }

        private void txtRegConfirmPass_Leave(object sender, EventArgs e)
        {
            if (txtRegConfirmPass.Text == "Confirm password")
            {
                txtRegConfirmPass.UseSystemPasswordChar = false;
                txtRegConfirmPass.ForeColor = Color.Gray;
            }
            else
            {
                if (txtRegConfirmPass.Text == "")
                {
                    txtRegConfirmPass.Text = "Confirm password";
                    txtRegConfirmPass.UseSystemPasswordChar = false;
                    txtRegConfirmPass.ForeColor = Color.Gray;
                }
                else
                {
                    txtRegConfirmPass.ForeColor = Color.Black;
                }
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            pnlDashBoard.Visible = true;
            pnlHome.Visible = false;

            pnlDashBoardCompo.Visible = false;
            pnlAdministrator.Visible = false;
            pnlCrawler.Visible = false;

            txtConfirmPass.Text = "Confirm password";
            txtFirstName.Text = "First name";
            txtLastName.Text = "Last name";
            txtLoginPassword.Text = "Password";
            txtLoginUserName.Text = "User name";
            txtPasswword.Text = "Password";
            txtRegConfirmPass.Text = "Confirm password";
            txtRegFirstName.Text = "First name";
            txtRegLastName.Text = "Last name";
            txtRegPassword.Text = "Password";
            txtRegUserName.Text = "User name";
            txtUserName.Text = "User name";            
        }

        private void btnSignOut_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void btnSignOut_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCrawler_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void btnCrawler_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
          
        }

        private void label3_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCrawler_Click(object sender, EventArgs e)
        {
       
        }


        private void btnCrawler_MouseMove_1(object sender, MouseEventArgs e)
        {
            btnCrawler.Size = new Size(82, 78);
            btnCrawler.Location = new Point(464, 44);
            lblCrawler.Visible = true;

            lblCrawler.Visible = true;           
        }

        private void btnCrawler_MouseLeave_1(object sender, EventArgs e)
        {
            if (CrawlerClicked == true)
            {
                lblCrawler.Visible = true;

                btnCrawler.Size = new Size(82, 78);
                btnCrawler.Location = new Point(464, 44);
            }
            else
            {
                lblCrawler.Visible = false;

                btnCrawler.Size = new Size(72, 68);
                btnCrawler.Location = new Point(466, 46);
            }
        }

        private void btnSignOut_MouseMove_1(object sender, MouseEventArgs e)
        {
            lblSignOut.Visible = true;

            btnSignOut.Size = new Size(50, 56);
            btnSignOut.Location = new Point(921, 10);
        }

        private void btnSignOut_MouseLeave_1(object sender, EventArgs e)
        {
            lblSignOut.Visible = false;

            btnSignOut.Size = new Size(40, 46);
            btnSignOut.Location = new Point(923, 12);
        }

        private void btnSignOut_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to sign out?", "Sign out", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                pnlDashBoard.Visible = false;
                pnlHome.Visible = true;

                btnLockScreen.Visible = false;
                lblLock.Visible = false;

                pnlHomeOption.Location = new Point(412, 139);
               
                pnlSignIn.Visible = false;
                pnlAddNewUser.Visible = false;
           }
        }

        private void label3_MouseMove_1(object sender, MouseEventArgs e)
        {
            label3.Font = new Font(label3.Font.FontFamily, 14);

            if (SystemAdminClicked == true)
            {
                label3.Location = new Point(666, 60);
            }
            else
            {
                label3.Location = new Point(664, 58);
            }
        }

        private void label3_MouseLeave_1(object sender, EventArgs e)
        {
            if (SystemAdminClicked == true)
            {
                label3.Font = new Font(label3.Font.FontFamily, 14);
                label3.Location = new Point(666, 60);
            }
            else
            {
                label3.Font = new Font(label3.Font.FontFamily, 11);
            }

            label3.Location = new Point(666, 60);
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            DashboardClicked = false;
            SystemAdminClicked = true;

            picBoxDasboard.Size = new Size(50, 40);
            picBoxDasboard.Location = new Point(268, 46);

            picBoxAdmin.Size = new Size(60, 50);
            picBoxAdmin.Location = new Point(599, 44);

            label2.Font = new Font(label3.Font.FontFamily, 11);
            label3.Font = new Font(label3.Font.FontFamily, 14);

            btnCrawler.Size = new Size(72, 68);
            btnCrawler.Location = new Point(466, 46);
            lblCrawler.Visible = false;

            CrawlerClicked = false;

            pnlCrawler.Visible = false;
            pnlAdministrator.Visible = true;
            pnlDashBoardCompo.Visible = false;
        }

        private void label2_MouseMove_1(object sender, MouseEventArgs e)
        {
            label2.Font = new Font(label2.Font.FontFamily, 14);

            if (DashboardClicked == true)
            {
                label2.Location = new Point(331, 59);
            }
            else
            {
                label2.Location = new Point(331, 59);
            }
        }

        private void label2_MouseLeave_1(object sender, EventArgs e)
        {
            if (DashboardClicked == true)
            {
                label2.Font = new Font(label2.Font.FontFamily, 14);
                label2.Location = new Point(333, 61);
            }
            else
            {
                label2.Font = new Font(label2.Font.FontFamily, 11);
            }

            label2.Location = new Point(331, 59);
        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            DashboardClicked = true;
            SystemAdminClicked = false;

            picBoxDasboard.Size = new Size(60, 50);
            picBoxDasboard.Location = new Point(266, 44);

            picBoxAdmin.Size = new Size(50, 40);
            picBoxAdmin.Location = new Point(601, 46);

            label2.Font = new Font(label3.Font.FontFamily, 14);
            label3.Font = new Font(label3.Font.FontFamily, 11);

            btnCrawler.Size = new Size(72, 68);
            btnCrawler.Location = new Point(466, 46);
            lblCrawler.Visible = false;

            CrawlerClicked = false;

            pnlCrawler.Visible = false;
            pnlDashBoardCompo.Visible = true;
            pnlAdministrator.Visible = false;
        }

        private void btnCrawler_Click_1(object sender, EventArgs e)
        {
            CrawlerClicked = true;

            picBoxDasboard.Size = new Size(50, 40);
            picBoxDasboard.Location = new Point(268, 46);

            picBoxAdmin.Size = new Size(50, 40);
            picBoxAdmin.Location = new Point(601, 46);

            label2.Font = new Font(label3.Font.FontFamily, 11);
            label3.Font = new Font(label3.Font.FontFamily, 11);

            pnlCrawler.Visible = true;
            pnlAdministrator.Visible = false;
            pnlDashBoardCompo.Visible = false;
        }

        private void btnAdminAddUser_MouseMove(object sender, MouseEventArgs e)
        {
            btnAdminAddUser.Size = new Size(61, 60);
            btnAdminAddUser.Location = new Point(373, 64);

            lblAddUser.Visible = true;
        }

        private void btnAdminAddUser_MouseLeave(object sender, EventArgs e)
        {
            btnAdminAddUser.Size = new Size(51, 50);
            btnAdminAddUser.Location = new Point(375, 66);

            lblAddUser.Visible = false;
        }

        private void btnDeleteUser_MouseMove(object sender, MouseEventArgs e)
        {
            btnDeleteUser.Size = new Size(61, 60);
            btnDeleteUser.Location = new Point(475, 64);

            lblRemoveUser.Visible = true;
        }

        private void btnDeleteUser_MouseLeave(object sender, EventArgs e)
        {
            btnDeleteUser.Size = new Size(51, 50);
            btnDeleteUser.Location = new Point(477, 66);

            lblRemoveUser.Visible = false;
        }

        private void btnEditUser_MouseMove(object sender, MouseEventArgs e)
        {
            btnEditUser.Size = new Size(61, 60);
            btnEditUser.Location = new Point(572, 64);

            lblEditUser.Visible = true;
        }

        private void btnEditUser_MouseLeave(object sender, EventArgs e)
        {
            btnEditUser.Size = new Size(51, 50);
            btnEditUser.Location = new Point(574, 66);
            
            lblEditUser.Visible = false;
        }

        private void btnDeleteSystemUser_MouseMove(object sender, MouseEventArgs e)
        {
            btnDeleteSystemUser.Size = new Size(61, 63);
            btnDeleteSystemUser.Location = new Point(475, 188);

            lblDelete.Visible = true;
        }

        private void btnDeleteSystemUser_MouseLeave(object sender, EventArgs e)
        {
            btnDeleteSystemUser.Size = new Size(51, 53);
            btnDeleteSystemUser.Location = new Point(477, 190);

            lblDelete.Visible = false;
        }

        private void btnAcceptNewReg_MouseMove(object sender, MouseEventArgs e)
        {
            btnAcceptNewReg.Size = new Size(110, 79);
            btnAcceptNewReg.Location = new Point(41, 16);

            lblAcceptNewReg.Visible = true;
        }

        private void btnAcceptNewReg_MouseLeave(object sender, EventArgs e)
        {
            btnAcceptNewReg.Size = new Size(100, 69);
            btnAcceptNewReg.Location = new Point(43, 18);

            lblAcceptNewReg.Visible = false;
        }

        private void btnAdminAddUser_Click(object sender, EventArgs e)
        {
            DropUserToDeleteOrEdit.Visible = false;
            btnDeleteSystemUser.Visible = false;

            txtFirstName.Visible = true;
            txtLastName.Visible = true;
            txtUserName.Visible = true;
            txtConfirmPass.Visible = true;
            txtPasswword.Visible = true;
            DropTypeOfUser.Visible = true;

            btnAcceptNewReg.Visible = true;

            txtConfirmPass.Text = "Confirm password";
            txtFirstName.Text = "First name";
            txtLastName.Text = "Last name";
            txtLoginPassword.Text = "Password";
            txtLoginUserName.Text = "User name";
            txtPasswword.Text = "Password";
            txtRegConfirmPass.Text = "Confirm password";
            txtRegFirstName.Text = "First name";
            txtRegLastName.Text = "Last name";
            txtRegPassword.Text = "Password";
            txtRegUserName.Text = "User name";
            txtUserName.Text = "User name";
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            DropUserToDeleteOrEdit.Visible = true;
            btnDeleteSystemUser.Visible = true;

            txtFirstName.Visible = false;
            txtLastName.Visible = false;
            txtUserName.Visible = false;
            txtConfirmPass.Visible = false;
            txtPasswword.Visible = false;
            DropTypeOfUser.Visible = false;

            btnAcceptNewReg.Visible = false;

            txtConfirmPass.Text = "Confirm password";
            txtFirstName.Text = "First name";
            txtLastName.Text = "Last name";
            txtLoginPassword.Text = "Password";
            txtLoginUserName.Text = "User name";
            txtPasswword.Text = "Password";
            txtRegConfirmPass.Text = "Confirm password";
            txtRegFirstName.Text = "First name";
            txtRegLastName.Text = "Last name";
            txtRegPassword.Text = "Password";
            txtRegUserName.Text = "User name";
            txtUserName.Text = "User name";
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            DropUserToDeleteOrEdit.Visible = true;
            btnDeleteSystemUser.Visible = true;

            btnDeleteSystemUser.Visible = false;

            txtFirstName.Visible = true;
            txtLastName.Visible = true;
            txtUserName.Visible = true;
            txtConfirmPass.Visible = true;
            txtPasswword.Visible = true;
            DropTypeOfUser.Visible = true;

            btnAcceptNewReg.Visible = true;

            txtConfirmPass.Text = "Confirm password";
            txtFirstName.Text = "First name";
            txtLastName.Text = "Last name";
            txtLoginPassword.Text = "Password";
            txtLoginUserName.Text = "User name";
            txtPasswword.Text = "Password";
            txtRegConfirmPass.Text = "Confirm password";
            txtRegFirstName.Text = "First name";
            txtRegLastName.Text = "Last name";
            txtRegPassword.Text = "Password";
            txtRegUserName.Text = "User name";
            txtUserName.Text = "User name";
        }

        private void txtFirstName_Enter(object sender, EventArgs e)
        {
            if (txtFirstName.Text == "First name")
            {
                txtFirstName.Text = "";
            }
            else
            {
                if (txtFirstName.Text == "")
                {
                    txtFirstName.Text = "First name";
                }
            }
        }

        private void txtFirstName_Leave(object sender, EventArgs e)
        {
            if (txtFirstName.Text == "First name")
            {

            }
            else
            {
                if (txtFirstName.Text == "")
                {
                    txtFirstName.Text = "First name";
                    txtFirstName.ForeColor = Color.Gray;
                }
                else
                {
                    txtFirstName.ForeColor = Color.Black;
                }
            }
        }

        private void txtLastName_Enter(object sender, EventArgs e)
        {
            if (txtLastName.Text == "Last name")
            {
                txtLastName.Text = "";
            }
            else
            {
                if (txtLastName.Text == "")
                {
                    txtLastName.Text = "Last name";
                }
            }
        }

        private void txtLastName_Leave(object sender, EventArgs e)
        {
            if (txtLastName.Text == "Last name")
            {

            }
            else
            {
                if (txtLastName.Text == "")
                {
                    txtLastName.Text = "Last name";
                    txtLastName.ForeColor = Color.Gray;
                }
                else
                {
                    txtLastName.ForeColor = Color.Black;
                }
            }
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            if (txtUserName.Text == "User name")
            {
                txtUserName.Text = "";
            }
            else
            {
                if (txtUserName.Text == "")
                {
                    txtUserName.Text = "User name";
                }
            }
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            if (txtUserName.Text == "User name")
            {

            }
            else
            {
                if (txtUserName.Text == "")
                {
                    txtUserName.Text = "User name";
                    txtUserName.ForeColor = Color.Gray;
                }
                else
                {
                    txtUserName.ForeColor = Color.Black;
                }
            }
        }

        private void txtPasswword_Enter(object sender, EventArgs e)
        {
            if (txtPasswword.Text == "Password")
            {
                txtPasswword.UseSystemPasswordChar = true;
                txtPasswword.Text = "";
                txtPasswword.ForeColor = Color.Black;
            }
            else
            {
                if (txtPasswword.Text == "")
                {
                    txtPasswword.Text = "Password";
                    txtPasswword.UseSystemPasswordChar = false;
                }
            }
        }

        private void txtPasswword_Leave(object sender, EventArgs e)
        {
            if (txtPasswword.Text == "Password")
            {
                txtPasswword.UseSystemPasswordChar = false;
                txtPasswword.ForeColor = Color.Gray;
            }
            else
            {
                if (txtPasswword.Text == "")
                {
                    txtPasswword.Text = "Password";
                    txtPasswword.UseSystemPasswordChar = false;
                    txtPasswword.ForeColor = Color.Gray;
                }
                else
                {
                    txtPasswword.ForeColor = Color.Black;
                }
            }
        }

        private void txtConfirmPass_Enter(object sender, EventArgs e)
        {
            if (txtConfirmPass.Text == "Confirm password")
            {
                txtConfirmPass.UseSystemPasswordChar = true;
                txtConfirmPass.Text = "";
                txtConfirmPass.ForeColor = Color.Black;
            }
            else
            {
                if (txtConfirmPass.Text == "")
                {
                    txtConfirmPass.Text = "Confirm password";
                    txtConfirmPass.UseSystemPasswordChar = false;
                }
            }
        }

        private void txtConfirmPass_Leave(object sender, EventArgs e)
        {
            if (txtConfirmPass.Text == "Confirm password")
            {
                txtConfirmPass.UseSystemPasswordChar = false;
                txtConfirmPass.ForeColor = Color.Gray;
            }
            else
            {
                if (txtConfirmPass.Text == "")
                {
                    txtConfirmPass.Text = "Confirm password";
                    txtConfirmPass.UseSystemPasswordChar = false;
                    txtConfirmPass.ForeColor = Color.Gray;
                }
                else
                {
                    txtConfirmPass.ForeColor = Color.Black;
                }
            }
        }

        
    }
}
