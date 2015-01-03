using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class ControlRoom : Room {

//	GameObject roomB;
//	RoomB RoomBScript;
	/* Use this for initialization	*/

	override public string EntKey {
		get { 
			return "ControlRoom"; 
		} 
	}
	
	override public List<string> Groups {
		get { 
			return new List<string>(new string[1]{"Room"});
		}
	}

	void Start () {

//		this.roomB = GameObject.FindGameObjectWithTag ("RoomB");
//		this.RoomBScript = roomB.GetComponent<RoomB>();
			
		this.environment = new Environment();
		this.doors = GameObject.FindGameObjectsWithTag ("DoorControlRoom");
		this.openDoor ();
		environment.setPowerState (true);
	}
	
	// Update is called once per frame
	void Update () {

						//Check for Power:
		//Debug.Log (this.environment.getPowerState ());

		}

	void OnTriggerEnter(Collider other) {

	}


}
