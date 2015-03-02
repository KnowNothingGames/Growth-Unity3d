using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

    public BoxCollider2D sword_collider;				// Prefab of the sword.
    public float speed = 20f;
    private float lastattack = 0;
    public PlayerDamage MP;
    public bool attk = false;
    public float nxtAttk = 0.3f;
	
    private Animator anim;
    public PlayerControl playerCtrl;

	// Use this for initialization
	void Start () {
		anim = transform.root.gameObject.GetComponent<Animator>();
        MP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamage>();
	}
	
	// Update is called once per frame
	void Update () {
        if (attk == true && (Time.time - lastattack) > nxtAttk) {
            attk = false;
        }
              
        // this may need to be moved to player controller and use a find like spellfind
        if(Input.GetButtonDown("Fire1"))
		{
			

           
           if ((Time.time - lastattack) > nxtAttk  && MP.MP >=25f)
            {
                MP.spellCost(25f);
                anim.SetTrigger("attack");
                attk = true;

                if (playerCtrl.facingRight)
                {

                    // not sure if I want to do raycast of collider here
                    BoxCollider2D weaponInstance = Instantiate(sword_collider, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as BoxCollider2D;
                    weaponInstance.transform.parent = transform;
                }
                else {

                    BoxCollider2D weaponInstance = Instantiate(sword_collider, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as BoxCollider2D;
                    weaponInstance.transform.parent = transform;
                
                }
                                        
               
               lastattack = Time.time;
                
                
            }
           
			
        
        
        }
	}
}