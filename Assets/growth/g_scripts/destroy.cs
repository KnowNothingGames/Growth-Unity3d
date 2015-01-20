using UnityEngine;
using System.Collections;

public class destroy : MonoBehaviour {

	public float killtime = 2f;
    
    // Use this for initialization
	void Start () {

      Destroy(gameObject, killtime);
    
    }
	
	// Update is called once per frame
	void Update () {
	
        


	}
}
