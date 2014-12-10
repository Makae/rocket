using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightSwitchA : AbstractLightSwitch {



	override public string EntKey {
		get { 
			return "SwitchA"; 
		} 
	}
	
	override public List<string> Groups {
		get { 
			return new List<string>(new string[1]{"LightSwitches"});
		}
	}


	void Start () {
		this.myLamp = GameObject.FindGameObjectWithTag("LampA").GetComponent<LampA>();
	}

	void Update () { 

		switchProcess ();
	
	}


}
