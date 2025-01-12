using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random;

public class LetterInventory : MonoBehaviour
{
    public static LetterInventory instance { get; private set; }
    private Queue<Letter> letters;

    [SerializeField] private List<EnvelopeData> letterType1; //Full info letters
    [SerializeField] private List<EnvelopeData> letterType2; //Missing info letters
    [SerializeField] private List<EnvelopeMailPair> letterType3; //Letters that can be opened
    [SerializeField] private int score1;
    [SerializeField] private int score2;
    [SerializeField] private int score3;
    

    private void Awake()
    {
        if (instance != null) throw new Exception("multiple instances of singleton LetterInventory exist");
        instance = this;

        letters = new Queue<Letter>();
        
        for (int i = 0; i < 20; i++) addLetter();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.instance.gameOver) return;
            LetterUI.instance.toggleActive();
        }
    }
    public void addLetter()
    {
        float roll = Random.Range(0f, 1f);
        Letter newLetter;

        if (roll < 0.3)
        {
            int randomIndex = Random.Range(0, letterType3.Count);
            newLetter = new OpenableLetter(letterType3[randomIndex], score3);
        }
        else if (roll < 0.6)
        {
            int randomIndex = Random.Range(0, letterType2.Count);
            newLetter = new Letter(letterType2[randomIndex], score2);
        }
        else
        {
            int randomIndex = Random.Range(0, letterType1.Count);
            newLetter = new Letter(letterType1[randomIndex], score1);
        }
        
        letters.Enqueue(newLetter);
    }

    public Letter nextLetter()
    {
        if (letters.Count == 0)
        {
            GameManager.instance.gameWon();
            return null;
        }
        Letter top = letters.Dequeue();
        letters.Enqueue(top);
        return getLetter();
    }

    public void removeLetter()
    {
        if (letters.Count == 0)
        {
            GameManager.instance.gameWon();
            return;
        }

        letters.Dequeue();
        Debug.Log("Dequeuing; new length : " + letters.Count);
        if (letters.Count == 0) GameManager.instance.gameWon();
    }
    

    public Letter getLetter()
    {
        if (letters.Count == 0) return null;
        return letters.Peek();
    }

    public int getNumLetters() => letters.Count;


}
