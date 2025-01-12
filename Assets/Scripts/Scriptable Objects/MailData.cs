using UnityEngine;

[CreateAssetMenu(fileName = "new mail", menuName = "Scriptable Objects/MailData")]
public class MailData : ScriptableObject
{
    [TextArea]
    public string content;
}
