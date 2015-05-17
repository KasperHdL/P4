using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
public class ButtonWithText : Button {
	public Text text;
	public bool isEditBtn = false;
	public Color defaultColor;
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

	public void highlight(bool isHighlight){
		if(isHighlight)
			targetGraphic.color = new Color(defaultColor.r-0.2f, defaultColor.g-0.2f, defaultColor.b-0.2f);
		else
			targetGraphic.color = defaultColor;
	}
}
