using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starts : MonoBehaviour
{
    public void Play()
    {
        Debug.Log("Start the game");
        Application.LoadLevel("MySpaceShipGame");
    }
}
