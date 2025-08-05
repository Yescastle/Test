using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);           // 업데이트 메서드에서는 델타타임을 꼭!!!!!!!!! 곱해줘야 한다.
    }
}
