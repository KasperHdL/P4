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
		if(Input.GetKeyDown (KeyCode.A)){
			for(int i = 0; i < 2; i++){
				Vector3 pos = new Vector3(Random.Range(0, 20),Random.Range(0, 20),Random.Range(0, 20));
				objectInstantiater(Random.Range(1, 10), pos, Random.Range(1, 10));
			}

			Debug.Log(transform.childCount);
		}

			updateChildren();
	}

	public void updateChildren(){

		for(int i = 0;i<transform.childCount;i++){
			for(int j = 0;j<transform.childCount;j++){
				if(i==j)continue;
				Vector3 force = gravitationalForceCalculator(transform.GetChild(i), transform.GetChild(j));
				Vector3 acc = force/transform.GetChild(i).GetComponent<Rigidbody>().mass;
				transform.GetChild(i).GetComponent<Rigidbody>().AddForce(acc);
			}
		}
	}

	public Vector3 gravitationalForceCalculator(Transform body1, Transform body2){

		float mass1 = body1.GetComponent<Rigidbody>().mass;
		float mass2 = body2.GetComponent<Rigidbody>().mass;
		float g = 6;
		Vector3 pos1 = body1.transform.position;
		Vector3 pos2 = body2.transform.position;
		float distance = Mathf.Sqrt(Mathf.Pow((pos1.x-pos2.x),2) +
									  Mathf.Pow((pos1.y-pos2.y),2) +
									  Mathf.Pow((pos1.z-pos2.z),2));
		Vector3 unitVector = ((pos1-pos2)/distance);

		Vector3 force = -g*((mass1*mass2)/distance)*unitVector;


		return force;

	}

	public Vector3 accelerationVector(Transform b, Vector3 force){
		/*a = f/m*/
		float mass = b.GetComponent<Rigidbody>().mass;
		Vector3 accVec = force/mass;

		return accVec;
	}

	public void objectInstantiater(float size, Vector3 pos, float mass){
		body = Object.Instantiate(bodyPrefab, pos, Quaternion.identity) as Transform;

		Vector3 scale = new Vector3(size, size, size);
		body.localScale = scale;
		body.GetComponent<Rigidbody>().mass = mass;

		body.parent = transform;
	}

}
