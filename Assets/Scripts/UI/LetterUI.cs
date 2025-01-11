using System;
using UnityEngine;
using UnityEngine.UI;

public class LetterUI : MonoBehaviour
{
    private Letter letter;
    [SerializeField] private CustomButton flipButton;
    [SerializeField] private CustomButton openButton;
    [SerializeField] private CustomButton nextButton;
    
    
    [SerializeField] private Image stackGraphic;
    [SerializeField] private LetterDisplay mainGraphic;

    [SerializeField] private Sprite stackGraphicNone;
    [SerializeField] private Sprite stackGraphicFew;
    [SerializeField] private Sprite stackGraphicMany;

    public static LetterUI instance { get; private set; }
    public bool active { get; private set; } = true;

    private void Awake()
    {
        if (instance != null) throw new Exception("instance of singleton LetterUI already exists");
        instance = this;
        
        flipButton.onClickEvent += flip;
        openButton.onClickEvent += open;
        nextButton.onClickEvent += putOnBottom;
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
        
    }

    private void open()
    {
        Debug.Log(letter.GetType());
        if (!(letter is OpenableLetter)) return;
        ((OpenableLetter)letter).openLetter();
        refresh();
    }

    private void putOnBottom()
    {
        letter = LetterInventory.instance.nextLetter();
        refresh();
    }

    private void refresh()
    {
        letter = LetterInventory.instance.getLetter();
        Debug.Log(letter.GetHashCode());
        int stackSize = LetterInventory.instance.getNumLetters();
        
        if (stackSize > 4) stackGraphic.sprite = stackGraphicMany;
        else if (stackSize > 1) stackGraphic.sprite = stackGraphicFew;
        else stackGraphic.sprite = stackGraphicNone;

        
        if (letter == null)
        {
            mainGraphic.gameObject.SetActive(false);   
        }

        if (letter.isOpen()) mainGraphic.initialize(letter, LetterDisplay.MODE.OPEN);
        else mainGraphic.initialize(letter, LetterDisplay.MODE.BACK);
    }
}
