﻿using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class Body : MonoBehaviour {
	////////////////////////////
	//      Sound Variables

	public 	AudioSource sound;
	public 	AudioClip clip;

	public 	float currentVolume;
	public 	float currentPitch;
	private float baseVolume;
	private float basePitch;

	public enum Type{
		None,
		Planet,
		DwarfStar
	}

	public Transform starLightTransform;
	public Light starLight;

	////////////////////////////
	//      Type Variables

	public Type type = Type.Planet;
	public float temperature;
	public float mass;
	public float density;
	public float radius;


	private float tempLightOffset;
	public float tempLightOffsetAmount = 700;

	////////////////////////////
	//      Body Variables

	public Vector3 randomStartVelocity = Vector3.zero;
	public Vector3 startVelocity;

	public Vector3[] positions;
	public Vector3[] velocities;

	public int indicesValid = 0;



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

//		Debug.Log(string.Format("m: {0} r: {1} p{2} v{3}",mass,radius,position,velocity));
		this.mass = mass;
		this.radius = radius;
		density = 1;

		positions = new Vector3[Settings.BODY_POSITION_LENGTH];
		velocities = new Vector3[Settings.BODY_POSITION_LENGTH];


		if(randomStartVelocity != Vector3.zero){
			velocity = new Vector3(	Random.Range(-randomStartVelocity.x,randomStartVelocity.x),
									Random.Range(-randomStartVelocity.y,randomStartVelocity.y),
									Random.Range(-randomStartVelocity.z,randomStartVelocity.z));
		}

		this.positions[0] = position;
		this.velocities[0] = velocity;

		transform.position = position;

		if(randomColor){
			color = new Color(Random.Range(.33f,1f),Random.Range(.33f,1f),Random.Range(.33f,1f));
			GetComponent<Renderer>().material.color = color;
		}

		dots = new Transform[(Settings.DOT_OFFSET == 0)?0:(Settings.BODY_POSITION_LENGTH/Settings.DOT_OFFSET)-1];
		for(int i = 0;i<dots.Length;i++){
			GameObject g = Instantiate(dotPrefab,positions[(i+1)*Settings.DOT_OFFSET],Quaternion.identity) as GameObject;
			g.transform.parent = GravitySystem.DOT_CONTAINER;
			dots[i] = g.transform;
			g.SetActive(false);

			g.GetComponent<Renderer>().material.color = color;
		}

		///////////////////
		//		SOUND
		sound.clip = clip;
		if(sound.enabled)
			sound.Play();
		sound.loop = true;

		baseVolume = Settings.BASE_VOLUME;
		basePitch = Settings.BASE_PITCH;

	}


	public void setType(Body.Type type){
		this.type = type;
		switch(type){
			case Type.Planet:
				GetComponent<Renderer>().enabled = true;
				starLight.enabled = false;
			break;
			case Type.DwarfStar:
				GetComponent<Renderer>().enabled = false;
				starLight.enabled = true;
			break;
		}
	}



	//////////////////////////////
	//    Position Handling

	public void cyclePosition(Vector3 position, Vector3 velocity){
		//move body
		transform.position = positions[1];
		//transform.position = Vector3.Lerp(transform.position,positions[1],1-Time.deltaTime);

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

			dots[i].position = positions[index];
			dots[i].gameObject.SetActive(true);

		}

		dotOffset++;

		if(dotOffset >= Settings.DOT_OFFSET)
			dotOffset = 0;
	}

	public void addPropAtIndex(Vector3 pos, Vector3 vel, int index){
		positions[index] = pos;
		velocities[index] = vel;
		indicesValid = index;
	}

	private void shiftPositions(){
		for(int i = 0;i<positions.Length-1;i++)
			positions[i] = positions[i+1];

		for(int i = 0;i<velocities.Length-1;i++)
			velocities[i] = velocities[i+1];
	}

	//////////////////////////////////
	//     Interaction Handling

	public void updateMass(double value){
		mass = (float)(value);
		sound.pitch = (mass/Settings.Planet.MASS_MAX_VALUE)*(1-basePitch) + basePitch;
		currentPitch = (mass/Settings.Planet.MASS_MAX_VALUE)*(1-basePitch) + basePitch;
	}

	public void updateRadius(float value){
		radius = value * (type == Type.Planet ? 1:100);
		float dia = (radius * 2);
		if(type == Type.DwarfStar)
			if(radius < 40)
				transform.localScale = new Vector3(dia*3,1,dia*3);
			else if(radius < 100)
				transform.localScale = new Vector3(dia*1.5f,1,dia*1.5f);
			else
				transform.localScale = new Vector3(dia,1,dia);

		else
			transform.localScale = new Vector3(dia,dia,dia);

		calculateTemperatureOffset();

		if(type == Type.Planet){
			sound.volume = (radius/Settings.Planet.RADIUS_MAX_VALUE)*(1-baseVolume) + baseVolume;
			currentVolume = (radius/Settings.Planet.RADIUS_MAX_VALUE)*(1-baseVolume) + baseVolume;
		} else if(type == Type.DwarfStar){
			sound.volume = ((radius/100)/Settings.Star.Dwarf.RADIUS_MAX_VALUE)*(1-baseVolume) + baseVolume;
			currentVolume = ((radius/100)/Settings.Star.Dwarf.RADIUS_MAX_VALUE)*(1-baseVolume) + baseVolume;
		}

	}

	public void updateDensity(float value){
		density = value;
	}

	public void updateTemperature(float value){
		temperature = value;

		calculateTemperatureOffset();

		if(type != Type.DwarfStar)
			return;

		for(int i = 1;i<Settings.Star.Dwarf.TEMPERATURE.Length;i++){
			float ct = Settings.Star.Dwarf.TEMPERATURE[i];
			float lt = Settings.Star.Dwarf.TEMPERATURE[i-1];
			if(value < ct && value >= lt){
				//found
				float step = (value-lt)/(ct-lt);
				starLight.color = Color.Lerp(Settings.Star.Dwarf.COLORS[i-1],Settings.Star.Dwarf.COLORS[i],step);
				break;
			}
		}
	}

	private void calculateTemperatureOffset(){
		if(radius < 100)
			tempLightOffsetAmount = -5;
		else
			tempLightOffsetAmount = (radius/100) * -5;

		if(temperature <= 6000){
			tempLightOffset = 1-((6000 - temperature)/(6000 - 2400)) * tempLightOffsetAmount;
		}else{
			tempLightOffset = 1-((temperature - 6000)/(40000 - 6000)) * tempLightOffsetAmount;
		}
		starLightTransform.localPosition = new Vector3(0,radius/7 + (type == Type.DwarfStar ? tempLightOffset: 1),0);
	}
}
