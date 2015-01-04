using UnityEngine;
using System.Collections;


//Contains utilites to pathing in unity , used for robot unit
public class PathUtils : MonoBehaviour { 

	//public var's
	public float pufferToReachTargetLocation = 30f;		//how much sooner should a robot return to a powerstation? in meter
	public float lengthToNearestPowersupply = 0.0f;		//holds the distance to next powersupply, will be calculated per update.

	//private var's
	private NavMeshAgent agent; 
	private Color c = Color.white; 

	//init the navMesh Agent to work with later
	public void Start() { 
		agent = this.gameObject.GetComponent<NavMeshAgent>();
	}


	public void Update() {
		//redraw the path every update.
		DrawPath (agent.path);
		calcRechargingStateChange (); 
	}

	//funktion to draw a visible line along the unity path of the owner
	void DrawPath(NavMeshPath path) {
		//Debug.Log ("draw path in pathUtils");
		if (path.corners.Length < 2) {
			c = Color.green;
			return;
		}

		//a try to, make the path, completed, invalid or partial, visible to the player/Developer, ... not yet working, dont know why..
		switch (path.status) {
		case NavMeshPathStatus.PathComplete:
			c = Color.white;
			break;
		case NavMeshPathStatus.PathInvalid:
			c = Color.red;
			break;
		case NavMeshPathStatus.PathPartial:
			c = Color.yellow;
			break;
		}
		
		Vector3 previousCorner = path.corners[0];
		
		int i = 1;
		while (i < path.corners.Length) {
		
	




			Vector3 currentCorner = path.corners[i];
			Debug.DrawLine(previousCorner, currentCorner, c);
			previousCorner = currentCorner;
			i++;
		}
		
	}

	//calc the possible distance a unit can move 
	void calcRechargingStateChange(){
			//Debug.Log ("nearest Powersupp");
			Vector3 nearestPowersupplyPos = gameObject.GetComponent<EnemyAI> ().FindClosestTagObject (Tags.powerSupply).transform.position;
	
	
			//Debug.Log (Vector3.Distance(transform.position, path.corners[i]));
			//Debug.Log (nearestPowersupplyPos);
	
			float robotSpeed = gameObject.GetComponent<NavMeshAgent> ().speed;
			//float batteryChange = gameObject.GetComponent<EnemyAI> ().powerChange;
			float batteryLevel = gameObject.GetComponent<EnemyAI> ().batteryLevel;
	
			lengthToNearestPowersupply = Vector3.Distance (transform.position, nearestPowersupplyPos);
			//Debug.Log ("path robotSpeed");
			//Debug.Log (robotSpeed);
			
			string stateOfRobot = gameObject.GetComponent<EnemyAI> ().state;
			//float possibleDistance = (batteryLevel / batteryChange) * robotSpeed;
			float possibleDistance = batteryLevel * robotSpeed;
		//if(possibleDistance < 0){
		//	possibleDistance = possibleDistance * -1;
		//}	
			//Debug.Log ("vergleich der Distanzwerte");
			//Debug.Log ("powersupply " + lengthToNearestPowersupply);
		//Debug.Log ("robotSpeed " + robotSpeed);
		//Debug.Log ("batteryLevel " + batteryLevel);
			//Debug.Log ("possible Distance " + possibleDistance);
	
			//wenn batterylevel * robotspeed + puffer < (kleiner) wird als die distanz
			if (possibleDistance < (lengthToNearestPowersupply + pufferToReachTargetLocation) && stateOfRobot != "Recharging") {
					//..set recharging State
					//criticalBatteryLevel > batteryLevel || stateOfRobot == "Recharging"
					gameObject.GetComponent<EnemyAI> ().Recharging ();
			} else {
					//.. do nothing
			}
	}

}