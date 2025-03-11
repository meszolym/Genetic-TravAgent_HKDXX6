using System.Runtime.InteropServices;
using System.Text;

namespace Genetic_TravAgent_HKDXX6.Models;

public class Tour
{
    public List<City> CityList { get; }
    public double Fitness { get; private set; }

    public Tour(List<City> cityList)
    {
        CityList = cityList;
        RecalculateFitness();
    }

    public void RecalculateFitness()
    {
        var total = 0.0;
        for (var i = 0; i < CityList.Count - 1; i++)
        {
            total += CityList[i].DistanceFrom(CityList[i + 1]);
        }

        Fitness = total;
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        for (var i = 0; i < CityList.Count - 1; i++)
        {
            sb.Append($"vector(({CityList[i].X}, {CityList[i].Y}), ({CityList[i + 1].X}, {CityList[i + 1].Y}))\n");
        }

        return sb.ToString();
    }
}