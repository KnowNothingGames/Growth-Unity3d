using UnityEngine;
using System.Collections;

public class GhostFollow : MonoBehaviour {

    // Use this for initialization


    public float xMargin = 1f;		// Distance in the x axis the player can move before the camera follows.
    public float yMargin = 1f;		// Distance in the y axis the player can move before the camera follows.
    public float smooth = 0.5f;
    private Transform player;
    private Transform inter;
    //private Rigidbody2D vel;
    public float moveForce = 365f;
    public bool Move = false;

    private float xKnock = 5000f;
    private float yKnock = 5000f;
    public float knockForce;
    public float distance;
    public float attackRange = 20f;
    public float speed = 3f;
    //changed to translate lerp
    public bool wandering = false;
    public SpriteRenderer Ene;
    public float angrySpeed = 365;
    private Enemy HP;
    private Vector2 pos2;

    public bool bump = false;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
         HP = gameObject.GetComponent<Enemy>();
    }

    void Update()
    {
        
        // move faster at 1 hp;
        if (HP.HP == 1)
        { 
            speed = angrySpeed;
        }
    }




     
    // Update is called once per frame
    void FixedUpdate()
    {
        if (wandering == false && Random.Range(0, 1000) < 1)
        {
            StartCoroutine(wanderWarn());
            
        }

        if (bump == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos2, (Time.deltaTime * speed) / 30);
        }

        
        distance = Vector3.Distance(transform.position, player.position);
        if (wandering)
        {
            wander();
        } else if 
         (distance <= attackRange && bump == false)
        {
            track();
        }
     

    }


    void OnCollisionEnter2D(Collision2D col)
    {

        //this needs t obe changed a get a value for hurt instead of being 1
        if (col.collider.tag == "Player" || col.collider.tag == "Obstacle")
        {
            // go get the damage cause by collision with an emeny this is no controlled on the enemy script
               
            
         //check if you hit the player if you did stop chasing him for backOff time
         
         
             xMargin = 1f;
             yMargin = 1f;
             gameObject.collider2D.enabled = false;
             StartCoroutine(backOff());
             bump = true;
     
     
            
            
            
            //StartCoroutine(knockBackStun());
            // add knockback here
            // if they are farther in the x knockback else knock forward
            // same for y up and down but currently only one sixth the force



            if
            (col.collider.transform.position.x > rigidbody2D.transform.position.x)
            {
                xKnock = transform.position.x - knockForce;
            }
            else
            {
                xKnock = transform.position.x + knockForce;
            }
            if
           (col.collider.transform.position.y > rigidbody2D.transform.position.y)
            {
                yKnock = -knockForce / 6;
            }
            else
            {
                yKnock = knockForce / 6;
            }
            pos2 = new Vector2(xKnock, yKnock);


            
            //rigidbody2D.AddForce(new Vector2(xKnock, yKnock));
                    
            

        }
    }





    void wander()
    {
        Vector3 loc = new Vector3(Random.Range(-100, 150), Random.Range(-100, 150), 0); 
        transform.position = Vector3.MoveTowards(transform.position, loc, (Time.deltaTime * speed) / 20);
        

    
    }

    IEnumerator wanderWarn()
    {
        
        Ene.color= new Color (1f,1f,0f,1f);
        yield return new WaitForSeconds(1.5f);
        wandering = true;
        StartCoroutine(wanderEn());

    }
    
    IEnumerator wanderEn()
    {
        yield return new WaitForSeconds(3f);
        Ene.color = new Color(1f, 1f, 1f, 1f);
        wandering = false;

    }


    bool CheckXMargin()
    {
        // Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
        return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
    }


    bool CheckYMargin()
    {
        // Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
        return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
    }
    
     // non trigger method    
     
    // trigger method
   
    /*
    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("trigger");    
        if (col.collider2D.tag == "Player")
        {
            xMargin = 2f;
            yMargin = 2f;
            gameObject.collider2D.enabled = false;
            StartCoroutine(backOff());
        }
    }
    */

    IEnumerator backOff()
    {
        yield return new WaitForSeconds(0.25f);
        bump = false;

        yield return new WaitForSeconds(0.75f);
        xMargin = 0.2f;
        yMargin = 0.2f;
        gameObject.collider2D.enabled = true;

    }

    void track()
    {
       
                
        if (CheckYMargin() || CheckXMargin())
        {

            transform.position = Vector3.MoveTowards(transform.position, player.position, (Time.deltaTime * speed)/100);
        }
       
    }
}
