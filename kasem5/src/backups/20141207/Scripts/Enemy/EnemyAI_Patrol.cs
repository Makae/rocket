using UnityEngine;
using System.Collections;
/*
 *@autor TSCM
 * First script to enable the Robot to patrol.
 * is deprechated
 * use MovePatrouille() if you want  a patrol ability
 * 
 * 
 * 
 */

public class EnemyAI_Patrol : MonoBehaviour {
	//public Transform Waypoints;
	public Transform[] Waypoints;
	public float Speed;
	public int curWayPoint;
	public bool doPatrol = true;
	public Vector3 Target;
	public Vector3 MoveDirection;
	public Vector3 Velocity;

	void Update(){
		if (curWayPoint < Waypoints.Length) {
				Target = Waypoints [curWayPoint].position;
				MoveDirection = Target - transform.position;
				Velocity = rigidbody.velocity;

				if (MoveDirection.magnitude < 1) {
						curWayPoint++;

				} else {
						Velocity = MoveDirection.normalized * Speed;
				}
		} else {
			if(doPatrol){
				curWayPoint = 0;
			}else{
				Velocity = Vector3.zero;
			}
		}

		//neue geschwindigkeit in die richtige richtung aufnehmen
		rigidbody.velocity = Velocity;
		transform.LookAt(Target); //Blickrichtung ändern
	} 
}
