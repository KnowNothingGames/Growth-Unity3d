﻿using UnityEngine;
using System.Collections;

public class SpellCast : MonoBehaviour {
    
    //register all spells here
	private _SpellImmolate _SpellImmolate;
    private _SpellShield _SpellShield;
    private _SpellAxe _SpellAxe;
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //  Spells calls
    public void castspell(string spellfound)
    {

        
                  
        
        
        
        if (spellfound == "_SpellImmolate")
        {
            gameObject.GetComponent<_SpellImmolate>().castMe();
        }
        else if (spellfound == "_SpellShield")
        {
            gameObject.GetComponent<_SpellShield>().castMe();
        } 
        else if (spellfound == "_SpellImmolate_cancel")
        {
            gameObject.GetComponent<_SpellImmolate>().cancel();
        }
        else if (spellfound == "_SpellShield_cancel")
        {
            gameObject.GetComponent<_SpellShield>().cancel();
        }
        else if (spellfound == "_SpellAxe")
        {
            gameObject.GetComponent<_SpellAxe>().castMe();
        }
        else if (spellfound == "_SpellAxe_cancel")
        {
            gameObject.GetComponent<_SpellAxe>().cancel();
        }



    }



}
