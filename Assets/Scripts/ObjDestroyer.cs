using UnityEngine;

public class ObjDestroyer : MonoBehaviour
{
    public GameObject objDestroyerPoint;

    // Start is called before the first frame update
    void Start()
    {
        objDestroyerPoint = GameObject.Find("ObjDestroyerPoint");    
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < objDestroyerPoint.transform.position.x)
        {
            Destroy(gameObject);
        } 
    }
}
