using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform playerTr;
    public float speed;

    private void Update()
    {
        //Vector3 direction = playerTr.position - transform.position;
        //direction.Normalize();  // direction = direction.normalized;
        //transform.position += direction * speed * Time.deltaTime;

        transform.LookAt(playerTr.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
