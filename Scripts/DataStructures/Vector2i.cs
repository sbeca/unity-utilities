using UnityEngine;

[System.Serializable]
public struct Vector2i
{
    public int x;
    public int y;

    public Vector2i(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void Set(int newX, int newY)
    {
        this.x = newX;
        this.y = newY;
    }

    public static Vector2i FromVector2Round(Vector2 from)
    {
        return new Vector2i(Mathf.RoundToInt(from.x), Mathf.RoundToInt(from.y));
    }

    public static Vector2i FromVector2Floor(Vector2 from)
    {
        return new Vector2i(Mathf.FloorToInt(from.x), Mathf.FloorToInt(from.y));
    }

    public static Vector2i FromVector2Ceil(Vector2 from)
    {
        return new Vector2i(Mathf.CeilToInt(from.x), Mathf.CeilToInt(from.y));
    }

    public static Vector2i left { get { return new Vector2i(-1, 0); } }
    public static Vector2i right { get { return new Vector2i(1, 0); } }
    public static Vector2i up { get { return new Vector2i(0, 1); } }
    public static Vector2i down { get { return new Vector2i(0, -1); } }
    public static Vector2i zero { get { return new Vector2i(0, 0); } }
    public static Vector2i one { get { return new Vector2i(1, 1); } }

    public static float Distance(Vector2i a, Vector2i b)
    {
        return Mathf.Sqrt((b.x - a.x) * (b.x - a.x) + (b.y - a.y) * (b.y - a.y));
    }

    public static Vector2i Min(Vector2i lhs, Vector2i rhs)
    {
        return new Vector2i(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
    }

    public static Vector2i Max(Vector2i lhs, Vector2i rhs)
    {
        return new Vector2i(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
    }

    public static Vector2i operator +(Vector2i a, Vector2i b)
    {
        return new Vector2i(a.x + b.x, a.y + b.y);
    }

    public static Vector2i operator -(Vector2i a)
    {
        return new Vector2i(-a.x, -a.y);
    }

    public static Vector2i operator -(Vector2i a, Vector2i b)
    {
        return new Vector2i(a.x - b.x, a.y - b.y);
    }

    public static Vector2i operator *(Vector2i a, int d)
    {
        return new Vector2i(a.x * d, a.y * d);
    }

    public static Vector2i operator *(Vector2i a, float d)
    {
        return new Vector2i(Mathf.RoundToInt((float)a.x * d), Mathf.RoundToInt((float)a.y * d));
    }

    public static Vector2i operator *(int d, Vector2i a)
    {
        return new Vector2i(a.x * d, a.y * d);
    }

    public static Vector2i operator *(float d, Vector2i a)
    {
        return new Vector2i(Mathf.RoundToInt((float)a.x * d), Mathf.RoundToInt((float)a.y * d));
    }

    public static Vector2i operator /(Vector2i a, int d)
    {
        return new Vector2i(a.x / d, a.y / d);
    }

    public static Vector2i operator /(Vector2i a, float d)
    {
        return new Vector2i(Mathf.RoundToInt((float)a.x / d), Mathf.RoundToInt((float)a.y / d));
    }

    public static bool operator ==(Vector2i lhs, Vector2i rhs)
    {
        return (lhs.x == rhs.x && lhs.y == rhs.y);
    }

    public static bool operator !=(Vector2i lhs, Vector2i rhs)
    {
        return (lhs.x != rhs.x || lhs.y != rhs.y);
    }

    public override bool Equals(object other)
    {
        bool result = false;
        if (!(other is Vector2i))
        {
            result = false;
        }
        else
        {
            Vector2i vector = (Vector2i)other;
            result = (this.x.Equals(vector.x) && this.y.Equals(vector.y));
        }
        return result;
    }

    public override string ToString()
    {
        return string.Format("({0}, {1})", this.x, this.y);
    }

    public override int GetHashCode()
    {
        return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
    }
}
