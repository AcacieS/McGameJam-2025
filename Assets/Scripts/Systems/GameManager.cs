using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private GameObject winWindowPrefab;
    [SerializeField] private GameObject lostWindowPrefab;
    public static GameManager instance;
    public bool gameOver { get; private set; }

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
        Transform canvas = FindAnyObjectByType<Canvas>().transform;
        GameObject window = Instantiate(winWindowPrefab, canvas);
        window.GetComponent<Scoreboard>().initialize(MailboxManager.instance.getTotalScore());
        gameOver = true;
    }

    public void gameLost()
    {
        Transform canvas = FindAnyObjectByType<Canvas>().transform;
        Instantiate(lostWindowPrefab, canvas);
        gameOver = true;
    }

    public void reloadGame()
    {
        if (!gameOver) return;
        
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
}
