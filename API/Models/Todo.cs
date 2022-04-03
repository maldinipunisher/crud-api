namespace API.Models
{
    public class Todo
    {
        private TodoContext context; 
        private int id { get; set; }
        private string name { get; set; }
        private string description { get; set; }

        public Todo(int id, string name, string description )
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }

        public int Id 
        {
            get { return id; } 
            set { id = value; }
        }

        public string Name
        {
            get { return name;  } 
            set { name = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

    }
}
