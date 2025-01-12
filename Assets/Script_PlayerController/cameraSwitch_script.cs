using UnityEngine;

public class cameraSwitch_script : MonoBehaviour
{
    public GameObject topDownCamera; 
    public GameObject firstPersonCamera; 
    public GameObject CourierPawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         SetCameraMode(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("1")){
            if(topDownCamera.activeSelf){
                SetCameraMode(true);
            }else{
                SetCameraMode(false);
            }
        }
        
    }
    private void SetCameraMode(bool isFirstPerson){
        CourierPawn.SetActive(!isFirstPerson);
        firstPersonCamera.SetActive(isFirstPerson);

        Debug.Log("topDownCamera"+!isFirstPerson);
        topDownCamera.SetActive(!isFirstPerson);//enabled = !isFirstPerson;
    }
}
