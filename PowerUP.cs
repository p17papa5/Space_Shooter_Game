using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
    public bool isShield;

    public bool isBoost;

    public bool isDoubleShot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") //an gini epafi me ton player
        {
            Destroy(gameObject); //na ginei destroy

            if(isShield) //an einai shield
            {
                Health.instance.ActivateShield(); //na dosei shield sto health
            }

            if(isBoost) //an einai speedboost
            {
                Movements.instance.ActivateSpeedBoost(); //na dosei to speed ston player
            }
            
            if(isDoubleShot) //an einai doubleshot
            {
                Movements.instance.doubleShotActive = true; //na 3ekinisei ta doubleshots
            }
        }
    }
}
