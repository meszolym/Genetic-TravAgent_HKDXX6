using Genetic_TravAgent_HKDXX6.Models;

namespace Genetic_TravAgent_HKDXX6;

public class Program
{
    public const int NUM_CITIES = 10;
    public static void Main(string[] args)
    {
        List<City> cities = new List<City>();
        for (int i = 0; i < NUM_CITIES; i++)
        {
            cities.Add(new City(Random.Shared.Next(0,100), Random.Shared.Next(0,100)));
        }
        
        Tour tour1 = new Tour(cities);
        Tour tour2 = new Tour(cities.OrderBy(x => Random.Shared.Next(0, 100)).ToList());
        
        Console.WriteLine($"Tour 1 Fitness: {tour1.Fitness}");
        Console.WriteLine($"Tour 2 Fitness: {tour2.Fitness}");
        
        Tour childTour = Tour.Crossover(tour1, tour2);
        
        Console.WriteLine("Child Tour Fitness: " + childTour.Fitness);
    }
}