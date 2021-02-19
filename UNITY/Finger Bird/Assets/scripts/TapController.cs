using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class TapController : MonoBehaviour
{
    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;

    public float tapForce = 10;
    public float tiltSmooth = 5;
    public Vector3 startPos;

    public AudioSource tapAudio;
    public AudioSource dieAudio;
    public AudioSource scoreAudio;

    Rigidbody2D rigidB;
    Quaternion downRotation; // Fancy rotation with 4 values (x, y, z, w)
    Quaternion forwardRotation; // Fancy rotation with 4 values (x, y, z, w)

    GameManager game; // a reference or instance
    TrailRenderer trail;

    // Start is called before the first frame update
    
	void Start() {
		rigidB = GetComponent<Rigidbody2D>();
		downRotation = Quaternion.Euler(0, 0 ,-100);
		forwardRotation = Quaternion.Euler(0, 0, 40);
		game = GameManager.Instance;
		rigidB.simulated = false;
		//trail = GetComponent<TrailRenderer>();
		//trail.sortingOrder = 20; 
	}
    
    void OnEnable(){
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable(){
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void OnGameStarted(){
        rigidB.velocity = Vector3.zero;
        rigidB.simulated = true;
    }

    void OnGameOverConfirmed(){
        transform.localPosition = startPos;
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
       if (game.GameOver) return;
       if (Input.GetMouseButtonDown(0))
       {
           tapAudio.Play();
           // Time.timeScale +=1; // Acelera o tempo do jogo a cada clique
           transform.rotation = forwardRotation;
           rigidB.velocity = Vector2.zero;
           rigidB.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
       } 
       
       transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);

    }


    void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "score_zone") {
            // Register a Score event
			OnPlayerScored(); // Evente sent to Gamemanager;
			scoreAudio.Play(); // Play a Sound
		}
		if (col.gameObject.tag == "dead_zone") {
            // Register a dead event
			rigidB.simulated = false;
			OnPlayerDied(); // Evente sent to Gamemanager;
			dieAudio.Play(); // Play a Sound
		}
	}
    

}


