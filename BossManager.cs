using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;
    public string bossName; //boss name

    public int currentHealth = 100; //to health tou boss einai 100 (gia test tha alla3ei meta)

    // public BattleShot[] shotToFire; // posa shot 8a petaei ana xrono

    public BattlePhase[] phases; // na mas dixnei ta phase
    public int currentPhase; //se pio phase eimaste
    public Animator bossAnimator; //boss animation

    public GameObject endExplosion; //end explosion animation
    public bool battleEnding; //battle end
    public float timeToExplosionEnd; // posi wra na kanei explosions
    public float waitToEndLevel; //se posi wra na teliosei to lv

    public Transform theBoss;

    public int scoreValue = 10000; //to boss na dinei 10000 points - score

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.bossName.text = bossName; //na di3ei to boss name
        UIManager.instance.bossHealthSlider.maxValue = currentHealth; //na 3ekinisei me max health
        UIManager.instance.bossHealthSlider.value = currentHealth; // na di3ei to current health tou boss - na ginete update
        UIManager.instance.bossHealthSlider.gameObject.SetActive(true); //na ani3ei-di3ei to healthslider tou boss

        MusicController.instance.playBoss(); //na 3ekinisei to boss music
    }

    // Update is called once per frame
    void Update()
    {
        /*for (int i = 0; i < shotToFire.Length; i++) // boss shots me for loop
        {
            shotToFire[i].shotCounter -= Time.deltaTime; //exoume balei max size gia shots 2
            if(shotToFire[i].shotCounter <=0) // kai 8a to kanei loop sinexia
            {
                shotToFire[i].shotCounter = shotToFire[i].timeBetweenShots;  //na feugei to shoot me ton xrono p to kaname setup
                Instantiate(shotToFire[i].theShot, shotToFire[i].firePoint.position, shotToFire[i].firePoint.rotation); //na feugei to shot apo to firepoint
            }
        } */
        if(!battleEnding) //an den teliosei to battleending
        {
            if (currentHealth <= phases[currentPhase].healthToEndPhase) //otan paei sto X HP
            {
                phases[currentPhase].removeAtPhaseEnd.SetActive(false); //na figei to shell
                Instantiate(phases[currentPhase].addPhaseEndExplotion, phases[currentPhase].newSpawnPoint.position, phases[currentPhase].newSpawnPoint.rotation); //na ginei explosion
                Instantiate(phases[currentPhase].addPhaseMoreExplotion, phases[currentPhase].newSpawnPoint.position, phases[currentPhase].newSpawnPoint.rotation); // na ginei explsion me particle

                currentPhase++; //na paei epomeno phase

                bossAnimator.SetInteger("Phase", currentPhase + 1); //na alla3ei to animator (extra moves)
            }
            else
            {
                for (int i = 0; i < phases[currentPhase].phaseShots.Length; i++) // boss shots me for loop
                {
                    phases[currentPhase].phaseShots[i].shotCounter -= Time.deltaTime; //exoume balei max size gia shots 2
                    if (phases[currentPhase].phaseShots[i].shotCounter <= 0) // kai 8a to kanei loop sinexia
                    {
                        phases[currentPhase].phaseShots[i].shotCounter = phases[currentPhase].phaseShots[i].timeBetweenShots;  //na feugei to shoot me ton xrono p to kaname setup
                        Instantiate(phases[currentPhase].phaseShots[i].theShot, phases[currentPhase].phaseShots[i].firePoint.position, phases[currentPhase].phaseShots[i].firePoint.rotation); //na feugei to shot apo to firepoint
                    }
                }
            }
        }
    }

    public void HurtBoss()
    {
        currentHealth--; //na pigenei katw katw to health tou
        UIManager.instance.bossHealthSlider.value = currentHealth; // Na ginete update

        if (currentHealth <= 0 && !battleEnding) //otan paei 0 to HP na teliosei to battle
        {
            /*Destroy(gameObject); //na figei apo tin othonei -- destroy
            UIManager.instance.bossHealthSlider.gameObject.SetActive(false); //na klisei to healthslider tou boss */

            battleEnding = true; //na teliosei to battle
            StartCoroutine(EndBattleCo()); //na 3ekinisei to coroutin
        }
    }

    public IEnumerator EndBattleCo() //corouting otan teliosei to battle
    {
        UIManager.instance.bossHealthSlider.gameObject.SetActive(false); //na klisei to healthslider tou boss
        Instantiate(endExplosion, theBoss.position, theBoss.rotation); //na kanei spawn to explosion
        bossAnimator.enabled = false; //na klisi to animator
        GameManager.instance.AddScore(scoreValue); //na prosthesei to scorevalue pou balame panw

        yield return new WaitForSeconds(timeToExplosionEnd); // na perimenei kapia deuterolepta

        theBoss.gameObject.SetActive(false); //na klisi to boss

        yield return new WaitForSeconds(waitToEndLevel); //na perimenei kapia deuterolepta gia na teliosei to lvl
        StartCoroutine(GameManager.instance.EndLevelCo()); //na kalesei to gamemanager endlvl
    }
}

[System.Serializable]
public class BattleShot //boss shots setup
{
    public GameObject theShot; //eikona
    public float timeBetweenShots; //xronos meta3i ton shots
    public float shotCounter; //posa shots 8a einai
    public Transform firePoint; // apo pou 8a feugoun ta shots
}
[System.Serializable]
public class BattlePhase
{
    public BattleShot[] phaseShots;
    public int healthToEndPhase; //na mpei sto epomeno phase me sxesi me to HP
    public GameObject removeAtPhaseEnd; // na figei to shell (visual update)
    public GameObject addPhaseEndExplotion; //Exoplosion
    public GameObject addPhaseMoreExplotion; //more explotion (particle system
    public Transform newSpawnPoint; //new spawnpoint
}