using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;

public class GravitySystem : MonoBehaviour {

	public GameObject bodyPrefab;

	public static Transform DOT_CONTAINER;

	public List<Body> bodies;

	private bool inited = false;

	public bool reset = false;
	private bool calcRunning = false;

	public bool uiHold = false;

	public int frameShifts = 0;


	// Use this for initialization
	void Start () {

		DOT_CONTAINER = GameObject.Find("DotContainer").transform;

		if(transform.childCount == 0){
			//initNewBodies(10);
		}
		else
			initChildrenBodies();

		inited = true;
	}

	public void initChildrenBodies(){
		bodies = new List<Body>(transform.childCount);

		int i = 0;
		foreach(Transform child in transform){
			Body b = child.GetComponent<Body>() as Body;
			b.construct();
			bodies.Add(b);
		}
		calcFuturePositions();
	}

	public void reInitChildren(){
		foreach(Body body in bodies){
			body.construct();
			Debug.Log("bPos " + body.positions[0] +", " + body.positions[1]);
		}
		calcFuturePositions();
	}

	public void initNewBodies(int numBodies){
		bodies = new List<Body>(numBodies);

		float rnd = 2000f;

		for(int i = 0;i<bodies.Count;i++){
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

	public void addBody(Body b){
		b.construct();
		bodies.Add(b);
		calcFuturePositions();
	}

	public void calcFuturePositions(){
		StopCoroutine("calculateFuturePositions");
		StartCoroutine("calculateFuturePositions");
	}

	private IEnumerator calculateFuturePositions(){
		//populate positions of bodies
		calcRunning = true;
		frameShifts = 0;
		int f = 1;
		while(!reset && f<Settings.BODY_POSITION_LENGTH + frameShifts){
			for(int i = 0;i<bodies.Count;i++){
				Vector3 acc = Vector3.zero;

				int tf = f-frameShifts;

				for(int j = 0;j<bodies.Count;j++){
					if(i==j)continue;
					acc += calculateGravitional(bodies[i],bodies[j],tf);
				}

				Vector3 prevVel = bodies[i].velocities[tf-1];
				Vector3 prevPos = bodies[i].positions[tf-1];

				Vector3 vel = prevVel + acc;
				Vector3 pos = prevPos + vel;


				bodies[i].addPropAtIndex(pos,vel,tf);
			}
			f++;
			if(f%10==0)
				yield return null;
		}
		calcRunning = false;
		if(reset){
			reset = false;
			calcFuturePositions();
		}
	}

	// Update is called once per frame
	void Update () {
		if(inited && !uiHold)
			updateBodies();

			//updateChildren();
		for(int i = 0; i < bodies.Count; i++){

		}

	}

	public void updateBodies(){
		for(int t = 0;t < Time.timeScale*10;t++){
			frameShifts ++;
			for(int i = 0;i<bodies.Count;i++){
				Vector3 acc = Vector3.zero;
				for(int j = 0;j<bodies.Count;j++){
					if(i==j)continue;

					acc += calculateGravitional(bodies[i],bodies[j]);

				}

				Vector3 vel = bodies[i].velocities[bodies[i].positions.Length-1] + acc;
				Vector3 pos = bodies[i].positions[bodies[i].positions.Length-1] + vel;
				bodies[i].cyclePosition(pos,vel);
			}
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

		if(distance == 0){
			Debug.LogWarning("Distance is zero - what is the chances");
			return Vector3.zero;
		}

		float force = Settings.GRAVITATIONAL_CONSTANT * ((effected.mass * effector.mass) / distance);
		Vector3 forceVector = delta.normalized * force;
		return forceVector / effected.mass;
	}

}
