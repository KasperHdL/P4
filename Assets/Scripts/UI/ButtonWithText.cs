using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
public class ButtonWithText : Button {
	public Text text;
	public bool isEditBtn = false;
	public ButtonWithText editButton;
	public TemperatureSlider temp;
	public PlanetSwitcher planetSwitcher;

	public int index;

	public override void OnPointerClick(PointerEventData eventData){
		if(temp != null)temp.handleJumpButton(index);
		else if(planetSwitcher != null){
			if(isEditBtn)
				planetSwitcher.handleEditButton(index);
			else
				planetSwitcher.handleButton(index);
		}
		base.OnPointerClick(eventData);
	}
}
