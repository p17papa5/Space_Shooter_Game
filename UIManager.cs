using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject gameOverScreen; //gameover screen

    public Text livesText; //live UI

    public Slider HealthBar, shieldBar; //healthbar , shieldbar

    public Text scoreText, hiScoreText; //score , high score

    public GameObject levelEndScreen; // end Screen

    public Text endLevelScore, endCurrentScore; //endscene score 
    public GameObject highScoreNotice; //endscene highscore

    public GameObject pauseScreen; //pause screen

    public string mainMenuName = "MainMenu"; //na blepoume pio epipedo einai energopiimeno

    public Slider bossHealthSlider; //boss Slider
    public Text bossName; // onoma tou boss

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart() //restart button
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //8a kanei restart to idio epipedo, blepei pio einai to scene kai to bazei 3ana apo tin arxi
        Time.timeScale = 1f; //na sinexisei o xronos meta to restast
    }

    public void QuitToMainManu() //main menu button
    {
        SceneManager.LoadScene(mainMenuName); //na kanei load to mainmenu scene
        Time.timeScale = 1f; //na sinexisei na pezei to game (xronos)
    }

    public void Resume()
    {
        GameManager.instance.PauseUnpause(); // na kanei pause - Unpause me to Resume button
    }
}
