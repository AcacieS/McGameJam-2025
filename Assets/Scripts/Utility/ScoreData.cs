public class ScoreData
{
    public readonly int score;
    public readonly int numDelivered;
    public readonly int numWronglyDelivered;
    public readonly int numRemaining;

    public ScoreData(int score, int numDelivered, int numWronglyDelivered, int numRemaining)
    {
        this.score = score;
        if (this.score < 0) this.score = 0;
        this.numDelivered = numDelivered;
        this.numWronglyDelivered = numWronglyDelivered;
        this.numRemaining = numRemaining;
    }
}
