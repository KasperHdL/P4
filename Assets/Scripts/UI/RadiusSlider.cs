using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadiusSlider : SliderWithText {

	public new void Start(){
		base.Start();
		text.text = value + " Mm";

		maxValue = Settings.RADI_MAX_VAL;
		minValue = Settings.RADI_MIN_VAL;
	}

	public override void OnDrag(PointerEventData eventData){
		controller.updateRadius(value);
		base.OnDrag(eventData);
	}

	public void setText(double val){
		/*
		10e3 Meter m
		10e6 Kilometer km
		10e9 Megameter Mm
		10e12 Gigameter Gm
		10e15 Terrameter Tm
		*/

		if(val < Mathf.Pow(10,6)){
			text.text = valueFormating(3, 2, val) + " km";
		} else if(val < Mathf.Pow(10, 9)){
			text.text = valueFormating(6, 2, val) + "E+3 km";
		} else if(val < Mathf.Pow(10, 12)){
			text.text = valueFormating(9, 2, val) + "E+6 km";
		}
	}
}
