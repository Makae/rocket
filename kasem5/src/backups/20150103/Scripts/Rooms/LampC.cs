using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LampC  : Lamp {
	
	override public string EntKey {
		get { 
			return "LampC"; 
		} 
	}
	
	override public List<string> Groups {
		get { 
			return new List<string>(new string[1]{"Lamps"});
		}
	}
	
	
	
	void Start () {

		this.parentRoom = GameObject.FindGameObjectWithTag ("RoomC");
		this.parentRoomScript = parentRoom.GetComponent<RoomC>();
		this.myLight = this.GetComponent<Light> ();
		
	}
	
	void Update ()  {
		lampControl ();
		
	}
	
}