using UnityEngine;
using System.Collections;

class SearchState : AbstractHState {

	//Deklarieren der Substates
	//PatrolState hat im Moment keine Substates


	public SearchState(HFSM sm, AbstractHState parent) : base(sm, parent){
	}
	//Beispiel dazu: Die StateMachine als Memberobjekt der Entity initiert sich beim Starten und kommt in den idle Zustand

	public override void doEntry() {	
		Debug.Log ("ENTER SEARCH");

	}

	public override void startDo(){
		Debug.Log ("DOING SEARCH STUFF");
		}

	public override void stopDo(){	}

	public override void doExit(){
		Debug.Log ("EXIT SEARCH ");}

	public override bool onMessage(Telegram tg) {
		return true; 
	}

}
