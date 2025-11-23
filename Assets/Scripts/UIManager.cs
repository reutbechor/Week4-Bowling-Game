using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI resultText;

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void ShowResult(string msg)
    {
        resultText.text = msg;
        resultText.gameObject.SetActive(true);
    }
}
