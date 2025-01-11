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

    

    private void Awake()
    {
        this.letter = LetterInventory.instance.getLetter();
        refresh();
        flipButton.onClickEvent += flip;
        openButton.onClickEvent += open;
        nextButton.onClickEvent += putOnBottom;

    }

    private void flip()
    {
        LetterDisplay.MODE mode;
        if (mainGraphic.state == LetterDisplay.MODE.BACK) mode = LetterDisplay.MODE.FRONT;
        else if (mainGraphic.state == LetterDisplay.MODE.FRONT) mode = LetterDisplay.MODE.BACK;
        else return;
        
        mainGraphic.initialize(letter, mode);
        
    }

    private void open()
    {
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
        int stackSize = LetterInventory.instance.getNumLetters();
        
        if (stackSize > 4) stackGraphic.sprite = stackGraphicMany;
        else if (stackSize > 1) stackGraphic.sprite = stackGraphicFew;
        else stackGraphic.sprite = stackGraphicNone;


        if (letter.isOpen()) mainGraphic.initialize(letter, LetterDisplay.MODE.OPEN);
        else mainGraphic.initialize(letter, LetterDisplay.MODE.FRONT);
    }
}
