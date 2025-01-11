using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LetterDisplay : MonoBehaviour
{
    [SerializeField] private Image letterImage;

    [SerializeField] private Sprite spriteFront;
    [SerializeField] private Sprite spriteBack;
    [SerializeField] private Sprite spriteOpen;

    [SerializeField] private GameObject stamp;
    
    [SerializeField] private TextMeshProUGUI senderText;
    [SerializeField] private TextMeshProUGUI receiverText;
    [SerializeField] private TextMeshProUGUI openedText;

    public MODE state { get; private set; }
    
    public enum MODE
    {
        FRONT, BACK, OPEN
    }
    
    public void initialize(Letter letter, MODE mode)
    {
        senderText.text = "";
        receiverText.text = "";
        openedText.text = "";
        stamp.SetActive(false);
        state = mode;
        
        switch (mode)
        {
            case MODE.FRONT:
                letterImage.sprite = spriteFront;
                stamp.SetActive(true);
                break;
            case MODE.BACK:
                letterImage.sprite = spriteBack;
                senderText.text = letter.envelope.senderAddress;
                receiverText.text = letter.envelope.receiverAddress;
                break;
            
            case MODE.OPEN:
                if (!(letter is OpenableLetter)) throw new Exception("must be of type OpenableLetter");
                OpenableLetter openableLetter = (OpenableLetter)letter;
                letterImage.sprite = spriteOpen;
                openedText.text = openableLetter.mail.content;
                break;
            
        }
        
    }
    
}
