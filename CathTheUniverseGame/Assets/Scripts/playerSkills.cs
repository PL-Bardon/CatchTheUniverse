using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class playerSkills : MonoBehaviour
{
    public playerAttributes PA;
    public InputManager input;
    public GameObject BulletSkill1;

    public GameObject Skill1;
    Slider slider1;
    float time1 = 3;
    bool isAvailable1;
    public TextMeshProUGUI skillTxt1;

    public GameObject Skill2;
    Slider slider2;
    float time2 = 3;
    bool isAvailable2;
    public TextMeshProUGUI skillTxt2;

    public GameObject Skill3;
    Slider slider3;
    float time3 = 3;
    bool isAvailable3;
    public TextMeshProUGUI skillTxt3;

    public GameObject Skill4;
    Slider slider4;
    float time4 = 3;
    bool isAvailable4;
    public TextMeshProUGUI skillTxt4;

    void Start()
    {
        PA = this.gameObject.GetComponent<playerAttributes>();

        slider1 = Skill1.transform.Find("Loading").GetComponent<Slider>();
        slider1.maxValue = time1;
        slider1.value = time1;

        slider2 = Skill2.transform.Find("Loading").GetComponent<Slider>();
        slider2.maxValue = time2;
        slider2.value = time2;

        slider3 = Skill3.transform.Find("Loading").GetComponent<Slider>();
        slider3.maxValue = time3;
        slider3.value = time3;

        slider4 = Skill4.transform.Find("Loading").GetComponent<Slider>();
        slider4.maxValue = time4;
        slider4.value = time4;

    }
    void Awake()
    {
        input = GameObject.FindWithTag("veryimportant").GetComponent<InputManager>();
    }
    void Update()
    {
        if (input == null)
            Debug.Log("Player SKill input null");
        if (input != null && (input.Skill1 + "") != "None")
        {
            skillTxt1.text = input.Skill1 + "";
            skillTxt2.text = input.Skill2 + "";
            skillTxt4.text = input.Skill4 + "";
            if (input.Skill3 == KeyCode.LeftShift)
                skillTxt3.text = "↑";
            else
                skillTxt3.text = input.Skill3 + "";
        }
        
        //SKILL 4 Gestion
        if(PA.shieldLife <= 0)
        {
            PA.ShieldSkill.SetActive(false);
            PA.shieldActive = false;
            PA.shieldLife = PA.maxShieldLife;
        }
        //SKILL 1
        if (slider1.value > 0)
        {
            slider1.value -= Time.deltaTime;
        }
        else
        {
            isAvailable1 = true;
            slider1.value = 0;
        }
        if (isAvailable1 && Input.GetKeyDown(InputManager.instance.Skill1) && Time.timeScale != 0)
        {
            //ROCKET
            slider1.maxValue = time1;
            slider1.value = time1;
            isAvailable1 = false;
            PA.shootBullet(BulletSkill1, 800, PA.damageBullet * 2);
        }
        //SKILL 2
        if (slider2.value > 0)
        {
            slider2.value -= Time.deltaTime;
        }
        else
        {
            isAvailable2 = true;
            slider2.value = 0;
        }
        if (isAvailable2 && Input.GetKeyDown(InputManager.instance.Skill2) && Time.timeScale != 0)
        {
            //HEAL
            slider2.maxValue = time2;
            slider2.value = time2;
            isAvailable2 = false;
            if (PA.maxHealth - PA.currentHealth > 100)
                PA.currentHealth += 100;
            else
                PA.currentHealth = PA.maxHealth;
        } 
        //SKILL 3
        if (slider3.value > 0)
        {
            slider3.value -= Time.deltaTime;
        }
        else
        {
            isAvailable3 = true;
            slider3.value = 0;
        }
        
        if (isAvailable3 && Input.GetKeyDown(InputManager.instance.Skill3) && Time.timeScale != 0)
        {
            //DASH
            slider3.maxValue = time3;
            slider3.value = time3;
            isAvailable3 = false;
            StartCoroutine(dashSkill3());
        }
        //SKILL 4
        if (slider4.value > 0)
        {
            slider4.value -= Time.deltaTime;
        }
        else
        {
            isAvailable4 = true;
            slider4.value = 0;
        }
        
        if (isAvailable4 && Input.GetKeyDown(InputManager.instance.Skill4) && Time.timeScale != 0)
        {
            //DASH
            slider4.maxValue = time4;
            slider4.value = time4;
            isAvailable4 = false;
            PA.ShieldSkill.SetActive(true);
            PA.shieldActive = true;
        }
        
    }
    IEnumerator dashSkill3()
    {
        PA.speedMove *= 8;
        yield return new WaitForSeconds(0.06F);
        PA.speedMove /= 8;
    }     
}
