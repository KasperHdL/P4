using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderWithText : Slider {

	public BodyController controller;

	[SerializeField]
	public Text text;

	public new void Start(){
		base.Start();

		text = transform.GetChild(3).GetComponent<Text>();
	}
}
