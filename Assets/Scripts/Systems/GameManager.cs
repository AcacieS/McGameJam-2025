using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject winWindowPrefab;
    [SerializeField] private GameObject lostWindowPrefab;
    public static GameManager instance;
    public bool gameOver { get; private set; }

    private void Awake()
    {
        gameOver = false;
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
}
