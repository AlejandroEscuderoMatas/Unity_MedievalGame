using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    
    
    public GameObject      m_onGameUI;
    public GameObject      m_pauseUI;
    public GameObject      m_gameOverUI;
    
    private GameObject     m_currentUI;
    private bool           m_gamePaused = false;
    private bool           m_playerDead = true;
    
    //ON GAME UI
    public TextMeshProUGUI m_healthText;
    public TextMeshProUGUI m_killsText;
    public TextMeshProUGUI m_roundText;
    public TextMeshProUGUI m_timeText;
    
    
    //PAUSE UI
    public Button          m_continueButton;
    public Button          m_exitButtonPause;
    public TextMeshProUGUI m_healthTextPause;
    public TextMeshProUGUI m_powerText;

    //GAME OVER UI
    public Button          m_newGameButton;
    public Button          m_exitButtonGameOver;
    public TextMeshProUGUI m_killsTextGameOver;
    public TextMeshProUGUI m_roundTextGameOver;

    [SerializeField]
    private KeyCode        m_optionsKey = KeyCode.Escape;

    private void Start()
    {
        m_onGameUI.SetActive(true);
        m_pauseUI.SetActive(false);
        m_gameOverUI.SetActive(false);
        m_playerDead = false;
        m_currentUI = m_onGameUI;
        
        
    }

    private void Update()
    {
        if (m_playerDead)
        {
            ChangeUI(m_gameOverUI);
            Time.timeScale = 0;
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        if (Input.GetKeyDown(m_optionsKey) && !m_gamePaused && !m_playerDead)
        {
            ChangeUI(m_pauseUI);
            Time.timeScale = 0;
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            
            m_gamePaused = true;
        }
        else if (Input.GetKeyDown(m_optionsKey) && m_gamePaused && !m_playerDead)
        {
            ChangeUI(m_onGameUI);
            Time.timeScale = 1;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            m_gamePaused = false;
        }
    }

    public void ChangeUI(GameObject UI)
    {
        m_currentUI.SetActive(false);
        m_currentUI = UI;
        m_currentUI.SetActive(true);
    }

    public void Continue()
    {
        ChangeUI(m_onGameUI);
        Time.timeScale = 1;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        m_gamePaused = false;
        MusicManager.Instance.PlaySfxMusic(AppSounds.BUTTON_SFX);
    }

    public void RestartGame()
    {
        MusicManager.Instance.PlaySfxMusic(AppSounds.BUTTON_SFX);
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        MusicManager.Instance.PlaySfxMusic(AppSounds.BUTTON_SFX);
        Application.Quit();
    }

    public void setHealthText(float health)
    {
        m_healthText.text = health.ToString();
        m_healthTextPause.text = "Vitality: " + health;
    }

    public void setPowerText(int power)
    {
        m_powerText.text = "Power: " + power;
    }

    public void setKillsText(float kills)
    {
        m_killsText.text = kills.ToString();
        m_killsTextGameOver.text = "Kills: " + kills;
    }

    public void setRoundText(int round)
    {
        m_roundText.text = "Round " + round;
        m_roundTextGameOver.text = "Round: " + round;
    }

    public void setTimeText(int time)
    {
        m_timeText.text = time.ToString();
    }

    public void setPlayerDead(bool state)
    {
        m_playerDead = state;
    }
}