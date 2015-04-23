using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MassSlider : SliderWithText {

	public new void Start(){
		base.Start();
		text.text = value + " kg";

		maxValue = Settings.MASS_MAX_VAL;
		minValue = Settings.MASS_MIN_VAL;
	}

	public override void OnDrag(PointerEventData eventData){
		controller.updateMass(value);
		base.OnDrag(eventData);
	}

	public void setText(double val){
		string value = val.ToString();
		if(val < 100){
			text.text = value + " kg";
		} else {
			text.text = value + " kg";
		}
	}
}
