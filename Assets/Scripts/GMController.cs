using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GMController : MonoBehaviour
{
    [SerializeField]
    Text scoreText;

    private int score = 0;

    private void Start()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void IncrementScore(int value = 1)
    {
        score += value;
        scoreText.text = "Score: " + score.ToString();
    }
}
