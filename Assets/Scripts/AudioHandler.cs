using UnityEngine;
using System.Collections;

public class AudioHandler : MonoBehaviour {

	public GravitySystem gs;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void volumeOscillation(Body body){
		float oscillationSpeed = (body.temperature/100)/Settings.Star.Dwarf.TEMPERATURE[Settings.Star.Dwarf.TEMPERATURE.Length-1];
		float x = Mathf.Sin(Time.time*oscillationSpeed)*0.2f;
		body.sound.volume = body.currentVolume*x;
		Debug.Log(body.sound.volume);
	}
}
