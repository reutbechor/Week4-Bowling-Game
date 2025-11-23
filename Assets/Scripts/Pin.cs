using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    public bool IsKnockedDown()
    {
        // �� ������ �� ���� ����� ��20 ����� � ��� ���
        float tilt = Quaternion.Angle(transform.rotation, startRot);

        return tilt > 20f;
    }

    public void ResetPin()
    {
        transform.position = startPos;
        transform.rotation = startRot;

        // ����� ������
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0;
        }
    }
}
