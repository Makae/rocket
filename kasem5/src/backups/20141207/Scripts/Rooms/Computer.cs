using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Computer : AbstractGameEntity {

	bool showMenu = false;
	bool guiLock = false;
	int clicks = 0;

	Dictionary<string, bool> options;
	List<string> elements;

	int bombCode = 1234;

	GameObject playerControl;
	GameObject mainCamera;
	Room RoomD;

	/*
	 * Um die Roboter auszuschalten wird ein Script mit einer Public function auf dem 
	 * GameController ausgeführt. Das einmal mit mockup Methode modellieren
	*/

	//Grund für Liste und Dictonary: http://answers.unity3d.com/questions/409835/out-of-sync-error-when-iterating-over-a-dictionary.html


	void Start () {
		this.options = new Dictionary<string, bool> () {
			{"elevator", false},
			{"bomb", false},
			{"room", false},
			{"robot", false} };
		this.elements = new List<string> () {
			{"elevator"},
			{"bomb"},
			{"room"},
			{"robot"}
		};

		playerControl = GameObject.FindGameObjectWithTag("Player");
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}

	public override string EntKey {
		get { 
			return "computer"; 
		} 
	}
	
	public override List<string> Groups {
		get { 
			return new List<string>(new string[1]{"machine"});
		}
	}

	

	void Update () {
	
		clicks = 0;

		foreach(KeyValuePair<string, bool> pair in options) {
			if(pair.Value)
				clicks++;
		}

		if (clicks == 2) {
				guiLock=true;
		} 


		//Replace with "Interaction with Object" from Tinu
		if(Input.GetKeyDown (KeyCode.P)) {
			showMenu = true;
			playerControl.GetComponent<MouseLook>().enabled = false;
			mainCamera.GetComponent<MouseLook>().enabled = false;
		}

	}

	void OnGUI() {

		if (showMenu) {

			if(guiLock) 
				GUI.enabled=false;
			else
				GUI.enabled=true;

			Time.timeScale = 0.0f;

						int posWidth = (Screen.width - 200) / 2;
						int posHeight = (Screen.height - 200) / 2;

						var centeredStyle = GUI.skin.GetStyle ("Label");
						centeredStyle.alignment = TextAnchor.UpperCenter;

						GUILayout.BeginArea (new Rect (posWidth, posHeight, 200, 400));
						GUILayout.Label ("Computer Options \n"+"(chose two of them)");
				
						if (GUILayout.Button ("Enable elevator")) {
							this.options["elevator"]=true;
						}
						if(this.options["elevator"]) {
							GUILayout.Label ("Elevator is on!");
							//+ Enable elevator
						}
		
						if (GUILayout.Button ("Bomb code"))
							getBombCode ();
						if(this.options["bomb"]) {
							GUILayout.Label ("Code. " +bombCode);
						}	


						if (GUILayout.Button ("Open treasure room"))
								openTreasureRoom ();
						if(this.options["room"]) {
							GUILayout.Label ("Treasure room unlocked!");
						}

						if (GUILayout.Button ("Paralyze robots"))
								paralyzeRobots ();

						if(this.options["robot"]) {
							GUILayout.Label ("Robots are paralyzed!");
						}


			//Diese Optionen habe ich immer
						GUI.enabled=true;
						if (GUILayout.Button ("EXIT")) {
								Time.timeScale = 1.0f;
								showMenu = false;
								playerControl.GetComponent<MouseLook>().enabled = true;
								mainCamera.GetComponent<MouseLook>().enabled = true;	
						}

						GUILayout.Label ("-----------------------");
						int clicksLeft = 2 - this.clicks;
						if(clicksLeft==2)
						GUILayout.Label ("Noch 2 Aktionen verfügbar");
						if(clicksLeft==1)
						GUILayout.Label ("Noch 1 Aktion verfügbar");
						if(clicksLeft==0)
						GUILayout.Label ("You're done!\n Reset for a new selection.");
						GUILayout.Label ("-----------------------");


						if (GUILayout.Button ("RESET")) {
								resetChoice ();
						}

						GUILayout.EndArea ();
				}

	}


	public void getBombCode() {
		//Set a new BombCode and write it down to the screen
		this.options["bomb"]=true;
		this.bombCode = 1984;
		/* Setzen des Codes auf die Bombe oder auf den GameController?	 */
	}
	public void openTreasureRoom() {
		this.options["room"]=true;
		this.RoomD = (Room) GameObject.FindGameObjectWithTag("RoomD").GetComponent<RoomD>();
		this.RoomD.openDoor();

	}

	public void resetChoice() {
		foreach (string value in elements) {
			this.options[value] = false;
		}

		this.clicks = 0;
		this.guiLock = false;


		if(this.RoomD !=null)
			this.RoomD.closeDoor();
	}

	public void paralyzeRobots() {
		this.options["robot"]=true;
	}


	public override void OnMessage(Telegram t) {
		Debug.Log ("Computer got Message");
		if (t.Args.ContainsKey ("computerCard")) {
			print ("Card accepted");				
			this.showMenu = true;
		}

	}


}
