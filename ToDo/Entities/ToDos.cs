using System.ComponentModel.DataAnnotations;

namespace ToDo.Entities
{
    public class ToDos
    {
        [Key]
        public int ToDoId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public long UserId { get; set; }
    }
}