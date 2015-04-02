using UnityEngine;
using System.Collections;

public class GravityClass : MonoBehaviour {

	public Transform bodyPrefab; 
	public Transform body;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey (KeyCode.A)){
			Vector3 pos = new Vector3(Random.Range(0, 20),Random.Range(0, 20),Random.Range(0, 20));
			objectInstantiater(Random.Range(1, 10), pos, Random.Range(1, 10));
		}
	}

	/*That takes 3 parameters(float size, Vector3 pos, float mass) 

	Initializes an object and sets the 3 parameters 

	Sets the hierarchical(transform) parent of the object to be this objects transform*/

	public void objectInstantiater(float size, Vector3 pos, float mass){
		body = Object.Instantiate(bodyPrefab, pos, Quaternion.identity) as Transform;
		
		Vector3 scale = new Vector3(size, size, size);
		body.localScale = scale;
		body.GetComponent<Rigidbody>().mass = mass;

		body.parent = transform;
	}

}
