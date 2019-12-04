namespace day_4.Models
{
    public interface IFoodDish
    {
        string Description { get; set; }
        string FromWho { get; set; }
        string Name { get; set; }
    }
}