using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb2d;

    private int count;
    public TMP_Text countText;

    // 연습 문제 1. 점수 표시 텍스트 추가
    private int score;
    public TMP_Text scoreText;

    public TMP_Text winText;

    private int pickupCnt;

    public BoxCollider2D eastWall;
    public BoxCollider2D westWall;
    public BoxCollider2D southWall;
    public BoxCollider2D northWall;

    private Collider2D collisionCollider;


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        score = 0;
        winText.text = "";

        // 리터럴 값 사용 최소화를 위한 픽업 개수 조절 코드
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
        pickupCnt = pickups.Length;

        SetCountText();
        SetScoreText();
    }

    private void FixedUpdate()
    {
        // 플레이어 이동
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);                        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 노란 운석 획득 시
        if (collision.CompareTag("Pickup"))
        {
            Destroy(collision.gameObject);
            count++;
            SetCountText();
            score += 10;                    // 연습 문제 2. 픽업 하나 당 10점으로 계산
            SetScoreText();
        }

        // 빨간 운석 획득 시
        if (collision.CompareTag("PickMinus"))
        {
            Destroy(collision.gameObject);
            score -= 8;
            SetCountText();
            SetScoreText();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 연습 문제 3. 벽에 부딪힐 때마다 5점 감소
        if (collision.gameObject.name == "Background")
        {
            score -= 5;
            SetScoreText();

            collision.collider.enabled = false;                     // 컴포넌트 활성화와 비활성화
            collisionCollider = collision.collider;
            Invoke("SetEnableCollider", 1f);
        }

        // 맨 위에 있는 노란 운석 획득 시(낚시용 오브젝트)
        if (collision.gameObject.name == "PickDeath")
        {
            gameObject.SetActive(false);
            winText.color = new Color(0.5f, 0, 0.5f);
            winText.text = "You are FISHED!!! \n HAHAHAHAHAHAH";
        }
    }

    void SetEnableCollider()
    {
        collisionCollider.enabled = true;                           // 단, 이렇게 코드를 짜면 1초 이내 다른 콜라이더에 충돌했을 때 처음 부딪혔던 콜라이더는 영영 안 켜지는 버그가 있다.
    }

    void SetCountText()
    {
        countText.text = "Count : " + count.ToString();
        if(count >= pickupCnt)
        {
            winText.text = $"Congraturations! \n Your Score is : {score}";

            Time.timeScale = 0;
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score : " + score.ToString();
        if (score < 0)
        {
            // 연습 문제 4. 점수 0점 미만 시 게임 오버
            winText.color = new Color(0.5f, 0, 0);        // 유니티 상의 255는 여기서는 1이다.
            winText.text = "Game Over!";
            scoreText.text = "Score : 0";

            // 점수 음수 될 때 오브젝트 비활성화
            gameObject.SetActive(false);
        }
    }
}

