using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Config : MonoBehaviour
{
    #region Local Variables

    public static bool IsDead;
    private static StartupType Startup;

    #endregion

    #region Components

    public AudioSource Music;
    public GameObject UICanvas;
    public GameObject StartCanvas;
    public GameObject PausePanel;
    public TextMeshProUGUI Score;

    public GameObject GameObj;
    public GameObject GroundObj;
    public GameObject PlayerObj;

    #endregion

    #region Enums

    private enum StartupType
    {
        Start,
        Restart
    }

    #endregion

    #region OnSceneStart

    void Start()
    {
        StartCanvas.SetActive(true);

        // In Case the Startup Type is Restart
        if (Startup == StartupType.Restart)
        {
            // Resets the Startup Type
            Startup = StartupType.Start;
            StartGame();
        }
    }

    #endregion

    #region Methods

    public void GameOver()
    {
        PausePanel.SetActive(true);
        UICanvas.SetActive(false);
        IsDead = true;
        Music.Stop();
        Cursor.visible = true;
        Score.text = $"Score: {Player.Score}";
    }

    public void Restart()
    {
        // Sets the Startup Type
        Startup = StartupType.Restart;
        // Loads the Game Scene (Resets Position and Instantiated Platforms)
        SceneManager.LoadScene("In-Game");
    }

    public void Quit()
    {
        // Closes the Application
        Application.Quit();
    }

    public void StartGame()
    {
        Music.Play();
        StartCanvas.SetActive(false);
        UICanvas.SetActive(true);
        PausePanel.SetActive(false);
        IsDead = false;
        Cursor.visible = false;
        // Resets the Player's Score
        Player.Score = 0;

        // Activates Scripts
        GameObj.GetComponent<MapGeneration>().enabled = true;
        GroundObj.GetComponent<Ground>().enabled = true;
        PlayerObj.GetComponent<Player>().enabled = true;
    }

    #endregion
}
