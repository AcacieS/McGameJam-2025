using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public MailboxID curMailID {get; private set;}

   
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
    }
}
