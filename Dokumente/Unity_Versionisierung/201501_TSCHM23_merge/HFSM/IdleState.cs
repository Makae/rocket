using UnityEngine;
using System.Collections;

class IdleState : AbstractHState {

	public IdleState(HFSM sm, AbstractHState parent) : base(sm, parent) {
	}

	public override bool onMessage(Telegram tg) {
		return true;
	}

	public override void doEntry() {	
		Debug.Log ("ENTERS IDLE");
	}

	public override void startDo(){
		Debug.Log ("DOING IDLETHINGS");
		}

	public override void stopDo(){	}

	public override void doExit(){
		Debug.Log ("EXIT IDLE");
	}


}
