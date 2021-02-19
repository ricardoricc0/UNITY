using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage = 20;
    public float speed = 20f;

    public Rigidbody2D rb;

    public GameObject impactEffect;

    private void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyPatrol enemy = hitInfo.GetComponent<EnemyPatrol>();

        if(enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log("Take Damage");
        }
        Destroy(gameObject);

        Instantiate(impactEffect, transform.position, transform.rotation);
    }
}
