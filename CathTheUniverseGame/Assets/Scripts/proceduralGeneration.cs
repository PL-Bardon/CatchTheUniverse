using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proceduralGeneration : MonoBehaviour
{
    public GameObject player;
    public GameObject Background;
    int size = 8200;
    int gap = 4000; //Size = 4100 * 2

    int mulVert = 0;
    int mulHor = 0;
    void Update()
    {
        Vector3 pos = player.transform.position - new Vector3(960, 540, 0);
        Debug.Log(pos + " : " + mulVert + " " + mulHor);
        if (pos.y > ((mulVert + 1) * gap))
            createNewBackground("up");
        if (pos.y < ((mulVert - 1) * gap))
            createNewBackground("down");
        if (pos.x > ((mulHor + 1) * gap))
            createNewBackground("right");
        if (pos.x < ((mulHor - 1) * gap))
            createNewBackground("left");
    }

    void createNewBackground(string dir)
    {
        switch(dir)
        {
            case "up":
                mulVert += 1;
                break;
            case "down":
                mulVert -= 1;
                break;
            case "right":
                mulHor += 1;
                break;
            case "left":
                mulHor -= 1;
                break;
            default:
                break;
        }
        Instantiate(Background, new Vector3(mulHor * size, mulVert * size, 1), Quaternion.identity);

    }
}
