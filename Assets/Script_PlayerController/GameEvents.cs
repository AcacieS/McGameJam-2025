using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake(){
        current = this;
    }
    public event Action onPressEWatchOpen;
    public void PressEWatchOpen(){
        Debug.Log("open event");
        if(onPressEWatchOpen!=null){
            onPressEWatchOpen();
        }
    }
    public event Action onPressEWatchClose;
    public void PressEWatchClose(){
        Debug.Log("close event");
        if(onPressEWatchClose!=null){
            onPressEWatchClose();
        }
    }
    public event Action onTriggerAddPaper;
    public void AddPaper(){
        Debug.Log("add paper event");
        if(onTriggerAddPaper!=null){
            onTriggerAddPaper();
        }
    }


}
