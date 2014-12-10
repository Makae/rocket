using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightSwitchD : AbstractLightSwitch {

	override public string EntKey {
		get { 
			return "SwitchD"; 
		} 
	}
	
	override public List<string> Groups {
		get { 
			return new List<string>(new string[1]{"LightSwitches"});
		}
	}


	// Use this for initialization
	void Start () {
		this.myLamp = GameObject.FindGameObjectWithTag("LampD").GetComponent<LampD>();
	}

	void Update () { 
		
		switchProcess ();
	}


}
