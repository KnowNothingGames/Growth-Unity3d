using UnityEngine;
using System.Collections;

public class weaponBrute : MonoBehaviour {

    public BoxCollider2D sword_collider;				// Prefab of the sword.
    public float speed = 20f;
    private float lastattack = 0;
    
    public bool attk = true;
    public float nxtAttk = 0.3f;
    public float weaponOffset;
   
    public enemyBrute enemyBrute;

    // Use this for initialization
    void Start()
    {
        enemyBrute = GetComponent<enemyBrute>(); 
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public void attack()
    {
        if ((Time.time - lastattack) > nxtAttk)
        {



            // this may need to be moved to player controller and use a find like spellfind
            

            if (enemyBrute.facingRight)
            {

                // not sure if I want to do raycast of collider here
                BoxCollider2D weaponInstance = Instantiate(sword_collider, new Vector2(transform.position.x + weaponOffset, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0))) as BoxCollider2D;
                weaponInstance.transform.parent = transform;
            }
            else
            {

                BoxCollider2D weaponInstance = Instantiate(sword_collider, new Vector2(transform.position.x - weaponOffset, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0))) as BoxCollider2D;
                weaponInstance.transform.parent = transform;

            }


            lastattack = Time.time;

        }
    }
            
        }
    

