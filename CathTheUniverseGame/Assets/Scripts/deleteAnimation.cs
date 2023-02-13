using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteAnimation : MonoBehaviour
{
    float die = 0;
    void Update()
    {
        die += Time.deltaTime;
        if (die > 1.5F)
            Destroy(this.gameObject);
    }
}
