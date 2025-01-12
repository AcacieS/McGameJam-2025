using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if(other.tag=="Paper"){
            Debug.Log("Paper Tag");
            GameEvents.current.AddPaper();
        }
    }
}
