using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    Vector3 rotAxis;
    float rotSpeed;

    private void Start()
    {
        rotAxis = Random.insideUnitSphere;
        rotSpeed = Random.Range(60f, 180f);
    }

    private void Update()
    {
        // ȸ�� ������ ���� ���� (���� �� ȸ�� �ӵ��� ���� ����)
        transform.Rotate(rotAxis * rotSpeed * Time.deltaTime);
    }
}
