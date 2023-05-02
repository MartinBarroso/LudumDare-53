using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI highScore;


    void Start()
    {

    }

    void OnEnable()
    {
        int finalScore = Score.score;
        var highestScore = PlayerPrefs.GetFloat("HighScore");

        if (finalScore > highestScore)
        {
            highestScore = finalScore;
            PlayerPrefs.SetFloat("HighScore", (float)finalScore);
        }

        score.text = "Score: " + finalScore;
        highScore.text = "HighScore: " + highestScore;
    }

#if UNITY_ANDROID
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMenu();
        }
    }
#endif

}
