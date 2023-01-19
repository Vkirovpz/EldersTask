namespace TestElders.Models
{
    public interface IRandomNumberGenerator
    {
        int GetBetween(int min, int max);
    }
}