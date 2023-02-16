using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;



public class spaceShip : MonoBehaviour
{
    public float currentHealth = 0F;
    public float maxHealth = 0F;
    public float speedMove = 0F; //Between 0 and 1 for Enemy
    public float speedBullet = 0F;
    public float speedShoot = 0F;
    public float damageBullet = 0F;

    public float lastShoot = 0F;

    public Slider healthBar;
    public GameObject healthBarObject;

}
