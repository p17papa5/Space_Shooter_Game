using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;   

    public WaveObject[] waves;  //posa wave na kanoun spawn - to [] einai gia na baloume posa wave theloume na spawnaroun

    public int currentWave; // na kanei track se pio wave einai

    public float timeToNextWave; //posi wra gia epomeno wave

    public bool canSpawnWave;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeToNextWave = waves[0].timeToSpawn; //time to the next wave 
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawnWave)
        {
            timeToNextWave -= Time.deltaTime; //xronos gia na spawnarei to epomeno wave
            if (timeToNextWave <= 0)
            {
                Instantiate(waves[currentWave].theWave, transform.position, transform.rotation); //na kanei spawn to wave

                if (currentWave < waves.Length - 1) //na min proxorisei sto epomeno wave an den yparxei
                {

                    currentWave++; //to epomeno wave (+1)

                    timeToNextWave = waves[currentWave].timeToSpawn; //posi wra gia to epomeno 
                }else
                {
                    canSpawnWave = false; //BUGFIX: den spawnarei astamatita to teleuteo wave
                }
            }
        }
    }
    public void ContinueSpawning() //BUGFIX: an px eixame 5 waves kai o player skotonotan sto telos t 4 8a enfanistei 1 extra wave
    {
        if(currentWave <= waves.Length -1 && timeToNextWave > 0) //BUGFIX
        {
            canSpawnWave = true; //na kanei spawn
        }
    }
}

[System.Serializable]
public class WaveObject
{
    public float timeToSpawn; //wra gia na spawnarei to wave
    public EnemyWave theWave; //pio wave na spawnarei
}
