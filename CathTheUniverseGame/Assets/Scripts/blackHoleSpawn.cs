using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackHoleSpawn : MonoBehaviour
{
    public GameObject BlackHole;
    public GameObject BlackHoleAbsorbing;
    public GameObject BlackHoleCatch;
    GameObject Player; 

    float currentTime = 0;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        currentTime += Time.deltaTime;
        
        if (currentTime > 2) //20
        {
            currentTime = 0;
            int x;
            int y;
            do 
            {
                x = Random.Range(-5000, 5000);
                y = Random.Range(-5000, 5000);
            }
            while(Mathf.Abs(x) + Mathf.Abs(y) > 1500);
            Vector2 newPos = Player.transform.position + new Vector3(x,y,0);
            Instantiate(BlackHole, newPos, Quaternion.identity, this.transform);          
            Instantiate(BlackHoleAbsorbing, newPos, Quaternion.identity, this.transform);          
        }
    }

}
