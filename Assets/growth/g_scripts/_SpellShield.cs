using UnityEngine;
using System.Collections;

public class _SpellShield : MonoBehaviour {
public PlayerDamage MP;
public bool on;
private bool wasOn;
public float cost = 50f;
public float costDrain = 0.5f;
public CircleCollider2D shield_collider; 
   
    // Use this for initialization
	void Start () {

        MP = gameObject.GetComponent<PlayerDamage>();
        // required to not interfere with other knockback invinc
        // this could become a problem when other invinc conditions start to apply
        // much like stun how to get different causes to not undo the same effect
        // I guess you could have an initate by feature.
        // also we should generat a new script called conditions

        wasOn = false;

	}
	
	// Update is called once per frame
	void Update () {

        
        // debt mp and stop regen
        if (on == true) {
            MP.spellCost(costDrain);
            MP.Invinc = true;
            wasOn = true;
            // if not enough mana turn off   
        }
        if (MP.MP < costDrain)
        {
            on = false;
           
        }
        
        // destroy if no mana or cancel called
        if (on == false)
        {
            // this will allow it to not interfere with invinc 
            if (wasOn == true) {
            MP.Invinc = false;
            wasOn = false;
            }
            
            Destroy(GameObject.Find("shield_collider(Clone)"));
        }





	}
    
    // required for all spells called by SpellCast
    public void cancel() {
        on = false;
       
    }
    
    
    
    // required for all spells called by SpellCast
    public void castMe(){

       if (on == false && MP.MP >= cost){ 

       CircleCollider2D shieldInstance = Instantiate(shield_collider, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as CircleCollider2D;
       shieldInstance.transform.parent = transform;
        MP.spellCost(cost);
        on = true;

       }

    }

   


}
