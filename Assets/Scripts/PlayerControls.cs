using System;
using UnityEngine;
using TMPro;

public class PlayerControls : MonoBehaviour
{
    public Rigidbody2D rb;
    public TMP_Text scoreText;

    private byte currentJumpCount = 0;
    private byte maxJumpCount = 2;
    private float baseSpeed = 3;
    private float speedIncrease = 0.001F;
    private float startingPosX;
    private string bestScore;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosX = transform.position.x;
        bestScore = PlayerPrefs.GetString("BestScore", "0");
        updateScoreText(score());
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.position.y < -7) 
        {
            setNewBestScoreIfAchieved();
            Application.LoadLevel(Application.loadedLevel); //TODO: obselete
        }

        rb.velocity = new Vector2(baseSpeed + speedIncrease, rb.velocity.y);

        if (Input.GetMouseButtonDown(0) && currentJumpCount < maxJumpCount) 
        {
            rb.velocity = new Vector2(rb.velocity.x, 6);
            currentJumpCount += 1;
        }

        speedIncrease += 0.005F;
        updateScoreText(score());
    }

    // void OnMouseDown() {
    // }

    void OnCollisionEnter2D(Collision2D col)
    {
        resetJumpCount();
    }

    private void resetJumpCount()
    {
        currentJumpCount = 0;
    }

    private void updateScoreText(ulong score)
    {
        scoreText.text = "Score: " + score.ToString() + Environment.NewLine + "Best: " + bestScore;
    }

    private ulong score()
    {
        ulong distanceMoved = (ulong)(transform.position.x - startingPosX);
        return distanceMoved;
    }

    private void setNewBestScoreIfAchieved()
    {
        if (score() > Convert.ToUInt64(bestScore))
        {
            PlayerPrefs.SetString("BestScore", score().ToString());
        }
    }

}

