using UnityEngine;
using System.Collections;

public class hit_enemy : MonoBehaviour {


    public int damage = 1;
    public int knockBackX = 100;
    public int knockBackY = 100;
    public float upframes;

    // Use this for initialization
    void Start()
    {
        Destroy(transform.parent.gameObject, upframes);
        Destroy(gameObject, upframes);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // If it hits an enemy...
        if (col.tag == "Player")
        {
            // ... find the Enemy script and call the Hurt function.
            PlayerDamage pDamage = col.gameObject.GetComponent<PlayerDamage>();
            
                pDamage.Hurt(damage);
                pDamage.knockBack(gameObject, knockBackX, knockBackY);
          
            


           
        }
       
    }


}
