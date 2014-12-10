using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightSwitchStart : AbstractLightSwitch {

	override public string EntKey {
		get { 
			return "SwitchStart"; 
		} 
	}
	
	override public List<string> Groups {
		get { 
			return new List<string>(new string[1]{"LightSwitches"});
		}
	}


	// Use this for initialization
	void Start () {
		this.myLamp = GameObject.FindGameObjectWithTag("LampStartRoom").GetComponent<LampStartRoom>();
	}

	void Update () { 
		
		switchProcess ();
	}


}
