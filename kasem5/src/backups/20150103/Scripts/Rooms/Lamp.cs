using UnityEngine;
using System.Collections;


public abstract class Lamp  : AbstractGameEntity {

	//Variablen für alle Lampen
	protected bool lampSwitcher = false;						//Ihr Schalter
	protected GameObject parentRoom = null;			//Der Raum in dem sie sind
	protected Room parentRoomScript = null;			//Das Script des Parentraumes um auf die Funktionen und Variablen zuzugreifen
	public Light myLight=null;							//Ihre Lichtkomponente


	public void lampControl() {
		//print ("LampControl aufgerufen für: " + this + " " +lampSwitcher + " ist der State des Switchers, zugehörig zur Lampe: "+ tag);
		if (lampSwitcher == true && parentRoomScript.environment.getPowerState ()) {
			myLight.enabled = true;
		} 
		
		
		else myLight.enabled = false;

	}

	public bool lampSwitching
	{
		get { return this.lampSwitcher; }
		set { this.lampSwitcher = value; }
	}


}
