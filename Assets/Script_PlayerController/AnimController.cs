using UnityEngine;

public class AnimController : MonoBehaviour
{
   private Animator currentAnim;

   private void Start(){
      GameEvents.current.onPressEWatchOpen += OnWatchOpen;
      GameEvents.current.onPressEWatchClose += OnWatchClose;
      currentAnim = GetComponent<Animator>();
   }
   void Update(){
      var stateInfo = currentAnim.GetCurrentAnimatorStateInfo(0);
      if(stateInfo.normalizedTime >= 1.0f){
         currentAnim.Play(stateInfo.fullPathHash, -1, 0.99f);
      }
      if (stateInfo.normalizedTime <= 0.0f) // Check if the animation is at the beginning
      {
         currentAnim.Play(stateInfo.fullPathHash, -1, 0.01f); // Restart from near the start
      }
   }
   private void OnWatchOpen(){
      //Debug.Log("open");
      currentAnim.SetFloat("Speed",1);
   }
   private void OnWatchClose(){
      currentAnim.SetFloat("Speed",-1.5f);
   }
   public void disactivateObj(){
      if(currentAnim.GetFloat("Speed")==-1.5f){
         gameObject.SetActive(false);
      }
      
   }
   
}
