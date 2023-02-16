using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackHoleAttributes : MonoBehaviour
{
    [SerializeField] float distanceFromPlayer;
    GameObject Player;
    playerAttributes PA;
    Rigidbody2D rb;

    public int pullForce;
    int pullRadius;

    void Start()
    {
        pullForce = 1000;
        pullRadius = 1500;
        Player = GameObject.FindWithTag("Player");
        if (Player != null)
        {
            PA = Player.GetComponent<playerAttributes>();
            rb = PA.GetComponent<Rigidbody2D>();
        }

    }
    void Update()
    {
        if (Time.timeScale != 0 && Player != null)
        {
            distanceFromPlayer = Mathf.Sqrt((this.transform.position.x - Player.transform.position.x) * (this.transform.position.x - Player.transform.position.x)) + Mathf.Sqrt((this.transform.position.y - Player.transform.position.y) * (this.transform.position.y - Player.transform.position.y));
            if (distanceFromPlayer < pullRadius)
            {
                if (distanceFromPlayer > 20)
                {
                    Vector3 forceDirection = transform.position - Player.transform.position;
                    float mul = Mathf.Exp(pullForce/distanceFromPlayer) * 10;
                    mul = Mathf.Clamp(mul,5,1000);
                    rb.AddForce(forceDirection.normalized * mul);
                }
            }
            else
            {
                if (Mathf.Abs(rb.velocity.x) > 1000 || Mathf.Abs(rb.velocity.y) > 1000)
                    rb.velocity = rb.velocity * 0.9F;
                else if (Mathf.Abs(rb.velocity.x) > 15 || Mathf.Abs(rb.velocity.y) > 15)
                    rb.velocity = rb.velocity * 0.995F;
                else
                    rb.velocity = Vector3.zero;
            }   
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
            Destroy(Player);
    }
}
