using System.Runtime.InteropServices;
using System.Text;

namespace Genetic_TravAgent_HKDXX6.Models;

public class Tour
{
    public List<City> CityList { get; }
    public double TourLength { get; private set; }

    public Tour(List<City> cityList)
    {
        CityList = cityList;
        RecalculateTourLength();
    }

    public void RecalculateTourLength() => TourLength = CityList.Select((city, i) => city.DistanceFrom(CityList[(i + 1) % CityList.Count])).Sum();

    public override string ToString()
    {
        StringBuilder sb = new();
        
        CityList.Select((c, i) => $"vector(({c.X}, {c.Y}), ({CityList[(i + 1) % CityList.Count].X}, {CityList[(i + 1) % CityList.Count].Y}))")
            .ToList()
            .ForEach(s => sb.AppendLine(s));

        return sb.ToString();
    }
}