using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightSwitchB : AbstractLightSwitch {

	override public string EntKey {
		get { 
			return "SwitchB"; 
		} 
	}
	
	override public List<string> Groups {
		get { 
			return new List<string>(new string[1]{"LightSwitches"});
		}
	}


	// Use this for initialization
	void Start () {
		this.myLamp = GameObject.FindGameObjectWithTag("LampB").GetComponent<LampB>();
	}

	void Update () { 
		
		switchProcess ();
	}


}
