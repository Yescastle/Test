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

    // ���� ���� 1. ���� ǥ�� �ؽ�Ʈ �߰�
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

        // ���ͷ� �� ��� �ּ�ȭ�� ���� �Ⱦ� ���� ���� �ڵ�
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
        pickupCnt = pickups.Length;

        SetCountText();
        SetScoreText();
    }

    private void FixedUpdate()
    {
        // �÷��̾� �̵�
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);                        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��� � ȹ�� ��
        if (collision.CompareTag("Pickup"))
        {
            Destroy(collision.gameObject);
            count++;
            SetCountText();
            score += 10;                    // ���� ���� 2. �Ⱦ� �ϳ� �� 10������ ���
            SetScoreText();
        }

        // ���� � ȹ�� ��
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
        // ���� ���� 3. ���� �ε��� ������ 5�� ����
        if (collision.gameObject.name == "Background")
        {
            score -= 5;
            SetScoreText();

            collision.collider.enabled = false;                     // ������Ʈ Ȱ��ȭ�� ��Ȱ��ȭ
            collisionCollider = collision.collider;
            Invoke("SetEnableCollider", 1f);
        }

        // �� ���� �ִ� ��� � ȹ�� ��(���ÿ� ������Ʈ)
        if (collision.gameObject.name == "PickDeath")
        {
            gameObject.SetActive(false);
            winText.color = new Color(0.5f, 0, 0.5f);
            winText.text = "You are FISHED!!! \n HAHAHAHAHAHAH";
        }
    }

    void SetEnableCollider()
    {
        collisionCollider.enabled = true;                           // ��, �̷��� �ڵ带 ¥�� 1�� �̳� �ٸ� �ݶ��̴��� �浹���� �� ó�� �ε����� �ݶ��̴��� ���� �� ������ ���װ� �ִ�.
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
            // ���� ���� 4. ���� 0�� �̸� �� ���� ����
            winText.color = new Color(0.5f, 0, 0);        // ����Ƽ ���� 255�� ���⼭�� 1�̴�.
            winText.text = "Game Over!";
            scoreText.text = "Score : 0";

            // ���� ���� �� �� ������Ʈ ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
    }
}

