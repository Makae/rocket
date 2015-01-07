using UnityEngine;
using System.Collections;
using System;
using System.Linq;

class PatrolState : AbstractHState {

	//Deklarieren der Substates
	//PatrolState hat im Moment keine Substates

	public EnemyAI enemyAi; 						//enemyAI Script

	public PatrolState(HFSM sm, AbstractHState parent) : base(sm, parent) {

	}
	//Beispiel dazu: Die StateMachine als Memberobjekt der Entity initiert sich beim Starten und kommt in den idle Zustand

	public override void doEntry() {	
		Debug.Log ("ENTERS ON PATROL STATE");	
	}

	public override void startDo(){
		//Debug.Log ("PATROLSTATE");
		//Debug.Log (sm.userObjectName);
		enemyAi = GameObject.Find(sm.userObjectName).GetComponent<EnemyAI>();
		//enemyAi = GameObject.FindGameObjectWithTag("Robot").GetComponent<EnemyAI>();

		//this.state = "Patrolling"; //set the state
		enemyAi.changeState("Patrolling"); //set the state
		// Set an appropriate speed for the NavMeshAgent.
		enemyAi.nav.speed = enemyAi.patrolSpeed;
		
		// If near the next waypoint or there is no destination...
		if(enemyAi.nav.destination == enemyAi.lastPlayerSighting.resetPosition || enemyAi.nav.remainingDistance < enemyAi.nav.stoppingDistance)
		{
			Debug.Log ("If near the next waypoint or there is no destination...");
			// ... increment the timer.
			enemyAi.patrolTimer += Time.deltaTime;
			
			// If the timer exceeds the wait time...
			if(enemyAi.patrolTimer >= enemyAi.patrolWaitTime)
			{
				// ... increment the wayPointIndex.
				if(enemyAi.wayPointIndex == enemyAi.patrolWayPoints.Length - 1)
					enemyAi.wayPointIndex = 0;
				else
					enemyAi.wayPointIndex++;
				
				// Reset the timer.
				enemyAi.patrolTimer = 0;
			}
		}
		else
			// If not near a destination, reset the timer.
			enemyAi.patrolTimer = 0;
		//Debug.Log ("set new waypoint position patrolling");
		// Set the destination to the patrolWayPoint.
		//Debug.Log ("wayPointIndex");
		//Debug.Log (wayPointIndex);
		enemyAi.nav.destination = enemyAi.patrolWayPoints[enemyAi.wayPointIndex].position;

				
	}

	public override void stopDo(){	}

	public override void doExit(){	
		Debug.Log ("LEAVING ON PATROL STATE");	
	}

	public override bool onMessage(Telegram tg) {
		return true; //Wenn das Telgram eine Wirkung zeigte, dann gib true zurück
	}
}
