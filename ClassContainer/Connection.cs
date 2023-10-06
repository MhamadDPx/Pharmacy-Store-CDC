using System.Data;
using System.Data.SqlClient;

namespace Pharmacy_Store.ClassContainer
{
    public class Connection
    {
        private string connectionString = "Data Source=MUHAMMED\\SQLEXPRESS;Initial Catalog=db_pharmacy;Integrated Security=True";

        public SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }

        public DataTable GetData(string query)
        {
            DataTable DT = new DataTable();

            using (SqlConnection con = GetConnection())
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                {
                    sda.Fill(DT);
                }
            }

            return DT;
        }

        public void InsertData(string TableName, string TableValues, bool HasEntryBy = true, bool HasUpdateBy = true)
        {
            string InsertQuery = $"INSERT INTO {TableName} VALUES ({TableValues}{(HasEntryBy == true ? $",{UserInfo.UserID},GETDATE()" : "")}{(HasUpdateBy == true ? $",NULL,NULL" : "")});";

            using (SqlConnection con = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(InsertQuery, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdatetData(string TableName, string TableValues, string TableWhere, bool HasUpdateBy = true)
        {
            string UpdateQuery = $"UPDATE {TableName} SET {TableValues}{(HasUpdateBy == true ? $",u_by={UserInfo.UserID},u_date=GETDATE()" : "")} WHERE {TableWhere};";

            using (SqlConnection con = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(UpdateQuery, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeletetData(string TableName, string TableWhere)
        {
            string DeleteQuery = $"UPDATE {TableName} SET archived=1 WHERE {TableWhere};";
            using (SqlConnection con = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(DeleteQuery, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
