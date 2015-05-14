using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public Camera cam;
	public GravitySystem gs;

	public Transform target;
	private Body body;
	public float offset;
	public float scrollSpeed = 100;

	void Start(){
		cam.transform.rotation = Quaternion.LookRotation(-Vector3.up);
		if(target != null){
			body = target.GetComponent<Body>();
			setTarget(target);
		}
	}

	void Update () {
		offset += -Input.mouseScrollDelta.y * scrollSpeed;


		if(offset < 50 + (target == null ? 0 : body.radius))
			offset = 50 + (target == null ? 0 : body.radius);
		else if(offset > 90050)
			offset = 90050;


		if(target == null){
			cam.transform.localPosition = Vector3.up * offset;
		}else{
			transform.position = body.positions[(gs.uiHold ? 0 : (int)(Time.timeScale*10))];
			cam.transform.localPosition = Vector3.up * offset;
		}
	}

	public void setTarget(Transform t){
		if(body != null)
			body.sound.enabled = true;

		target = t;
		body = target.GetComponent<Body>();
		body.sound.enabled = false;

		transform.position = target.position;
		cam.transform.localPosition = Vector3.up * (offset + body.radius);

	}
}
