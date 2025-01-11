using UnityEngine;

public class WatchController : MonoBehaviour
{
   private void Start(){
    GameEvents.current.onPressEWatch += OnWatchShow;
   }
   private void OnWatchShow(){
        Debug.Log("display");
   }
}
