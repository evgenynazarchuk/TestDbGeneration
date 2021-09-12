using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDbGeneration.Models
{
    [Table(name: "person", Schema = "app")]
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
