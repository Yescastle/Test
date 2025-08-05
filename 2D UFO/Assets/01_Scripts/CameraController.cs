using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;


    private void Start()
    {
        // 카메라 벡터 값 지정
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
