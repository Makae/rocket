using UnityEngine;
using System.Collections;

class TopLevel : AbstractHState {

	public TopLevel(HFSM sm, AbstractHState parent) : base(sm, parent) {
	}

	public override void doEntry() {	
		Debug.Log ("Initialization");
	}

	public override void startDo(){
	}

	public override void stopDo(){	}
	public override void doExit(){
		Debug.Log ("EXIT TOPLEVEL");
	}

	public override bool onMessage(Telegram tg) {
		return true; //Wenn das Telgram eine Wirkung zeigte, dann gib true zurück
	}

}
