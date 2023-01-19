namespace TestElders.Models
{
    public class Cell
    {
        public Cell(Position position)
        {
            Position = position ?? throw new ArgumentNullException(nameof(position));
        }

        public Position Position { get; }

        public IList<Animal> Animals { get; } = new List<Animal>();

        public void Visit(Animal animal)
        {
            if (animal is null) throw new ArgumentNullException(nameof(animal));

            animal.Move(this);
            Animals.Add(animal);          
        }

        public void Spawn(Animal animal)
        {
            if (animal is null) throw new ArgumentNullException(nameof(animal));

            Animals.Add(animal);
        }

        public void Leave(Animal animal)
        {
            if (animal is null) throw new ArgumentNullException(nameof(animal));

            Animals.Remove(animal);
        }
    }
}
