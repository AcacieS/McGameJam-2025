using System;
using UnityEngine;

public class Letter
{
    public EnvelopeData envelope { get; protected set; }
    public readonly int value;

    public Letter(EnvelopeData envelope, int value)
    {
        this.envelope = envelope;
        this.value = value;
    }
    

    public virtual bool isOpen()
    {
        return false;
    }
    
}
