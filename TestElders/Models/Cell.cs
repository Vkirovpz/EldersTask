namespace TestElders.Models
{
    public class Cell
    {
        private readonly List<Animal> animals = new();

        public Cell(Position position)
        {
            Position = position ?? throw new ArgumentNullException(nameof(position));
        }

        public Position Position { get; }

        public IEnumerable<Animal> Animals => animals.AsReadOnly();

        public void Visit(Animal animal)
        {
            if (animal is null) throw new ArgumentNullException(nameof(animal));

            animal.Move(this);
            animals.Add(animal);
        }

        public void Spawn(IAnimalCreator animalCreator)
        {
            if (animalCreator is null) throw new ArgumentNullException(nameof(animalCreator));
            
            var animal = animalCreator.Create(this);
            animals.Add(animal);
        }

        public void Leave(Animal animal)
        {
            if (animal is null) throw new ArgumentNullException(nameof(animal));

            animals.Remove(animal);
        }
    }
}
