using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderWithText : Slider {

	public UIController controller;
	public Text text;

	public new void Start(){
		base.Start();
	}

	public string valueFormating(int powVal, float decimals, double value){
		int intVal;
		float d = Mathf.Pow(10, decimals);
		value = value/Mathf.Pow(10,powVal);
		value = value*d;
		intVal = (int)(value);
		value = (double)(intVal)/d;

		return value.ToString();
	}
}
