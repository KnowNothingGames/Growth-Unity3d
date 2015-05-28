using UnityEngine;
using System.Collections;

public class weaponBrute : MonoBehaviour {

    // this weapon uses a game object as the weapon instead of just a box collider because the pivot is at the bottom
    
    //public BoxCollider2D weapon_collider;				// Prefab of the sword.

  //  public float knockBackX = 100f;
 //   public float knockBackY = 100f;
    public int KnockBackDamage = 0;

    
    public GameObject weaponInstance;
    private GameObject weaponInstanceClone;
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
    private float iteration;



    // Use this for initialization
    void Start()
    {
        enemyBrute = GetComponent<enemyBrute>();
       
    }
        
    // Update is called once per frame
        void Update () {

             if (iteration > 0)
             {
                 iteration = iteration - 1;
                 weaponInstanceClone.transform.Rotate(new Vector3(0, 0, rotation / swingFrames));
             }
             
             //    weaponInstance.transform.eulerAngles = new Vector3(0, 0, rotation);
         
    }
           IEnumerator swing()
    {        
        // we should have this functions destory the weapon
        yield return new WaitForSeconds(swingDelay);
        iteration = swingFrames;

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
              
              weaponInstanceClone = Instantiate(weaponInstance, new Vector2(transform.position.x + weaponOffset, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
       
            }
            else
            {
              weaponInstanceClone = Instantiate(weaponInstance, new Vector2(transform.position.x - weaponOffset, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
               
            }
            weaponInstanceClone.transform.parent = transform;
           
            // destory weapon after sum of all 3 times passed to avoid errors
            Destroy(weaponInstanceClone, (aliveExtra + swingDelay + swingTime));
            StartCoroutine(swing());
            lastattack = Time.time;

        }
    }
            
        }
    

