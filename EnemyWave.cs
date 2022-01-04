using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DetachChildren(); //ta objects na bgenoun ektos ton waves
        Destroy(gameObject);        // kai na ginonte destroy otan skotononte h otan bgenoun ektos othonis
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
