using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReatailStore.Domain.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public double TotalPrice { get; set; } = 0;
        public double TotalClearPrice { get; set; } = 0;
        public ICollection<ReportItem> ReportItems { get; set; }
        //public int ShopId { get; set; }
        //public Shop Shop { get; set; }
    }
}
