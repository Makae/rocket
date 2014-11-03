using UnityEngine;
using System.Collections;

class PatrolState : AbstractHState {

	//Deklarieren der Substates
	//PatrolState hat im Moment keine Substates


	public PatrolState(HFSM sm, AbstractHState parent) : base(sm, parent) {
	}
	//Beispiel dazu: Die StateMachine als Memberobjekt der Entity initiert sich beim Starten und kommt in den idle Zustand

	public override void doEntry() {	
		Debug.Log ("ENTERS ON PATROL STATE");	
	}

	public override void startDo(){
		Debug.Log ("***PATROLSTATE***");
			
		}

	public override void stopDo(){	}

	public override void doExit(){	
		Debug.Log ("LEAVING ON PATROL STATE");	
	}

	public override bool onMessage(Telegram tg) {
		return true; //Wenn das Telgram eine Wirkung zeigte, dann gib true zurück
	}
}
