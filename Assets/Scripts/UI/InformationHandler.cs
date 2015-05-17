﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InformationHandler : MonoBehaviour {

	public 	Text text;
	public 	Text infoText;

	private Body body;
	private RectTransform rt;
	private float boxWidth;

	// Use this for initialization
	void Start () {
		rt 				= transform as RectTransform;
		boxWidth		= rt.rect.width;
	}

	// Update is called once per frame
	void Update () {
		if(body != null){
			setText();
			if(body.type == Body.Type.Planet)
				rt.sizeDelta = new Vector2(boxWidth, 55f);
			else
				rt.sizeDelta = new Vector2(boxWidth, 90f);
		}
	}

	public void setText(){
		if(body != null){
			if(body.type == Body.Type.DwarfStar){
				text.text =
				"Type:"			+
				"\nClass:"		+
				"\nTemperature:"	+
				"\nRadius:"		+
				"\nRadius:"		+
				"\nMass:"			+
				"\nMass:"
				;

				infoText.text =
						"Dwarf Star" 						+
				"\n" + 	body.classification							+
				"\n" + 	body.temperature.ToString("F0") 			+ " kelvin" +
				"\n" + 	(body.radius/Settings.EARTH_RADIUS_TO_SUN).ToString("F3") 			+ " suns" +
				"\n" + 	body.radius.ToString("F0") 					+ " earths" +
				"\n" + 	(body.mass/Settings.EARTH_MASS_TO_SUN).ToString("F3") 			+ " suns" 	+
				"\n" + 	body.mass.ToString("F0")					+ " earths"
				;
			} else{
				text.text =
				"Type:" 			+
				"\nRadius:" 		+
				"\nRadius:" 		+
				"\nMass:"
				;

				infoText.text =
						"Planet" 						+
				"\n" + 	(body.radius/Settings.EARTH_RADIUS_TO_SUN).ToString("F3") 								+ " suns" +
				"\n" + 	body.radius.ToString("F1") 								+ " earths" +
				"\n" + 	body.mass.ToString("F1")									+ " earths"
				;
			}


		} else
			Debug.LogError("INFORMATIONHANDLER: BODY IS NOT INSTANTIATED");
	}

	public void setBody(Body body){
		this.body = body;
	}
}
