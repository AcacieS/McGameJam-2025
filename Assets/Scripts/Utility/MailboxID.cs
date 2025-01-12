using UnityEngine;

public class MailboxID:MonoBehaviour
{
    [SerializeField] private string id;
    
    public string getID() => id;
    
}
