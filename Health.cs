using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Health instance;

    public int currentHealth;
    public int maxHealth;

    public GameObject DeathEffect;
    public GameObject MoreDeathEffect;

    public float InvincibleLenth = 2f; //na min mporei na dekti dmg gia kapia secs
    private float InvincibleCounter; // wra gia inv
    public SpriteRenderer theSR; //na fenese aoratos

    public int shieldPower; //shield
    public int shieldMaxPower = 2; //to sheild kanei block 2 hits
    public GameObject theShield;

    private void Awake() //energopiite otan kati einai active dld to health, otan anigei to game tha anigei k to health proto
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIManager.instance.HealthBar.maxValue = maxHealth; //na einai gemato to healthbar
        UIManager.instance.HealthBar.value = currentHealth; // na pigenei eki p einai to currentHealth

        UIManager.instance.shieldBar.maxValue = shieldMaxPower; //na einai gemato to shieldbar
        UIManager.instance.shieldBar.value = shieldPower; //na pigenei eki p einai to current shield
    }

    // Update is called once per frame
    void Update()
    {
        if (InvincibleCounter >= 0) //na min mporei na dekti dmg gia kapia secs
        {
            InvincibleCounter -= Time.deltaTime;

            if(InvincibleCounter <= 0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f); //na ginei to xroma tou opos einai stin arxi (full)
            }
        }
    }

    public void DMGPlayer() // new function DMG, opote troi dmg tha kaloume auto to function
    {
        if (InvincibleCounter <= 0) //na min mporei na dekti dmg gia kapia secs
        {
            if (theShield.activeInHierarchy)
            {
                shieldPower--; //na pigenei katw to life tou shield an troei hit

                if(shieldPower <= 0)
                {
                    theShield.SetActive(false); //na telionei
                }
                UIManager.instance.shieldBar.value = shieldPower; //na pigenei eki p einai to current shield
            }
            else 
            { 

                currentHealth--; // gia na xanei dmg
                UIManager.instance.HealthBar.value = currentHealth; //na paei to healthbar pio katw an faei hit

                if (currentHealth <= 0)
                {
                    Instantiate(DeathEffect, transform.position, transform.rotation); //death effect
                    Instantiate(MoreDeathEffect, transform.position, transform.rotation); //death particle system
                    gameObject.SetActive(false);    //diactivate the objects

                    GameManager.instance.KillPlayer(); //an den exei lives na pethanei

                    WaveManager.instance.canSpawnWave = false; //otan pethanis den tha spawnarei waves
                }

                Movements.instance.doubleShotActive = false; //an faei hit tha ginei off
            }
        }
    }

    public void Respawn() //respawn
    {
        gameObject.SetActive(true);
        currentHealth = maxHealth; //na kanei respwan me max health (3 hits)
        UIManager.instance.HealthBar.value = currentHealth; // na paei to health sto full otan kanei respawn

        InvincibleCounter = InvincibleLenth; //na sindeonte matexitous - na douleuoun tautoxrona
        theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .3f); //na ginei inv molis kanei respwan
    }

    public void ActivateShield()
    {
        theShield.SetActive(true);
        shieldPower = shieldMaxPower; //to shield power einai = me to max power p einai 2 hits

        UIManager.instance.shieldBar.value = shieldPower; //na pigenei eki p einai to current shield
    }
}
