using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class MailboxManager : MonoBehaviour
{
    public static MailboxManager instance { get; private set; }
    private Dictionary<string, Mailbox> mailboxes;

    [SerializeField] private int wronglyDeliveredPenalty; //same for not delivered
    

    private void Awake()
    {
        if (instance != null) throw new Exception("multiple instances of singleton MailboxManager exist");
        instance = this;
        
        initializeMailboxes();
    }

    private void initializeMailboxes()
    {
        mailboxes = new Dictionary<string, Mailbox>();

        var allMailboxes = GameObject.FindGameObjectsWithTag("Mailbox");
        foreach (var mailboxObject in allMailboxes)
        {
            string id = mailboxObject.GetComponent<MailboxID>().getID();
            Debug.Log(id);
            mailboxes.Add(id, new Mailbox(id));
        }
    }

    public void addLetter(MailboxID mailboxID, Letter letter)
    {
        if (!mailboxes.ContainsKey(mailboxID.getID()))
            throw new Exception("trying to add to illegal mailbox: check ID is correct");
        mailboxes[mailboxID.getID()].addLetter(letter);
    }

    public ScoreData getTotalScore()
    {
        int numDelivered = 0;
        int numWronglyDelivered = 0;
        int numRemaining = LetterInventory.instance.getNumLetters();
        int score = 0;
        
        foreach (var mailbox in mailboxes.Values)
        {
            List<Letter> delivered = mailbox.getDelivered();
            foreach (var letter in delivered)
            {
                score += letter.value;
                numDelivered++;
            }

            numWronglyDelivered += mailbox.getNumWronglyDelivered();
            score -= wronglyDeliveredPenalty * numWronglyDelivered;
        }

        return new ScoreData(score, numDelivered, numWronglyDelivered, numRemaining);
    }
}
