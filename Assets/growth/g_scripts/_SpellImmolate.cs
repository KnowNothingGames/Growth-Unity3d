using UnityEngine;
using System.Collections;

public class _SpellImmolate : MonoBehaviour
{
    private RaycastHit2D hit;
    public PlayerControl playerCtrl;
    public Object explosion;
    public PlayerDamage MP;
    public float cost = 100f;
    public int damage = 2;

    // Use this for initialization
    void Start()
    {

        playerCtrl = gameObject.GetComponent<PlayerControl>();
        MP = gameObject.GetComponent<PlayerDamage>();
        explosion = Resources.Load("explosion");
        // not sure how to do it for the explosion
          
    }

    // Update is called once per frame w physics
    void FixedUpdate()
    {

    }

    // spells need to have a cancel function wheter is does anything or not.
    public void cancel() { }

    public void castMe()
    {


        //raycast in direction get first hit layermask only gets things on Enemis layers notice the wierd syntax 1 << 9 etc
        if (playerCtrl.facingRight)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.right, 500f, 1 << LayerMask.NameToLayer("Enemies"));
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, -Vector2.right, 500f, 1 << LayerMask.NameToLayer("Enemies"));
        }

        if (hit.collider != null && MP.MP >= cost)
        {

            // if you have enough MP cast the spell
            MP.spellCost(cost);


            hit.collider.gameObject.GetComponent<Enemy>().Hurt(damage);

            // if killed burn it up
            if (hit.collider.gameObject.GetComponent<Enemy>().HP <= damage)
            {

                Instantiate(explosion, hit.collider.gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));

            }

        }
    }

   




}