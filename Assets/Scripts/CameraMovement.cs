using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	private Camera cam;
	public GravitySystem gs;

	public Transform target;
	private Body body;
	public float offset;
	public float scrollSpeed = 20f;

	public bool disableMouseDrag = true;

	[HideInInspector]
	public bool allowMouseDrag = true;

	private bool mouseDrag = false;
	private Vector3 startDragPos = Vector3.zero;

	public float dragSpeed = 1f;

	private Vector3 direction;

	void Start(){
		cam = GetComponent<Camera>();
		if(disableMouseDrag)
			direction = Vector3.up;

			setTarget(target);

	}

	void Update () {
		if(target == null)
			updatePositionForAllBodies();
		else{
			if(allowMouseDrag && !disableMouseDrag)
				mouseDragUpdate();
			updatePositionForTarget();
		}
	}

	void updatePositionForAllBodies(){
		foreach(Transform child in gs.transform){
			Vector3 screenPos = cam.WorldToScreenPoint(child.position);
		}
	}

	void updatePositionForTarget(){
		Vector3 delta = transform.position - target.position;
		direction = direction.normalized * (offset + body.radius);
			transform.position = target.position + direction;
	}

	void mouseDragUpdate(){
		Vector3 delta = transform.position - target.position;

		if(mouseDrag){
			//update
			if(Input.GetMouseButtonUp(0))
				mouseDrag = false;
			else{
				Vector3 mousePosition = new Vector3(0,Input.mousePosition.y,0);;
				Vector3 to = startDragPos - mousePosition;
				to *= dragSpeed;
				to += delta;

				to.x = 0;
				delta.x = 0;


				direction = Vector3.RotateTowards(delta,to,Time.deltaTime,0f);

				transform.rotation = Quaternion.LookRotation(-direction);

				startDragPos = mousePosition;
			}

		}else if(Input.GetMouseButtonDown(0)){
			mouseDrag = true;
			startDragPos = new Vector3(0,Input.mousePosition.y,0);
		}
	}

	public void setTarget(Transform t){
		if(t == null){
			//camera should target everyone


		}else{
			target = t;
			body = target.GetComponent<Body>();

			direction = Vector3.up;
			transform.rotation = Quaternion.LookRotation(-direction);
			transform.position = target.position + direction * (offset + body.radius);
		}

	}
}
