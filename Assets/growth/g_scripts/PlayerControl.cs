using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
    private int jumpcount = 0;              // Counts jumps until grounded
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
	private bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.
    public string currentSpellOne = "_spellNull";
    public string currentSpellTwo = "_spellNull";
    public PlayerDamage MP;

	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		anim = GetComponent<Animator>();
        SpellFind();
    }


    
    // search components for script that starts with "_Spell" reserveed for spells
    // currently only look for two spells
    void SpellFind()
    {
     Component[] find = GetComponents(typeof(Component));

     foreach (Component item in find){
    

         string spellfound = item.GetType().Name;

         Debug.Log(spellfound);
         
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
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  

		// if grounded reset jumps
        if (grounded)
        { 
            jumpcount = 0;
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


        // If the jump button is pressed and the player is grounded then the player should jump. Or is they havent used all their jmups
        if (Input.GetButtonDown("Jump") && grounded || Input.GetButtonDown("Jump") && jumpcount < jumps && MP.MP >= 50f)
            
            jump = true;
	}

      
            
           
    
    
    
    
    void FixedUpdate ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");

		// The Speed animator parameter is set to the absolute value of the horizontal input.
		anim.SetFloat("Speed", Mathf.Abs(h));

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * rigidbody2D.velocity.x < maxSpeed)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * h * moveForce);

		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);

		// If the input is moving the player right and the player is facing left...
		if(h > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			// ... flip the player.
			Flip();

		// If the player should jump...
		if(jump) 
		{
			// Set the Jump animator trigger parameter.
			anim.SetTrigger("Jump");

			// Play a random jump audio clip.
			//int i = Random.Range(0, jumpClips.Length);
			//AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));


            jump = false;       
            
            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            if (jumpcount > 0) { MP.spellCost(50f); }

            jumpcount += 1;
            
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
			if(!audio.isPlaying)
			{
				// Choose a random, but different taunt.
				tauntIndex = TauntRandom();

				// Play the new taunt.
				audio.clip = taunts[tauntIndex];
				audio.Play();
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
