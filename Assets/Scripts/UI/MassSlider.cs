using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MassSlider : SliderWithText {

	public override void OnDrag(PointerEventData eventData){
		controller.updateMass(value);

		if(value < 100)
			text.text = value + " kg";
		else
			text.text = (int)value + " kg";

		base.OnDrag(eventData);
	}
}
