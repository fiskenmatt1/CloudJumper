using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    public Animator animator;
    public GameObject playerCharacter;
    public PlayerControls playerCharacterScript;

    void Start()
    {
        playerCharacterScript = playerCharacter.GetComponent<PlayerControls>();
    }

    void Update()
    {
        if (Vector2.Distance(gameObject.transform.position, playerCharacter.transform.position) < 2)
        {
            animator.SetBool("ShouldAttack", true);
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
