using UnityEngine;
using System.Collections;

public class _SpellAxe : MonoBehaviour {


    public BoxCollider2D axe;	
    public PlayerControl playerCtrl;
    public PlayerDamage MP;
    public float cost = 50f;
    // Use this for initialization
	void Start () {

        playerCtrl = gameObject.GetComponent<PlayerControl>();
        MP = gameObject.GetComponent<PlayerDamage>();


	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void cancel() { }

    public void castMe()
    {
        //  add spell here

        
        
        
        BoxCollider2D weaponInstance = Instantiate(axe, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as BoxCollider2D;
        
        
        // I want the location not the stickiness
        //weaponInstance.transform.parent = transform;

        if (playerCtrl.facingRight)
        {

            weaponInstance.rigidbody2D.AddForce(new Vector2(1000f, 1000f));
            //weaponInstance.rigidbody2D.AddTorque(500f);
        
        } 
        else
        {
            weaponInstance.rigidbody2D.AddForce(new Vector2(-1000f, 1000f));
           // weaponInstance.rigidbody2D.AddTorque(-500f);
        }
        
             
                  
    }


}
