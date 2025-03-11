namespace Genetic_TravAgent_HKDXX6.Models;

public class City(double x, double y)
{
    public double X { get; set; } = x;
    public double Y { get; set; } = y;

    public double DistanceFrom(City otherCity) =>
        Math.Sqrt(Math.Pow(X - otherCity.X, 2) + Math.Pow(Y - otherCity.Y, 2));
    
    public override bool Equals(object? obj) => obj is City city && X == city.X && Y == city.Y;
    public override int GetHashCode() => HashCode.Combine(X, Y);
    public static bool operator ==(City? left, City? right) => (left, right) switch
    {
        (null, null) => true,
        (null, _) => false,
        (_, null) => false,
        _ => left.Equals(right)
    };
    public static bool operator !=(City? left, City? right) => !(left == right);
    public override string ToString() => $"({X}, {Y})";
}