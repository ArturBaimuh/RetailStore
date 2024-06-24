using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ReatailStore.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int ProviderId { get; set; }
        public int StockId { get; set; }
        public int Amount { get; set; }
        public int SectionId { get; set; }
        public DateTime DateValidFrom { get; set; }
        public DateTime DateValidTo { get; set; }
        public string Barcode { get; set; }
        public double Weight { get; set; }
        public bool isAmount { get; set; }
        public bool isWight { get; set; }
        public Provider Provider { get; set; }
        public Section Section { get; set; }
        public Stock Stock{ get; set; }
    }
}
