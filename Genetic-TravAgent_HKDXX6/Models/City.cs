﻿namespace Genetic_TravAgent_HKDXX6.Models;

public class City
{
    public City(double x, double y)
    {
        X = x;
        Y = y;
    }
    public double X { get; set; }
    public double Y { get; set; }
    
    public double DistanceFrom(City otherCity) => Math.Sqrt(Math.Pow(X - otherCity.X, 2) + Math.Pow(Y - otherCity.Y, 2));

    public override bool Equals(object? obj)
    {
        if (obj is not City otherCity)
        {
            return false;
        }

        return X == otherCity.X && Y == otherCity.Y;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
    
    public static bool operator ==(City? left, City? right)
    {  
        if (left is null && right is null) return true;

        if (left is null || right is null) return false;

        return left.Equals(right);
    }

    public static bool operator !=(City? left, City? right)
    {
        return !(left == right);
    }
}