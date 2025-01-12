using UnityEngine;

public class MailboxID:MonoBehaviour
{
    [SerializeField] private string id;
    private Animator currentAnim;
    public string getID() => id;
    private void Start(){
        currentAnim = GetComponent<Animator>();
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
