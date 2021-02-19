using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    player Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject.transform.parent.gameObject.GetComponent<player>();
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.layer == 8)
        {
            Player.isJumping = false;
        }
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.layer == 8)
        {
            Player.isJumping = true;
        }
    }


}
