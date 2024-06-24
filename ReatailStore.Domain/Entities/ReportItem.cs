using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReatailStore.Domain.Entities
{
    public class ReportItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public double Procent { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; } = 0;
        public bool isAmount { get; set; }
        public bool isWight { get; set; }
    }
}
