using UnityEngine;

public class TriggerHand : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("e")){
            GameEvents.current.PressEWatch();
        }
    }
}
