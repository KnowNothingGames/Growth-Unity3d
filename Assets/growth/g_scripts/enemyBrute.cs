using UnityEngine;
using System.Collections;

// inherits from enemy
public class enemyBrute : Enemy {

    public Transform foot;
    public bool Wall;
    public bool Ground;
    public int x = 1;
    // Use this for initialization
	void Start () {

        

	}


    void FixedUpdate()
    {
        Ground = Physics2D.Linecast(transform.position, foot.position, 1 << LayerMask.NameToLayer("Ground"));
        Wall = Physics2D.Linecast(transform.position, foot.position, 1 << LayerMask.NameToLayer("Wall")); 
    }
	// Update is called once per frame
	void Update () {

               
        if (!Ground || Wall ){
            Flip();
            
        }
        if (Ground && !Wall)
        {
            transform.Translate(new Vector3(x,0,0) * Time.deltaTime);
        }
	}

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        x = x * -1;
        transform.localScale = theScale;
    }

}
