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
    public enum Screens { Pause, GameOver, Win, GameUi }
    private Dictionary<Screens, GameObject> organize;

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

        organize = new Dictionary<Screens, GameObject>();
        organize.Add(Screens.Pause, pauseMenu);
        organize.Add(Screens.GameOver, gameOverScreen);
        organize.Add(Screens.Win, winScreen);
        organize.Add(Screens.GameUi, gameUi);
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
        ShowPanel(Screens.GameUi);
        player.enabled = true;
        Time.timeScale = 1f;
        isPause = false;
    }

    public void Pause()
    {
        ShowPanel(Screens.Pause);
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
        ShowPanel(Screens.GameOver);
        gameUi.SetActive(false);
        player.enabled = false;
        Time.timeScale = 0f;
        isPause = true;
    }

    public void WinScreen()
    {
        OnWinScreen?.Invoke();
        ShowPanel(Screens.Win);
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

    public void ShowPanel(Screens panelType)
    {
        foreach (var panel in organize.Values)
        {
            if (panel != null) panel.SetActive(false);
        }
        if (organize.ContainsKey(panelType) && organize[panelType] != null)
        {
            organize[panelType].SetActive(true);
        }
    }

}
