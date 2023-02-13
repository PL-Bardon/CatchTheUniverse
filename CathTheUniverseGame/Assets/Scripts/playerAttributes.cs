using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class playerAttributes : spaceShip
{
    private scoreHistory _scoreHistory;
    private gameOverMenu endGame;
    public GameObject Bullet;
    public GameObject Laser;
    public Transform Canvas;
    public Transform firePoint;
    public GameObject ShieldSkill;
    public GameObject explosionDie;

    public TextMeshProUGUI currentCoin;
    public int totalCoin = 0;

    public float currentRotation;
    public bool shieldActive;
    public float shieldLife;
    public float maxShieldLife;

    public TextMeshProUGUI speedShootText;
    public TextMeshProUGUI damageBulletText;
    public TextMeshProUGUI healhTotalText;

    public TextMeshProUGUI currentKill;
    public int totalKill;

    void Start()
    {
        _scoreHistory = GameObject.FindWithTag("veryimportant").GetComponent<scoreHistory>();
        endGame = GameObject.Find("Canvas").GetComponent<gameOverMenu>();
        healthBar = GameObject.Find("/Canvas/Design/HealthBarPlayer").GetComponent<Slider>();
        healthBar.maxValue = currentHealth;
        healthBar.value = currentHealth;

        shieldActive = false;
        ShieldSkill = this.transform.Find("Shield").gameObject;
        ShieldSkill.SetActive(false);

        maxShieldLife = 100f;
        shieldLife = maxShieldLife;
        totalKill = 0;
    }

    public void Update()
    {
        healthBar.value = currentHealth;
        healthBar.maxValue = maxHealth;
        lastShoot += Time.deltaTime;
        currentCoin.text = "Stars : " + totalCoin;
        currentKill.text = "Kills : " + totalKill;

        //Stats
        speedShootText.text = $"Attack speed : {1/speedShoot:#.##} /s";
        damageBulletText.text = "Damage/Bullet : " + damageBullet;
        healhTotalText.text = "Total health : " + currentHealth + "/" + maxHealth;

        //Health
        if (currentHealth <= 0)
        {
            _scoreHistory.checkScore(totalKill);
            endGame.showEndPannel();
            GameObject animExplosion = Instantiate(explosionDie, this.transform.position, Quaternion.identity);
            animExplosion.transform.localScale *= 5;
            Destroy(healthBar.gameObject);
            Destroy(this.gameObject);
        }
        //MOVE
        if (Input.GetKey(InputManager.instance.right))
            this.transform.position += Vector3.right * Time.deltaTime * speedMove;
        if (Input.GetKey(InputManager.instance.left))
            this.transform.position += Vector3.left * Time.deltaTime * speedMove;
        if (Input.GetKey(InputManager.instance.up))
            this.transform.position += Vector3.up * Time.deltaTime * speedMove;
        if (Input.GetKey(InputManager.instance.down))
            this.transform.position += Vector3.down * Time.deltaTime * speedMove;

        //LOOKING
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
 
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
        currentRotation = rot_z + 90;
        
        //SHOOT
        if (Input.GetKey(KeyCode.Mouse0) && Time.timeScale != 0)
        {
            if (speedShoot >= 0.12F && lastShoot >= speedShoot)
            {
                lastShoot = 0;
                shootBullet(Bullet, speedBullet, damageBullet);
            }
            else if (speedShoot <= 0.12F)
            {
                Laser.SetActive(true);
            }
        }
        else
        {
            Laser.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.tag == "Coin")
        {
            int toGain = other.transform.gameObject.GetComponent<coinReunification>().value;
            totalCoin += toGain;
            int toAdd = (int)Mathf.Clamp(toGain * 5, 0, this.maxHealth - this.currentHealth);
            this.currentHealth += toAdd;
            Destroy(other.gameObject);
        }
    }

    public void shootBullet(GameObject bulletPrefab, float bulletSpeed, float bulletDamage)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.Euler(0, 0, currentRotation), Canvas);
        bullet.name = "My Bullet";
        Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
        body.AddForce(firePoint.up * -bulletSpeed, ForceMode2D.Impulse);
        amoMovement amo = bullet.GetComponent<amoMovement>();
        if (amo != null)
            amo.damage = bulletDamage;
    }
}
