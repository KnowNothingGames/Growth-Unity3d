using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {
    private RaycastHit2D hit;
    public PlayerControl playerCtrl;
    public GameObject explosion;
    public int damage = 2;
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        
        if (Input.GetButtonDown("Fire2"))
        {

            
            //raycast in direction get first hit layermask only gets things on Enemis layers notice the wierd syntax 1 << 9 etc
            if (playerCtrl.facingRight)
            {
                hit = Physics2D.Raycast(transform.position, Vector2.right, 100f, 1 << LayerMask.NameToLayer("Enemies"));
            } else {
                hit = Physics2D.Raycast(transform.position, -Vector2.right, 100f, 1 << LayerMask.NameToLayer("Enemies"));
            }

            if (hit.collider != null)
            {

               hit.collider.gameObject.GetComponent<Enemy>().Hurt(damage);
               
                // if killed burn it up
                if (hit.collider.gameObject.GetComponent<Enemy>().HP == 0)
                      
               
               {
                    Instantiate(explosion, hit.collider.gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
               }
            }


        }

	}
}
