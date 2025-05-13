using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Declarations

    public static event Action OnGameOver;
    public static event Action OnWinScreen;

    [Header("References")]
    private bool isPause = false;

    [Header("Game Objects")]
    private Corn corn;
    private UIManager uiManager;
    private Player player;
    [SerializeField] private GameObject cornPrefab;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject gameUi;

    #endregion

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        uiManager = UIManager.instance;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
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
        pauseMenu.SetActive(false);
        gameUi.SetActive(true);
        player.enabled = true;
        Time.timeScale = 1f;
        isPause = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        gameUi.SetActive(false);
        player.enabled = false;
        Time.timeScale = 0f;
        isPause = true;
    }
    public void Restart()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
        gameOverScreen.SetActive(true);
        gameUi.SetActive(false);
        player.enabled = false;
        Time.timeScale = 0f;
        isPause = true;
    }

    public void WinScreen()
    {
        OnWinScreen?.Invoke();
        winScreen.SetActive(true);
        player.enabled = false;
        Time.timeScale = 0f;
        isPause = true;
    }

    private void OnEnable()
    {
        Fox.OnFoxDied += HandleFoxDeath;
    }

    private void OnDisable()
    {
        Fox.OnFoxDied -= HandleFoxDeath;
    }

    private void HandleFoxDeath(Fox fox)
    {
        Debug.Log("dinner time!");
    }
}
