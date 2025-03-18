using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    // declarar variáveis 
    public Canvas StatsMenu;
    public bool StatsOnOff = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
                StatsOnOff = !StatsOnOff;
                StatsMenu.enabled = StatsOnOff;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Jogo desligado.");
        Application.Quit();
    }

    public void MainMenu()
    {
        // Fechar o jogo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
