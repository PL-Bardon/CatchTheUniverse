using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cmaeraLock : MonoBehaviour
{
    public Transform Player;
    void Update()
    {
        if (Player != null)
            this.transform.position = new Vector3(Player.position.x, Player.position.y, -10);
    }
}
