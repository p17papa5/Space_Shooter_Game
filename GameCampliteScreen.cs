using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCampliteScreen : MonoBehaviour
{
    public float timeBetweenText; //xronos meta3i ton text

    public bool canExit; // mporei na kanei exit

    public string mainManuName = "MainMenu"; //na paei sto mainmenu

    public Text message, score, pressKey;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowTextCO()); // na 3ekinisei to Coroutine
    }

    // Update is called once per frame
    void Update()
    {
        if(canExit && Input.anyKeyDown) // an patisis kapio koumpi 
        {
            SceneManager.LoadScene(mainManuName); // na paei sto mainmanu
        }
    }

    public IEnumerator ShowTextCO() // CoRutin gia na bgenoun ta text sto endscreen
    {
        yield return new WaitForSeconds(timeBetweenText); // na bgei to 1o text (you beat the game)
        message.gameObject.SetActive(true); // na to 3ekinisei
        yield return new WaitForSeconds(timeBetweenText); // na bgei to 1o text (message)
        score.text = "Final Score: " + PlayerPrefs.GetInt("CurrentScore"); // na enfanistei sto Final Score to Current Score
        message.gameObject.SetActive(true); // na to 3ekinisei
        yield return new WaitForSeconds(timeBetweenText);  // na bgei to 1o text (Score)
        pressKey.gameObject.SetActive(true); // na to 3ekinisei
        canExit = true; // na mporei na bgei e3w (mainmanu)
    }
}
