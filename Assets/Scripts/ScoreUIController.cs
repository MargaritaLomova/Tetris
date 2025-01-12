using TMPro;
using UnityEngine;

public class ScoreUIController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text recordText;

    public void UpdateScore(int value)
    {
        scoreText.text = $"{value}";
    }

    public void UpdateRecord(int value)
    {
        recordText.text = $"{value}";
    }
}