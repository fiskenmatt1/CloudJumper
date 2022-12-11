using UnityEngine;

public class PlatGenerator : MonoBehaviour
{
    public GameObject plat;
    public GameObject enemy;
    public Transform generatorPoint;
    public float distanceBetweenPlats;

    private float platWidth;
    private float platHeight;
    private float enemyHeight;
    private float newPlatYVal;
    private float lowestPossiblePlatYVal;
    private float lastPlatYVal;
    private float maxJumpHeight = 3.5F; // NOTE: not true max jump height, just a difficult but reachable height
    private int numOfPlatsGeneratedWithoutEnemy = 0;
    private int numOfPlatsGeneratedWithoutEnemyUpperLimit = 12;

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D platBoxCollider = plat.GetComponent<BoxCollider2D>();
        platWidth = platBoxCollider.size.x;
        platHeight = platBoxCollider.size.y;

        BoxCollider2D enemyBoxCollider = plat.GetComponent<BoxCollider2D>();
        enemyHeight = enemyBoxCollider.size.y;

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
            transform.position = new Vector2(transform.position.x + (platWidth / 2) + distanceBetweenPlats, transform.position.y);
            Vector2 newPlatVector = new Vector2(transform.position.x, newPlatYVal);
            GameObject generatedPlat = Instantiate(plat, newPlatVector, transform.rotation);
            lastPlatYVal = newPlatYVal;
            // distanceBetweenPlats += 0.01F;
            numOfPlatsGeneratedWithoutEnemy++;

            int randomizer = Random.Range(numOfPlatsGeneratedWithoutEnemy, numOfPlatsGeneratedWithoutEnemyUpperLimit + 1);
            if (randomizer == numOfPlatsGeneratedWithoutEnemyUpperLimit)
            {
                Vector2 newEnemyVector = new Vector2(generatedPlat.transform.position.x, newPlatYVal + (platHeight / 2) + enemyHeight);
                Instantiate(enemy, newEnemyVector, transform.rotation);
                numOfPlatsGeneratedWithoutEnemy = 0;
            }
        }
        // increase distance of next plat spawn (x)
        distanceBetweenPlats += 0.001F;
    }
}
