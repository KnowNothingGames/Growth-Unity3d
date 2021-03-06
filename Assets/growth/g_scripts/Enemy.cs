﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float moveSpeed = 2f;		// The speed the enemy moves at.
	public int HP = 2;					// How many times the enemy can be hit before it dies.
	public Sprite deadEnemy;			// A sprite of the enemy when it's dead.
	public Sprite damagedEnemy;			// An optional sprite of the enemy when it's damaged.
	public AudioClip[] deathClips;		// An array of audioclips that can play when the enemy dies.
	public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.
	public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying
    
    
    // move sprite renderer out of enemy script
    //public SpriteRenderer Ene;


	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
	private bool dead = false;			// Whether or not the enemy is dead.
	private Score score;				// Reference to the Score script.

	
	void Awake()
	{
	
	}

	void FixedUpdate ()
	{
				
		
		
	}
	
	public void Hurt(int x)
	{
		        
        HP = HP - x;
        if (HP <= 0 && !dead)
        {
            Death();
        }

	}
	
	void Death()
	{
        Destroy(gameObject);
	}


	
}
