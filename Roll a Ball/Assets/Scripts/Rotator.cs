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
        // 회전 방향을 랜덤 적용 (적용 후 회전 속도도 랜덤 적용)
        transform.Rotate(rotAxis * rotSpeed * Time.deltaTime);
    }
}
