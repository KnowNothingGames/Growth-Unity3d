using UnityEngine;
using System.Collections;

public class _SpellAxe : MonoBehaviour {


    public BoxCollider2D axe;	
    public PlayerControl playerCtrl;
    public PlayerDamage MP;
    public float cost = 50f;
    private float lastattack = 0;
    public float torque = -1000f;
    public float xForce = 250f;
    public float yForce = 600f;
    public float bonusForce = 0f;
    public float bonusMultiply;
    // Use this for initialization

  
    
    void Start () {

        playerCtrl = gameObject.GetComponent<PlayerControl>();
        MP = gameObject.GetComponent<PlayerDamage>();
        

	}
	
	// Update is called once per frame
	void Update () {
         
	}

    void FixedUpdate()
    {     
    bonusForce = gameObject.GetComponent<Rigidbody2D>().velocity.x * bonusMultiply; 
    }



    public void cancel() { }

    public void castMe()
    {



        if ((Time.time - lastattack) > 0.2 && MP.MP >= cost)
        {
            MP.spellCost(cost);

            BoxCollider2D weaponInstance = Instantiate(axe, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as BoxCollider2D;
            


            if (playerCtrl.facingRight)
            {

                weaponInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce + bonusForce, yForce));
                weaponInstance.GetComponent<Rigidbody2D>().AddTorque(torque);

            }
            else
            {
                weaponInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(-xForce + bonusForce, yForce));
                weaponInstance.GetComponent<Rigidbody2D>().AddTorque(torque);
            }


            lastattack = Time.time;

        }  
                  
    }


    public void castAxe()
    {



        if ((Time.time - lastattack) > 0.2 && MP.MP >= 50f)
        {
           
            
            MP.spellCost(50f);
            


            BoxCollider2D weaponInstance = Instantiate(axe, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as BoxCollider2D;
            


            if (playerCtrl.facingRight)
            {

                weaponInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce + bonusForce, yForce));
                weaponInstance.GetComponent<Rigidbody2D>().AddTorque(torque);

            }
            else
            {
                weaponInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(-xForce + bonusForce, yForce));
                weaponInstance.GetComponent<Rigidbody2D>().AddTorque(torque);
            }


            lastattack = Time.time;

        }

    }




}
