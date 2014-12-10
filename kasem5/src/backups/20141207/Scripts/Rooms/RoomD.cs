using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class RoomD : Room {

	override public string EntKey {
		get { 
			return "RoomD"; 
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
		this.doors = GameObject.FindGameObjectsWithTag ("DoorRoomD");
		//EntityManager.AddToGroup ("Rooms", this);
	
	}
	

	void Update () {


		}

	void OnTriggerEnter(Collider other) {

	}


	public override void OnMessage(Telegram t) {
		print ("Als Room A habe ich das Telegram empfangen, das an alle Räume ging");

		if (t.Message == "POWER_ON")
			print ("POWER IN D EINGESCHALTET!");
		this.environment.setPowerState(true);
		}
}
