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

        void OnCollisionEnter2D(Collision2D col)
    { 
        if (col.gameObject.tag == "MainC")
        {
            animator.SetBool("ShouldAttack", true);

            playerCharacterScript.KillPlayer(DeathType.Explode);
        }
    }
}
