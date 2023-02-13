using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amoMovement : MonoBehaviour
{
    public float damage;
    public GameObject explosion;

    void Update()
    {
        Vector3 posCam = Camera.main.transform.position;
        if (this.transform.gameObject.name == "My Bullet")
            if (this.transform.position.x > (posCam.x + Screen.width/2) || this.transform.position.x < (posCam.x - Screen.width/2) || this.transform.position.y > (posCam.y + Screen.height/2) || this.transform.position.y < (posCam.y - Screen.height/2))
                Destroy(this.gameObject);
        else//Bullet from enemy
            if (this.transform.position.x > (posCam.x + Screen.width) || this.transform.position.x < (posCam.x - Screen.width) || this.transform.position.y > (posCam.y + Screen.height) || this.transform.position.y < (posCam.y - Screen.height))
                Destroy(this.gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.tag == "Enemy")
        {
            if (this.transform.gameObject.name == "My Bullet")
            {
                other.transform.gameObject.GetComponent<enemyAttributes>().currentHealth -= damage;
                Instantiate(explosion, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }

        }
        if (other.transform.gameObject.tag == "Player")
        {
            playerAttributes PA = other.transform.gameObject.GetComponent<playerAttributes>();
            if (PA.shieldActive)
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
        if (other.transform.gameObject.tag == "Bullet")
        {
            Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
