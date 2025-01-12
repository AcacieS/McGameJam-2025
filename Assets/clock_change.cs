using UnityEngine;

public class clock_change : MonoBehaviour
{
    public clock_script Time_script;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time_script.doHour();
        Time_script.doMin();
        Time_script.doSec();
        
    }
}
