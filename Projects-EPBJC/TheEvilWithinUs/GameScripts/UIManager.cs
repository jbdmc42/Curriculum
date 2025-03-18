using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject ExitButton;
    public GameObject MainMenuButton;
    public GameObject canvasHUD;
    public GameObject canvasDeath;
    public GameObject canvasScore;
    public GameObject scopeUI;
    public GameObject respawnPoint;
    public PointSystem pointSystem;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI restartText;
    public PlayerHealth playerHealth;
    public PopUpSystem popUpSystem;
    public Image[] lifeImages;
    public bool isRestarted = false;
    public GameObject canvasPause;
    public bool isUIVisible = true;

    // public GameObject infoPanel;
    // public GameObject player;

    private CharacterController controller;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        canvasPause.SetActive(false);
        canvasHUD.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        GetReferences();
        Canvas();
        Pause();
        UIToggle();
    }
    private void GetReferences()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Canvas()
    {
        if (playerHealth.IsDead == true)
        {
            canvasDeath.SetActive(true);
            canvasHUD.SetActive(false);
            scopeUI.SetActive(false);
            canvasPause.SetActive(false);
        }
        else if (popUpSystem.PlayerWon == true)
        {
            canvasScore.SetActive(true);
            canvasDeath.SetActive(false);
            canvasHUD.SetActive(false);
            scopeUI.SetActive(false);
            canvasPause.SetActive(false);
        }
        else
        {
            if (scopeUI.activeSelf == false && isUIVisible == true)
            {
                canvasHUD.SetActive(true);
            }
            else
            {
                canvasHUD.SetActive(false);
            }
            canvasDeath.SetActive(false);
            canvasScore.SetActive(false);
            canvasPause.SetActive(false);

            if (playerHealth.lifes == 1)
            {
                titleText.SetText("GAME OVER");
                restartText.SetText("Restart");
            }
        }
    }
    public void Respawn()
    {
        if (playerHealth.lifes > 1)
        {
            controller.enabled = false;
            transform.position = respawnPoint.transform.position;
            transform.rotation = respawnPoint.transform.rotation;
            controller.enabled = true;
            playerHealth.CurrentHealth(playerHealth.maxHealth);

            lifeImages[playerHealth.lifes - 1].gameObject.SetActive(false);

            pointSystem.MinusPoints(1000);
            playerHealth.lifes--;
        }
    }
    public void Restart()
    {
        isRestarted = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void RespawnAndRestart()
    {
        if (playerHealth.lifes == 1)
        {
            Restart();
        }
        else
        {
            Respawn();
        }
    }
    public void PressMainMenu()
    {
        isRestarted = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public bool IsRestarted
    {
        get { return isRestarted; }
    }
    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                isPaused = false;
            } 
            else
            {
                isPaused = true;
            }
        }
        if (isPaused) 
        {
            PauseGame();
        }
        else if (isPaused == false && canvasDeath.activeSelf == false && canvasScore.activeSelf == false && scopeUI.activeSelf == false && isUIVisible == true)
        {
            ResumeGame();
        }
    }
    public void ResumeGame()
    {
        isPaused = false;

        canvasPause.SetActive(false);

        canvasHUD.SetActive(true);

        Time.timeScale = 1f;
    }
    public void PauseGame()
    {
        canvasPause.SetActive(true);

        canvasHUD.SetActive(false);

        Time.timeScale = 0f;
    }
    public bool IsPaused
    {
        get { return isPaused; }
    }
    public void UIToggle()
    {
        if (Input.GetKeyDown(KeyCode.F1) && isPaused == false)
        {
            if (canvasHUD.activeSelf)
            {
                canvasHUD.SetActive(false);

                isUIVisible = false;
            }
            else
            {
                canvasHUD.SetActive(true);

                isUIVisible = true;
            }
        }
    }
}
