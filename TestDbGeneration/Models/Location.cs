using System.ComponentModel.DataAnnotations.Schema;

namespace TestDbGeneration.Models
{
    [Table(name: "location", Schema = "dict")]
    public class Location
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
