using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LampB  : Lamp {
	
	override public string EntKey {
		get { 
			return "LampB"; 
		} 
	}
	
	override public List<string> Groups {
		get { 
			return new List<string>(new string[1]{"Lamps"});
		}
	}
	
	
	
	void Start () {
		this.parentRoom = GameObject.FindGameObjectWithTag ("RoomB");
		this.parentRoomScript = parentRoom.GetComponent<RoomB>();
		this.myLight = this.GetComponent<Light> ();
		
	}
	
	void Update ()  {
		lampControl ();
		
	}
	
}