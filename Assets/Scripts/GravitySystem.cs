using UnityEngine;
using System.Collections;

public class GravitySystem : MonoBehaviour {

	public GameObject bodyPrefab;

	public static Transform DOT_CONTAINER;

	public Body[] bodies;

	private bool inited = false;


	// Use this for initialization
	void Start () {

		DOT_CONTAINER = GameObject.Find("DotContainer").transform;

		if(transform.childCount == 0)
			initNewBodies(10);
		else
			initChildrenBodies();

		calculateFuturePositions();
		inited = true;
	}

	public void initChildrenBodies(){
		bodies = new Body[transform.childCount];

		int i = 0;
		foreach(Transform child in transform){
			Body b = child.GetComponent<Body>() as Body;
			b.construct();
			bodies[i++] = b;
		}
	}

	public void initNewBodies(int numBodies){
		bodies = new Body[numBodies];

		float rnd = 2000f;

		for(int i = 0;i<bodies.Length;i++){
			GameObject g = Instantiate(bodyPrefab,Vector3.zero,Quaternion.identity) as GameObject;
			int mass = 100000;
			float rad = Random.Range(10,1000);
			Vector3 pos = new Vector3(Random.Range(-rnd,rnd),Random.Range(-rnd,rnd),Random.Range(-rnd,rnd));

			g.transform.parent = transform;

			Body b = g.GetComponent<Body>() as Body;
			b.construct(mass,rad, pos, Vector3.zero);
			bodies[i] = b;
		}
	}

	public void calculateFuturePositions(){
		//populate positions of bodies
		for(int f = 1;f < Settings.BODY_POSITION_LENGTH;f++){
			for(int i = 0;i<bodies.Length;i++){
				Vector3 acc = Vector3.zero;
				for(int j = 0;j<bodies.Length;j++){
					if(i==j)continue;

					acc += calculateGravitional(bodies[i],bodies[j],f);

				}

				Vector3 prevVel = bodies[i].velocities[bodies[i].velocities.Length-1];
				Vector3 prevPos = bodies[i].positions[bodies[i].positions.Length-1];

				Vector3 vel = prevVel + acc;
				Vector3 pos = prevPos + vel;

				bodies[i].addPropAtIndex(pos,vel,f);
			}
		}
	}

	// Update is called once per frame
	void Update () {

		if(inited)
			updateBodies();


			//updateChildren();
	}

	public void updateBodies(){
		for(int i = 0;i<bodies.Length;i++){
			Vector3 acc = Vector3.zero;
			if(bodies[i].freezePosition)continue;
			for(int j = 0;j<bodies.Length;j++){
				if(i==j)continue;

				acc += calculateGravitional(bodies[i],bodies[j]);

			}

			Vector3 vel = bodies[i].velocities[bodies[i].positions.Length-1] + acc;
			Vector3 pos = bodies[i].positions[bodies[i].positions.Length-1] + vel;
			bodies[i].cyclePosition(pos,vel);
		}
	}

	public Vector3 calculateGravitional(Body effected, Body effector,int index = -1){
		if(index == 0 || index > Settings.BODY_POSITION_LENGTH){
			Debug.LogError("invalid index: out of range");
			return Vector3.zero;
		}else if(index == -1){
			index = effected.positions.Length;
		}

		Vector3 delta = effector.positions[index-1] - effected.positions[index-1];

		float distance = delta.magnitude;
		if(distance == 0)return Vector3.zero;
		Vector3 unitVector = delta.normalized;

		Vector3 force = Settings.GRAVITATIONAL_CONSTANT * ((effected.mass * effector.mass) / distance) * unitVector;

		return force / effected.mass;
	}

}
