using UnityEngine;

public class paper_script : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameEvents.current.onTriggerAddPaper+=OnAddPaper;
        
    }

    private void OnAddPaper(){
        Debug.Log("I add letter");
        Destroy(gameObject);
    }
}
