using UnityEngine;
using System.Collections;

class InitState : AbstractHState {
	
	public InitState(HFSM sm, AbstractHState parent) : base(sm, parent) {
	}
	
	public override bool onMessage(Telegram tg) {
		return true;
	}
	
	public override void doEntry() {	
		Debug.Log ("ENTERS InitState");
	}
	
	public override void startDo(){
		Debug.Log ("DOING InitState");
	}
	
	public override void stopDo(){	}
	
	public override void doExit(){
		Debug.Log ("EXIT InitState");
	}
	
	
}
