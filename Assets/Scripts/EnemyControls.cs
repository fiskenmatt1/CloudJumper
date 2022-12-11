using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    public Animator animator;
    public GameObject playerCharacter;
    public AudioSource audioSource;
    public AudioClip audioClipSlash;

    private PlayerControls playerCharacterScript;
    private bool didAttack = false;

    void Start()
    {
        playerCharacterScript = playerCharacter.GetComponent<PlayerControls>();
    }

    void Update()
    {
        if (!didAttack && Vector2.Distance(gameObject.transform.position, playerCharacter.transform.position) < 2)
        {
            animator.SetBool("ShouldAttack", true);

            audioSource.PlayOneShot(audioClipSlash);

            didAttack = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "MainC")
        {
            playerCharacterScript.KillPlayer(DeathType.Explode);
        }
    }
}
