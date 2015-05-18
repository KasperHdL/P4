using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlanetSwitcher : MonoBehaviour {
	public 	Transform buttonPrefab;
	public 	UIController controller;
	public 	CameraMovement camMov;
	public 	GravitySystem gs;
	public 	List<ButtonWithText> buttons;
	public 	Button nextButton;
	public 	Button previousButton;

	private Color defaultButtonColor;

	private RectTransform nB;
	private RectTransform pB;

	private int maxButtonCount;
	private int currentBodyCount;
	private int currentButtonIndex;
	private float buttonWidth = 50;
	private float panelWidth;
	private float panelHeight = 85;

	private RectTransform panelRT;
	private RectTransform buttonRT;

	private float buttonSpace = 5;

	// Use this for initialization
	void Start () {
		maxButtonCount 		= 10;
		defaultButtonColor 	= Color.white;
		panelRT 			= transform as RectTransform;
		panelWidth 			= panelRT.rect.width;
		panelHeight 		= panelRT.rect.height;

		currentBodyCount 	= 0;
		buttons 			= new List<ButtonWithText>();
	}

	public void newBody(Body body){
		currentBodyCount++;
		createButton();
	}

	public void handleButton(int i){
		buttons[currentButtonIndex].highlight(false);
		if(gs.bodies[i] != null){
			currentButtonIndex = i;
			buttons[currentButtonIndex].highlight(true);
			camMov.setTarget(gs.bodies[i].transform);
			controller.previousBody = gs.bodies[i];
			controller.informationHandler.setBody(gs.bodies[i]);
		} else
			Debug.Log("No body found!");
	}

	public void handleEditButton(int i){
		controller.editBody(gs.bodies[i]);
	}

	public void createButton(){
		RectTransform t = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity) as RectTransform;
		ButtonWithText b = t.GetComponent<ButtonWithText>();
		b.index = currentBodyCount-1;
		b.editButton.index = b.index;

		b.planetSwitcher = this;
		b.editButton.planetSwitcher = this;

		if(currentBodyCount <= maxButtonCount){
			panelWidth = (buttonWidth+buttonSpace)*currentBodyCount + buttonSpace;
			panelRT.sizeDelta = new Vector2(panelWidth, panelHeight);
		}

		t.SetParent(transform);
		t.anchoredPosition = new Vector2((buttonWidth+buttonSpace)*currentBodyCount - buttonWidth/2,15);

		buttons.Add(b);
		if(currentBodyCount > maxButtonCount){
			if(b.transform.position.x + buttonWidth/2 >= transform.position.x + panelWidth/2)
				b.gameObject.SetActive(false);
			else if(b.transform.position.x - buttonWidth/2 <= transform.position.x - panelWidth/2)
				b.gameObject.SetActive(false);
			else
				b.gameObject.SetActive(true);
		}
	}

	public void deleteButton(int i){
		Destroy(buttons[i].gameObject);
		buttons.RemoveAt(i);
		currentBodyCount--;
		if(currentBodyCount <= maxButtonCount){
			panelWidth = (buttonWidth+buttonSpace)*currentBodyCount + buttonSpace;
			panelRT.sizeDelta = new Vector2(panelWidth, panelHeight);
		}
		for(int j = 0;j<buttons.Count;j++){
			ButtonWithText b = buttons[j];

			b.index = j;
			b.editButton.index = j;

			RectTransform t = b.transform as RectTransform;
			t.anchoredPosition = new Vector2((buttonWidth+buttonSpace)*(j+1) - buttonWidth/2,15);
		}

		updateButtons();

	}

	public void updateButtons(){
		for(int i = 0;i<gs.bodies.Count;i++){
			if(gs.bodies[i].type == Body.Type.Planet){
				buttons[i].text.text = "Planet";
				buttons[i].targetGraphic.color = gs.bodies[i].color;
				buttons[i].defaultColor = gs.bodies[i].color;
			}else{
				buttons[i].text.text = "Dwarf Star";
				buttons[i].targetGraphic.color = gs.bodies[i].starLight.color;
				buttons[i].defaultColor = gs.bodies[i].starLight.color;
			}
		}

		if(currentBodyCount <= maxButtonCount){
			nextButton.gameObject.SetActive(false);
			previousButton.gameObject.SetActive(false);
		} else {
			nextButton.gameObject.SetActive(true);
		}
	}

	public void shiftButtons(bool next){
		RectTransform bt;
		for(int i = 0;i<buttons.Count;i++){
			bt = buttons[i].transform as RectTransform;
			if(next)
				bt.anchoredPosition = new Vector2(bt.anchoredPosition.x - (buttonWidth+buttonSpace), bt.anchoredPosition.y);
			else
				bt.anchoredPosition = new Vector2(bt.anchoredPosition.x + (buttonWidth+buttonSpace), bt.anchoredPosition.y);

			if(buttons[i].transform.position.x + buttonWidth/2 >= transform.position.x + panelWidth/2)
				buttons[i].gameObject.SetActive(false);
			else if(buttons[i].transform.position.x - buttonWidth/2 <= transform.position.x - panelWidth/2)
				buttons[i].gameObject.SetActive(false);
			else
				buttons[i].gameObject.SetActive(true);
		}


		if(buttons[0].transform.position.x > transform.position.x - panelWidth/2)
			previousButton.gameObject.SetActive(false);
		else
			previousButton.gameObject.SetActive(true);

		if(buttons[buttons.Count-1].transform.position.x < transform.position.x + panelWidth/2)
			nextButton.gameObject.SetActive(false);
		else
			nextButton.gameObject.SetActive(true);
	}
}
