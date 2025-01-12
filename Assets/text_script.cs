using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class text_script : MonoBehaviour
{
    //public GameObject mouseImg;
    public GameObject NPC_dialogue;
    public Text dialogueText;
    public string[] dialogue;

    private int index = -1;
    public float wordSpeed;
    private bool canContinue = false;
    private bool mouseActivate = true;
    public enum AdvanceType { Click, PressE, PressQ, AreaTrigger, Press1, ItemCount };
    private AdvanceType currentAdvanceType = AdvanceType.Click;
    private int currentLetterCount;
    private int requiredLetterCount;
    private bool leftIntroArea = false;
    void Start(){
        //mouseImg.SetActive(true);
        NextLine();
    }
    
    void Update()
    {
        if (canContinue && IsAdvanceConditionMet())
        {
            AdvanceDialogue();
        }
        SetAdvanceTypeAtIndexDefault(2, AdvanceType.PressE);
        SetAdvanceTypeAtIndex(4, AdvanceType.PressE);
        SetAdvanceTypeAtIndexDefault(5, AdvanceType.PressQ);
        SetAdvanceTypeAtIndex(7, AdvanceType.AreaTrigger);
    }
    private bool IsAdvanceConditionMet(){
        switch (currentAdvanceType){
            case AdvanceType.Click:
                return Input.GetMouseButtonDown(0);
            case AdvanceType.PressE:
                return Input.GetKeyDown(KeyCode.E);
            case AdvanceType.PressQ:
                return Input.GetKeyDown(KeyCode.Q);
            case AdvanceType.AreaTrigger:
                return leftIntroArea;
            case AdvanceType.Press1:
                return Input.GetKeyDown("1");
            case AdvanceType.ItemCount:
                return currentLetterCount==requiredLetterCount;
            default:
                return false;
        }
    }
    public void SetLeftIntroArea(){
        leftIntroArea = true;
    }
    public void SetAdvanceTypeAtIndexDefault(int switchIndex, AdvanceType advanceType, int letterCountRequirement = 0){
        SetAdvanceTypeAtIndex(switchIndex, advanceType, letterCountRequirement);
        SetAdvanceTypeAtIndex(switchIndex+1, AdvanceType.Click);
    }
    public void SetAdvanceTypeAtIndex(int switchIndex, AdvanceType advanceType, int letterCountRequirement = 0){
        if(index == switchIndex){
            SetAdvanceType(advanceType, letterCountRequirement);
            //SetAdvanceTypeAtIndex(3, AdvanceType.PressE);
        }
    }
    public void SetAdvanceType(AdvanceType advanceType, int letterCountRequirement = 0){
        currentAdvanceType = advanceType;
        requiredLetterCount = letterCountRequirement;
    }
    public void AdvanceDialogue(){
        if(dialogueText.text == dialogue[index]){
                    NextLine();
                    
                }else{
                    StopAllCoroutines();
                    StartCoroutine(Typing());
            }
    }
    public void StartDialogueAt(int startIndex){
        StopAllCoroutines();
        canContinue = false;
        dialogueText.text = "";
        index = startIndex;
        StartCoroutine(Typing());
    }
    public IEnumerator Typing(){
        foreach(char letter in dialogue[index].ToCharArray()){
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        if(dialogueText.text == dialogue[index]){
            canContinue = true;
            StartCoroutine(DisappearText(5));
        }
    }
     IEnumerator DisappearText(int seconds){
        yield return new WaitForSeconds(seconds);
        dialogueText.text = "";
        index++;
     }
    public void NextLine(){
        StopAllCoroutines();
        canContinue = false;
        if(index<dialogue.Length-1){
            index++;
            dialogueText.text="";
            StartCoroutine(Typing());
       }else{
            NPC_dialogue.SetActive(false);
            canContinue=false;
            ResetText();
        }
    }

    public void ResetText(){
        dialogueText.text = "";
        index=0;
        
    }
   
}


