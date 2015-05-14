using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlanetSwitcher : MonoBehaviour {
	public 	Transform buttonPrefab;
	public UIController controller;
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
	private float buttonOffset = 5;

	// Use this for initialization
	void Start () {
		maxButtonCount 		= 5;
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
		if(gs.bodies[i] != null){
				camMov.setTarget(gs.bodies[i].transform);
		} else
			Debug.Log("No body found!");
	}

	public void handleEditButton(int i ){
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
<<<<<<< HEAD
		t.anchoredPosition = new Vector2((buttonWidth+buttonSpace)*currentBodyCount - buttonWidth/2,buttonOffset/2);
=======
		t.anchoredPosition = new Vector2((buttonWidth+buttonSpace)*currentBodyCount - buttonWidth/2,15);
>>>>>>> 2e0c12274274ce48fae3dc221e52bae18f6d2906

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

	public void updateButtons(){
		for(int i = 0;i<buttons.Count;i++){
			if(gs.bodies[i].type == Body.Type.Planet){
				buttons[i].text.text = "Planet";
				buttons[i].targetGraphic.color = defaultButtonColor;
			}else{
				buttons[i].text.text = "Dwarf Star";
				buttons[i].targetGraphic.color = gs.bodies[i].starLight.color;
			}
		}

		if(currentBodyCount <= maxButtonCount){
			nextButton.gameObject.SetActive(false);
			previousButton.gameObject.SetActive(false);
		} else {			
			nextButton.gameObject.SetActive(true);
			previousButton.gameObject.SetActive(true);
		}
	}

	public void shiftButtons(bool next){
		RectTransform bt;
		for(int i = 0;i<buttons.Count;i++){
			if(currentBodyCount > maxButtonCount){
				if(buttons[i].transform.position.x + buttonWidth/2 >= transform.position.x + panelWidth/2)
					buttons[i].gameObject.SetActive(false);
				else if(buttons[i].transform.position.x - buttonWidth/2 <= transform.position.x - panelWidth/2)
					buttons[i].gameObject.SetActive(false);
				else
					buttons[i].gameObject.SetActive(true);
			}

			bt = buttons[i].transform as RectTransform;
			if(next)
				bt.anchoredPosition = new Vector2(bt.anchoredPosition.x - (buttonWidth+buttonSpace), bt.anchoredPosition.y);
			else
				bt.anchoredPosition = new Vector2(bt.anchoredPosition.x + (buttonWidth+buttonSpace), bt.anchoredPosition.y);

		}
	}
}
