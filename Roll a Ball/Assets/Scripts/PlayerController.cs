using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;

    private int count;
    public Text countText;
    public Text winText;

    private int score;
    public Text scoreText;

    private float runningTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetScoreText();
        SetCountText();
        winText.text = "";
    }

    private void Update()
    {
        //runningTime += Time.deltaTime;
        //Debug.Log(runningTime);

        //Debug.Log(Time.time);
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(movement * speed);

        // movement.magnitude : movement ∫§≈Õ¿« ≈©±‚
        // movement.normalized : movement ∫§≈Õ¿« ¡§±‘»≠, movement ∫§≈Õ ∫Ø»≠X
        // movement.Normalize() : movement ∫§≈Õ¿« ¡§±‘»≠, movement ∫§≈Õ ∫Ø»≠O
        // Vector3.up = new Vector3(0, 1, 0)
        // Vector3.zero = new Vector3(0, 0, 0)
        // Vector3.one = new Vector3(1, 1, 1)
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            PickUpController pc = other.GetComponent<PickUpController>();
            score += pc.score;
            SetScoreText();

            ++count;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            winText.text = "Game Over!";
            Time.timeScale = 0;
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score : " + score.ToString();
    }

    void SetCountText()
    {
        countText.text = "Count : " + count.ToString();
        if (count >= 19)
        {
            winText.text = "You Win!";
            SceneManager.LoadScene("MiniGame2");
        }
    }
}
