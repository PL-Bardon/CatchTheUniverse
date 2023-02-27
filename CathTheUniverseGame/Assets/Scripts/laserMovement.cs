using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserMovement : MonoBehaviour
{
    public GameObject explosion;
    playerAttributes PA;

    void Start()
    {
        //Get the player's script to be able to use his damage and his attack speed
        PA = GameObject.FindWithTag("Player").GetComponent<playerAttributes>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //If the laser hit an enemy, it will deal damage to it.
        //If it's hit a bullet, the laser will destroy it.
        if (other.transform.gameObject.tag == "Enemy")
        {
            float newDamage = (1/PA.speedShoot) - 9F + (PA.damageBullet/5);
            other.transform.gameObject.GetComponent<enemyAttributes>().currentHealth -= Mathf.Round(newDamage);
            Instantiate(explosion, other.transform.position, Quaternion.identity);
        }
        if (other.transform.gameObject.tag == "Bullet")
        {
            Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
