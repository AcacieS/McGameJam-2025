using UnityEditor.Build.Content;
using UnityEngine;

public class clock_script : MonoBehaviour
{
    public GameObject secHand;
    public GameObject minHand;
    public GameObject hourHand;

    private float secRotation;
    private float minRotation;
    private float hourRotation;
    private float goalTime = 15f;
    private float goalTimeReal;
    private float currentTime = 0f;


    private float timeScale = 60f;//60f;
    void Start()
    {
        goalTimeReal = goalTime*60f;
        secRotation = 62f;
        minRotation = -90f;
        hourRotation = 90f;
        //rotateSpeed = Time.deltaTime/60;

        secHand.transform.localRotation = Quaternion.Euler(0, secRotation, 0);
        minHand.transform.localRotation = Quaternion.Euler(0, minRotation, 0);
        hourHand.transform.localRotation = Quaternion.Euler(0, hourRotation, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;
        currentTime+=deltaTime;
        float tolerance = 0.01f; // Adjust tolerance as needed
        if (Mathf.Abs(currentTime - goalTimeReal) <= tolerance)
        {
            //GameManager.instance.gameOver;
        }

        secRotation += (360f/60f) * timeScale * deltaTime;
        minRotation += (360f/3600f) * timeScale * deltaTime;
        hourRotation += (360f/43200f) * timeScale * deltaTime;
    }
    public float getTime(){
        return currentTime;
    }
    public void doSec(){
        secHand.transform.localRotation = Quaternion.Euler(0, secRotation, 0);
    }
    public void doMin(){
        minHand.transform.localRotation = Quaternion.Euler(0, minRotation, 0);
    }
    public void doHour(){
        hourHand.transform.localRotation = Quaternion.Euler(0, hourRotation, 0);
    }
}
