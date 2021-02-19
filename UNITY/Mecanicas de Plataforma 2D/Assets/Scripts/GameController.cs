using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int totalScore;

    public Text scoreText;
    public Text scoreTextUI;

    public GameObject gameOver;
    public GameObject nextLevelUI;

    public static GameController instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void UpdateScoreText()
    {
        scoreText.text = totalScore.ToString();
        scoreTextUI.text = totalScore.ToString();
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }

    public void ShowNextLevelUI()
    {
        nextLevelUI.SetActive(true);
    }

    public void ResetGame(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }

    public void NextLevel(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }
   
}
