using UnityEngine;

public class TriggerHand : MonoBehaviour
{
    bool watchIsOn = false;
    public GameObject[] activateObjs;

    void Update()
    {
        if(Input.GetKeyUp("q")){
            if(watchIsOn){
                
                GameEvents.current.PressEWatchClose();
                watchIsOn = false;
            }else{
                foreach(GameObject activateObj in activateObjs){
                    activateObj.SetActive(true);
                }
                GameEvents.current.PressEWatchOpen();
                watchIsOn = true;
            }
        }
    }
}
