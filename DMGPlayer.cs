using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Health.instance.DMGPlayer(); //apo to health na kanei dmg to object ston pexti
        }
    }
}
