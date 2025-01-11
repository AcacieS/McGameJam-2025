using UnityEngine;

[CreateAssetMenu(fileName = "new envelope", menuName = "Scriptable Objects/Envelope")]
public class EnvelopeData : ScriptableObject
{
    [SerializeField] private string senderAddress;
    [SerializeField] private string receiverAddress;
    [SerializeField] private Sprite stamp;
    
    [SerializeField] private int mailBoxID;
}
