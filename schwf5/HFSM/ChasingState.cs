using UnityEngine;
using System.Collections;

class ChasingState : AbstractHState {

	//Deklarieren der Substates
//KEINE SUBSTATES

	public ChasingState(HFSM sm, AbstractHState parent) : base(sm, parent){
	}
	//Beispiel dazu: Die StateMachine als Memberobjekt der Entity initiert sich beim Starten und kommt in den idle Zustand

	public override bool onMessage(Telegram tg) {
		return true; //Wenn das Telgram eine Wirkung zeigte, dann gib true zurück
	}

	public override void doEntry() {
		Debug.Log ("ENTERS CHASING STATE");

	}

	public override void startDo(){
		Debug.Log ("DOING CHASING STUFF");
		}

	public override void stopDo(){	}

	public override void doExit(){	
		Debug.Log ("EXIT CHASING STATE");}


}
