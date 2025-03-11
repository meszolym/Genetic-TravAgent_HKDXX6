namespace Genetic_TravAgent_HKDXX6.Models;

public class Tour
{
    public List<City> CityList { get; set; } //Order is important!
    public double Fitness { get; set; }

    public Tour(List<City> cityList)
    {
        CityList = cityList;
        Fitness = GetFitness();
    }

    public double GetFitness()
    {
        var total = 0.0;
        for (int i = 0; i < CityList.Count - 1; i++)
        {
            total += CityList[i].DistanceFrom(CityList[i + 1]);
        }

        return total;
    }

    public void Mutate(Random? random = null)
    {
        random ??= Random.Shared;
        var index1 = random.Next(0, CityList.Count);
        var index2 = random.Next(0, CityList.Count);
        (CityList[index1], CityList[index2]) = (CityList[index2], CityList[index1]);
    }

    public static Tour Crossover(Tour tour1, Tour tour2, Random? random = null)
    {
        if (tour1.CityList.Any(x => !tour2.CityList.Contains(x))
            || tour2.CityList.Any(x => !tour1.CityList.Contains(x)))
        {
            throw new ArgumentException("Tours don't have the same cities.");
        }

        random ??= Random.Shared;
        int start = random.Next(tour1.CityList.Count);
        int end = random.Next(start, tour1.CityList.Count);

        List<City> childCities = new List<City>(new City[tour1.CityList.Count]);

        for (int i = start; i < end; i++)
        {
            childCities[i] = tour1.CityList[i];
        }

        // Fill the remaining cities from parent2, avoiding duplicates
        int currentIndex = 0;
        for (int i = 0; i < tour2.CityList.Count; i++)
        {
            if (!childCities.Contains(tour2.CityList[i]))
            {
                while (childCities[currentIndex] != default(City))
                {
                    currentIndex++;
                }

                childCities[currentIndex] = tour2.CityList[i];
            }
        }

        return new Tour(childCities);
    }
}