using UnityEngine;
using System.Collections;

class AkkuState : AbstractHState {

	public AkkuState(HFSM sm, AbstractHState parent) : base(sm, parent) {

	}
	//Beispiel dazu: Die StateMachine als Memberobjekt der Entity initiert sich beim Starten und kommt in den idle Zustand

	public override bool onMessage(Telegram tg) {
		return true; //Wenn das Telgram eine Wirkung zeigte, dann gib true zurück
	}

	public override void doEntry() {	

		Debug.Log ("ENTERS AKKUSTAT");
		
	}

	public override void startDo(){
		Debug.Log ("***AkkuState***");
		}

	public override void stopDo(){
		Debug.Log ("END AKKUSTAT THINGS");
	}

	public override void doExit(){
		Debug.Log ("EXIT AKKUSTAT THINGS");}


}
