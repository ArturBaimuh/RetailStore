using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReatailStore.Domain.Entities
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MarkupId { get; set; }
        public Markup Markup { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
