using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LetterDisplay : MonoBehaviour
{
    [SerializeField] private Image letterImage;

    [SerializeField] private Sprite spriteFront;
    [SerializeField] private List<Sprite> spritesBack;
    [SerializeField] private Sprite spriteOpen;
    [SerializeField] private List<TMP_FontAsset> fontAssets;
    
    [SerializeField] private TextMeshProUGUI senderText;
    [SerializeField] private TextMeshProUGUI receiverText;
    [SerializeField] private TextMeshProUGUI openedText;

    public MODE state { get; private set; }
    
    public enum MODE
    {
        FRONT, BACK, OPEN
    }

    private void Awake()
    {
        senderText.text = "";
        receiverText.text = "";
        openedText.text = "";
    }


    public void initialize(Letter letter, MODE mode)
    {
        senderText.text = "";
        receiverText.text = "";
        openedText.text = "";
        state = mode;
        
        int index2 = letter.seed % fontAssets.Count;
        senderText.font = fontAssets[index2];
        receiverText.font = fontAssets[index2];
        openedText.font = fontAssets[index2];
        
        
        switch (mode)
        {
            case MODE.FRONT:
                letterImage.sprite = spriteFront;
                break;
            case MODE.BACK:
                int index = letter.seed % spritesBack.Count;
                Debug.Log(index);
                letterImage.sprite = spritesBack[index];
                
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
