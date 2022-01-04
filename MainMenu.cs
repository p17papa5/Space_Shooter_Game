using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("Current Lives", 3); // na 3ekinisei to game me 3 lifes
        PlayerPrefs.SetInt("CurrentScore", 0); //na 3ekinisei to game me 0 score

        SceneManager.LoadScene(firstLevel); // na kanei load to 1o level
    }

    public void QuitGame()
    {
        Application.Quit(); //na ginei quit apo to game
    }    
}
