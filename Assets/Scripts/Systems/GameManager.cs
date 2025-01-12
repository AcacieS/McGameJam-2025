using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject winWindow;
    [SerializeField] private GameObject lostWindow;
    [SerializeField] private GameObject creditsWindow;
    public static GameManager instance;
    public bool gameOver { get; private set; }
    private bool shownCredits = false;

    private void Awake()
    {
        if (instance != null) throw new Exception("multiple instances of singleton GameManager exist");
        instance = this;
        gameOver = false;
        

    }
    

    private void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space)) reloadGame();
        }
    }

    public void gameWon()
    {
        Debug.Log("GAME WON!");
        Transform canvas = FindAnyObjectByType<Canvas>().transform;
        GameObject window = Instantiate(winWindow, canvas);
        window.GetComponent<Scoreboard>().initialize(MailboxManager.instance.getTotalScore());
        gameOver = true;
    }

    public void gameLost()
    {
        Debug.Log("GAME LOST!");
        Transform canvas = FindAnyObjectByType<Canvas>().transform;
        Instantiate(lostWindow, canvas);
        gameOver = true;
    }

    public void reloadGame()
    {
        
        if (!gameOver) return;
        
        if (!shownCredits)
        {
            Debug.Log("Showing credits");
            Transform canvas = FindAnyObjectByType<Canvas>().transform;
            Instantiate(creditsWindow, canvas);
            shownCredits = true;
            return;
        }
        
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
}
