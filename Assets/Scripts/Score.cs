using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI ScoreText;
    public static  int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public static void AddScore(int newScore)
    {
        score += newScore;
    }

    public void UpdateScore()
    {
        ScoreText.text = "$: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }
}
