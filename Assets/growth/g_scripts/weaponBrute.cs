using UnityEngine;
using System.Collections;

public class weaponBrute : MonoBehaviour {

    // this weapon uses a game object as the weapon instead of just a box collider because the pivot is at the bottom
    
    //public BoxCollider2D weapon_collider;				// Prefab of the sword.

  //  public float knockBackX = 100f;
 //   public float knockBackY = 100f;
    public int KnockBackDamage = 0;

    
    public GameObject weaponInstance;
    // here
   

    private float lastattack = 0;
    
    public bool attk = true;
    public float nxtAttk;
    public float weaponOffset;
    public bool swinging = false;
    public float aliveExtra;
    public float swingDelay;
    public float swingTime;

    public enemyBrute enemyBrute;

    public int damage = 1;
    public int knockBackX = 100;
    public int knockBackY = 100;
    

    public float rotation;
    public float swingFrames;


    // Use this for initialization
    void Start()
    {
        enemyBrute = GetComponent<enemyBrute>();
 
    
    }
      
    
    // Update is called once per frame
   void Update()
    {
       
     }
 
     void FixedUpdate () {

         if (swinging == true)
         {
             if (weaponInstance.transform.rotation.eulerAngles.z > rotation)
             {
                 weaponInstance.transform.Rotate(new Vector3(0, 0, (rotation) / (swingFrames / 5) * Time.deltaTime * 10));
             }
             if (weaponInstance.transform.rotation.eulerAngles.z < rotation)
             {
                 weaponInstance.transform.eulerAngles = new Vector3(0, 0, rotation);
             }

         }
    }

       IEnumerator swing()
    {

        Debug.Log("here");
        // we should have this functions destory the weapon
        yield return new WaitForSeconds(swingDelay);
        swinging = true;
        yield return new WaitForSeconds(1);
        swinging = false;
    }     
 
      

        void OnTriggerEnter2D(Collider2D col)
        {
            // If it hits an enemy...
            if (col.tag == "Player")
            {
                // ... find the Enemy script and call the Hurt function.
                PlayerDamage pDamage = col.gameObject.GetComponent<PlayerDamage>();

                pDamage.Hurt(damage);
            //   pDamage.knockBack(gameObject, knockBackX, knockBackY);
            }

        }

    public void attack()
    {
        if ((Time.time - lastattack) > nxtAttk)
        {
            // this may need to be moved to player controller and use a find like spellfind
       
            if (enemyBrute.facingRight)
            {
              
               weaponInstance = Instantiate(weaponInstance, new Vector2(transform.position.x + weaponOffset, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
       
            }
            else
            {
                 weaponInstance = Instantiate(weaponInstance, new Vector2(transform.position.x - weaponOffset, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
               
            }
            weaponInstance.transform.parent = transform;
           
            // destory weapon after sum of all 3 times passed to avoid errors
            Destroy(weaponInstance, (aliveExtra + swingDelay + swingTime + 1f));
            StartCoroutine(swing());
            lastattack = Time.time;

        }
    }
            
        }
    

