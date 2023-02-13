using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinReunification : MonoBehaviour
{
    public GameObject prefab;
    public int value = 1;
    public int ID;

    void Start()
    {
        prefab = this.gameObject;
        ID = GetInstanceID();
        checkIfCoinAround();
    }

    public void checkIfCoinAround()
    {
        RaycastHit2D[] ray = Physics2D.CircleCastAll(this.transform.position, 50, Vector2.down);
        int i = 0;
        while (i < ray.Length && ray[i].transform.gameObject.tag != "Coin") 
            i++;
        if (i < ray.Length)
        {
            GameObject toMerge = ray[i].transform.gameObject;
            if (ID < toMerge.GetComponent<coinReunification>().ID)
            {
                Vector3 self = this.transform.position;
                Vector3 other = toMerge.transform.position;
                Vector3 midPoint = new Vector3((self.x+other.x)/2.0f, (self.y+other.y)/2.0f, this.transform.position.z);
                GameObject newGo = Instantiate(prefab, midPoint, Quaternion.identity);
                newGo.name = "BigCoin";
                newGo.transform.localScale *= 1.1F;
                newGo.GetComponent<coinReunification>().value = this.value + toMerge.transform.gameObject.GetComponent<coinReunification>().value;
                Destroy(toMerge);
                Destroy(this.gameObject);
            }
        }

    }
}
