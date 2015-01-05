using UnityEngine;
using System.Collections;
using System;
using System.Linq;

class RechargingState : AbstractHState {

	//Deklarieren der Substates
	public EnemyAI enemyAi; 						//enemyAI Script

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
		enemyAi = GameObject.Find(sm.userObjectName).GetComponent<EnemyAI>();


		//save actual state and position, only do it on first entering 
		if (enemyAi.state != "Recharging") {
			enemyAi.changeState("Recharging");
			//lastState = this.state;
			enemyAi.myActualPosition = enemyAi.transform.position;
			enemyAi.myLastPosition = enemyAi.myActualPosition;
		}
		
		//set new target location
		enemyAi.nav.destination = enemyAi.FindClosestTagObject(Tags.powerSupply).transform.position;
		
		
		//recharging automatically if near a powerstation, ...
		
		
		//..change to last state when you are at maxBattaryLevel
		if(enemyAi.maxBatteryLevel <= enemyAi.batteryLevel){
			//back to last operation
			enemyAi.changeState(enemyAi.lastState); //set the state
			//this.state = ; //set the last state
			enemyAi.nav.destination = enemyAi.myLastPosition;//return to the last location
		}

	}

	public override void stopDo(){	}

	public override void doExit(){	}


}
