using System;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour
{
    public Rigidbody2D rb;
    public TMP_Text scoreText;
    public Camera mainCamera;
    public AudioSource audioSource;
    public AudioClip audioClipJump;
    public AudioClip audioClipFall;

    public static bool playerIsDead = false;

    private byte currentJumpCount = 0;
    private byte maxJumpCount = 2;
    private float baseSpeed = 3;
    private float speedIncrease = 0.001F;
    private float startingPosX;
    private string bestScore;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1F;
        playerIsDead = false;
        rb = GetComponent<Rigidbody2D>();
        startingPosX = transform.position.x;
        bestScore = PlayerPrefs.GetString("BestScore", "0");
        updateScoreText(score());
    }

    // Update is called once per frame
    void Update()
    {
        // Don't update when player dead or paused
        if (playerIsDead || PopupMenu.isPaused) { return; }

        if (rb.position.y < -7) 
        {
            if (!playerIsDead) 
            {
                audioSource.PlayOneShot(audioClipFall);
                playerIsDead = true;
            }
            setNewBestScoreIfAchieved();
            PopupMenu.pause(playerIsDead);
        }

        rb.velocity = new Vector2(baseSpeed + speedIncrease, rb.velocity.y);

        if (Input.GetMouseButtonDown(0) && currentJumpCount < maxJumpCount && !mousePositionOnPauseButton()) 
        {
            audioSource.PlayOneShot(audioClipJump);
            rb.velocity = new Vector2(rb.velocity.x, 6);
            currentJumpCount += 1;
        }

        speedIncrease += 0.005F;
        updateScoreText(score());

        // zoom out camera slowly over time (max 50) 
        // NOTE: increase should probably be linked to score or player speed rather than frames
        if (mainCamera.orthographicSize < 50F)
        {
            mainCamera.orthographicSize += 0.001F;
        }
    }

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
        scoreText.text = "SCORE: " + score.ToString() + Environment.NewLine + "BEST: " + bestScore;
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
            PopupMenu.didAchieveNewBest = true;
        }
    }

    // Determine if mouse is over the pause button
    private bool mousePositionOnPauseButton()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        bool onPauseButton = false;

        if (raycastResults.Count > 0)
        {
            foreach (var result in raycastResults)
            {
                if (result.gameObject.tag == "PauseButton")
                {
                    onPauseButton = true;
                }
            }
        }

        return onPauseButton;
    }

}

