using UnityEngine;

public class AnimController : MonoBehaviour
{
   private Animator currentAnim;

   private void Start(){
      GameEvents.current.onPressEWatchOpen += OnWatchOpen;
      GameEvents.current.onPressEWatchClose += OnWatchClose;
      currentAnim = GetComponent<Animator>();
   }
   private void OnWatchOpen(){
      Debug.Log("open");
      
      var stateInfo = currentAnim.GetCurrentAnimatorStateInfo(0);
      if (stateInfo.normalizedTime >= 1.0f) // Check if the animation is at the end
      {
         currentAnim.Play(stateInfo.fullPathHash, -1, 0.99f); 
      }
      currentAnim.SetFloat("Speed",1);
   }
   private void OnWatchClose(){
      var stateInfo = currentAnim.GetCurrentAnimatorStateInfo(0);
      if (stateInfo.normalizedTime <= 0.0f) // Check if the animation is at the beginning
      {
         currentAnim.Play(stateInfo.fullPathHash, -1, 0.01f); // Restart from near the start
      }
        
      currentAnim.SetFloat("Speed",-1);
   }
   public void disactivateObj(){
      if(currentAnim.GetFloat("Speed")==-1){
         gameObject.SetActive(false);
      }
      
   }
   
}
