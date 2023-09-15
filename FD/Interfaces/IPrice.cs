namespace FD.Interfaces
{
    public interface IPrice
    {
        decimal Rate { get; set; }
        int? Threshold { get; set; }
    }
}
