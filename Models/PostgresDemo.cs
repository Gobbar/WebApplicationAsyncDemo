using System.ComponentModel.DataAnnotations;

namespace WebApplicationAsyncDemo.Models
{
    public class PostgresDemo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
