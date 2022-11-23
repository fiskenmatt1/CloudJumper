using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatGenerator : MonoBehaviour
{
    public GameObject plat;
    public Transform generatorPoint;
    public float distanceBetweenPlats;

    private float platWidth;
    private float newPlatYVal;
    private float lowestPossiblePlatYVal;
    private float lastPlatYVal;
    private float maxJumpHeight = 3.5F; // NOTE: not true max jump height, just a difficult but reachable height

    // Start is called before the first frame update
    void Start()
    {
        platWidth = plat.GetComponent<BoxCollider2D>().size.x;
        // set defaul platform y vals
        newPlatYVal = transform.position.y;
        lowestPossiblePlatYVal = transform.position.y;
        lastPlatYVal = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < generatorPoint.position.x)
        {
            newPlatYVal = Random.Range(lowestPossiblePlatYVal, (lastPlatYVal + maxJumpHeight));
            transform.position = new Vector3(transform.position.x + (platWidth / 2) + distanceBetweenPlats, transform.position.y, transform.position.z);
            Vector2 newPlatVector = new Vector2(transform.position.x, newPlatYVal);
            Instantiate(plat, newPlatVector, transform.rotation);
            lastPlatYVal = newPlatYVal;
            // distanceBetweenPlats += 0.01F;
        }
        // increase distance of next plat spawn (x)
        // NOTE: gap increase rate should be same as player speed increase (TODO: read from global setting?)
        distanceBetweenPlats += 0.005F;
    }
}
