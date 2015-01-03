using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class RoomB : Room {

	override public string EntKey {
		get { 
			return "RoomB"; 
		} 
	}
	
	override public List<string> Groups {
		get { 
			return new List<string>(new string[1]{"Rooms"});
		}
	}


void Start () {

		this.environment = new Environment();
		this.doors =  GameObject.FindGameObjectsWithTag("DoorRoomB");
		//EntityManager.AddToGroup ("Rooms", this);
	
	}
	

	void Update () {


		}

	void OnTriggerEnter(Collider other) {

	}


	public override void OnMessage(Telegram t) {
		print ("Als Room B habe ich das Telegram empfangen, das an alle Räume ging");

		if (t.Message == "POWER_ON") {
						print ("POWER IN B EINGESCHALTET!");
						this.environment.setPowerState (true);
			this.openDoor();
				}
			
		}
}
