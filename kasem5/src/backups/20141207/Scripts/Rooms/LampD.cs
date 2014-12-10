using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LampD  : Lamp {
	
	override public string EntKey {
		get { 
			return "LampD"; 
		} 
	}
	
	override public List<string> Groups {
		get { 
			return new List<string>(new string[1]{"Lamps"});
		}
	}
	
	
	
	void Start () {
		this.parentRoom = GameObject.FindGameObjectWithTag ("RoomD");
		this.parentRoomScript = parentRoom.GetComponent<RoomD>();
		this.myLight = this.GetComponent<Light> ();
		
	}
	
	void Update ()  {
		lampControl ();
		
	}
	
}