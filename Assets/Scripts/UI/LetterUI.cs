using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterUI : MonoBehaviour
{
    private Letter letter;
    [SerializeField] private CustomButton flipButton;
    [SerializeField] private CustomButton openButton;
    [SerializeField] private CustomButton nextButton;
    [SerializeField] private CustomButton deliverButton;
    [SerializeField] private Image deliverImage;

    [SerializeField] private AudioClip windSound;
    [SerializeField] private AudioClip paperSound;

    [SerializeField] private TextMeshProUGUI remainingText;
    
    [SerializeField] private LetterDisplay mainGraphic;



    public static LetterUI instance { get; private set; }
    public bool active { get; private set; } = true;

    private void Awake()
    {
        if (instance != null) throw new Exception("instance of singleton LetterUI already exists");
        instance = this;
        
        flipButton.onClickEvent += flip;
        openButton.onClickEvent += open;
        nextButton.onClickEvent += putOnBottom;
        deliverButton.onClickEvent += deliverLetter;
        toggleActive();
    }

    

    public void toggleActive()
    {
        active = !active;
        
        CanvasGroup group = GetComponent<CanvasGroup>();
        if (!active)
        {
            group.alpha = 0;
            group.blocksRaycasts = false;
            letter = null;
        }
        else
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player)
            {
                var controller = player.GetComponent<PlayerControl>();
                if (controller.curMailID != null)
                {
                    deliverButton.setActive(true);
                    deliverImage.color = new Color32(255, 255, 255, 255);
                }
                else
                {
                    deliverButton.setActive(false);
                    deliverImage.color = new Color32(255, 255, 255, 80);
                }
            }

            group.alpha = 1;
            group.blocksRaycasts = true;
            refresh();
        }
        
    }
    
    

    private void flip()
    {
        LetterDisplay.MODE mode;
        if (mainGraphic.state == LetterDisplay.MODE.BACK)
        {
            mode = LetterDisplay.MODE.FRONT;
            openButton.setActive(true);
        }
        else if (mainGraphic.state == LetterDisplay.MODE.FRONT)
        {
            mode = LetterDisplay.MODE.BACK;
            openButton.setActive(false);
        }
        else return;
        
        mainGraphic.initialize(letter, mode);
        
        SoundManagerScript.instance.PlaySound(paperSound);
    }

    private void open()
    {
        Debug.Log("is openable? " + letter is OpenableLetter);
        if (!(letter is OpenableLetter)) return;
        ((OpenableLetter)letter).openLetter();
        refresh();
        
        SoundManagerScript.instance.PlaySound(paperSound);
    }

    private void putOnBottom()
    {
        letter = LetterInventory.instance.nextLetter();
        refresh();
        
        SoundManagerScript.instance.PlaySound(paperSound);
    }

    private void deliverLetter()
    {
        GameObject player = GameObject.FindWithTag("Player");
        var controller = player.GetComponent<PlayerControl>();
        if (controller.curMailID == null) return;
        MailboxManager.instance.addLetter(controller.curMailID, letter);
    }
    
    

    private void refresh()
    {
        letter = LetterInventory.instance.getLetter();
        Debug.Log(letter.GetHashCode());
        int stackSize = LetterInventory.instance.getNumLetters();

        remainingText.text = "Remaining letters : " + stackSize;

        
        if (letter == null)
        {
            mainGraphic.gameObject.SetActive(false);   
        }

        if (letter.isOpen()) mainGraphic.initialize(letter, LetterDisplay.MODE.OPEN);
        else mainGraphic.initialize(letter, LetterDisplay.MODE.BACK);
    }
}
