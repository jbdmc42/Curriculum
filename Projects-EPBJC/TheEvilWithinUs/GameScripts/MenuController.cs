using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // declarar as variáveis de paineis
    public GameObject settingsPanel;
    public GameObject settings1;
    public GameObject settings2;
    public GameObject settings3;
    public GameObject creditsPanel;
    public GameObject settingsPanels;

    // declarar as variáveis de botões
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

    // iniciar a cutscene ou transição
    public void PressPlay()
    {
        // Iniciar a cutscene
        // Aqui você iniciaria a cutscene, e depois iniciaria o jogo
        StartGame();
    }

    // ativar o menu de confirmação
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

    // desativar o menu de confirmação
    public void PressNo()
    {
       
    }

    // ativar o menu de definições
    public void PressSettings()
    {
        settingsPanel.SetActive(true);
        settings1.SetActive(true);
        settings2.SetActive(false);
        settings3.SetActive(false);
       
    }

    // desativar o menu de definições
    public void PressCloseSettings()
    {
        
        settingsPanel.SetActive(false);
    }

    // ativar o menu de créditos
    public void PressCredits()
    {
        creditsPanel.SetActive(true);
        
    }

    // desativar o menu de créditos
    public void PressCloseCredits()
    {
        creditsPanel.SetActive(false);
    }

    // ativar a tab de definições 1
    public void PressTab1()
    {
        settings1.SetActive(true);
        settings2.SetActive(false);
        settings3.SetActive(false);
    }

    // ativar a tab de definições 2
    public void PressTab2()
    {
        settings1.SetActive(false);
        settings2.SetActive(true);
        settings3.SetActive(false);
    }

    // ativar a tab de definições 3
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

