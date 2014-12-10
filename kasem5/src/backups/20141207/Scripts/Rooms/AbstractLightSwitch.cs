using UnityEngine;
using System.Collections;

public abstract class AbstractLightSwitch: AbstractGameEntity  {
	
	protected Lamp myLamp = null;
	protected bool inRange = false;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			inRange = true;
			print ("Ich bin im bereich vom switch für: "+myLamp.tag);
		}
	}


	void OnTriggerExit(Collider other) {
		if (other.tag == "Player")
			inRange = false;

	}

	
	public void switchProcess() {

		if (inRange) {

						if (Input.GetKeyDown (KeyCode.H)) {
								print ("LightSwitch has been pressed");
			
								if (myLamp.lampSwitching) {
										myLamp.lampSwitching = false;
										print ("Lichtschalter "+this.tag+ " : AUS");
								} else {
										myLamp.lampSwitching = true;
										print ("Lichtschalter "+this.tag+ " : EIN");
								}
						}
				}

	}


}
