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
		string value = val.ToString();
		if(val < 100){
			text.text = value + " Mm";
		} else {
			text.text = value + " Mm";
		}
	}
}
