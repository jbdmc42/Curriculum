using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Animations.Rigging;
using Unity.VisualScripting;

public class PointSystem : MonoBehaviour
{
    public float totalPoints = 0;
    public float growthRate = 50f;
    public PopUpSystem popUpSystem;
    public AudioManager audioManager;
    public Image[] stars;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText2;
    public TextMeshProUGUI timeText;

    private float enlargeScale = 1.5f;
    private float shrinkScale = 1f;
    private float enlargeDuration = 0.25f;
    private float shrinkDuration = 0.25f;
    private float growthScore = 0;
    private float time = 0;
    private float gameTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        StarsInicialScale();
    }

    // Update is called once per frame
    void Update()
    {
        gameTime = Time.deltaTime;
        scoreText.SetText(growthScore + "");
        scoreText2.SetText("Score: " + growthScore);
        timeText.SetText("Time: " + gameTime);

        if (popUpSystem.PlayerWon == true)
        {
            if (growthScore != totalPoints && totalPoints > growthScore)
            {
                growthScore += growthRate;

                if (growthScore == growthRate)
                {
                    audioManager.Play("score");
                }

                else if(growthScore == totalPoints)
                {
                    audioManager.Stop("score");
                }

                if (growthScore == 3000)
                {
                    audioManager.Play("star");
                    StartCoroutine(EnlargeAndShrinkStar(stars[0]));
                }

                if (growthScore == 6000)
                {
                    audioManager.Play("star");
                    StartCoroutine(EnlargeAndShrinkStar(stars[1]));
                }

                if (growthScore == 10000)
                {
                    audioManager.Play("star");
                    StartCoroutine(EnlargeAndShrinkStar(stars[2]));
                }
            }
        }

        Score();
    }
    private void Score()
    {
        time += Time.deltaTime;

        if (time >= 60f)
        {
            totalPoints -= 150f;
            time = 0f;
        }
    }
    public void ScorePoints(float points)
    {
        totalPoints += points;
    }
    public void MinusPoints(float points)
    {
        totalPoints -= points;
    }
    private IEnumerator EnlargeAndShrinkStar(Image star)
    {
        yield return StartCoroutine(ChangeStarScale(star, enlargeScale, enlargeDuration));

        yield return StartCoroutine(ChangeStarScale(star, shrinkScale, shrinkDuration));
    }
    private IEnumerator ChangeStarScale(Image star, float targetScale, float duration)
    {
        Vector3 initialScale = star.transform.localScale;
        Vector3 finalScale = new Vector3(targetScale, targetScale, targetScale);

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            star.transform.localScale = Vector3.Lerp(initialScale, finalScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        star.transform.localScale = finalScale;
    }
    private void StarsInicialScale()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].transform.localScale = Vector3.zero;
        }
    }
    public float TotalPoints
    {
        get { return totalPoints; }
    }
}
