using UnityEngine;
using System.Collections;

public class _SpellShield : MonoBehaviour {
public PlayerDamage MP;
public bool on;
public float cost = 50f;
public float costDrain = 0.5f;
public CircleCollider2D shield_collider; 
   
    // Use this for initialization
	void Start () {

        MP = gameObject.GetComponent<PlayerDamage>();


	}
	
	// Update is called once per frame
	void Update () {


        // debt mp and stop regen
        if (on == true) {
            MP.spellCost(costDrain);
        
        // if not enough mana turn off   
        }
        if (MP.MP < costDrain)
        {
            on = false;

        }
        
        // destroy if no mana or cancel called
        if (on == false)
        {
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
