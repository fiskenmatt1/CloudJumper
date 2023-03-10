using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(4.5F, 1.5F, -8F);

    private PlayerControls playerCharacterScript;
    private bool freezeCamera = false;

    void Start()
    {
        GameObject playerCharacter = GameObject.FindGameObjectWithTag("MainC");
        playerCharacterScript = playerCharacter.GetComponent<PlayerControls>();
    }

    void Update()
    {
        if (freezeCamera) { return; }

        if (playerCharacterScript.playerIsDead)
        {
            freezeCamera = true;
        }
    }

    void LateUpdate()
    {
        if (!freezeCamera)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
