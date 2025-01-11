using System;
using UnityEngine;

public class Letter
{
    public EnvelopeData envelope { get; protected set; }

    public Letter(EnvelopeData envelope)
    {
        this.envelope = envelope;
    }
    
    protected Letter(){}

    public virtual bool isOpen()
    {
        return false;
    }
    
}
