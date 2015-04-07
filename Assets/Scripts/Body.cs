using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour {

	public Vector3[] positions;
	public Vector3[] velocities;

	public float mass;
	public float radius;


	public GameObject dotPrefab;

	//lines
	Transform[] dots;

	public void construct(float mass, float radius, Vector3 position, Vector3 velocity){
		this.mass = mass;
		this.radius = radius;

		float dia = radius * 2;
		transform.localScale = new Vector3(dia,dia,dia);

		positions = new Vector3[Settings.BODY_POSITION_LENGTH];
		velocities = new Vector3[Settings.BODY_POSITION_LENGTH];


		this.positions[0] = position;
		this.velocities[0] = velocity;

		transform.position = position;

		dots = new Transform[positions.Length/Settings.DOT_OFFSET];
		for(int i = 0;i<dots.Length;i++){
			GameObject g = Instantiate(dotPrefab,positions[i*Settings.DOT_OFFSET],Quaternion.identity) as GameObject;
			g.transform.parent = GravityClass.DOT_CONTAINER;
			dots[i] = g.transform;
		}

	}

	public void cyclePosition(Vector3 position, Vector3 velocity){
		//move body
		transform.position = positions[1];

		//shift array
		shiftPositions();

		//add position
		positions[positions.Length-1] = position;
		velocities[velocities.Length-1] = velocity;

		//update lines
		//TODO add velocity rotation perhaps?
		for(int i = 0;i<dots.Length;i++){
			//lineObjects[i/lineOffset].rotation = Quaternion.Euler(0,Mathf.Atan2(vel.x,vel.z)/Mathf.PI * 180,0);
			dots[i].position = positions[i * Settings.DOT_OFFSET];
		}
	}

	public void addPropAtIndex(Vector3 pos, Vector3 vel, int index){
		positions[index] = pos;
		velocities[index] = vel;
	}

	private void shiftPositions(){
		for(int i = 0;i<positions.Length-1;i++)
			positions[i] = positions[i+1];

		for(int i = 0;i<velocities.Length-1;i++)
			velocities[i] = velocities[i+1];
	}
}
