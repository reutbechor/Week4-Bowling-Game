using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PinsManager : MonoBehaviour
{


    public float checkDelay = 2.5f;       // זמן לחכות אחרי זריקה
    public BallController ball;
    public TMP_Text scoreText;

    private List<Pin> pins = new List<Pin>();

    private int turn = 1;                 // תור 1 ואז תור 2
    private int totalKnocked = 0;         // ספירה מצטברת של כל הפינים שנפלו

    public TMP_Text resultText;

    void Start()
    {
        // אוספים את כל הפינים בתחילת המשחק
        pins = new List<Pin>(FindObjectsOfType<Pin>());
        resultText.text = "";
    }

    public void OnBallThrown()
    {
        StartCoroutine(WaitAndCheckPins());
    }

    private IEnumerator WaitAndCheckPins()
    {
        yield return new WaitForSeconds(checkDelay);

        List<Pin> standing = new List<Pin>();
        int fallenThisTurn = 0;

        // בדיקה מי נפל
        foreach (var p in pins)
        {
            if (p == null) continue;

            if (p.IsKnockedDown())
            {
                fallenThisTurn++;
                Destroy(p.gameObject);          // ? מוחקים מהמשחק פינים שנפלו
            }
            else
            {
                standing.Add(p);               // ? רק הפינים שנותרו עומדים נשמרים
            }
        }

        totalKnocked += fallenThisTurn;
        scoreText.text = "Pins knocked: " + totalKnocked;





        pins = standing;   //  כעת נשארים רק הפינים ששרדו

        // אם זה היה התור הראשון - מכינים תור שני
        if (turn == 1)
        {
            turn = 2;
            ResetStandingPins();
            ball.ResetBall();
            yield break;
        }

        // אם זה היה התור השני ? סיום משחק
        EndGame();
    }

    // איפוס רק לפינים ששרדו (עומדים)
    void ResetStandingPins()
    {
        foreach (var p in pins)
        {
            if (p != null)
                p.ResetPin();
        }
    }

    void EndGame()
    {
        if (pins.Count == 0)
        {
            resultText.color = Color.green;
            resultText.text = "VICTORY!";
            Invoke("LoadNextLevel", 2f);
        }
        else
        {
            resultText.color = Color.red;
            resultText.text = "DEFEAT!";
            Invoke("RestartLevel", 2f);
        }
    }
    void LoadNextLevel()
{
    // לוקחים את האינדקס של הסצנה הנוכחית
    int currentIndex = SceneManager.GetActiveScene().buildIndex;

    // אם יש עוד סצנה אחרי – נטען אותה
    if (currentIndex + 1 < SceneManager.sceneCountInBuildSettings)
    {
        SceneManager.LoadScene(currentIndex + 1);
    }
    else
    {
        SceneManager.LoadScene(0); // חוזר ל-Level1
    }
}

void RestartLevel()
{
    // טוען מחדש את השלב הנוכחי
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
}
