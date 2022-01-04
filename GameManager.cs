using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentLives = 30; // 3 zoes

    public float respawnTime = 2f; //respawn time

    public int currentScore;
    private int highScore = 500; //TEST

    public bool levelEnding; //level Ending

    private int levelScore; //score tou level mas

    public float waitForLevelEnd = 5f; //level end deley 5secs

    public string nextLevel; //next level

    private bool canPause; //na mporei na ginei pause

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        currentLives = PlayerPrefs.GetInt("Current Lives"); //na sinexisei me tis zoes p eixe apo proigoumena epipeda
        UIManager.instance.livesText.text = "x " + currentLives; //na ginete update ta lives

        highScore = PlayerPrefs.GetInt("High Score"); //na dei apo ta arxia poso einai to teleuteo highscore
        UIManager.instance.hiScoreText.text = "HighScore: " + highScore; //na ginei update to highscore an alla3e

        currentScore = PlayerPrefs.GetInt("CurrentScore"); //na di3ei to score p egine sto teleuteo game - auto p epe3es prin na termatisis
        UIManager.instance.scoreText.text = "Score: " + currentScore; //na ginete update to score

        canPause = true; //na mporei na ani3ei to pauseScene otan einai genika mesa sto game (oxi GameOver - EndScene)
    }

    void Update() //na ginei update poses zoes exeis (katw aristera)
    {
        if(levelEnding)
        {
            Movements.instance.transform.position += new Vector3(Movements.instance.boostSpeed * Time.deltaTime, 0f, 0f); //otan bgei to level complite na kini8ei se auti tin kateu8insi
        }

        if(Input.GetKeyDown(KeyCode.Escape) && canPause) //na kaloume to function pauseUnpause otan patame to ESC
        {
            PauseUnpause();
        }
    }

    public void KillPlayer()
    {
        currentLives--; //poses zoes exei akoma (na pigenei katw)
        UIManager.instance.livesText.text = "x " + currentLives; //otan pethanei na dei poses zoes exei akoma kai na tis gra4ei sto UI (TEXT)

        if (currentLives > 0) //BUGFIX respawn >
        {
            //Respawn
            StartCoroutine(RespawnCounter()); //respawn
        }
        else
        {
            //Game Over
            UIManager.instance.gameOverScreen.SetActive(true);  //na ginei active to GameOverScreen
            WaveManager.instance.canSpawnWave = false;          //na stamatisei na spawnarei waves

            MusicController.instance.PlayGameOver(); //na pe3ei to gameOverMusic otan xasis
            PlayerPrefs.SetInt("High Score", highScore); //na dei apo ta arxia tou pexti pio einai to pio megalo highscore (na ginei save to highscore)

            canPause = false; //na min mporei na ani3ei to pauseMenu sto GameOverScene
        }
    }

    public IEnumerator RespawnCounter() //respawn timer - diaforetiko me ton pragmatiko xrono (ingame time)
    {
        yield return new WaitForSeconds(respawnTime);  //na perimenei mexri to epomeno respawn
        Health.instance.Respawn();

        WaveManager.instance.ContinueSpawning(); //na sinexisei na spawnarei ta waves 
    }

    public void AddScore(int scoreToAdd) //add score
    {
        currentScore += scoreToAdd; //na anebenei to score
        levelScore += scoreToAdd; //gia na pros8e8ei to score apo ta proigoumena epipeda sto epomeno level
        UIManager.instance.scoreText.text = "Score: " + currentScore; // na grafei to score

        if(currentScore > highScore) //highscore - na grapsi to kainourio highscore an einai megalitero apo to palio
        {
            highScore = currentScore;
            UIManager.instance.hiScoreText.text = "HighScore: " + highScore;
            //PlayerPrefs.SetInt("High Score", highScore); //na dei apo ta arxia tou pexti pio einai to pio megalo highscore
        }
    }

    public IEnumerator EndLevelCo() //CoRoutine
    {
        UIManager.instance.levelEndScreen.SetActive(true); //na ani3ei to endlevel screen
        Movements.instance.stopMovement = true; //na stamatisi na kinite to spaceship meta to endscreen
        levelEnding = true; //telionei to level
        MusicController.instance.PlayVictory(); // na mpei to VictoryMusic
        canPause = false; //na min mporei na ani3ei to pause menu sto EndScene
        yield return new WaitForSeconds(.5f); //na enfanizete to endlevel otan telionei ena lv
        UIManager.instance.endLevelScore.text = "Level Score: " + levelScore; //na ginete upgrade to endlevelscore
        UIManager.instance.endLevelScore.gameObject.SetActive(true); //gia na to energopiisoume
        yield return new WaitForSeconds(.5f); //na enfanizete to endlevel otan telionei ena lv

        PlayerPrefs.SetInt("CurrentScore", currentScore); //curret score
        UIManager.instance.endCurrentScore.text = "Total Score: " + currentScore; //na ginete update sinexos to current score
        UIManager.instance.endCurrentScore.gameObject.SetActive(true); //gia na to energopiisoume

        if(currentScore == highScore) //an ginei high score
        {
            yield return new WaitForSeconds(.5f);
            UIManager.instance.highScoreNotice.SetActive(true); //na energopiithe to high score text
        }

        PlayerPrefs.SetInt("High Score", highScore); //na dei apo ta arxia tou pexti pio einai to pio megalo highscore
        PlayerPrefs.SetInt("Current Lives: ", currentLives); //na sinexeisei me tis zoes p exei

        yield return new WaitForSeconds(waitForLevelEnd); //na perimenei gia to epomeno epipedo 5sec

        SceneManager.LoadScene(nextLevel); //na mpei to epomeno epipedo
    }

    public void PauseUnpause() //pause unpause
    {
        if(UIManager.instance.pauseScreen.activeInHierarchy) //
        {
            UIManager.instance.pauseScreen.SetActive(false); //otan einai anikti na tin klisei
            Time.timeScale = 1f; //na 3ekinisei na kinite meta to pause (ta objects)
            Movements.instance.stopMovement = false; // na 3ekinisei na kinite o Player (SpaceShip)
        } else
        {
            UIManager.instance.pauseScreen.SetActive(true); //otan einai klisti na tin ani3ei
            Time.timeScale = 0f; //gia na pagonei o xronos, na stamatisoun ta objects na kinounte
            Movements.instance.stopMovement = true; // na stamatisi na kinite o Player (SpaceShip)
        }
    }
}