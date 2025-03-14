﻿using Genetic_TravAgent_HKDXX6.Models;

namespace Genetic_TravAgent_HKDXX6;

public class Program
{
    const int NUM_CITIES = 20;

    public static void Main(string[] args)
    {
        var cities = new List<City>();
        for (var i = 0; i < NUM_CITIES; i++)
        {
            cities.Add(new City(Random.Shared.Next(0, 100), Random.Shared.Next(0, 100)));
        }

        var ga = new GeneticAlgorithm(cities, 100, 0.5, 0.05, 10);

        //var bestTour = ga.RunForTime(TimeSpan.FromMinutes(1));
        //var bestTour = ga.RunWithGenCap(10000);
        var bestTour = ga.RunForDelta(3, 1000);
        Console.Write("Cities: [");
        for (var i = 0; i < cities.Count; i++)
        {
            Console.Write(cities[i].ToString());
            if (i != cities.Count - 1)
            {
                Console.Write(", ");
            }
        }

        Console.WriteLine("]");

        Console.WriteLine("Best tour:\n" + bestTour.ToString());
    }
}