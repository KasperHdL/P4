using UnityEngine;
using System.Collections;

public class BodyController : MonoBehaviour {

	public Body body;

	public RadiusSlider radiusSlider;
	public MassSlider massSlider;

	// Use this for initialization
	void Start () {
		radiusSlider.controller = this;
		massSlider.controller = this;
	}

	// Update is called once per frame
	void Update () {

	}

	public void updateMass(float value){
		body.updateMass(value);
	}

	public void updateRadius(float value){
		body.updateRadius(value);
	}
}
