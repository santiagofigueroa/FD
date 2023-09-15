using FD.Interfaces;

namespace FD.Models
{
    public class Price : IPrice
    {
        public decimal Rate { get; set; }
        public int? Threshold { get; set; }
    }
}
