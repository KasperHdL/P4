using UnityEngine;
using System.Collections;
using System.IO;

public class Spawner : MonoBehaviour {

	public GameObject prefab;
	private GameObject obj;

	private readonly bool drawCube = true;


	public Transform direction;

	private bool roundStarted = false;

	private bool waitForRestart = false;



	private readonly string directory = "D:/KasperHdL/Desktop/TestFiles/";
	private readonly string filenamePrepend = "participant_";
	private readonly string filenameAppend = ".txt";
	private int nextNum = 0;

	private readonly int maxRounds = 10;

	private int round = 0;
	private float[] correctAngles = new float[10];
	private float[] angles = new float[10];

	private float correctAngle;

	private float offset = 5;


	void Start(){
		findNextFileNumber();
	}

	void init(){
		findNextFileNumber();
		round = 0;

		correctAngles = new float[10];
		angles = new float[10];
		roundStarted = false;
		waitForRestart = false;
	}

	void findNextFileNumber(){
		DirectoryInfo dir = new DirectoryInfo(directory);

		int max = 0;

		foreach (FileInfo file in dir.GetFiles()){
			if(file.Name == filenamePrepend + max + filenameAppend)
				max++;

		}

		nextNum = max;
	}

	// Update is called once per frame
	void Update () {
		if(roundStarted){
			//update and wait for click

	        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

	        RaycastHit hitinfo;
	        if (Physics.Raycast(ray, out hitinfo, Mathf.Infinity, 1<<8)){
				Vector3 mPos = hitinfo.point;

				float angle = Vector3.Angle(Vector3.right,mPos);
				direction.LookAt(mPos);

				 if(Input.GetMouseButtonDown(0)){
		        	//clicked
				 	correctAngles[round-1] = correctAngle;

				 	angles[round-1] = (angle/180)*Mathf.PI * (mPos.y>0 ? -1:1);
				 	Destroy(obj);
				 	roundStarted = false;
		        }
	        }
		}else if(waitForRestart){
			if(Input.GetKeyDown(KeyCode.Space)){
				init();
			}
		}else{
			if(round == maxRounds){
				string s = "angle,\t\tcorrect angle;\r\n";
				for(int i = 0;i<maxRounds;i++){
					s += angles[i].ToString("F10") + ",\t" + correctAngles[i].ToString("F10") + ";\r\n";
				}
 				File.WriteAllText(directory + filenamePrepend + nextNum + filenameAppend, s);

 				findNextFileNumber();
 				waitForRestart = true;
			}else{
				newRound();
				roundStarted = true;
			}
		}



	}

	private void newRound(){
		float angle = Random.Range(-Mathf.PI,Mathf.PI);

		correctAngle = angle;

		obj = Instantiate(prefab, new Vector3(Mathf.Cos(angle)*offset,(drawCube ? 0:-10),Mathf.Sin(angle)*offset), Quaternion.identity) as GameObject;
		round++;
	}

}
