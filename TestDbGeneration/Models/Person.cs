using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDbGeneration.Models
{
    [Table(name: "person", Schema = "testapp")]
    public class Person : DbContext
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
