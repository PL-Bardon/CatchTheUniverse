using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketSkill : MonoBehaviour
{
    public float damage = 100F;
    public GameObject explosion;
    bool haveFound;

    void Start()
    {
        damage = 100;
        haveFound = false;
    }
    void Update()
    {
        Vector3 posCam = Camera.main.transform.position;
        if (this.transform.position.x > (posCam.x + Screen.width/2) || this.transform.position.x < (posCam.x - Screen.width/2) || this.transform.position.y > (posCam.y + Screen.height/2) || this.transform.position.y < (posCam.y - Screen.height/2))
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.tag == "Enemy" && !haveFound)
        {
            haveFound = true;
            RaycastHit2D[] ray = Physics2D.CircleCastAll(this.transform.position, 150, Vector2.down);
            for (int i = 0; i < ray.Length; i++)
            {

                if (ray[i].transform.gameObject.tag == "Enemy")
                {
                    ray[i].transform.gameObject.GetComponent<enemyAttributes>().currentHealth -= this.damage;
                }
            }
            GameObject boom = Instantiate(explosion, this.transform.position, Quaternion.identity);
            boom.transform.localScale *= 2.5F;
            Destroy(this.gameObject);
        }
    }
}
