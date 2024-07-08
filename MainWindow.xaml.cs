using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace DormManagementSystemAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string connectionString = "server=localhost;database=bnk_dorm;user=root;password=joanbalaca";
        public MainWindow()
        {
            InitializeComponent();
        }
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        private bool ValidateUser(string username, string password)
        {
            bool isValid = false;

            string query = "SELECT COUNT(1) FROM Admin WHERE username=@Username AND Password=@password";

            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count == 1)
                        {
                            isValid = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            return isValid;
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTxtbox.Text;
            string password = passwordBox.Password;

            if (ValidateUser(username, password))
            {
                MessageBox.Show("Login successful!");
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
                this.Hide();
                adminWindow.Closed += (s, args) => this.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
           
        }
    }
}