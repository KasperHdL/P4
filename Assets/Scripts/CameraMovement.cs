using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public Transform target;
	public float offset;
	public float scrollSpeed = 20f;

	private bool mouseDrag = false;
	private Vector3 startDragPos = Vector3.zero;

	public float dragSpeed = 1f;

	private Vector3 direction;

	void Start(){
		if(target == null)
			Debug.LogError("Camera target is not set!");

		direction = Vector3.forward*3 + Vector3.up;
		transform.rotation = Quaternion.LookRotation(-direction);
		transform.position = target.position + direction * offset;

	}

	void Update () {
		if(target == null)
			return;

		mouseDragUpdate();
	}

	void mouseDragUpdate(){
		Vector3 delta = transform.position - target.position;

		offset -= Input.mouseScrollDelta.y * scrollSpeed;

		if(mouseDrag){
			//update
			if(Input.GetMouseButtonUp(0))
				mouseDrag = false;
			else{
				Vector3 mousePosition = new Vector3(0,Input.mousePosition.y,0);;
				Vector3 to = startDragPos - mousePosition;
				to *= dragSpeed;
				to += delta;


				direction = Vector3.RotateTowards(delta,to,Time.deltaTime,0f);

				Debug.DrawRay(target.position, direction, Color.red);

				transform.rotation = Quaternion.LookRotation(-direction);

				startDragPos = mousePosition;
			}

		}else if(Input.GetMouseButtonDown(0)){
			mouseDrag = true;
			startDragPos = new Vector3(0,Input.mousePosition.y,0);
		}


		direction = direction.normalized * offset;
		transform.position = target.position + direction;

	}
}
