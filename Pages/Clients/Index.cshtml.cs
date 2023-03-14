using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CRUD.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<StudentInfo> StudentList = new List<StudentInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=crud;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        { 
                            while (reader.Read())
                            {
                                StudentInfo student = new StudentInfo();
                                student.id = "" + reader.GetInt32(0);
                                student.name = reader.GetString(1);
                                student.email = reader.GetString(2);
                                student.phone = reader.GetString(3);
                                student.address = reader.GetString(4);

                                StudentList.Add(student);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception" + e.ToString());
            }
        }
    }

    public class StudentInfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
    }
}
