using UnityEngine;
using System.Collections;

public class Countdown : MonoBehaviour {

	private float timer;
	private string time  = "test";

	void Awake() {
		timer = 400f;

	}
	
	void Update() {

		timer -= Time.deltaTime; //timer = Zeit zwischen den Frames
	
	}

	void OnGUI() {
		GUI.skin.box.fontSize=12;

		if (timer < 100f) {
			GUI.color = Color.red;
			GUI.skin.box.fontSize=16;
		}

		if (timer < 0f) {
				
			print ("you lost");
			Time.timeScale = 0.0f;

		}

		GUI.Box(new Rect(20,20,100,40), "Time left:\n"+ Mathf.Ceil(timer).ToString());

	}

}
