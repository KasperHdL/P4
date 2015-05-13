using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
public class ButtonWithText : Button {
	public Text text;
	public TemperatureSlider temp;
	public int index;

	public override void OnPointerClick(PointerEventData eventData){
		if(temp != null)temp.handleJumpButton(index);
		base.OnPointerClick(eventData);
	}
}
