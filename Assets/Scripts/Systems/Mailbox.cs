using System.Collections.Generic;
using NUnit.Framework;

public class Mailbox
{
    public readonly string id;
    private List<Letter> contents;

    public Mailbox(string id)
    {
        this.id = id;
        contents = new List<Letter>();
    }

    public List<Letter> getDelivered()
    {
        List<Letter> delivered = new List<Letter>();
        foreach (var letter in contents)
        {
            if (letter.envelope.mailBoxID == id) delivered.Add(letter);
        }

        return delivered;
    }

    public int getNumWronglyDelivered()
    {
        int tally = 0;
        foreach (var letter in contents)
        {
            if (letter.envelope.mailBoxID != id) tally++;
        }

        return tally;
    }
}
