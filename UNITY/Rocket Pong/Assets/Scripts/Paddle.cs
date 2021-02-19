using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Side side;
    [SerializeField] private float resetTime;
    public bool isAI;
    private Ball ball;

    private float randomYOffset;
    private BoxCollider2D col;
    private bool firstIncoming;
    private bool overridePosition;

    private Vector2 forwardDirection;
    public enum Side { left, right }

    private void Start()
    {
        col = GetComponent<BoxCollider2D>();

        if (side == Side.left)
        {
            forwardDirection = Vector2.right;
        }
        else if (side == Side.right)
        {
            forwardDirection = Vector2.left;
        }
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
    }

    private void Update()
    {
        if (!overridePosition){
            MovePaddle();
        }
        
    }

    private void MovePaddle(){
        float targetYPosition = GetNewYPosition();
        ClampPosition(ref targetYPosition);

        transform.position = new Vector3(transform.position.x, targetYPosition, transform.position.z);
    }

    private void ClampPosition(ref float targetYPosition)
    {
        float minY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0)).y;
        float maxY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)).y;

        targetYPosition = Mathf.Clamp(targetYPosition, minY, maxY);
    }

    private float GetNewYPosition()
    {
        float result = transform.position.y;

        if (isAI)
        {
            if (BallIncoming()){
                if (firstIncoming){
                    // print("First Time Incoming");
                    firstIncoming = false;
                    randomYOffset = GetRandomOffset();
                }   
                result = Mathf.MoveTowards(transform.position.y, ball.transform.position.y + randomYOffset, moveSpeed * Time.deltaTime); 
            }
            else{
                firstIncoming = true;
            }        
        }
        else
        {
            float movement = Input.GetAxisRaw("Vertical " + side.ToString()) * moveSpeed * Time.deltaTime;
            result = transform.position.y + movement;
        }

        return result;
    }

    private bool BallIncoming()
    {
        float dotD = Vector2.Dot(ball.velocity, forwardDirection);
        return dotD < 0f;
    }

    private float GetRandomOffset(){
        float maxOffset = col.bounds.extents.y;
        return Random.Range(-maxOffset, maxOffset);
    }

    public void Reset(){
        StartCoroutine(ResetRoutine());
    }

    private IEnumerator ResetRoutine(){
        overridePosition = true;
        float startPosition = transform.position.y;
        for (float timer = 0; timer < resetTime; timer += Time.deltaTime){
            float targetYPosition = Mathf.Lerp(startPosition, 0f, timer / resetTime);
            transform.position = new Vector3 (transform.position.x, targetYPosition, transform.position.z);
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        overridePosition = false;
    }

}
