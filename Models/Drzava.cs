using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrzavaNaselje.Models
{
    public class Drzava
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public ICollection<Naselje> Naselja { get; set; }
    }
}
