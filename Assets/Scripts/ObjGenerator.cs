using UnityEngine;

public class ObjGenerator : MonoBehaviour
{
    public GameObject plat;
    public GameObject enemy;
    public Transform generatorPoint;

    private float distanceBetweenPlats = 3.5F;
    private float platWidth;
    private float platHeight;
    private float enemyHeight;
    private float newPlatYVal;
    private float newPlatYVal2;
    private float lowestPossiblePlatYVal;
    private float lastPlatYVal;
    private float maxJumpHeight = 3.5F; // NOTE: not true max jump height, just a difficult but reachable height
    private int numOfPlatsGeneratedWithoutEnemy = 0;
    private int numOfPlatsGeneratedWithoutEnemyUpperLimit = 12;
    private bool plat2OffsetPositive = true;

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
            transform.position = new Vector2(transform.position.x + (platWidth / 2) + distanceBetweenPlats, transform.position.y);

            newPlatYVal = Random.Range(lowestPossiblePlatYVal, (lastPlatYVal + maxJumpHeight));
            Vector2 newPlatVector = new Vector2(transform.position.x, newPlatYVal);
            GameObject generatedPlat = Instantiate(plat, newPlatVector, transform.rotation);

            lastPlatYVal = newPlatYVal;

            numOfPlatsGeneratedWithoutEnemy++;

            GameObject generatedPlat2 = null;

            if (Random.Range(0, 3) == 1) 
            {
                float plat2Offset = plat2OffsetPositive ? Random.Range(2, 4) : Random.Range(-2, -4); 
                newPlatYVal2 = newPlatYVal + Random.Range((enemyHeight * 3F), (enemyHeight * 4.5F));
                Vector2 newPlatVector2 = new Vector2(transform.position.x + plat2Offset, newPlatYVal2); //WARNING: Range(-2.5F, 2.5F) could cause overlap as distance between plats can be less than 5 
                generatedPlat2 = Instantiate(plat, newPlatVector2, transform.rotation);
                numOfPlatsGeneratedWithoutEnemy++;
            }
            else
            {
                // plat2 not generated so can safely switch offset direction without worrying about platform overlap
                plat2OffsetPositive = !plat2OffsetPositive;
            }

            int randomizer = Random.Range(numOfPlatsGeneratedWithoutEnemy, numOfPlatsGeneratedWithoutEnemyUpperLimit + 1);
            if (numOfPlatsGeneratedWithoutEnemy >= numOfPlatsGeneratedWithoutEnemyUpperLimit ||
                randomizer == numOfPlatsGeneratedWithoutEnemyUpperLimit)
            {
                float platYValToUse = newPlatYVal;
                float platXValToUse = generatedPlat.transform.position.x;

                if (generatedPlat2 != null)
                {
                    if (Random.Range(0, 2) == 1)
                    {
                        platYValToUse = newPlatYVal2;
                        platXValToUse = generatedPlat2.transform.position.x;
                    }
                }

                Vector2 newEnemyVector = new Vector2(platXValToUse, platYValToUse + (platHeight / 2) + enemyHeight);
                Instantiate(enemy, newEnemyVector, transform.rotation);

                numOfPlatsGeneratedWithoutEnemy = 0;
            }
        }
        // increase distance of next plat spawn (x)
        distanceBetweenPlats += 0.0005F;
    }
}
