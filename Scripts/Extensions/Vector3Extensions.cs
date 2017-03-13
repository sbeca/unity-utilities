using UnityEngine;

public static class Vector3Extensions
{
    /// <summary>
    /// Returns a new Vector3 based on this Vector3 but with a new X value.
    /// </summary>
    public static Vector3 WithX(this Vector3 v, float x)
    {
        return new Vector3(x, v.y, v.z);
    }

    /// <summary>
    /// Returns a new Vector3 based on this Vector3 but with a new Y value.
    /// </summary>
    public static Vector3 WithY(this Vector3 v, float y)
    {
        return new Vector3(v.x, y, v.z);
    }

    /// <summary>
    /// Returns a new Vector3 based on this Vector3 but with a new Z value.
    /// </summary>
    public static Vector3 WithZ(this Vector3 v, float z)
    {
        return new Vector3(v.x, v.y, z);
    }

    /// <summary>
    /// Returns a new Vector3 based on this Vector3 but with the X value incremented by a value.
    /// </summary>
    public static Vector3 WithIncrementedX(this Vector3 v, float xIncrement)
    {
        return new Vector3(v.x + xIncrement, v.y, v.z);
    }

    /// <summary>
    /// Returns a new Vector3 based on this Vector3 but with the Y value incremented by a value.
    /// </summary>
    public static Vector3 WithIncrementedY(this Vector3 v, float yIncrement)
    {
        return new Vector3(v.x, v.y + yIncrement, v.z);
    }

    /// <summary>
    /// Returns a new Vector3 based on this Vector3 but with the Z value incremented by a value.
    /// </summary>
    public static Vector3 WithIncrementedZ(this Vector3 v, float zIncrement)
    {
        return new Vector3(v.x, v.y, v.z + zIncrement);
    }

    /// <summary>
    /// Returns a new Vector3 rotated around the X axis by angle.
    /// </summary>
    public static Vector3 RotateX(this Vector3 v, float angle)
    {
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        float ty = v.y;
        float tz = v.z;
        v.y = (cos * ty) - (sin * tz);
        v.z = (cos * tz) + (sin * ty);

        return v;
    }

    /// <summary>
    /// Returns a new Vector3 rotated around the Y axis by angle.
    /// </summary>
    public static Vector3 RotateY(this Vector3 v, float angle)
    {
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        float tx = v.x;
        float tz = v.z;
        v.x = (cos * tx) + (sin * tz);
        v.z = (cos * tz) - (sin * tx);

        return v;
    }

    /// <summary>
    /// Returns a new Vector3 rotated around the Z axis by angle.
    /// </summary>
    public static Vector3 RotateZ(this Vector3 v, float angle)
    {
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (cos * ty) + (sin * tx);

        return v;
    }

    /// <summary>
    /// Returns the pitch of this Vector3.
    /// </summary>
    public static float GetPitch(this Vector3 v)
    {
        float len = Mathf.Sqrt((v.x * v.x) + (v.z * v.z));    // Length on xz plane.
        return -Mathf.Atan2(v.y, len);
    }

    /// <summary>
    /// Returns the yaw of this Vector3.
    /// </summary>
    public static float GetYaw(this Vector3 v)
    {
        return Mathf.Atan2(v.x, v.z);
    }

    /// <summary>
    /// Returns true if 2 Vector3s are equal, taking into account float inaccuracy.
    /// </summary>
    public static bool Approximately(this Vector3 v1, Vector3 v2)
    {
        return (Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.y, v2.y) && Mathf.Approximately(v1.z, v2.z));
    }

    /// <summary>
    /// Returns a new Vector3 with containing the absolute values of x, y, and z.
    /// </summary>
    public static Vector3 Abs(this Vector3 v)
    {
        return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    }
}
