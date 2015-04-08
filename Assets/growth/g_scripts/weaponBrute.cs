using UnityEngine;
using System.Collections;

public class weaponBrute : MonoBehaviour {

    // this weapon uses a game object as the weapon instead of just a box collider because the pivot is at the bottom
    
    //public BoxCollider2D weapon_collider;				// Prefab of the sword.

    public float knockBackX = 100f;
    public float knockBackY = 100f;
    public int KnockBackDamage = 0;

    public GameObject weapon_collider_pivot;
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
                //BoxCollider2D weaponInstance = Instantiate(weapon_collider, new Vector2(transform.position.x + weaponOffset, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0))) as BoxCollider2D;
                GameObject weaponInstance = Instantiate(weapon_collider_pivot, new Vector2(transform.position.x + weaponOffset, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                weaponInstance.transform.parent = transform;
               
            }
            else
            {

                GameObject weaponInstance = Instantiate(weapon_collider_pivot, new Vector2(transform.position.x - weaponOffset, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                weaponInstance.transform.parent = transform;
            }

            lastattack = Time.time;

        }
    }
            
        }
    

