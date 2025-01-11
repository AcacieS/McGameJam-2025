using Unity.VisualScripting;

public class OpenableLetter : Letter
{
    public MailData mail { get; private set; }
    private bool open;
    
    public OpenableLetter(EnvelopeMailPair dataPair)
    {
        this.mail = dataPair.mailData;
        this.envelope = dataPair.envelopeData;
    }
    
    public OpenableLetter(EnvelopeData envelope, MailData mail)
    {
        this.mail = mail;
        this.envelope = envelope;
    }

    public override bool isOpen()
    {
        return open;
    }
}
