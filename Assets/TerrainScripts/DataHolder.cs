using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHolder
{
    public static int Oktawy = 5;
    public static float Persistance = 0.5f;
    public static float Amplitude = 1f;

    public static int SetOctawy
    {
        get
        {
            return Oktawy;
        }
        set
        {
            Oktawy = value;
        }
    }
    public static float SetPersistance
    {
        get
        {
            return Persistance;
        }
        set
        {
            Persistance = value;
        }
    }
    public static float SetAmplitude
    {
        get
        {
            return Amplitude;
        }
        set
        {
            Amplitude = value;
        }
    }
}
