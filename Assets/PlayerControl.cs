using System.Reflection;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public MailboxID curMailID {get; private set;}
    public GameObject IntroArea;
    public text_script text_script;
    private void Update(){
        //  if(LetterUI.instance.active){
        //     GetComponent<FirstPersonController>().enabled = false;
        //  }else{
        //     GetComponent<FirstPersonController>().enabled = true;
        //  }
    }
    private void OnTriggerEnter(Collider other){
        if(other.tag=="Paper"){
            Debug.Log("Paper Tag");
            GameEvents.current.AddPaper();
        }
        if (other.tag == "Mailbox"){
            MailboxID id = other.gameObject.GetComponent<MailboxID>();
            curMailID = id;
            //Debug.Log("mailbox"+curMailID.getID());
        }
        
    }

    private void onTriggerExit(Collider other){
        if (other.tag == "Mailbox"){
            curMailID= null;
        }
        if(other.gameObject == IntroArea){
            text_script.SetLeftIntroArea();
        }
    }
}
