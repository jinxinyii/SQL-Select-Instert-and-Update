using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DB_Programming
{
    public partial class FrmUpdateMember : Form
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        private SqlDataReader sqlReader;

        public DataTable dataTable;
        public BindingSource bindingSource;

        private long StudentID;
        private int Age;
        private string FirstName, MiddleName, LastName, Gender, Program;
        public FrmUpdateMember()
        {
            InitializeComponent();
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Elmar\source\repos\DB_Programming\ClubDB.mdf;Integrated Security=True";
            sqlConnect = new SqlConnection(connectionString);
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            StudentID = long.Parse(cbStudentNo.Text);
            FirstName = txtFirstName.Text;
            MiddleName = txtMiddleName.Text;
            LastName = txtLastName.Text;
            Age = int.Parse(txtAge.Text);
            Gender = cbGender.Text;
            Program = cbProgram.Text;

            sqlCommand = new SqlCommand("Update ClubMembers Set studentid = @StudentID, " +
                "firstname = @FirstName, middlename = @MiddleName, lastname = @LastName, " +
                "age = @Age, gender = @Gender, program = @Program Where studentid = @StudentID", sqlConnect);
            sqlCommand.Parameters.Add("@StudentID", SqlDbType.BigInt).Value = StudentID;
            sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName;
            sqlCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = MiddleName;
            sqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName;
            sqlCommand.Parameters.Add("@Age", SqlDbType.Int).Value = Age;
            sqlCommand.Parameters.Add("@Gender", SqlDbType.VarChar).Value = Gender;
            sqlCommand.Parameters.Add("@Program", SqlDbType.VarChar).Value = Program;
            sqlConnect.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();

            this.Hide();
            FrmClubRegistration call = new FrmClubRegistration();
            call.Show();
        }

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {
            sqlConnect.Open();
            string ViewClubMembersID = "Select StudentID From ClubMembers";
            SqlCommand command = new SqlCommand(ViewClubMembersID, sqlConnect);
            sqlReader = command.ExecuteReader();
            while (sqlReader.Read())
                cbStudentNo.Items.Add(sqlReader[0].ToString());
            sqlConnect.Close();

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

        private void cbStudentNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ViewClubMembers = "Select FirstName, MiddleName, LastName, Age, Gender, " +
                "Program From ClubMembers Where StudentID = '" + cbStudentNo.Text + "'";
            sqlCommand = new SqlCommand(ViewClubMembers, sqlConnect);
            sqlConnect.Open();
            sqlReader = sqlCommand.ExecuteReader();
            while (sqlReader.Read())
            {
                string firstname = (string)sqlReader["FirstName"].ToString();
                txtFirstName.Text = firstname;
                string middlename = (string)sqlReader["MiddleName"].ToString();
                txtMiddleName.Text = middlename;
                string lastname = (string)sqlReader["LastName"].ToString();
                txtLastName.Text = lastname;
                string age = (string)sqlReader["Age"].ToString();
                txtAge.Text = age;
                string gender = (string)sqlReader["Gender"].ToString();
                cbGender.Text = gender;
                string program = (string)sqlReader["Program"].ToString();
                cbProgram.Text = program;
            }
            sqlConnect.Close();
        }
    }
}
