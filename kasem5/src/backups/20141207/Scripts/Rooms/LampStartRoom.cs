using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LampStartRoom  : Lamp {
	
	override public string EntKey {
		get { 
			return "LampStartRoom"; 
		} 
	}
	
	override public List<string> Groups {
		get { 
			return new List<string>(new string[1]{"Lamps"});
		}
	}
	
	
	
	void Start () {
		
		this.lampSwitcher = true; //Am Anfang ist das Licht in Raum A eingeschaltet
		this.parentRoom = GameObject.FindGameObjectWithTag ("StartRoom");
		this.parentRoomScript = parentRoom.GetComponent<StartRoom>();
		this.myLight = this.GetComponent<Light> ();
		
	}
	
	void Update ()  {
		lampControl ();
		
	}
	
}