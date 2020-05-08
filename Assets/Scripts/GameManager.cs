using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject titleScreen;
    public GameObject gameLoseText;
    public GameObject gameWinText;
    public Button restartButton;

    [Header("Gameplay")]
    //public List<GameObject> spawners;
    public Animator fadeInOut;

    //private int spawnerCount = 3;

    public int fear = 0;
    public int maxFear;
    private int ambientFear = 1;
    private float increaseSpeed = 1;
    public bool gameActive;

    public void StartGame()
    {
        gameActive = true;
        StartCoroutine(AmbientFearIncrease());
        titleScreen.SetActive(false);
        fadeInOut.SetBool("gameActive", true);
    }

    // For stretch goal - want to randomize starting locations of spawners
    //public void SpawnTarget(List<GameObject> targets, int spawnCount)
    //{
    //    if (gameActive)
    //    {
    //        for (int i = 0; i < spawnCount; i++)
    //        {
    //            int index = Random.Range(0, targets.Count);
    //            Instantiate(targets[index]);
    //        }
    //    }        
    //}

    IEnumerator AmbientFearIncrease()
    {
        while (gameActive)
        {
            yield return new WaitForSecondsRealtime(increaseSpeed);
            fear += ambientFear;
            if (fear >= maxFear)
            {
                GameLost();
            }
        }
        
    }

    void GameLost()
    {
        gameActive = false;
        fadeInOut.SetBool("gameActive", false);
        fadeInOut.SetBool("gameLose", true);
        gameLoseText.SetActive(true);
        restartButton.gameObject.SetActive(true);
        Debug.Log("Fear has reached maximum, you are lost in nightmare");
    }

    public void GameWin()
    {
        gameActive = false;
        fadeInOut.SetBool("gameActive", false);
        fadeInOut.SetBool("gameWin", true);
        gameWinText.SetActive(true);
        restartButton.gameObject.SetActive(true);
        Debug.Log("You've escaped!");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
