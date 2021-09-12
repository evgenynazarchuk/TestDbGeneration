using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDbGeneration.Models
{
    [Table(name: "location", Schema = "testapp")]
    public class Location
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
