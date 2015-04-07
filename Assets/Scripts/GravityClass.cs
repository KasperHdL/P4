using UnityEngine;
using System.Collections;

public class GravityClass : MonoBehaviour {


	public Transform bodyPrefab;
	public Transform body;
	public Transform sun;

	public GameObject linePrefab;
	public Transform lineContainer;
	public Transform[] lineObjects;


	public float gravitationalForce = 6;

	int lineOffset = 20;
	Vector3[] bodyPos;
	public int bodyCount = 0;

	// Use this for initialization
	void Start () {

		lineObjects = new Transform[5000];
		for(int i = 0;i<lineObjects.Length;i++){
			GameObject g = Instantiate(linePrefab,Vector3.zero,Quaternion.identity) as GameObject;
			g.transform.parent = lineContainer;
			lineObjects[i] = g.transform;
		}

		foreach(Transform child in transform)
			if(child.gameObject.name != "Sun")body = child;
			else sun = child;


		//calc gravy

		Vector3 prevPos = body.position;
		Vector3 prevVel = new Vector3(200f,0,-10);
		Vector3 prevAcc = gravitationalForceCalculator(body, sun) / body.GetComponent<Rigidbody>().mass;

		bodyPos = new Vector3[lineObjects.Length * lineOffset];

		for(int i = 0;i<lineObjects.Length*lineOffset;i++){


			float mass1 = body.GetComponent<Rigidbody>().mass;
			float mass2 = sun.GetComponent<Rigidbody>().mass;

			Vector3 pos2 = sun.transform.position;

			float distance = Mathf.Sqrt(Mathf.Pow((prevPos.x-pos2.x),2) +
										  Mathf.Pow((prevPos.y-pos2.y),2) +
										  Mathf.Pow((prevPos.z-pos2.z),2));

			Vector3 unitVector = ((prevPos-pos2)/distance);

			Vector3 force = -gravitationalForce*((mass1*mass2)/distance)*unitVector;
			Vector3 acc = force / body.GetComponent<Rigidbody>().mass;

			Vector3 vel = prevVel + acc;
			Vector3 pos = prevPos + vel;
			if(i%lineOffset == 0){

				lineObjects[i/lineOffset].rotation = Quaternion.Euler(0,Mathf.Atan2(vel.x,vel.z)/Mathf.PI * 180,0);
				lineObjects[i/lineOffset].position = pos - lineObjects[i/lineOffset].forward * lineObjects[i/lineOffset].localScale.z/2;
			}
			bodyPos[i] = pos;
			prevPos = pos;
			prevVel = vel;
			prevAcc = acc;
		}

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
		if(bodyCount < bodyPos.Length){
			body.position = bodyPos[bodyCount];
			bodyCount++;
		}

			//updateChildren();
	}

	public void update

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
		Vector3 pos1 = body1.transform.position;
		Vector3 pos2 = body2.transform.position;
		float distance = Mathf.Sqrt(Mathf.Pow((pos1.x-pos2.x),2) +
									  Mathf.Pow((pos1.y-pos2.y),2) +
									  Mathf.Pow((pos1.z-pos2.z),2));
		Vector3 unitVector = ((pos1-pos2)/distance);

		Vector3 force = -gravitationalForce*((mass1*mass2)/distance)*unitVector;


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
