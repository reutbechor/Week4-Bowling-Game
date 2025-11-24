using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBehaviour : MonoBehaviour
{
    private Vector3 startPos;      // ������ ������� �� ����
    private Quaternion startRot;   // ������ �������
    private Rigidbody2D rb;        // ���������� �� ����

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // ������ �� ������ ������� ��������� �� ����
        startPos = transform.position;
        startRot = transform.rotation;
    }

    // ���� �� ���� ������/������ (����� �� ������� ����� ���� �� ��)
    public void SetKinematic(bool value)
    {
        rb.bodyType = value ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;

        // �������� ���� ������ � ������ ������ ������
        if (value)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    // ����� �� ���� ���� ������� ���
    public void ResetPin()
    {
        transform.position = startPos;
        transform.rotation = startRot;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    // ���� �� ���� "���" (��� ������ �����)
    public bool IsFallen()
    {
        // ������ �� ����� �-Z ������� ���� ����� 0-180
        float angle = Mathf.Abs(transform.eulerAngles.z);
        if (angle > 180f)
        {
            angle = 360f - angle;
        }

        // �� ������ ����� �-20 ����� - ���� ������
        return angle > 20f;
    }
}