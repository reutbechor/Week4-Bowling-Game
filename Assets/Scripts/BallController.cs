using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector3 dragStartPos;
    private bool isDragging = false;

    private Vector3 startPos;   // ����� ������ �� �����

    public PinsManager pinsManager; // ����� ����� ������

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;   // *** ���� ��� ��� ���� Null ***
    }

    void OnMouseDown()
    {
        isDragging = true;
        dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragStartPos.z = 0;
    }

    void OnMouseUp()
    {
        if (!isDragging) return;

        isDragging = false;

        Vector3 dragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragEndPos.z = 0;

        // ��� ��� ����� �� ������
        Vector2 force = (dragStartPos - dragEndPos) * 350f;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.AddForce(force);

        // ������� ����� ������ ������ ����
        if (pinsManager != null)
        {
            pinsManager.OnBallThrown();
        }
    }

    // ����� ����� �������
    public void ResetBall()
    {
        transform.position = startPos;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}
