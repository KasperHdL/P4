using UnityEngine;
using System.Collections;

public class GravityClass : MonoBehaviour {

	public GameObject bodyPrefab;

	public static Transform DOT_CONTAINER;

	public Body[] bodies;


	// Use this for initialization
	void Start () {

		DOT_CONTAINER = GameObject.Find("DotContainer").transform;

		init();
	}

	public void init(){
		bodies = new Body[100];

		float rnd = 2000f;

		for(int i = 0;i<bodies.Length;i++){
			GameObject g = Instantiate(bodyPrefab,Vector3.zero,Quaternion.identity) as GameObject;
			float mass = Random.Range(10,2000);
			Vector3 pos = new Vector3(Random.Range(-rnd,rnd),Random.Range(-rnd,rnd),Random.Range(-rnd,rnd));
			//Vector3 vel = new Vector3(Random.Range(-100,100),Random.Range(-100,100),Random.Range(-100,100));
			Debug.Log(g);
			Body b = g.GetComponent<Body>() as Body;
			b.construct(mass, mass, pos, Vector3.zero);
			bodies[i] = b;
		}

		//populate positions of bodies
		for(int f = 1;f < Settings.BODY_POSITION_LENGTH;f++){
			for(int i = 0;i<bodies.Length;i++){
				Vector3 acc = Vector3.zero;
				for(int j = 0;j<bodies.Length;j++){
					if(i==j)continue;

					acc += calculateGravitional(bodies[i],bodies[j],f);

				}

				Vector3 vel = bodies[i].velocities[bodies[i].positions.Length-1] + acc;
				Vector3 pos = bodies[i].positions[bodies[i].positions.Length-1] + vel;
				bodies[i].addPropAtIndex(pos,vel,f);
			}
		}

	}

	// Update is called once per frame
	void Update () {


		updateBodies();


			//updateChildren();
	}

	public void updateBodies(){
		for(int i = 0;i<bodies.Length;i++){
			Vector3 acc = Vector3.zero;
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
		Vector3 unitVector = delta.normalized;

		Vector3 force = Settings.GRAVITATIONAL_CONSTANT * ((effected.mass * effector.mass) / distance) * unitVector;

		return force / effected.mass;
	}

}
