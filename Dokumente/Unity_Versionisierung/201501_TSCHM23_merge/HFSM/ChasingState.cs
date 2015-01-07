using UnityEngine;
using System.Collections;
using System;
using System.Linq;

class ChasingState : AbstractHState {

	public EnemyAI enemyAi; 						//enemyAI Script

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
		//Debug.Log (" CHASING State");
		enemyAi = GameObject.Find(sm.userObjectName).GetComponent<EnemyAI>();
		enemyAi.changeState("Chasing"); //set the state
		
		// Create a vector from the enemy to the last sighting of the player.
		Vector3 sightingDeltaPos = enemyAi.enemySight.personalLastSighting - enemyAi.transform.position;
		//Debug.Log (sightingDeltaPos);
		//Debug.Log (sightingDeltaPos.sqrMagnitude);
		// If the the last personal sighting of the player is not close...
		if(sightingDeltaPos.sqrMagnitude > enemyAi.minPlayerDetectionRange)
			// ... set the destination for the NavMeshAgent to the last personal sighting of the player.
			enemyAi.nav.destination = enemyAi.enemySight.personalLastSighting;
		
		// Set the appropriate speed for the NavMeshAgent.
		enemyAi.nav.speed = enemyAi.chaseSpeed;
		
		// If near the last personal sighting...
		if(enemyAi.nav.remainingDistance < enemyAi.nav.stoppingDistance)
		{
			// ... increment the timer.
			enemyAi.chaseTimer += Time.deltaTime;
			
			// If the timer exceeds the wait time...
			if(enemyAi.chaseTimer >= enemyAi.chaseWaitTime)
			{
				// ... reset last global sighting, the last personal sighting and the timer.
				enemyAi.lastPlayerSighting.position = enemyAi.lastPlayerSighting.resetPosition;
				enemyAi.enemySight.personalLastSighting = enemyAi.lastPlayerSighting.resetPosition;
				enemyAi.chaseTimer = 0f;
			}
		}
		else
			// If not near the last sighting personal sighting of the player, reset the timer.
			enemyAi.chaseTimer = 0f;
	}

	public override void stopDo(){	
		Debug.Log ("STOP CHASING STUFF");
	}

	public override void doExit(){	
		Debug.Log ("EXIT CHASING STATE");}


}
