using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake(){
        current = this;
    }
    public event Action onPressEWatch;
    public void PressEWatch(){
        if(onPressEWatch!=null){
            onPressEWatch();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
