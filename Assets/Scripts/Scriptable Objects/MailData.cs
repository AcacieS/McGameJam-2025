using UnityEngine;

[CreateAssetMenu(fileName = "new mail", menuName = "Scriptable Objects/MailData")]
public class MailData : ScriptableObject
{
    [SerializeField] private string content;
}
