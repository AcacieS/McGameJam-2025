using System.Reflection;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public MailboxID curMailID {get; private set;}
    public GameObject IntroArea;
    public text_script text_script;
    [SerializeField] private AudioClip mailboxSound;
    [SerializeField] private AudioClip mailboxCloseSound;
    
    private void Update(){
         if(LetterUI.instance.active){
            GetComponent<FirstPersonController>().enabled = false;
         }else{
            GetComponent<FirstPersonController>().enabled = true;
         }
    }
    private void OnTriggerEnter(Collider other){
        if(other.tag=="Paper"){
            Debug.Log("Paper Tag");
            GameEvents.current.AddPaper();
        }
        if (other.tag == "Mailbox"){
            Debug.Log("PLAYER intersecting mailbox Trigger Collider");
            Animator mailbox_anim = other.gameObject.GetComponent<Animator>();
            mailbox_anim.SetFloat("Speed",1);
            MailboxID id = other.gameObject.GetComponent<MailboxID>();
            curMailID = id;
            Debug.Log("mailbox"+curMailID.getID());
            SoundManagerScript.instance.PlaySound(mailboxSound);
        }
       
        
    }

    private void OnTriggerExit(Collider other){
        if (other.tag == "Mailbox"){
            Debug.Log("the same mailbox");
            Animator mailbox_anim = other.gameObject.GetComponent<Animator>();
            mailbox_anim.SetFloat("Speed",-1.5f);
            curMailID= null;
            SoundManagerScript.instance.PlaySound(mailboxCloseSound);
        }
        if(other.gameObject == IntroArea){
            text_script.SetLeftIntroArea();
        }
    }
}
