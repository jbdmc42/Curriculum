using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // declarar as vari�veis de paineis
    public GameObject settingsPanel;
    public GameObject settings1;
    public GameObject settings2;
    public GameObject settings3;
    public GameObject creditsPanel;
    public GameObject settingsPanels;

    // declarar as vari�veis de bot�es
    public GameObject settingsB1;
    public GameObject settingsB2;
    public GameObject settingsB3;
    public GameObject closeSettings;
    public GameObject closeCredits;
    public GameObject settingsButton;
    public GameObject creditsButton;
    public GameObject playButton;
    public GameObject f4Button;

    // declarar o que fazer quando o jogo for iniciado
    private void Start()
    {
        settings2.SetActive(false);
        settings3.SetActive(false);
        creditsPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    // iniciar a cutscene ou transi��o
    public void PressPlay()
    {
        // Iniciar a cutscene
        // Aqui voc� iniciaria a cutscene, e depois iniciaria o jogo
        StartGame();
    }

    // ativar o menu de confirma��o
    public void PressQuit()
    {
        
        creditsPanel.SetActive(false);
    }

    // sair do jogo
    public void PressYes()
    {
        Debug.Log("Jogo desligado.");
        Application.Quit();
    }

    // desativar o menu de confirma��o
    public void PressNo()
    {
       
    }

    // ativar o menu de defini��es
    public void PressSettings()
    {
        settingsPanel.SetActive(true);
        settings1.SetActive(true);
        settings2.SetActive(false);
        settings3.SetActive(false);
       
    }

    // desativar o menu de defini��es
    public void PressCloseSettings()
    {
        
        settingsPanel.SetActive(false);
    }

    // ativar o menu de cr�ditos
    public void PressCredits()
    {
        creditsPanel.SetActive(true);
        
    }

    // desativar o menu de cr�ditos
    public void PressCloseCredits()
    {
        creditsPanel.SetActive(false);
    }

    // ativar a tab de defini��es 1
    public void PressTab1()
    {
        settings1.SetActive(true);
        settings2.SetActive(false);
        settings3.SetActive(false);
    }

    // ativar a tab de defini��es 2
    public void PressTab2()
    {
        settings1.SetActive(false);
        settings2.SetActive(true);
        settings3.SetActive(false);
    }

    // ativar a tab de defini��es 3
    public void PressTab3()
    {
        settings1.SetActive(false);
        settings2.SetActive(false);
        settings3.SetActive(true);
    }

    // iniciar o jogo
    public void StartGame()
    {
        // Iniciar o jogo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

