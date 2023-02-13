using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public playerAttributes PA;

    public GameObject pausePannel;
    public GameObject settingPannel;
    
    private int healthPrice;
    private int damagePrice;
    private int speedPrice;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI speedText;

    void Start()
    {
        pausePannel.SetActive(false);
        healthPrice = 3;
        damagePrice = 1;
        speedPrice = 5;
        healthText.text = "Price : " + healthPrice;
        damageText.text = "Price : " + damagePrice;
        speedText.text = "Price : " + speedPrice;
    }

    void Update()
    {
        healthText.text = "Price : " + healthPrice;
        damageText.text = "Price : " + damagePrice;
        speedText.text = "Price : " + speedPrice;
        if (PA != null && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape)))
        {
            pauseUnpause();
        }  
    }

    public void upgradeHealth()
    {
        if (PA.totalCoin >= healthPrice)
        {
            PA.totalCoin -= healthPrice;
            healthPrice += 2;
            PA.maxHealth += 100;
            PA.currentHealth += 100;
        }
    }
    public void upgradeDamage()
    {
        if (PA.totalCoin >= damagePrice)
        {
            PA.totalCoin -= damagePrice;
            damagePrice += 2;
            PA.damageBullet += 10;
        }
    }
    public void upgradeSpeed()
    {
        if (PA.totalCoin >= speedPrice)
        {
            PA.totalCoin -= speedPrice;
            speedPrice += 3;
            PA.speedShoot -= 0.02F;
        }
    }
    public void pauseUnpause()
    {
        Time.timeScale = (Time.timeScale - 1) * (Time.timeScale - 1);
        pausePannel.SetActive(!pausePannel.activeSelf);
    }

    public void showSettings()
    {
        settingPannel.SetActive(!settingPannel.activeSelf);
    }
    public void quit()
    {
        GameObject.FindWithTag("veryimportant").GetComponent<scoreHistory>().SaveScore();
        Application.Quit();
    }
}
