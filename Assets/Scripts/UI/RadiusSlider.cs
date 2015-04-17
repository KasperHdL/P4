using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadiusSlider : SliderWithText {

	public override void OnDrag(PointerEventData eventData){
		controller.updateRadius(value);

		if(value < 100)
			text.text = value + " m";
		else
			text.text = (int)value + " m";

		base.OnDrag(eventData);
	}
}
