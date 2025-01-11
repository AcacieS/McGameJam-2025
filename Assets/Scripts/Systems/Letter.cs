using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Letter
{
    public EnvelopeData envelope { get; protected set; }
    public readonly int value;
    public readonly int seed;

    public Letter(EnvelopeData envelope, int value)
    {
        this.envelope = envelope;
        this.value = value;
        seed = Random.Range(0, Int32.MaxValue);
    }
    

    public virtual bool isOpen()
    {
        return false;
    }
    
}
