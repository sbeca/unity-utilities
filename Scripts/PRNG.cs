using UnityEngine;
using System;
using System.IO;

public class PRNG
{
    StreamWriter sw = null;

    private ulong[] s = new ulong[16];
    int p;

    // for compatibility with Random.
    public ulong seed
    {
        set { InitWithSeed(value); }
    }

    // for compatibility with Random.
    public float value
    {
        get { return Float(1.0f); }
    }

    public PRNG(bool log)
    {
        InitLog(log);
    }

    ~PRNG()
    {
        if (sw != null)
        {
            sw.Close();
        }
    }

    public PRNG(bool log, ulong seed)
    {
        InitLog(log);
        InitWithSeed(seed);
    }

    public PRNG(bool log, string seed)
    {
        InitLog(log);
        // Utility function to let you initialise using a string seed.
        InitWithSeed((ulong)seed.GetHashCode());
    }

    private void InitLog(bool log)
    {
        if (log)
        {
#if UNITY_EDITOR
            sw = new StreamWriter("seedlog.txt");
#else
            sw = new StreamWriter(Application.persistentDataPath + "/seedlog.txt");
#endif
            sw.Write("PRNG seeds for ");
            sw.WriteLine(DateTime.Now);
            sw.WriteLine("-------------------");
        }
    }

    private void InitWithSeed(ulong seed)
    {
        if (sw != null)
        {
            sw.WriteLine(seed);
            sw.Flush();
        }
        // We need a non-zero seed in order to produce useful output, so if
        // we've been given a zero seed, let's set it to some oether arbitrary
        // (but repeatable) value.
        if (seed == 0)
        {
            seed = 4; // chosen by fair dice roll.
                        // guaranteed to be random.
        }

        // We actually need a lot more state data than just this 64 bits -- we're
        // going to be using Xorshift1024*, which requires 1024 bits of state.  So
        // we're going to use our 64 bits of seed data as the state of an Xorshift64*
        // PRNG, and use that smaller PRNG to generate the initial state for our real,
        // full PRNG.
        //
        // This Xorshift64* implementation is adapted from here:
        //
        // http://xorshift.di.unimi.it/xorshift64star.c

        for (int i = 0; i < 16; i++)
        {
            seed ^= seed >> 12; // a
            seed ^= seed << 25; // b
            seed ^= seed >> 27; // c
            s[i] = seed * 2685821657736338717;
        }
        p = 0;
    }

    private ulong Next()
    {
        // This is the core of our pseudo-random number generation.  This function
        // generates 64 bits of pseudo-random data, which can then be consumed by
        // the other (externally visible) functions on this class.
        //
        // What follows is an implementation of Xorshift1024*;  an xorshift-based
        // PRNG which uses 1024 bits of state, adapted from the implementation here:
        //
        // http://xorshift.di.unimi.it/xorshift1024star.c

        ulong s0 = s[p];
        ulong s1 = s[p = (p + 1) & 15];
        s1 ^= s1 << 31; // a
        s1 ^= s1 >> 11; // b
        s0 ^= s0 >> 30; // c
        return (s[p] = s0 ^ s1) * 1181783497276652981;
    }

    public float Float(float maxValue)
    {
        float result = Next() / (float)ulong.MaxValue;
        result *= maxValue;
        return result;
    }

    public float Range(float minValue, float maxValue)
    {
        float delta = maxValue - minValue;
        return Float(delta) + minValue;
    }

    public int Range(int minValue, int maxValue)
    {
        float delta = maxValue - minValue;
        return (int)Float(delta) + minValue;
    }
}
