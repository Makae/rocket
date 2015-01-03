using UnityEngine;
using System.Collections;

public class DoNotPushSwitch : MonoBehaviour {

	//Bei der Aktivierung des DoNotPushSwitches wird Raum A mit der LiftKeyCard geöffnet, aber gleichzeitig auch der Roboter freigelassen
	
	bool online;
	MessageDispatcher md;
	
	void Start () {
		//Zu beginn des Spiels ist der Schalter off und der Room A verschlossen
		this.online = false;
		this.md = MessageDispatcher.Instance ();
		EntityManager.RegisterEntity ("DoNotPushSwitch", this);
	}
	

	void Update () {

		}
				

void OnTriggerEnter(Collider other) { //So betätige ich ihn mal
			Debug.Log ("In range of the DoNotPushSwitch");
				
			if (renderer.material.color != Color.green) {
						renderer.material.color = Color.green; //is on
			this.online=true;
			this.md.Dispatch(0, "RoomPowerControl", "RoomA", "OPEN_DOOR");	
} 
		else { //dann schaltet er sich aus
			renderer.material.color = Color.red;
			this.online=false;
		}
	}				

}