using UnityEngine;
using System.Collections;

public class weaponBrute : MonoBehaviour {

    // this weapon uses a game object as the weapon instead of just a box collider because the pivot is at the bottom
    
    //public BoxCollider2D weapon_collider;				// Prefab of the sword.

    public float knockBackX = 100f;
    public float knockBackY = 100f;
    public int KnockBackDamage = 0;

    public GameObject weapon_collider_pivot;
    public GameObject weaponInstance;
    public float speed = 20f;
    private float lastattack = 0;
    
    public bool attk = true;
    public float nxtAttk = 0.3f;
    public float weaponOffset;
    public bool swinging = false;
   
    public enemyBrute enemyBrute;

    // Use this for initialization
    void Start()
    {
        enemyBrute = GetComponent<enemyBrute>(); 
    }

    // Update is called once per frame
    void Update()
    {
       if (swinging == true){
           Quaternion target = Quaternion.Euler(0, 0, -90); 
           weaponInstance.transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * 1000f);
       }
    }


        IEnumerator swing(){
            // we should have this functions destory the weapon
            yield return new WaitForSeconds(0.5f);
            swinging = true;
            yield return new WaitForSeconds(5f);
            swinging = false;
        }

    public void attack()
    {
        if ((Time.time - lastattack) > nxtAttk)
        {
            // this may need to be moved to player controller and use a find like spellfind
       
            if (enemyBrute.facingRight)
            {

               
               weaponInstance = Instantiate(weapon_collider_pivot, new Vector2(transform.position.x + weaponOffset, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                weaponInstance.transform.parent = transform;
                StartCoroutine(swing());
         
            
              
               
            }
            else
            {
                 weaponInstance = Instantiate(weapon_collider_pivot, new Vector2(transform.position.x - weaponOffset, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                weaponInstance.transform.parent = transform;
                StartCoroutine(swing());
            }

            lastattack = Time.time;

        }
    }
            
        }
    

