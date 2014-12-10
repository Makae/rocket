using UnityEngine;
using System.Collections;

public class RoomPowerSwitch : MonoBehaviour {

	//Bei Aktivierung des PowerButtons wird ein Telegram an die Gruppe Rooms verschickt, dass der Primärstrom wieder da ist.
	//Dies erreicht alle Environments der Räume, welche die Message als Gruppenmembers erhalten und setzt dort die powerState Variable auf true.
	
	bool online;
	GameObject RoomB;
	MessageDispatcher md;
	
	void Start () {
		//Zu beginn des Spiels ist der Primärstrom ja ausgeschaltet
		this.online = false;
		this.md = MessageDispatcher.Instance ();

		//Register GameObject in Entitymanager anhand Beispiel, wie sich ein Raum-X registrieren muss
		//EntityManager.AddToGroup ("Room", this);

		EntityManager.RegisterEntity ("RoomPowerSwitch", this);

		//Im Prototyp habe ich Räume direkt angesprochen
		//RoomB = GameObject.FindGameObjectWithTag ("RoomB");
		//RoomBScript = RoomB.GetComponent<RoomB>();
	}
	

	void Update () {

		}
				

void OnTriggerEnter(Collider other) { //So betätige ich ihn mal
				Debug.Log ("In range of the PowerSwitch");
				
			if (renderer.material.color != Color.green) {
						renderer.material.color = Color.green; //is on
			this.online=true;
			this.md.Dispatch(0, "RoomPowerControl", "group:Rooms", "POWER_ON");	
			//Nebst der Message an die Räume, dass der Strom wieder verfügbar ist, schaltet auch das Licht für den Kontrollraum wieder auf weiss,
			//weil nun nichts mehr auf Notstrom läuft
			Light LampControlRoom = GameObject.FindGameObjectWithTag("LampControlRoom").GetComponent<Light>();
			LampControlRoom.color = Color.white;

			} 
		else { //dann schaltet er sich aus
						renderer.material.color = Color.red;
						this.online=false;
		}
	}				

}