using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour {

	public Vector3 randomStartVelocity = Vector3.zero;
	public Vector3 startVelocity;

	public Vector3[] positions;
	public Vector3[] velocities;

	public float density;
	public float volume;
	public float mass = 1;
	public float radius;


	public GameObject dotPrefab;

	//lines
	private Transform[] dots;
	private int dotOffset = 0;

	public bool randomColor = false;
	public Color color;

	private float rot = 0;
	public float rotSpeed;



	////////////////////////////
	//      Construction

	public void construct(){construct(mass,radius,transform.position,startVelocity);}
	public void construct(float mass, float radius, Vector3 position, Vector3 velocity){
		this.mass = mass;
		this.radius = radius;
		volume = ((4/3)*Mathf.PI)*Mathf.Pow(radius, 3);
		density = mass/volume;

		float dia = radius * 2;
		transform.localScale = new Vector3(dia,dia,dia);

		positions = new Vector3[Settings.BODY_POSITION_LENGTH];
		velocities = new Vector3[Settings.BODY_POSITION_LENGTH];


		if(randomStartVelocity != Vector3.zero){
			velocity = new Vector3(Random.Range(-randomStartVelocity.x,randomStartVelocity.x),Random.Range(-randomStartVelocity.y,randomStartVelocity.y),Random.Range(-randomStartVelocity.z,randomStartVelocity.z));
		}

		this.positions[0] = position;
		this.velocities[0] = velocity;

		transform.position = position;

		if(randomColor){
			color = new Color(Random.Range(.33f,1f),Random.Range(.33f,1f),Random.Range(.33f,1f));
		}

		GetComponent<Renderer>().material.color = color;
		dots = new Transform[(Settings.DOT_OFFSET == 0)?0:(Settings.BODY_POSITION_LENGTH/Settings.DOT_OFFSET)-1];
		for(int i = 0;i<dots.Length;i++){
			GameObject g = Instantiate(dotPrefab,positions[(i+1)*Settings.DOT_OFFSET],Quaternion.identity) as GameObject;
			g.transform.parent = GravitySystem.DOT_CONTAINER;
			dots[i] = g.transform;
			g.SetActive(false);

			g.GetComponent<Renderer>().material.color = color;
		}
	}




	//////////////////////////////
	//    Position Handling

	public void cyclePosition(Vector3 position, Vector3 velocity){
		//move body
		//transform.position = positions[1];
		transform.position = Vector3.Lerp(transform.position,positions[1],1-Time.deltaTime);

		rot += Time.deltaTime * rotSpeed;
		transform.rotation = Quaternion.Euler(0,rot,0);

		//shift array
		shiftPositions();

		//add position
		positions[positions.Length-1] = position;
		velocities[velocities.Length-1] = velocity;

		//update lines
		for(int i = 0;i<dots.Length;i++){
//			dots[i].rotation = Quaternion.Euler(0,Mathf.Atan2(velocities[i * Settings.DOT_OFFSET].x,velocities[i * Settings.DOT_OFFSET].x.z)/Mathf.PI * 180,0);
			int index = (i+1) * Settings.DOT_OFFSET - dotOffset;
			if(positions[index] != null){
				dots[i].position = positions[index];
				dots[i].gameObject.SetActive(true);
			}
		}

		dotOffset++;

		if(dotOffset >= Settings.DOT_OFFSET)
			dotOffset = 0;
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

	//////////////////////////////////
	//     Interaction Handling

	public void updateMass(float value){
		mass = value;
	}

	public void updateRadius(float value){
		radius = value;
		float dia = radius * 2;
		transform.localScale = new Vector3(dia,dia,dia);
	}

	public void updateVolume(float value){
		volume = value;
	}

	public void updateDensity(float value){
		density = value;
	}

}
