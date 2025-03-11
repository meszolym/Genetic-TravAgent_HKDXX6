using Genetic_TravAgent_HKDXX6.Models;

namespace Genetic_TravAgent_HKDXX6;

public class GeneticAlgorithm
{
    private readonly List<City> _cities;
    private readonly int _populationSize;
    private readonly int _generations;
    private readonly double _mutationRate;
    private readonly double _eliteRate;
    private readonly int _tournamentSize;
    private readonly Random _random;

    public GeneticAlgorithm(List<City> cities, int populationSize, int generations, double mutationRate,
        double eliteRate, int tournamentSize, Random? random = null)
    {
        if (cities is null || cities.Count == 0)
            throw new ArgumentNullException(nameof(cities), "Cities cannot be null or empty.");
        _cities = cities;

        if (populationSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(populationSize), "Population size must be greater than 0.");
        _populationSize = populationSize;

        if (generations <= 0)
            throw new ArgumentOutOfRangeException(nameof(generations), "Generations must be greater than 0.");
        _generations = generations;

        if (mutationRate is > 1 or < 0)
            throw new ArgumentOutOfRangeException(nameof(mutationRate), "Mutation rate must be between 0 and 1.");
        _mutationRate = mutationRate;

        if (eliteRate is > 1 or < 0)
            throw new ArgumentOutOfRangeException(nameof(eliteRate), "Elite rate must be between 0 and 1.");
        _eliteRate = eliteRate;

        if (tournamentSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(tournamentSize), "Tournament size must be greater than 0.");
        _tournamentSize = tournamentSize;

        _random = random ?? Random.Shared;
    }

    public Tour Run()
    {
        var population = GenerateInitialPopulation();

        for (var generation = 0; generation < _generations; generation++)
        {
            population = Evolve(population);
            Console.WriteLine($"Generation {generation++}: Best TourLength = {population.Min(t => t.TourLength)}");
        }

        return population.OrderBy(t => t.TourLength).First();
    }

    private List<Tour> GenerateInitialPopulation()
    {
        var population = new List<Tour>();
        for (var i = 0; i < _populationSize; i++)
        {
            var cityList = _cities.OrderBy(x => _random.Next()).ToList();
            population.Add(new Tour(cityList));
        }

        return population;
    }

    private List<Tour> Evolve(List<Tour> population)
    {
        var newPopulation = new List<Tour>();

        var eliteCount = (int)Math.Round(_eliteRate * _populationSize);
        newPopulation.AddRange(population.OrderBy(t => t.TourLength).Take(eliteCount));

        // Generate offspring through crossover and mutation
        while (newPopulation.Count < _populationSize)
        {
            var parent1 = TournamentSelection(population);
            var parent2 = TournamentSelection(population);
            var child = Crossover(parent1, parent2);

            if (_random.NextDouble() < _mutationRate)
            {
                Mutate(child);
            }

            newPopulation.Add(child);
        }

        return newPopulation;
    }

    private void Mutate(Tour tour)
    {
        var index1 = _random.Next(0, tour.CityList.Count);
        var index2 = _random.Next(0, tour.CityList.Count);
        (tour.CityList[index1], tour.CityList[index2]) = (tour.CityList[index2], tour.CityList[index1]);

        tour.RecalculateTourLength();
    }

    private Tour Crossover(Tour tour1, Tour tour2)
    {
        if (tour1.CityList.Any(x => !tour2.CityList.Contains(x))
            || tour2.CityList.Any(x => !tour1.CityList.Contains(x)))
        {
            throw new ArgumentException("Tours don't have the same cities.");
        }

        var start = _random.Next(tour1.CityList.Count);
        var end = _random.Next(start, tour1.CityList.Count);

        var childCities = new List<City?>(new City[tour1.CityList.Count]);

        for (var i = start; i < end; i++)
        {
            childCities[i] = tour1.CityList[i];
        }

        var currentIndex = 0;

        foreach (var city in tour2.CityList.Where(c => !childCities.Contains(c)))
        {
            while (childCities[currentIndex] != null) //find first empty slot
            {
                currentIndex++;
            }

            childCities[currentIndex] = city;
        }

        return new Tour(childCities!);
    }

    private Tour TournamentSelection(List<Tour> population) => population.OrderBy(x => _random.Next())
        .Take(_tournamentSize).OrderBy(t => t.TourLength).First();
}