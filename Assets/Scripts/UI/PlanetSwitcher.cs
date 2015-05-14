using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlanetSwitcher : MonoBehaviour {
	public 	Transform buttonPrefab;
	public 	CameraMovement camMov;
	public 	GravitySystem gs;

	private int currentBodyCount;
	private float buttonWidth = 50;
	private float panelWidth;
	private float panelHeight = 60;

	private RectTransform panelRT;
	private RectTransform buttonRT;

	private float buttonSpace = 5;
	private float buttonOffset = 5;

	// Use this for initialization
	void Start () {

		panelRT 			= transform as RectTransform;
		panelWidth 			= panelRT.rect.width;
		panelHeight 		= panelRT.rect.height;

		currentBodyCount 	= 0;
	}

	public void newBody(Body body){
		currentBodyCount++;
		updateButtons();
	}

	public void handleButton(int i){
		if(gs.bodies[i] != null){
			camMov.setTarget(gs.bodies[i].transform);
		} else
			Debug.Log("No body found!");
	}

	public void updateButtons(){
		RectTransform t = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity) as RectTransform;
		ButtonWithText b = t.GetComponent<ButtonWithText>();
		b.index = currentBodyCount-1;
		b.planetSwitcher = this;

		panelWidth = (buttonWidth+buttonSpace)*currentBodyCount + buttonSpace;
		panelRT.sizeDelta = new Vector2(panelWidth, panelHeight);

		t.SetParent(transform);
		t.anchoredPosition = new Vector2((buttonWidth+buttonSpace)*currentBodyCount - buttonWidth/2,buttonOffset/2);


	}
}
