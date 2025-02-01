using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 50f;
    public Rigidbody rb;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI winLoseText;
    public Image winLoseImage;
    public int health = 5;
    private int score = 0;

    void Start()
    {
        SetScoreText();
        SetHealthText();
        winLoseImage.gameObject.SetActive(false); // Masquer l'écran de fin au départ
    }

    void Update()
    {
        if (health <= 0)
        {
            EndGame(false);
        }

        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("menu");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            score++;
            SetScoreText();
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Trap"))
        {
            health--;
            SetHealthText();
        }
        if (other.CompareTag("Goal"))
        {
            EndGame(true);
        }
    }

    private void SetScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    private void SetHealthText()
    {
        healthText.text = "Health: " + health;
    }

    private void SetEndScreen(bool win)
    {
        winLoseImage.gameObject.SetActive(true);
        if (win)
        {
            winLoseText.text = "You Win!";
            winLoseText.color = Color.black;
            winLoseImage.color = Color.green;
        }
        else
        {
            winLoseText.text = "Game Over!";
            winLoseText.color = Color.white;
            winLoseImage.color = Color.red;
        }
    }

    private void EndGame(bool win)
    {
        SetEndScreen(win);
        StartCoroutine(LoadScene(1)); // Recharger après 3 secondes
    }
    #region Methods
        
    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(this.gameObject.scene.name);


    }
    #endregion
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontalInput, 0, verticalInput);
        rb.AddForce(move * speed);
    }
}
