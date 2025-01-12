using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private CustomButton playAgainButton;

    public void initialize(ScoreData score)
    {
        mainText.text = string.Format("Letters delivered : {0} \nLetters not delivered : {1}\nScore : {2}",
            score.numDelivered, score.numWronglyDelivered, score.score);

        playAgainButton.onClickEvent += playAgain;
    }

    private void playAgain()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

}
