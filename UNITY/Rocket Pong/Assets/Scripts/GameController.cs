using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }
    [SerializeField] private int leftScore;
    [SerializeField] private int rightScore;
    [SerializeField] private int scoreToWin = 2;
    [SerializeField] private bool inMenu;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Paddle leftPaddle;
    [SerializeField] private Paddle rightPaddle;

    private Ball ball;
    private Paddle.Side serveSide;

    private void Awake(){
        instance = this;
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();

    }

    public void DoMenu(){
        inMenu = true;
        leftPaddle.isAI = rightPaddle.isAI = true;
        leftScore = rightScore = 0;
        uiManager.UpdateScoreText(leftScore, rightScore);
        ResetGame();
    }

    public void Score(Paddle.Side side){
        if (side == Paddle.Side.left){
            leftScore++;
        }
        else if (side == Paddle.Side.right){
            rightScore++;
        }

        uiManager.UpdateScoreText(leftScore, rightScore);
        serveSide = side;

        if (IsGameOver()){
            if (inMenu){
                ResetGame();
                leftScore = rightScore = 0;
            }
            else{
                ball.gameObject.SetActive(false);
                uiManager.ShowGameOver(side);
            }     
        }
        else{
            ResetGame();
        }
        
    }

    private bool IsGameOver(){
        bool result = false;
        if (leftScore >= scoreToWin || rightScore >= scoreToWin){
            result = true;
        }
        return result;
    }

    private void ResetGame(){
        ball.gameObject.SetActive(true);
        ball.Reset(serveSide);
        leftPaddle.Reset();
        rightPaddle.Reset();
    }

    #region UI Methods

    public void GoToMenu(){
        uiManager.ShowMenu();
        DoMenu();
    }

    public void Replay(){
        InitializeGame();
        uiManager.OnGameStart();
    }

    public void StartOnePlayer(){
        leftPaddle.isAI = true;
        rightPaddle.isAI = false;
        InitializeGame();
    }

    public void StartTwoPlayers(){
        leftPaddle.isAI = false;
        rightPaddle.isAI = false;
        InitializeGame();
    }

    private void InitializeGame(){
        inMenu = false;
        leftScore = rightScore = 0;
        uiManager.UpdateScoreText(leftScore, rightScore);
        ResetGame();
        uiManager.OnGameStart();
    }

    public void Quit(){
        Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif    
    }

    #endregion
}
