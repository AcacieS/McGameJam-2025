using TMPro;
using UnityEngine;

public class MailboxID:MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private TextMeshPro mailboxNumber;
    [SerializeField] private AudioClip mailboxSound;
    private Animator currentAnim;
    public string getID() => id;
    private void Start(){
        currentAnim = GetComponent<Animator>();
        var words = id.Split(' ');
        mailboxNumber.text = words[0];
        Debug.Log("id : " + id + "setting mailbox num: " + words[0]);
    }

    private void Update(){
      var stateInfo = currentAnim.GetCurrentAnimatorStateInfo(0);
      if(stateInfo.normalizedTime >= 1.0f){
         currentAnim.Play(stateInfo.fullPathHash, -1, 0.99f);
      }
      if (stateInfo.normalizedTime <= 0.0f) 
      {
         currentAnim.Play(stateInfo.fullPathHash, -1, 0.01f); 
      }
   }
    
}
