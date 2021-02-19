using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance; // da acesso aos outros scripts através da variável instance.

    public GameObject gameOverScreen;
   
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void ResetGame(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }

}
