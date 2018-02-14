using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
   public static bool GameIsPaused = false;

   public GameObject pauseMenuUI;

   public GameObject pauseMenuBackground;

   private void Start()
   {
      Resume();
   }

   void Update ()
   {
		if (Input.GetKeyDown(KeyCode.Escape))
      {
         if (GameIsPaused)
         {
            Resume();
         }
         else
         {
            Pause();
         }
      }
	}

   public void Resume()
   {
      pauseMenuUI.SetActive(false);
      pauseMenuBackground.SetActive(false);
      Time.timeScale = 1f;
      GameIsPaused = false;
   }

   private void Pause()
   {
      pauseMenuUI.SetActive(true);
      pauseMenuBackground.SetActive(true);
      Time.timeScale = 0f;
      GameIsPaused = true;
   }

   public void LoadMenu()
   {
      Time.timeScale = 1f;
      SceneManager.LoadScene("MainMenu");
   }
}
