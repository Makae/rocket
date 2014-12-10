using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class RoomA : Room {

	override public string EntKey {
		get { 
			return "RoomA"; 
		} 
	}
	
	override public List<string> Groups {
		get { 
			//print ("Raum A hat sich registriert in Gruppe Rooms");
			return new List<string>(new string[1]{"Rooms"});

		}
	}


void Start () {

		this.environment = new Environment();
		this.doors = GameObject.FindGameObjectsWithTag ("DoorRoomA");

	}
	

	void Update () {


		}

	void OnTriggerEnter(Collider other) {

	}


	public override void OnMessage(Telegram t) {

		if (t.Message == "OPEN_DOOR") {
			print ("Türe in Raum A wird geöffnet");
			this.openDoor();
		}

		if (t.Message == "POWER_ON") {
			print ("POWER IN A EINGESCHALTET!");
			this.environment.setPowerState(true);
			//this.openDoor();
		}
	}
}