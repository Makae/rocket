using UnityEngine;
using System.Collections;

class RechargingState : AbstractHState {

	//Deklarieren der Substates

	public RechargingState(HFSM sm, AbstractHState parent) : base(sm, parent) {

	}
	//Beispiel dazu: Die StateMachine als Memberobjekt der Entity initiert sich beim Starten und kommt in den idle Zustand

	public override bool onMessage(Telegram tg) {
		return true; //Wenn das Telgram eine Wirkung zeigte, dann gib true zurück
	}

	public override void doEntry() {
		Debug.Log ("ENTER RECHARGING STATE");	
	 //Enters OnAkku from TopLevel. Standard wechsel zu Idle

	}

	public override void startDo(){
		Debug.Log ("RECHARGING STATE");	

		}

	public override void stopDo(){	}

	public override void doExit(){
		Debug.Log ("EXIT RECHARGING STATE");
	}


}
