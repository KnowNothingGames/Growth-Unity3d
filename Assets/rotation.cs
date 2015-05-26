using UnityEngine;
using System.Collections;

public class rotation : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Vector3 rot = new Vector3(0, 0, -45); 
        transform.Rotate(rot / 0.2f);
	}
	
	// Update is called once per frame
	void Update () {

        
       // Quaternion target = Quaternion.Euler(0, 0, 45);
       // gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, target, (Time.deltaTime / 1f));

               
	
	}
}
