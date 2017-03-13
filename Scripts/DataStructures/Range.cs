using UnityEngine;

[System.Serializable]
public struct Range
{
    public float From;
    public float To;

    public Range(float fromValue, float toValue)
    {
        From = fromValue;
        To = toValue;
    }

    public float RandomValue()
    {
        return Random.Range(From, To);
    }

    public float RandomValue(PRNG prng)
    {
        return prng.Range(From, To);
    }

    public static Range operator +(Range a, Range b)
    {
        return new Range(a.From + b.From, a.To + b.To);
    }

    public static Range operator -(Range a, Range b)
    {
        return new Range(a.From - b.From, a.To - b.To);
    }

    public static Range operator -(Range a)
    {
        return new Range(-a.From, -a.To);
    }

    public static Range operator *(Range a, float d)
    {
        return new Range(a.From * d, a.To * d);
    }

    public static Range operator *(float d, Range a)
    {
        return new Range(a.From * d, a.To * d);
    }

    public static Range operator /(Range a, float d)
    {
        return new Range(a.From / d, a.To / d);
    }

    public static bool operator ==(Range lhs, Range rhs)
    {
        return (lhs.From == rhs.From && lhs.To == rhs.To);
    }

    public static bool operator !=(Range lhs, Range rhs)
    {
        return (lhs.From != rhs.From || lhs.To != rhs.To);
    }

    public override bool Equals(object other)
    {
        if (other == null)
        {
            return false;
        }

        if (!(other is Range))
        {
            return false;
        }

        Range otherRange = (Range)other;
        return (From == otherRange.From && To == otherRange.To);
    }

    public override int GetHashCode()
    {
        return this.From.GetHashCode() ^ this.To.GetHashCode() << 2;
    }
}
