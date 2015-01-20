using UnityEngine;
using System.Collections;

public class Follow: MonoBehaviour {

	// Use this for initialization


    public float xMargin = 1f;		// Distance in the x axis the player can move before the camera follows.
    public float yMargin = 1f;		// Distance in the y axis the player can move before the camera follows.
    public float smooth = 1f;
    private Transform player;
    private Transform inter;
    private Rigidbody2D vel;


    void Awake () {


         player = GameObject.FindGameObjectWithTag("Player").transform;
        
	
    }

    // Update is called once per frame
	void Update(){

        track();
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


        

        float cameraX = transform.position.x;
        float cameraY = transform.position.y;

        float playerX = player.position.x;
        float playerY = player.position.y;
        //float smoother;
        
        
        // adjust smoothness according to velocity mag
        //smoother = smooth * vel.velocity.magnitude;
        
        if (CheckYMargin() || CheckXMargin())
        {
                  
            
           
           //  smoother 
           float xNew = Mathf.Lerp(cameraX, playerX,  Time.deltaTime * smooth);
           float yNew = Mathf.Lerp(cameraY, playerY, Time.deltaTime * smooth);
            
           
           transform.position = new Vector3(xNew, yNew, 0);


        }
        

    }

}
