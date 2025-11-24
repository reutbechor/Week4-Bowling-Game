using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PinsManager : MonoBehaviour
{


    public float checkDelay = 2.5f;       // ��� ����� ���� �����
    public BallController ball;
    public TMP_Text scoreText;

    private List<Pin> pins = new List<Pin>();

    private int turn = 1;                 // ��� 1 ��� ��� 2
    private int totalKnocked = 0;         // ����� ������ �� �� ������ �����

    public TMP_Text resultText;

    void Start()
    {
        // ������ �� �� ������ ������ �����
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

        // ����� �� ���
        foreach (var p in pins)
        {
            if (p == null) continue;

            if (p.IsKnockedDown())
            {
                fallenThisTurn++;
                Destroy(p.gameObject);          // ? ������ ������ ����� �����
            }
            else
            {
                standing.Add(p);               // ? �� ������ ������ ������ ������
            }
        }

        totalKnocked += fallenThisTurn;
        scoreText.text = "Pins knocked: " + totalKnocked;





        pins = standing;   //  ��� ������ �� ������ �����

        // �� �� ��� ���� ������ - ������ ��� ���
        if (turn == 1)
        {
            turn = 2;
            ResetStandingPins();
            ball.ResetBall();
            yield break;
        }

        // �� �� ��� ���� ���� ? ���� ����
        EndGame();
    }

    // ����� �� ������ ����� (������)
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
        // ������ �� ������� �� ����� �������
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        // �� �� ��� ���� ���� � ���� ����
        if (currentIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0); // ���� �-Level1
        }
    }

    void RestartLevel()
    {
        // ���� ���� �� ���� ������
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
