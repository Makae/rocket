using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LampA  : Lamp {

	override public string EntKey {
		get { 
			return "LampA"; 
		} 
	}
	
	override public List<string> Groups {
		get { 
			return new List<string>(new string[1]{"Lamps"});
		}
	}



	void Start () {

		this.parentRoom = GameObject.FindGameObjectWithTag ("RoomA");
		this.parentRoomScript = parentRoom.GetComponent<RoomA>();
		this.myLight = this.GetComponent<Light> ();

	}

	void Update ()  {
		lampControl ();

	}
	
}