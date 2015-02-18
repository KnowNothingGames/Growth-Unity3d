using UnityEngine;
using System.Collections;

public class axehit : MonoBehaviour {

    public int damage = 1;

    public float upframes;

    // Use this for initialization
    void Start()
    {

        Destroy(gameObject, upframes);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // If it hits an enemy...
        if (col.tag == "Enemy")
        {
            // ... find the Enemy script and call the Hurt function.
            col.gameObject.GetComponent<Enemy>().Hurt(1);

            // Call the explosion instantiation.
            
        }
    }



}
