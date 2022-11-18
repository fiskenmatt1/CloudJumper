using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatDestroyer : MonoBehaviour
{
    public GameObject platDestroyerPoint;

    // Start is called before the first frame update
    void Start()
    {
        platDestroyerPoint = GameObject.Find("PlatDestroyerPoint");    
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < platDestroyerPoint.transform.position.x)
        {
            Destroy(gameObject);
        } 
    }
}
