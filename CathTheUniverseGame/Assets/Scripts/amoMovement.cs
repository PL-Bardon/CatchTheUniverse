using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amoMovement : MonoBehaviour
{
    public float damage;
    public GameObject explosion;
    SpriteRenderer sr;

    void Start()
    {
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        if (damage <= 100)
        {
            sr.color = Color.white;
        }
        else if (damage <= 200)
        {
            sr.color = Color.cyan;
        }
        else
        {
            sr.color = Color.blue;
        }
    }

    void Update()
    {
        //Get the current postiont of the main camera
        Vector3 posCam = Camera.main.transform.position;

        //Check if the actual bullet of this script, is shoot by the player
        //It will destroy the bullet it it goes to far from the player
        if (this.transform.gameObject.name == "My Bullet")//Bullet form the player
            if (this.transform.position.x > (posCam.x + Screen.width/2) || this.transform.position.x < (posCam.x - Screen.width/2) || this.transform.position.y > (posCam.y + Screen.height/2) || this.transform.position.y < (posCam.y - Screen.height/2))
                Destroy(this.gameObject);
        else//Bullet from enemy
            if (this.transform.position.x > (posCam.x + Screen.width) || this.transform.position.x < (posCam.x - Screen.width) || this.transform.position.y > (posCam.y + Screen.height) || this.transform.position.y < (posCam.y - Screen.height))
                Destroy(this.gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check it the bullet hit an enemy
        if (other.transform.gameObject.tag == "Enemy")
        {
            //Check it the bullet was shoot by the player
            if (this.transform.gameObject.name == "My Bullet")
            {
                //substract life to the enemi
                other.transform.gameObject.GetComponent<enemyAttributes>().currentHealth -= damage;
                //Create an animation of explosion
                Instantiate(explosion, this.transform.position, Quaternion.identity);
                //Destroy the bullet
                Destroy(this.gameObject);
            }

        }
        //Check if the rocket hit the player
        if (other.transform.gameObject.tag == "Player")
        {
            playerAttributes PA = other.transform.gameObject.GetComponent<playerAttributes>();
            if (PA.shieldActive) //Check if the player have his shield active to substract damage to shield life
            {
                if (damage > PA.shieldLife)
                {
                    PA.ShieldSkill.SetActive(false);
                    PA.shieldActive = false;
                    PA.currentHealth -= (damage - PA.shieldLife);
                }
                PA.shieldLife -= damage;
            }
            else
            {
                PA.currentHealth -= damage;
            }
            Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        //Check if this bullet hit another bullet
        if (other.transform.gameObject.tag == "Bullet")
        {
            Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
