using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour, Iknockable {

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
    
    // need to write this to use the knockback interface
    public void knockBack(GameObject obj, int x, int y)
    {

        Invinc = true;
        StartCoroutine(Invincible());
        StartCoroutine(knockBackStun());

        if
               (obj.transform.position.x > GetComponent<Rigidbody2D>().transform.position.x)
        {
            x = x * -1;
        }
       
        if
       (obj.transform.position.y > GetComponent<Rigidbody2D>().transform.position.y)
        {
            y = y * -1;
        }
                
        
        GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y));
        KnockBackStun = true;


        
            
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
        drak.color = new Color(1f, 1f, 1f, 0.5f);
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

        
        Debug.Log(x);
        // Reduce the number of hit points by one.

        if (Invinc == false)
        {
            HP = HP - x;
        }
    }

    // you die and the level reloads
    void Death()
    {
        Destroy(gameObject);
        Application.LoadLevel("growth_1");
    }

}
