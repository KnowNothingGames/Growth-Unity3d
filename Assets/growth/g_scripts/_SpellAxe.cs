﻿using UnityEngine;
using System.Collections;

public class _SpellAxe : MonoBehaviour {


    public BoxCollider2D axe;	
    public PlayerControl playerCtrl;
    public PlayerDamage MP;
    public float cost = 50f;
    private float lastattack = 0;
    
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



        if ((Time.time - lastattack) > 0.2 && MP.MP >= 50f)
        {
            MP.spellCost(50f);

            BoxCollider2D weaponInstance = Instantiate(axe, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as BoxCollider2D;



            if (playerCtrl.facingRight)
            {

                weaponInstance.rigidbody2D.AddForce(new Vector2(250f, 600f));
                weaponInstance.rigidbody2D.AddTorque(-1000f);

            }
            else
            {
                weaponInstance.rigidbody2D.AddForce(new Vector2(-250f, 600f));
                weaponInstance.rigidbody2D.AddTorque(-1000f);
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

                weaponInstance.rigidbody2D.AddForce(new Vector2(250f, 600f));
                weaponInstance.rigidbody2D.AddTorque(-1000f);

            }
            else
            {
                weaponInstance.rigidbody2D.AddForce(new Vector2(-250f, 600f));
                weaponInstance.rigidbody2D.AddTorque(-1000f);
            }


            lastattack = Time.time;

        }

    }




}
