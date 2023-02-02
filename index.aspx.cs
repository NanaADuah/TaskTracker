using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace TaskTracker
{
    public partial class index : System.Web.UI.Page
    {
        public TaskCount values;
        protected IList<Tasks> tasks;
        private SqlConnection connection;
        private string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Nana Duah\\Documents\\VS Projects\\TaskTracker\\App_Data\\Source.mdf\";Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            viewTasks();   
        }

        protected void btnSaveTask_Click(object sender, EventArgs e)
        {
            string taskName;
            DateTime taskDate;
            int setReminder;

            taskName = tbTaskName.Text;
            taskDate = DateTime.Now;      //TODO: Add ability to set custom date
            setReminder = chkSetReminder.Checked ? 1 : 0;
            connection = new SqlConnection(connStr);
            try
            {
                connection.Open();
                string command = $"INSERT INTO Tasks (TaskName, TaskDate, SetReminder) VALUES ('{taskName}','{taskDate}', '{setReminder}')";
                SqlCommand cmd = new SqlCommand(command, connection);
                cmd.ExecuteNonQuery();
                lblMessages.Text = "New task added!";
                connection.Close();
                Response.Redirect("index.aspx");
            }
            catch(SqlException ex)
            {
                lblMessages.Text = String.Format("{0}", ex.Message);
            }

        }

        private void viewTasks()
        {
            string command = "SELECT TaskID, TaskName, SetReminder, TaskDate FROM Tasks ORDER BY TaskDate DESC";
            
            tasks = new List<Tasks>();
            values = new TaskCount();
            connection = new SqlConnection(connStr);
            try
            {
                connection.Open();
                SqlCommand countCmd = new SqlCommand("SELECT COUNT(*) AS Total FROM [Tasks]", connection);
                values.count = int.Parse(countCmd.ExecuteScalar().ToString());
                countCmd.Dispose();

                SqlCommand cmd = new SqlCommand(command, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                if (values.count > 0)
                {
                    while (reader.Read())
                    {
                        tasks.Add(new Tasks
                        {
                            TaskID = int.Parse(reader.GetValue(0).ToString()),
                            TaskName = reader.GetValue(1).ToString(),
                            SetReminder = (reader.GetValue(2).ToString() == "1") ? true : false ,
                            TaskDate = DateTime.Parse(reader.GetValue(3).ToString()),
                        });
                    }
                }
            }
            catch (SqlException ex)
            {
                lblMessages.Text = String.Format("{0}", ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public class TaskCount
        {
            public int count { get; set; }
        }

        public void deleteTask(int id)
        {
            
        }
        public class Tasks
        {
            public int TaskID { get; set; }
            public string TaskName { get; set; }
            public bool SetReminder { get; set; }
            public DateTime TaskDate { get; set; }
        }

        protected void iDeleteTask_Click(object sender, EventArgs e)
        {
            values.count -= 1;
            string query = $"DELETE FROM Tasks WHERE TaskID = {values.count}";
            connection = new SqlConnection(connStr);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                lblMessages.Text = "Deleted task";

            }
            catch (SqlException ex)
            {
                lblMessages.Text = ex.Message;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}