using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserMovement : MonoBehaviour
{
    public GameObject explosion;
    playerAttributes PA;

    void Start()
    {
        PA = GameObject.FindWithTag("Player").GetComponent<playerAttributes>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
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
