using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	// Use this for initialization


    public float xMargin = 1f;		// Distance in the x axis the player can move before the camera follows.
    public float yMargin = 1f;		// Distance in the y axis the player can move before the camera follows.
    public float smooth = 1f;
    public bool tracking = false;
    private Transform player;
    private Transform inter;
    private Rigidbody2D vel;
    public float speed = 10f;

    void Start () {


        vel = GameObject.FindGameObjectWithTag("Player").rigidbody2D;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = new Vector3(player.position.x, player.position.y, -10);
	
    }

    // Update is called once per frame
	void Update(){

        transform.position = new Vector3(player.position.x, player.position.y, -10);
      /// using simple trackin right now the lerp is fucked up
      //  track();
	}


    bool CheckXMargin()
    {
        // Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
        return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
    }


    bool CheckYMargin()
    {
        // Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
        return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
    }



    void track()
    {

        float speedy = Vector3.Distance(transform.position, player.position);
        speedy = Mathf.Abs(speedy) * speed;
        
        
        // adjust smoothness according to velocity mag
       
        if (CheckYMargin() || CheckXMargin())
        {
        Vector2 tmp = Vector2.MoveTowards(transform.position, player.position, (Time.deltaTime * speedy) /100);

        Debug.Log(tmp.x);
         transform.position = new Vector3(tmp.x, tmp.y, -10);
        
           


        }
       
       
       

    }

}
