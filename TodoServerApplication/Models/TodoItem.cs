namespace TodoServerApplication.Models
{
    public class TodoItem
    {
        public Guid id { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public string category { get; set; }

        public bool status { get; set; }
    }
}
