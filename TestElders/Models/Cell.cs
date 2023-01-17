namespace TestElders.Models
{
    public class Cell
    {
        public Cell(Position position)
        {
            Position = position;
        }

        public Position Position { get; }

        public IList<Animal> Animals { get; } = new List<Animal>();

        public void Visit(Animal animal)
        {
            animal.Move(Position);

            Animals.Add(animal);
            
        }

        public void Leave(Animal animal)
        {
            Animals.Remove(animal);
        }
    }
}
