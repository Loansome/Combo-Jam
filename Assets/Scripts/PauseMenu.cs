using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverMenuUI;
    public GameObject winMenuUI;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Scroll 1");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<AudioManager>().Play("Select");
            if (isPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play("Select");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void GameOver()
    {
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void Win()
    {
        winMenuUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void LoadMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        FindObjectOfType<AudioManager>().Stop("Scroll 1");
        FindObjectOfType<AudioManager>().Stop("Game Over");
        FindObjectOfType<AudioManager>().Stop("Win");
        FindObjectOfType<AudioManager>().Play("Select");
        SceneManager.LoadScene("Level 1");
    }
    public void QuitGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        FindObjectOfType<AudioManager>().Play("Select");
        Debug.Log("Quitting");
        SceneManager.LoadScene("Title Screen");
    }
}
