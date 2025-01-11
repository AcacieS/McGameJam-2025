using Unity.VisualScripting;

public class OpenableLetter : Letter
{
    public MailData mail { get; private set; }
    private bool open;

    public OpenableLetter(EnvelopeMailPair dataPair, int value)
        : base(dataPair.envelopeData, value)
    {
        this.mail = dataPair.mailData;
    }
    

    public override bool isOpen()
    {
        return open;
    }
}
