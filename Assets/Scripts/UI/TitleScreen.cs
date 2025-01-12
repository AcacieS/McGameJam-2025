using System;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private Image tutorialGraphic;

    private int stage;

    private void Awake()
    {
        stage = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (stage == 0) tutorialGraphic.gameObject.SetActive(true);
            else if (stage >= 1) Destroy(gameObject);

            stage++;
        }
    }
}
