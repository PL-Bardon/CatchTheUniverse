using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class enemyAttributes : spaceShip
{
    GameObject Player;
    public GameObject Coin;
    public GameObject Bullet;
    public GameObject spawnAnim;
    public Transform firePoint;

    float currentRotation;

    public float attackDistance = 1000F;

    [SerializeField] float distanceFromPlayer;

    bool isRotating;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        healthBar = GameObject.Find("/Environnement/" + this.name + "/Canvas/HealthBar").GetComponent<Slider>();
        healthBarObject = GameObject.Find("/Environnement/" + this.name + "/Canvas/HealthBar");
        healthBar.transform.position = this.transform.position;
        healthBar.maxValue = currentHealth;
        healthBar.value = currentHealth;
        healthBarObject.SetActive(false);

        this.speedMove = 0.2F;

        isRotating = false;

        Instantiate(spawnAnim, this.transform.position, Quaternion.identity);
    }
    void Update()
    {
        if (Player != null)
        {
            if(healthBarObject != null && currentHealth > 0 && currentHealth < maxHealth)
                healthBarObject.SetActive(true);
            healthBar.value = currentHealth;
            healthBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 75, 0));
            distanceFromPlayer = Mathf.Sqrt((this.transform.position.x - Player.transform.position.x) * (this.transform.position.x - Player.transform.position.x)) + Mathf.Sqrt((this.transform.position.y - Player.transform.position.y) * (this.transform.position.y - Player.transform.position.y));
            lastShoot += Time.deltaTime;

            //Is Alive
            if (this.currentHealth <= 0)
            {
                GameObject newCoin = Instantiate(Coin, this.transform.position, Quaternion.identity);
                playerAttributes pa = Player.GetComponent<playerAttributes>();
                if (this.name.Contains("Boss"))
                {
                    int div = (pa.totalKill/20);
                    newCoin.GetComponent<coinReunification>().value *= div;
                }
                pa.totalKill += 1;
                Destroy(this.gameObject);
            }

            //LOOKING
            Vector3 Target = Player.transform.position;
            GameObject Entity = this.gameObject;
            float AngleRad = Mathf.Atan2(Target.y - Entity.transform.position.y, Target.x - Entity.transform.position.x);
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            currentRotation = AngleDeg + 90;
            this.transform.rotation = Quaternion.Euler(0, 0, currentRotation);


            //Move
            if (distanceFromPlayer >= attackDistance)
            {
                Vector3 direction = Player.transform.position - this.transform.position + new Vector3(0,0,1);
                this.transform.position += (direction * Time.deltaTime * speedMove);
            }
            //Attack
            else if (this.gameObject.name.Contains("Enemy3") && (int)(speedShoot - lastShoot) < 0.05F && !isRotating)
            {
                StartCoroutine(rotateShoot());
            }
            else if (!this.gameObject.name.Contains("Enemy3") && lastShoot > speedShoot)
            {
                lastShoot = 0F;
                GameObject bullet = Instantiate(Bullet, firePoint.position, Quaternion.Euler(0, 0, currentRotation));
                bullet.name = "Enemy Bullet";
                Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
                body.AddForce(firePoint.up * speedBullet, ForceMode2D.Impulse);
                bullet.GetComponent<amoMovement>().damage = damageBullet;
            }
        }
    }

    IEnumerator rotateShoot()
    {
        isRotating = true;
        int i = 0;
        while (i < 100)
        {
            this.transform.Rotate(0, 0, 3*i, Space.World);
            yield return new WaitForSeconds(0.0001F);
            i++;
        }
        GameObject bullet = Instantiate(Bullet, firePoint.position, Quaternion.Euler(0, 0, currentRotation));
        bullet.name = "Enemy Bullet";
        Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
        body.AddForce(firePoint.up * speedBullet, ForceMode2D.Impulse);
        bullet.GetComponent<amoMovement>().damage = damageBullet;
        while (i < 120)
        {
            this.transform.Rotate(0, 0, 3*i, Space.World);
            yield return new WaitForSeconds(0.0001F);
            i++;
        }
        isRotating = false;
        lastShoot = 0F;

    }
}
