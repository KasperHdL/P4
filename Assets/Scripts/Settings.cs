using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {

	public readonly static int BODY_POSITION_LENGTH = 1000;
	public readonly static int DOT_OFFSET = 0;

	public readonly static float GRAVITATIONAL_CONSTANT = 5f;

	//PLANET SLIDER VALUES:
	public readonly static float MASS_MAX_VAL = 250.0f*(1.988f*Mathf.Pow(10,30)); // kg
	public readonly static float MASS_MIN_VAL = 3.0f*Mathf.Pow(10,11); // kg

	public readonly static float RADI_MAX_VAL = 1.708f*696000f*1000; // Metres
	public readonly static float RADI_MIN_VAL = 1.6f*1000;	// Metres
}

