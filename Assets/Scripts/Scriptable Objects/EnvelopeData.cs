using UnityEngine;

[CreateAssetMenu(fileName = "new envelope", menuName = "Scriptable Objects/Envelope")]
public class EnvelopeData : ScriptableObject
{
    public string senderAddress;
    public string receiverAddress;
    public Sprite stamp;
    
    public string mailBoxID;
}
