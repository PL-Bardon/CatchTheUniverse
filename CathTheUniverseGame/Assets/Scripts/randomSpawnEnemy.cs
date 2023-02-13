using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawnEnemy : MonoBehaviour
{
    public GameObject Player;
    public List<GameObject> Enemies;

    float totalTimeGame = 0F;

    [SerializeField] int totalEnemies = 0;
    int rangeEnemies = 1000;
    float currentTime = 4F;

    [SerializeField] float SpawningDelai = 1F; //2.5
    [SerializeField] public float LimitSpawningDelai = 0.2F;

    int gap1 = 90;
    int gap2 = 180;
    int gapBoss = 20;

    int currentGapBoss = 20;
    int currentBossKill = 0;
    playerAttributes PA;
    GameObject currentBoss;

    int numberAllEnemies = 0;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        PA = Player.GetComponent<playerAttributes>();
        currentBoss = null;
    }
    void Update()
    {
        if(Player != null)
        {
            totalTimeGame += Time.deltaTime;
            currentTime += Time.deltaTime;
            if (currentBossKill != PA.totalKill && PA.totalKill >= currentGapBoss)
            {
                currentGapBoss += gapBoss;
                currentBossKill = PA.totalKill;
                totalEnemies += 1;
                int mod = (currentBossKill / gapBoss) + 1;
                Vector2 newPos = Player.transform.position + new Vector3(0,((currentGapBoss/gapBoss)+5) * 50,0);
                currentBoss = Instantiate(Enemies[3], newPos, Quaternion.identity, this.transform);
                currentBoss.transform.localScale *= (2F + (mod/5F));
                enemyAttributes enemi = currentBoss.GetComponent<enemyAttributes>();
                enemi.maxHealth *= mod;
                enemi.currentHealth *= mod;
                enemi.damageBullet *= mod;
                currentBoss.name = "Boss_" + currentBossKill;
            }
            else
            {
                if ((totalEnemies-PA.totalKill) < 100)
                {
                    if (currentTime >= SpawningDelai)
                    {
                        int x = Random.Range(-rangeEnemies, rangeEnemies);
                        int y = Random.Range(-rangeEnemies, rangeEnemies);
                        if(Mathf.Abs(x) + Mathf.Abs(y) > 100)
                        {
                            GameObject toSpawn = enemyToSpawn();
                            Vector2 newPos = Player.transform.position + new Vector3(x,y,0);
                            GameObject obj = Instantiate(toSpawn, newPos, Quaternion.identity, this.transform);
                            if (totalTimeGame > gap1)
                            {
                                int nbrEnemy = (obj.name[5] - '0');
                                enemyAttributes enemyAtt = obj.GetComponent<enemyAttributes>();
                                enemyAtt.maxHealth += (Mathf.Round(totalTimeGame - gap1) * nbrEnemy / 2);
                                enemyAtt.currentHealth += (Mathf.Round(totalTimeGame - gap1) * nbrEnemy / 2);
                                if(totalTimeGame > gap2)
                                    enemyAtt.damageBullet += Mathf.Round((Mathf.Round(totalTimeGame - gap2) * nbrEnemy / 5));
                            }
                            obj.name = toSpawn.name + "_" + numberAllEnemies++;
                            totalEnemies++;
                            obj = null;
                            currentTime = 0;
                            if (SpawningDelai > LimitSpawningDelai)
                            {
                                if (SpawningDelai > 2f)
                                    SpawningDelai -= (SpawningDelai * 0.011F);
                                else if (SpawningDelai > 1.5f)
                                    SpawningDelai -= (SpawningDelai * 0.0065F);
                                else if (SpawningDelai > 1f)
                                    SpawningDelai -= (SpawningDelai * 0.01F);
                                else if (SpawningDelai > 0.5f)
                                    SpawningDelai -= (SpawningDelai * 0.0085F);
                                else    
                                    SpawningDelai -= (SpawningDelai * 0.0035F);
                            } 
                        }
                    }
                }
                else
                {
                   testWeakEnemi();
                }
            }
        }
    }

    public GameObject enemyToSpawn()
    {
        int rg = (int)totalTimeGame / 6;
        int rdm = Random.Range(1, rg);
        if (rdm % 10 == 0) //Around 60 sec
            return Enemies[2];
        else if (rdm % 5 == 0) //Around 30 sec
            return Enemies[1];
        else
            return Enemies[0];
    }

    void testWeakEnemi()
    {
        totalEnemies -= 1;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).name.Contains("Enemy1"))
            {
                Destroy(this.transform.GetChild(i).gameObject);
                return;
            }  
        }
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).name.Contains("Enemy2"))
            {
                Destroy(this.transform.GetChild(i).gameObject);
                return;
            }  
        }
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).name.Contains("Enemy3"))
            {
                Destroy(this.transform.GetChild(i).gameObject);
                return;
            }  
        }
    }
}   
