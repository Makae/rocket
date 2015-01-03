using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightSwitchC : AbstractLightSwitch {

	override public string EntKey {
		get { 
			return "SwitchC"; 
		} 
	}
	
	override public List<string> Groups {
		get { 
			return new List<string>(new string[1]{"LightSwitches"});
		}
	}


	// Use this for initialization
	void Start () {
		this.myLamp = GameObject.FindGameObjectWithTag("LampC").GetComponent<LampC>();
	}

	void Update () { 
		
		switchProcess ();
	}


}
