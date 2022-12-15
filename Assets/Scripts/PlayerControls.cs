using System;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public enum DeathType
{
    Fall,
    Explode
}

public class PlayerControls : MonoBehaviour
{
    public TMP_Text scoreText;
    public Camera mainCamera;
    public AudioSource audioSource;
    public AudioClip audioClipJump;
    public AudioClip audioClipFall;
    public AudioClip audioClipExplode;
    public Animator animator;
    public ParticleSystem jumpParticles;
    public ParticleSystem deathParticles;

    public static bool playerIsDead = false;

    private Rigidbody2D rb;
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
        UpdateScoreText(Score());
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
                KillPlayer(DeathType.Fall);
            }
        }

        rb.velocity = new Vector2(baseSpeed + speedIncrease, rb.velocity.y);

        if (Input.GetMouseButtonDown(0) && currentJumpCount < maxJumpCount && !MousePositionOnPauseButton()) 
        {
            audioSource.PlayOneShot(audioClipJump);

            jumpParticles.Play();

            rb.velocity = new Vector2(rb.velocity.x, 7.5F);

            currentJumpCount += 1;
        }

        speedIncrease += 0.003F;
        UpdateScoreText(Score());

        // zoom out camera slowly over time (max 50) 
        // NOTE: increase should probably be linked to score or player speed rather than frames
        if (mainCamera.orthographicSize < 50F)
        {
            mainCamera.orthographicSize += 0.001F;
        }
    }

    public void KillPlayer(DeathType deathType)
    {
        // freezes camera on death
        mainCamera.GetComponent<Rigidbody2D>().simulated = true;

        playerIsDead = true;

        SetNewBestScoreIfAchieved();

        switch (deathType)
        {
            case DeathType.Fall:
                {
                    FallDeath();

                    StartCoroutine(DisplayPauseMenu(0.75F));

                    break;
                }
            case DeathType.Explode:
                {
                    ExplodeDeath();

                    StartCoroutine(DisplayPauseMenu(1.25F));

                    break;
                }     
        }
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // restrict jump reset and run animation to collisions with upper side of cloud 
        if (col.transform.position.y < transform.position.y)
        {
            ResetJumpCount();

            animator.SetBool("ShouldRun", true);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        animator.SetBool("ShouldRun", false);
    }

    private void ResetJumpCount()
    {
        currentJumpCount = 0;
    }

    private void UpdateScoreText(ulong score)
    {
        scoreText.text = "SCORE: " + score.ToString() + Environment.NewLine + "BEST: " + bestScore;
    }

    private ulong Score()
    {
        ulong distanceMoved = (ulong)(transform.position.x - startingPosX);
        return distanceMoved;
    }

    private void SetNewBestScoreIfAchieved()
    {
        if (Score() > Convert.ToUInt64(bestScore))
        {
            PlayerPrefs.SetString("BestScore", Score().ToString());
            PopupMenu.didAchieveNewBest = true;
        }
    }

    // Determine if mouse is over the pause button
    private bool MousePositionOnPauseButton()
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

    private void ExplodeDeath()
    {
        audioSource.PlayOneShot(audioClipExplode);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

        rb.velocity = new Vector2(0, 0);

        deathParticles.Play();
    }

    private void FallDeath()
    {
        audioSource.PlayOneShot(audioClipFall);

        rb.velocity = new Vector2(0, -20);
    }

    IEnumerator DisplayPauseMenu(float withDelay = 0F)
    {
        yield return new WaitForSeconds(withDelay);

        PopupMenu.Pause(playerIsDead);
    }
}

