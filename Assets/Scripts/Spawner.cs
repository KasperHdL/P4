using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject obj;
	int counter = 0;
	int correct = 0;
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

				if(counter < 20){
					if(hit.collider.gameObject.tag == "BackRight"){
						Destroy(GameObject.FindWithTag("SoundCube"));
						Instantiate(obj, RandomPosition(), Quaternion.identity);
						counter += 1;
						Debug.Log("BACK - WRONG");
					}
					if(hit.collider.gameObject.tag == "Front"){
						Destroy(GameObject.FindWithTag("SoundCube"));
						Instantiate(obj, RandomPosition(), Quaternion.identity);
						counter += 1;
						Debug.Log("FRONT - WRONG");

					}
					if(hit.collider.gameObject.tag == "Left"){
						Destroy(GameObject.FindWithTag("SoundCube"));
						Instantiate(obj, RandomPosition(), Quaternion.identity);
							counter += 1;

						Debug.Log("LEFT - WRONG");

					}
					if(hit.collider.gameObject.tag == "Right"){
						Destroy(GameObject.FindWithTag("SoundCube"));
						Instantiate(obj, RandomPosition(), Quaternion.identity);
							counter += 1;

						Debug.Log("RIGHT - WRONG");

					}

					if(hit.collider.gameObject.tag == "BackLeft"){
						Destroy(GameObject.FindWithTag("SoundCube"));
						Instantiate(obj, RandomPosition(), Quaternion.identity);
							counter += 1;

						Debug.Log("RIGHT - WRONG");
						
					}

					if(hit.collider.gameObject.tag == "FrontLeft"){
						Destroy(GameObject.FindWithTag("SoundCube"));
						Instantiate(obj, RandomPosition(), Quaternion.identity);
							counter += 1;

						Debug.Log("RIGHT - WRONG");
						
					}

					if(hit.collider.gameObject.tag == "FrontRight"){
						Destroy(GameObject.FindWithTag("SoundCube"));
						Instantiate(obj, RandomPosition(), Quaternion.identity);
							counter += 1;

						Debug.Log("RIGHT - WRONG");
						
					}
					if(hit.collider.gameObject.tag == "SoundCube"){
						Destroy(GameObject.FindWithTag("SoundCube"));
						Instantiate(obj, RandomPosition(), Quaternion.identity);
							counter += 1;
						correct += 1;
						Debug.Log ("CORRECT!!!!!!");

					}


				}

			}

			if(counter == 20){
				Debug.Log("You got " + correct + " correct answers");
				counter += 1;
			}

		}

	}

	Vector3 RandomPosition()
	{
		int rnd = Random.Range(1,8);
		Vector3 pos = new Vector3(0, 0, 0);
		switch (rnd)
		{
		case 1:
			//Front
			pos = new Vector3(0,25,300);
			break;
			
		case 2:
			pos = new Vector3(200,25,200);
			break;
			
		case 3:
			//Right
			pos = new Vector3(300,25,0);
			break;
			
		case 4:
			//Left
			pos = new Vector3(-300,25,0);
			break;

		case 5:
			pos = new Vector3(-200,25,200);
			break;
		
		case 6:
			pos = new Vector3(200,25,-200);
			break;

		case 7:
			pos = new Vector3(-200,25,-200);
			break;
		}
		
		return pos;
	}
}
