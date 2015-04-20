using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadiusSlider : SliderWithText {

	public new void Start(){

		text.text = (int)value + " Mm";
	}

	public override void OnDrag(PointerEventData eventData){
		controller.updateRadius(value);

		if(value < 100)
			text.text = value + " Mm";
		else
			text.text = (int)value + " Mm";

		base.OnDrag(eventData);
	}
}
