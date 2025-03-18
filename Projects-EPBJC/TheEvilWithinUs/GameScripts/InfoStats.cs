using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoStats : MonoBehaviour
{
    public PointSystem pointSystem;
    public PopUpCarPart popUpCarPart;
    public TextMeshProUGUI infoScoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI killText;
    public TextMeshProUGUI carPartsText;
    public GameObject title;

    private int killCounter = 0;
    private float gameTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        Toogle();
    }

    // Update is called once per frame
    void Update()
    {
        Stats();
        Toogle();
    }
    private void Toogle()
    {
        if (Input.GetKey(KeyCode.T))
        {
            timeText.gameObject.SetActive(true);

            infoScoreText.gameObject.SetActive(true);

            killText.gameObject.SetActive(true);

            carPartsText.gameObject.SetActive(true);

            title.gameObject.SetActive(true);
        }
        else
        {
            timeText.gameObject.SetActive(false);

            infoScoreText.gameObject.SetActive(false);

            killText.gameObject.SetActive(false);

            carPartsText.gameObject.SetActive(false);

            title.gameObject.SetActive(false);
        }
    }
    private void Stats()
    {
        gameTime += Time.deltaTime;

        timeText.SetText("Time: " + (int)gameTime + "s");

        infoScoreText.SetText("Score: " + pointSystem.TotalPoints);

        killText.SetText("Kills: " + killCounter);

        carPartsText.SetText("Car Parts: " + popUpCarPart.TotalCollected + "/7");
    }
    public void Kills(int kills)
    {
        killCounter += kills;
    }
}