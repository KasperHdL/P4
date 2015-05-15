using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour {

	public Camera cam;
	public CameraMovement cm;

	public Body body;

	public GravitySystem gs;

	public PlanetSwitcher planetSwitcher;

	public VectorUI velocity;

	public TimeSlider timeSlider;

	public InformationHandler informationHandler;

	public RadiusSlider radiusSlider;
	public MassSlider massSlider;
	public DensitySlider densitySlider;
	public TemperatureSlider temperatureSlider;

	public GameObject typeSelector;

	public Button leftButton;
	public Text leftButtonText;

	public Button rightButton;
	public Text rightButtonText;

	private float canvasWidth;

	public State state = State.SimState;
	public Body.Type selectedType = Body.Type.None;
	public ActiveSliders activeSliders;

	public float maxDistortion;

	public enum State{
		PropState,
		VeloState,
		SimState
	}


#region Unity Methods
	public void Start () {
		gs.uiHold = true;
		RectTransform objectRectTransform = gameObject.GetComponent<RectTransform> ();
		canvasWidth = objectRectTransform.rect.width;
		radiusSlider.controller = this;
		massSlider.controller = this;
		temperatureSlider.controller = this;
		velocity.controller = this;

		setState(state);

		maxDistortion = Settings.MAX_DISTORTION;
	}

#endregion

	public void editBody(Body body){
		gs.uiHold = true;
		setBody(body);


	}

	public void newBody(Body body){
		planetSwitcher.newBody(body);

	    informationHandler.gameObject.SetActive(true);
		setBody(body);
	}

	public void setBody(Body body){
		this.body = body;

		updateType(body.type);
		setState(State.PropState);

		updateValues();
	}

	public void setState(State s){
		switch(s){
			case State.PropState:{
				updateActiveSliders(activeSliders);

				velocity.gameObject.SetActive(false);
				timeSlider.gameObject.SetActive(false);

				typeSelector.SetActive(true);
				planetSwitcher.gameObject.SetActive(false);

				leftButton.gameObject.SetActive(true);
				rightButton.gameObject.SetActive(true);

				RectTransform rt = rightButton.transform as RectTransform;
				rt.sizeDelta = new Vector2(50,30);
				rt.anchoredPosition = new Vector2(canvasWidth-rt.rect.width/2,0-rt.rect.height/2);


				rt = leftButton.transform as RectTransform;
				rt.sizeDelta = new Vector2(60,30);
				rt.anchoredPosition = new Vector2(rt.rect.width/2,0-rt.rect.height/2);


	        	cm.setTarget(body.transform);
	        	informationHandler.setBody(body);

	        	for(int i = 0; i < gs.bodies.Count; i++)
	        		if(gs.bodies[i] != body)
	        			gs.bodies[i].sound.enabled = false;
	        		else
	        			gs.bodies[i].sound.enabled = true;

				leftButtonText.text = "Cancel";
				rightButtonText.text = "Next";
			}break;
			case State.VeloState:{
				updateActiveSliders(ActiveSliders.None);

				velocity.gameObject.SetActive(true);
				timeSlider.gameObject.SetActive(false);
				typeSelector.SetActive(false);

				leftButton.gameObject.SetActive(true);
				rightButton.gameObject.SetActive(true);
				planetSwitcher.gameObject.SetActive(false);

				RectTransform rt = rightButton.transform as RectTransform;
				rt.sizeDelta = new Vector2(50,30);
				rt.anchoredPosition = new Vector2(canvasWidth-rt.rect.width/2,0-rt.rect.height/2);

				rt = leftButton.transform as RectTransform;
				rt.sizeDelta = new Vector2(70,30);
				rt.anchoredPosition = new Vector2(rt.rect.width/2,0-rt.rect.height/2);

				leftButtonText.text = "Previous";
				rightButtonText.text = "Finish";
			}break;
			case State.SimState:{

				updateActiveSliders(ActiveSliders.None);
				typeSelector.SetActive(false);
				timeSlider.gameObject.SetActive(true);

				velocity.gameObject.SetActive(false);

				leftButton.gameObject.SetActive(false);
				rightButton.gameObject.SetActive(false);
				planetSwitcher.gameObject.SetActive(true);

				planetSwitcher.updateButtons();

				gs.uiHold = false;

	        	for(int i = 0; i < gs.bodies.Count; i++)
	        		if(gs.bodies[i] != body)
	        			gs.bodies[i].sound.enabled = true;
	        		else
	        			gs.bodies[i].sound.enabled = false;

			}break;
		}
		state = s;
	}



#region Button Handlers
	public void handleNavigationalButton(int i){
		//setup for setting the vel
		// 0 is left button - 1 is right button

		switch(state){
			case State.PropState:{
				if(i == 0)
					setState(State.SimState);
				else
					setState(State.VeloState);
			}break;
			case State.VeloState:{
				if(i == 0)
					setState(State.PropState);
				else
					setState(State.SimState);
			}break;
		}
	}

	public void handleTypeButton(int i ){
		updateType((Body.Type)i);
	}

#endregion


	private void updateActiveSliders(ActiveSliders activeSliders){
		//activate appropiate sliders
		radiusSlider.gameObject.SetActive((activeSliders & ActiveSliders.Radius) == ActiveSliders.Radius);
		massSlider.gameObject.SetActive((activeSliders & ActiveSliders.Mass) == ActiveSliders.Mass);
		//densitySlider.gameObject.SetActive((activeSliders & ActiveSliders.Density) == ActiveSliders.Density);
		temperatureSlider.gameObject.SetActive((activeSliders & ActiveSliders.Temperature) == ActiveSliders.Temperature);

		//update extremes
		radiusSlider.updateExtremes(selectedType);
		massSlider.updateExtremes(selectedType);
		temperatureSlider.updateExtremes(selectedType);

		//update location
		int c = 0;
		if(radiusSlider.gameObject.activeSelf){
			RectTransform rt = radiusSlider.transform as RectTransform;
			rt.anchoredPosition = new Vector2(-35  ,c++ * 40 + 20);
		}
		if(massSlider.gameObject.activeSelf){
			RectTransform rt = massSlider.transform as RectTransform;
			rt.anchoredPosition = new Vector2(-35  ,c++ * 40 + 20);
		}
		if(temperatureSlider.gameObject.activeSelf){
			RectTransform rt = temperatureSlider.transform as RectTransform;
			rt.anchoredPosition = new Vector2(-35  ,c++ * 40 + 20);
		}
	}

	private void updateType(Body.Type type){
		selectedType = type;
		switch(type){
			case Body.Type.Planet:{
				activeSliders = Settings.Planet.SLIDERS;
			}break;
			case Body.Type.DwarfStar:{
				activeSliders = Settings.Star.Dwarf.SLIDERS;
			}break;
		}
		body.setType(type);
		resetBodySoundValues(body);
		updateActiveSliders(activeSliders);
		updateValues();
	}

	private void updateValues(Body body = null){
		updateMass((body == null ? massSlider.value : body.mass));
		updateRadius((body == null ? radiusSlider.value : body.radius));
		updateTemperature((body == null ? temperatureSlider.value : body.mass));
		if(body == null)
			updateVelocity(velocity.value);
		else
			updateVelocity(body.velocities[(int)(Time.timeScale*10)]);
	}

#region Slider Calls

	public void updateVelocity(Vector2 value){updateVelocity(new Vector3(value.x,0,value.y));}
	public void updateVelocity(Vector3 value){
		body.startVelocity = value;
		body.construct();
		//update Dots
		gs.calcFuturePositions();
	}

	public void updateMass(double value){
		body.updateMass(value);
		gs.calcFuturePositions();
	}

	public void updateRadius(float value){
		body.updateRadius(value);
	}

	public void updateDensity(float value){
		body.updateDensity(value);
	}

	public void updateTemperature(float value){
		body.updateTemperature(value);
	}
#endregion

	public void resetBodySoundValues(Body body){
		body.sound.volume = Settings.RESET_VOLUME;
		body.sound.pitch = Settings.RESET_PITCH;
	}

}
