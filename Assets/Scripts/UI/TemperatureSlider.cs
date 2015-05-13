using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class TemperatureSlider : SliderWithText {

	public GameObject classPrefab;

	public RectTransform classContainer;

	private Body.Type currentType = Body.Type.None;

	public new void Start(){
		base.Start();
		setText(value);
	}

	public void handleJumpButton(int i){

		float ct = Settings.Star.Dwarf.TEMPERATURE[i+1];
		float lt = Settings.Star.Dwarf.TEMPERATURE[i];
		value = (lt+(ct-lt)/2);

		controller.updateTemperature(value);
		setText(value);
	}

	public void updateExtremes(Body.Type type){
		switch(type){
			case Body.Type.DwarfStar:
				if(currentType == type)
					return;
				maxValue = Settings.Star.Dwarf.TEMPERATURE[Settings.Star.Dwarf.TEMPERATURE.Length - 1];
				minValue = Settings.Star.Dwarf.TEMPERATURE[0];

				for(int i = 1;i<Settings.Star.Dwarf.TEMPERATURE.Length;i++){
					float nt = Settings.Star.Dwarf.TEMPERATURE[i];
					float pt = Settings.Star.Dwarf.TEMPERATURE[i-1];
					float value = pt+((nt-pt)/2);//midpoint
					float per = (value-minValue)/(maxValue-minValue);

					GameObject g = Instantiate(classPrefab,Vector3.zero,Quaternion.identity) as GameObject;
					g.transform.SetParent(classContainer);

					RectTransform rt = g.transform as RectTransform;
					rt.anchoredPosition = new Vector2(0,15);

					float fullWidth = Mathf.Abs(g.transform.localPosition.x)*2;
					rt.anchoredPosition = new Vector2(fullWidth*per,15);
					value = (((nt-pt))/(maxValue-minValue));
					rt.sizeDelta = new Vector2(value*fullWidth,30);

					ButtonWithText btw = g.GetComponent<ButtonWithText>();
					btw.text.text = Settings.Star.Dwarf.CLASSIFICATION[i-1].ToString();
					btw.temp = this;
					btw.index = i-1;

					g.GetComponent<Image>().color = Color.Lerp(Settings.Star.Dwarf.COLORS[i-1],Settings.Star.Dwarf.COLORS[i],.5f);

				}


			break;
		}
		currentType = type;
	}

	public override void setText(float val){
		text.text = value.ToString("F0") + " " + Settings.Star.Dwarf.TEMPERATURE_UNIT;
	}
}
