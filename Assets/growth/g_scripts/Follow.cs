using UnityEngine;
using System.Collections;

public class Follow: MonoBehaviour {

	// Use this for initialization


    public float xMargin = 1f;		// Distance in the x axis the player can move before the camera follows.
    public float yMargin = 1f;		// Distance in the y axis the player can move before the camera follows.
    public float smooth = 0.5f;
    private Transform player;
    private Transform inter;
    //private Rigidbody2D vel;
    public float moveForce = 365f;
    public bool Move = false;

    private float xKnock = 5000f;
    private float yKnock = 5000f;
    public float knockForce;
    //changed to translate lerp
    
    void Awake () {


         player = GameObject.FindGameObjectWithTag("Player").transform;
        
	
    }

    // Update is called once per frame
	void FixedUpdate(){

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
        float h = Input.GetAxis("Horizontal");



        float cameraX = transform.position.x;
        float cameraY = transform.position.y;

        float playerX = player.position.x;
        float playerY = player.position.y;
        //float smoother;



        // move using addfroce
        Vector2 playerVec = GameObject.FindGameObjectWithTag("Player").transform.position - gameObject.transform.position;
        //  rigidbody2D.AddForce(playerVec * moveForce);



        // move using transfrom
        float xNew = Mathf.Lerp(cameraX, playerX, Time.deltaTime * smooth);
        float yNew = Mathf.Lerp(cameraY, playerY, Time.deltaTime * smooth);

      //  transform.position = new Vector3(xNew, yNew, 0);
        

        // using move position to move characters
        Vector2 speed = new Vector2(3, 0);
        GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position + speed * Time.deltaTime);
        // adjust smoothness according to velocity mag
        //smoother = smooth * vel.velocity.magnitude;

    }


    

        

    }     
            
          
   

    

