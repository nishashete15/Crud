using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CRUD.Pages.Clients
{
    public class AddNewModel : PageModel
    {
        public StudentInfo studentInfo = new StudentInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            studentInfo.name = Request.Form["name"];
            studentInfo.email = Request.Form["email"];
            studentInfo.phone = Request.Form["phone"];
            studentInfo.address = Request.Form["address"];

            if (studentInfo.name.Length == 0 || studentInfo.email.Length == 0 || studentInfo.phone.Length == 0 || studentInfo.address.Length == 0)
            {
                errorMessage = "All the fields  are Required";
                return;
            }

            //Save the new student into database
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=crud;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Clients" +
                                 "(name, email, phone, address) VALUES" +
                                 "(@name, @email, @phone, @address);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", studentInfo.name);
                        command.Parameters.AddWithValue("@email", studentInfo.email);
                        command.Parameters.AddWithValue("@phone", studentInfo.phone);
                        command.Parameters.AddWithValue("@address", studentInfo.address);

                        command.ExecuteNonQuery(); 
                    }
                }

			}
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            {

            }

            studentInfo.name = "";
            studentInfo.email = "";
            studentInfo.phone = "";
            studentInfo.address = "";
            successMessage = "New Student is Added Successfully";

            Response.Redirect("/Clients/Index");

        }
    }
}
