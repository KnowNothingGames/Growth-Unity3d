using UnityEngine;
using System.Collections;

// inherits from enemy
public class enemyBrute : Enemy {

    public Transform foot;
    public bool Wall;
    public bool Ground;
    
    public float distance;
    public float patrolRange;
    public float attackRange;
    public bool facingRight = true;
    private int x = 1;
    
    
    public int knockDam = 2;
    public int knockForceX = 200;
    public int knockForceY = 30;
    
    
    private Transform player;
    public weaponBrute weaponBrute;
    // Use this for initialization
	void Start () {

        weaponBrute = GetComponent<weaponBrute>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

	}


    void FixedUpdate()
    {
        Ground = Physics2D.Linecast(transform.position, foot.position, 1 << LayerMask.NameToLayer("Ground"));
        Wall = Physics2D.Linecast(transform.position, foot.position, 1 << LayerMask.NameToLayer("Wall"));
  
    }
    
    // Update is called once per frame
	void Update () {

        distance = Vector3.Distance(transform.position, player.position);
        
        if (distance >= patrolRange)
        {
            patrol();
        }

        else if (distance < patrolRange && distance > attackRange)
        {
            follow();
        }     
        
        
        else if (distance < attackRange) {
            weaponBrute.attack();
        } 
         
         
    }

    // knockback should be added to derived class because of different trigger conditions
    void OnTriggerEnter2D(Collider2D col)
    {
        // If it hits an enemy...
        if (col.gameObject.tag == "Player")
        {
            // ... find the Enemy script and call the Hurt function.
            PlayerDamage pDamage = col.gameObject.GetComponent<PlayerDamage>();
           
                pDamage.Hurt(knockDam);
                pDamage.knockBack(gameObject, knockForceX, knockForceY);
            
        }
    }
    
    
    void patrol()
    {
        if (!Ground || Wall)
        {
            Flip();

        }
        if (Ground && !Wall)
        {
          
                   
                transform.Translate(new Vector3(x, 0, 0) * (Time.deltaTime * moveSpeed));
        
            
        
        }
    }

    void follow()
    {

        if ((player.position.x - transform.position.x) < 0 && facingRight)
        { 
          Flip();
        }
        if ((player.position.x - transform.position.x) >= 0 && !facingRight)
        {
            Flip();
        }


        if (Ground && !Wall)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), (Time.deltaTime * moveSpeed));
        }         
        
    }
           
    void Flip()
    {
        // Switch the way the player is labelled as facing.
        

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        facingRight = !facingRight;
        theScale.x *= -1;
        x *= -1;
        transform.localScale = theScale;
    }

}
