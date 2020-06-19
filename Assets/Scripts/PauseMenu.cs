using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    //public : accessible from other scripts
    //static : we just want to check if the game is currently paused, we don't want to reference this specific PauseMenu script
    //bool : true or false :: Is the game Paused or Not?
    //false by default, our game isn't paused by default

    public GameObject pauseMenuUI;
    //This GameObject will reffer to our PauseMenu Canvas

    // Update is called once per frame
    
    private void OnEnable()
    {
        //we're setting up each control from our InputSystem;
        var playerController = new PlayerController();
        playerController.Enable();
        playerController.Main.Pause.performed += PauseOnPerformed;
    }

    private void PauseOnPerformed(InputAction.CallbackContext obj)
    {
            if (GameIsPaused)
            {
                Resume();
                //Pressing the Pause Key (here "P") when the game is already paused will automatically Resume it;
            } else
            {
                Pause();
                //Pressing the Pause Key when the game is running will Pause the game;
            }
    }

    
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.P) || (Input.GetKeyDown(KeyCode.Escape)))
        {
            if (GameIsPaused)
            {
                Resume();
                //Pressing the Pause Key (here "P") when the game is already paused will automatically Resume it;
            } else
            {
                Pause();
                //Pressing the Pause Key when the game is running will Pause the game;
            }
        }*/
    }


    public void Resume()
    //We want take away PauseMenu
    //We want to set time back to normal
    //We want to put GameIsPaused to false
    {
        pauseMenuUI.SetActive(false);
        //Takes away PauseMenu

        Time.timeScale = 1f;
        //We set the timespeed at its default value

        GameIsPaused = false;
    }

    void Pause()
    //We want to bring up PauseMenu
    //We want to freeze time in our game
    //We want to put GameIsPaused to true
    {
        pauseMenuUI.SetActive(true);
        //Enables our Game Object, here the whole PauseMenu Canvas

        Time.timeScale = 0f;
        //Time.timeScale = the speed at which time is passing
        //Can be used to create slowmotion effects
        //Set to 0 in order to freeze the game

        GameIsPaused = true;
    }

        public void MainMenu()
    {
        //this function will allow us to open the Menu scene;
                Time.timeScale = 1f;
                SceneManager.LoadScene("Menu_A");
    }
}
