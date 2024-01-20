using System;
using System.Windows.Forms;

namespace DB_Programming
{
    public partial class FrmClubRegistration : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;
        private int ID, Age, count;
        private string FirstName, MiddleName, LastName, Gender, Program;

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmUpdateMember call = new FrmUpdateMember();
            call.Show();

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            ID = RegistrationID();
            StudentID = long.Parse(txtStudentNo.Text);
            FirstName = txtFirstName.Text;
            MiddleName = txtMiddleName.Text;
            LastName = txtLastName.Text;
            Age = int.Parse(txtAge.Text);
            Gender = cbGender.Text;
            Program = cbProgram.Text;
            clubRegistrationQuery.RegisterStudent(ID, StudentID, FirstName,
                MiddleName, LastName, Age, Gender, Program);
        }

        private long StudentID;
        private void FrmClubRegistration_Load(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
            string[] ListOfProgram = new string[]
            {
                "BSIT",
                "BSCPE",
                "BSBA",
                "BSAIS",
                "BSA",
                "BSCM",
                "BSTM"
            };
            for (int i = 0; i < ListOfProgram.Length; i++)
            {
                cbProgram.Items.Add(ListOfProgram[i].ToString());
            }

            string[] ListOfGender = new string[]
            {
                "Male",
                "Female"
            };
            for (int i = 0; i < ListOfGender.Length; i++)
            {
                cbGender.Items.Add(ListOfGender[i].ToString());
            }
        }
        public FrmClubRegistration()
        {
            InitializeComponent();
            clubRegistrationQuery = new ClubRegistrationQuery();
        }
        public void RefreshListOfClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            dataGridView.DataSource = clubRegistrationQuery.bindingSource;
        }
        public int RegistrationID()
        {
            return count++;
        }
    }
}