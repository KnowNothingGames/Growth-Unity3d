using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
    public int jumpcount = 0;              // Counts jumps until grounded
    
    public int jumps = 2;                   // jumps allowed before grounded again 2 = double jump 3 = triple etc
	
    public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	public AudioClip[] taunts;				// Array of clips for when the player taunts.
	public float tauntProbability = 50f;	// Chance of a taunt happening.
	public float tauntDelay = 1f;			// Delay for when the taunt should happen.


	private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
    private Transform weaponPoint;
    private bool grounded = false;			// Whether or not the player is grounded.
    private RaycastHit2D wallHit;
    public bool walled = false;	        // are they touching a wall
    public bool wallUsed = true;
    private Collider2D newWallHit;
    private Collider2D oldWallHit;
    
    
    private Animator anim;					// Reference to the player's animator component.
    
    public string currentSpellOne;
    public string currentSpellTwo;
        
    
    public PlayerDamage MP;
    public GameObject weapon;
   
    public float wallGrav = 2f;

    private Sword attacking;
    private float wTime;

    private float jumpTime;
    public float hangTime;
    public float jBoost;
    private bool boost;

    void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
        weaponPoint = transform.Find("weaponPoint");
        currentSpellOne = "_spellNull";
        currentSpellTwo = "_spellNull";
        anim = GetComponent<Animator>();
        // this is not great going to have to have a method for getting the weapon like getting the spell
        attacking = weapon.GetComponent<Sword>();
        
        
        SpellFind();
    }


    
    // search components for script that starts with "_Spell" reserveed for spells
    // currently only looks for two spells
    void SpellFind()
    {
     Component[] find = GetComponents(typeof(Component));

     foreach (Component item in find){
    

         string spellfound = item.GetType().Name;

                 
         string spellfoundsub = spellfound.Substring(0, 6);
         
         
         if (spellfoundsub == "_Spell")
        {

            if (currentSpellOne == "_spellNull")
            {
                currentSpellOne = spellfound;
            }
            else
            {
                currentSpellTwo = spellfound;
                
            
            }
       }
     
     
     }

    }



    

    //** look into line cast
	void Update()
	{



        // If the jump button is pressed and the player is grounded then the player should jump. Or is they havent used all their jmups
        // as it stands right now you can double jump up a wall. then do wall jumping - not sure if I like this. 
        if (Input.GetButtonDown("Jump") && grounded && MP.KnockBackStun == false || Input.GetButtonDown("Jump") && jumpcount < jumps && MP.MP >= 50f && MP.KnockBackStun == false)
        {
            
            //grounded will reset this to quickly for it to register the first jump so it will only count after the first jump
            jumpcount += 1;
           
            jump = true;
            anim.SetTrigger("jump");
            jumpTime = Time.time;
            if (!grounded) { 
                MP.spellCost(50f); 
            }

        }
        // I think this is what is might be causeing the inconsitant jumps
        if (Input.GetButtonUp("Jump") || Time.time > jumpTime + hangTime)
        {
            jump = false;
            boost = true;
            // Make sure the player can't jump again until the jump conditions from Update are satisfied.

        }
       
        if (Input.GetButtonDown("Fire2"))
        {
           gameObject.GetComponent<SpellCast>().castspell(currentSpellOne);
        }
        if (Input.GetButtonUp("Fire2"))
        {
            gameObject.GetComponent<SpellCast>().castspell(currentSpellOne + "_cancel");
        }
          
        
        if (Input.GetButtonDown("Fire3"))
        {

           
            gameObject.GetComponent<SpellCast>().castspell(currentSpellTwo);
        }
        if (Input.GetButtonUp("Fire3"))
        {
           gameObject.GetComponent<SpellCast>().castspell(currentSpellTwo + "_cancel" );
        }

       

       


	    }

      
      
    void FixedUpdate ()
	{

        anim.SetBool("grounded", grounded);
        anim.SetBool("wallUsed", wallUsed);
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        
        // if grounded reset jumps
        if (grounded)
        {
            jumpcount = 0;
        }
         // Is the player touching a wall, currently uses weapon point should probably added a new locator.


        if (wallUsed == false)
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 3.5f;
        }
        
        wallHit = Physics2D.Linecast(transform.position, weaponPoint.position, 1 << LayerMask.NameToLayer("Wall"));
                                
        // check to see if the last wall you hit was the same one, if it isnt update, if it is wallused = true
        if (wallHit)
        {
            newWallHit = wallHit.collider;
                                     
            // if you hit a new wall stick to the wall for float amount of time - this makes wall jump a lot easier                         
            if (newWallHit != oldWallHit)
            {
                oldWallHit = newWallHit;
                                
            }
            else
            {
                wallUsed = true;
            }
                  
        
            if (wallUsed && ! grounded)
            {
                gameObject.GetComponent<Rigidbody2D>().gravityScale = wallGrav;
                        
            }
         
        }
        
                
        // clear out the walled if you jump off the wall
        if (!wallHit )
        {
            wallUsed = false;
        }
        
        // if your on the wall not ground and haven't used walljump, you can jump
        if (wallHit && !grounded && wallUsed == false)
        {
         // this will ensure wall jumps always cost magic 
         
            jumpcount = jumps -1;
            wallUsed = true;
                  
         }

           

        // If the player should jump...
        if (jump)
        {
            
            
            // Set the Jump animator trigger parameter.
           

            // Play a random jump audio clip.
            //int i = Random.Range(0, jumpClips.Length);
            //AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

            // Add a vertical force to the player.

            // if this is the first frame of the jump times the force by the float
            if (boost == true)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce * jBoost));
                boost = false;
            }
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            
        }


        // horizontal movement


        // Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");

		// The Speed animator parameter is set to the absolute value of the horizontal input.
		anim.SetFloat("Speed", Mathf.Abs(h));

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed && MP.KnockBackStun == false)
			// ... add a force to the player.
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce );

		// If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed && MP.KnockBackStun == false)
			// ... set the player's velocity to the maxSpeed in the x axis.
			GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		//flip
        // If the input is moving the player right and the player is facing left...

        // if you are mid attack don't flip();
        if (!attacking.attk)
        {
            if (h > 0 && !facingRight)
                // ... flip the player.
                Flip();
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (h < 0 && facingRight)
                // ... flip the player.
                Flip();
        }
		
	}
	
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	public IEnumerator Taunt()
	{
		// Check the random chance of taunting.
		float tauntChance = Random.Range(0f, 100f);
		if(tauntChance > tauntProbability)
		{
			// Wait for tauntDelay number of seconds.
			yield return new WaitForSeconds(tauntDelay);

			// If there is no clip currently playing.
			if(!GetComponent<AudioSource>().isPlaying)
			{
				// Choose a random, but different taunt.
				tauntIndex = TauntRandom();

				// Play the new taunt.
				GetComponent<AudioSource>().clip = taunts[tauntIndex];
				GetComponent<AudioSource>().Play();
			}
		}
	}


	int TauntRandom()
	{
		// Choose a random index of the taunts array.
		int i = Random.Range(0, taunts.Length);

		// If it's the same as the previous taunt...
		if(i == tauntIndex)
			// ... try another random taunt.
			return TauntRandom();
		else
			// Otherwise return this index.
			return i;
	}
}
