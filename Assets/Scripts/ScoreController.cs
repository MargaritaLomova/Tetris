using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private int record;
    private int currentScore;

    private ScoreUIController scoreUI;

    private void Awake()
    {
        scoreUI = FindObjectOfType<ScoreUIController>();

        record = PlayerPrefs.GetInt("Record");

        ResetCurrentScore();

        scoreUI.UpdateScore(currentScore);
        scoreUI.UpdateRecord(record);
    }

    public void AddPoints(int pointsCount)
    {
        currentScore += pointsCount;
        if(currentScore > 999999)
        {
            currentScore = 999999;
        }
        scoreUI.UpdateScore(currentScore);

        if (currentScore > record)
        {
            record = currentScore;
            PlayerPrefs.SetInt("Record", record);
            scoreUI.UpdateRecord(record);
        }
    }

    public void ResetCurrentScore()
    {
        currentScore = 0;
        scoreUI.UpdateScore(currentScore);
    }
}