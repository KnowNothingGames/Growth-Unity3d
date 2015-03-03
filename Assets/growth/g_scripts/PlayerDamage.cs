using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour {

    public int HP;
    public bool Invinc;
    public float MP;
    public float MPMax;
    public bool Mregen = true;
    public float mRegenRate = 0.2f;

    private float xKnock;
    private float yKnock;

    public float knockForceX;
    public float knockForceY;
    public bool KnockBackStun = false;
    public SpriteRenderer drak;
    public float mRegenTime;
    public float mRegenWait = 3f;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        if ((Time.time - mRegenTime) > mRegenWait)
        {
            Mregen = true;

        }

        
        // if you fall below 50 you die
        if (gameObject.transform.position.y < -50)
        {
            Death();
        } 


        // if regen is true and you are less than max add to MP
        if (Mregen == true && MP < MPMax ) { 
        MP += mRegenRate;
        }
        // check for overflow if so then set to max
        if (MP > MPMax)
        {
            MP = MPMax;
        }
        // if you have no hp die
        if (HP <= 0)
        {
          Death();
        }
	
	}

    // if you collider with a 2d collider with tag enemey and you are not invincible call hurt 
    // set invincle to true and start the timer
    void OnCollisionEnter2D(Collision2D col)
    {

        //this needs t obe changed a get a value for hurt instead of being 1
        if (col.collider.tag == "Enemy" && Invinc == false)
        {
            
            
            
            
            
            // go get the damage cause by collision with an emeny this is no controlled on the enemy script
            Enemy knockD = col.gameObject.GetComponent<Enemy>();
            knockForceX = knockD.knockForceX;
            knockForceY = knockD.knockForceY;
            Hurt(knockD.knockDam);
            Invinc = true;
            StartCoroutine(Invincible());
           
            StartCoroutine(knockBackStun());
        // add knockback here
            // if they are farther in the x knockback else knock forward
            // same for y up and down but currently only one sixth the force
            
                       

                if
                (col.collider.transform.position.x > rigidbody2D.transform.position.x)
                {
                    xKnock = -knockForceX;
                }
                else
                {
                    xKnock = knockForceX;
                }
                if
               (col.collider.transform.position.y > rigidbody2D.transform.position.y)
                {
                    yKnock = -knockForceY;
                }
                else
                {
                    yKnock = knockForceY;
                }



                rigidbody2D.AddForce(new Vector2(xKnock, yKnock));
                KnockBackStun = true;

               
                
                //set the alpha to  the last float
            drak.color = new Color(1f, 1f, 1f, 0.65f);
            
        }
    }

    
    // debt MP spell cost stop Mpregen temporarily
    public void spellCost(float cost)
    {

        MP -= cost;
        Mregen = false;
        // changed to timer instead of co-routine
        mRegenTime = Time.time;
        
        
            
    }
       

    IEnumerator Invincible()
    {
        yield return new WaitForSeconds(0.75f);
        Invinc = false;
        
        // set the alpha back to full
        drak.color = new Color(1f, 1f, 1f, 1f);
    }

    IEnumerator knockBackStun()
    {
        yield return new WaitForSeconds(0.3f);
        KnockBackStun = false;

    }
            
   
    public void Hurt(int x)
    {
        // Reduce the number of hit points by one.
        HP = HP - x;
    }

    // you die and the level reloads
    void Death()
    {
        Destroy(gameObject);
        Application.LoadLevel("growth_1");
    }

}
