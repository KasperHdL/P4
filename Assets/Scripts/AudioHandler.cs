using UnityEngine;
using System.Collections;

public class AudioHandler : MonoBehaviour {

	public GravitySystem gs;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < gs.bodies.Count; i++){
			if(gs.bodies[i].type == Body.Type.DwarfStar){
				volumeOscillation(gs.bodies[i]);
			}
		}
	}

	public void volumeOscillation(Body body){
		float oscillationSpeed = (body.temperature/Settings.Star.Dwarf.TEMPERATURE[Settings.Star.Dwarf.TEMPERATURE.Length-1])*20;
		float x = (Mathf.Sin(Time.time*oscillationSpeed)+2)/3;
//		Debug.Log(x);
		body.sound.volume = body.currentVolume*x;
		//Debug.Log(body.sound.volume);
	}
}
