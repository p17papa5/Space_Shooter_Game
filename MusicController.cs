using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;

    public AudioSource levelMusic, bossMusic, victoryMusic, gameOverMusic; //ta audio p 8a baloume

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        levelMusic.Play(); //na pe3ei to levelMusic me to p 3ekina
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StopMusic() //na stamatisei h musiki
    {
        levelMusic.Stop();
        bossMusic.Stop();
        victoryMusic.Stop();
        gameOverMusic.Stop();
    }

    public void playBoss() //na stamatisi h musiki
    {
        StopMusic(); //na stamatisi h musiki
        bossMusic.Play(); //na pe3ei to boss music
    }

    public void  PlayVictory()
    {
        StopMusic(); //na stamatisi h musiki
        victoryMusic.Play(); //na pe3ei to victory music
    }

    public void PlayGameOver()
    {
        StopMusic(); //na stamatisi h musiki
        gameOverMusic.Play(); // na pe3ei to game over music
    }

}
