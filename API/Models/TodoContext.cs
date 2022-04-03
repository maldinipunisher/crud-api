using MySql.Data.MySqlClient;
namespace API.Models
{
    public class TodoContext
    {
        private string connectionString { get; set; }

        public TodoContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public Todo Get(int id)
        {
            Todo? todo = null; 
            using (MySqlConnection connection = GetConnection())
            {

                connection.Open();
                MySqlCommand command = new MySqlCommand("select * from todo where id =" + id, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        todo = new Todo(Convert.ToInt32(reader["id"]), reader["name"].ToString()!, reader["decription"].ToString()!);
                    }
                }
            }
            return todo! ;

        }

        public Todo Create(Todo todo) {
            using (MySqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open(); 
                    MySqlCommand command = new MySqlCommand("INSERT INTO todo (id, name, decription) VALUES ('" + todo.Id + "','" + todo.Name + "', '" + todo.Description +"')", connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            todo = new Todo( Convert.ToInt32(reader["id"]), reader["name"].ToString()! , reader["decription"].ToString()! );
                        }
                    }

                } catch (Exception e)
                {
                    Console.WriteLine("Error " + e.Message); 
                }
                return todo; 
            }
        }

        public Todo Update(Todo todo)
        {
            using (MySqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("UPDATE todo SET name='" + todo.Name + "', decription='" + todo.Description + "' where id = " + todo.Id, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            todo = new Todo(Convert.ToInt32(reader["id"]), reader["name"].ToString()!, reader["decription"].ToString()!);
                        }
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error " + e.Message);
                }
                return todo;
            }
        }

        public bool Delete(int id)
        {
            using (MySqlConnection connection = GetConnection())
            {

                
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("delete from todo where id =" + id, connection);
                    int success =command.ExecuteNonQuery();
                    if(success == 1)
                    {
                        return true; 
                    }else
                    {
                        return false; 
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                    return false; 
                }
            }
        }



        public List<Todo> GetAll()
        {
            List<Todo> list = new List<Todo>();
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("select * from todo", connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Todo(Convert.ToInt32(reader["id"]),
                            reader["name"].ToString()!,
                            reader["decription"].ToString()!
                            ));
                    }
                }
            }
            return list;
        }

    }
}
