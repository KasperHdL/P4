using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public Vector3 front;
	public Vector3 left;
	public Vector3 right;
	public Vector3 back;



	public GameObject obj;

	void Start(){
		Instantiate(obj, RandomPosition(), Quaternion.identity);

	}

	// Update is called once per frame
	void Update () {


		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)){
				//Debug.Log (hit.transform.name);
				if(hit.collider.gameObject.tag == "Back"){
					Destroy(GameObject.FindWithTag("SoundCube"));
					Instantiate(obj, RandomPosition(), Quaternion.identity);
					Debug.Log("BACK - WRONG");
				}
				if(hit.collider.gameObject.tag == "Front"){
					Destroy(GameObject.FindWithTag("SoundCube"));
					Instantiate(obj, RandomPosition(), Quaternion.identity);
					Debug.Log("FRONT - WRONG");

				}
				if(hit.collider.gameObject.tag == "Left"){
					Destroy(GameObject.FindWithTag("SoundCube"));
					Instantiate(obj, RandomPosition(), Quaternion.identity);
					Debug.Log("LEFT - WRONG");

				}
				if(hit.collider.gameObject.tag == "Right"){
					Destroy(GameObject.FindWithTag("SoundCube"));
					Instantiate(obj, RandomPosition(), Quaternion.identity);
					Debug.Log("RIGHT - WRONG");

				}
				if(hit.collider.gameObject.tag == "SoundCube"){
					Destroy(GameObject.FindWithTag("SoundCube"));
					Instantiate(obj, RandomPosition(), Quaternion.identity);
					Debug.Log ("CORRECT!!!!!!");

				}

			}

		}

	}

	Vector3 RandomPosition()
	{
		int rnd = Random.Range(1,5);
		Vector3 pos = new Vector3(0, 0, 0);
		switch (rnd)
		{
		case 1:
			pos = new Vector3(0,25,300);
			break;
			
		case 2:
			pos = new Vector3(0,25,-300);
			break;
			
		case 3:
			pos = new Vector3(300,25,0);
			break;
			
		case 4:
			pos = new Vector3(-300,25,0);
			break;
		}
		
		return pos;
	}
}
