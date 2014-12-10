using UnityEngine;
using System.Collections;

public class RestartLevel : MonoBehaviour {
	GameObject enemy;
	private EnemyAI enemyai;
	private LastPlayerSighting lastPlayerSighting;

	void Start(){
		//get the robot Object to access lastPlayer position and 
		enemy = transform.parent.gameObject;


	}

	void Update(){
		//Debug.Log ("XX Restert Level : position enemy");
		//Debug.Log (enemy.transform.position.ToString());
	}

	//reset the position of the robot to the last position of playersighted
	void resetPosition(){
		lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
		//GameObject gameController = GameObject.FindGameObjectWithTag<Tags.gameController>;
		lastPlayerSighting.position = lastPlayerSighting.resetPosition;
		//EnemyAI enemyai;
	}




	//if trigger is activated
	void OnTriggerStay (Collider other)
	{
		if(other.gameObject.tag == Tags.player) //.. from player object
		{
			Debug.Log ("player in Deadzone!");
			this.resetPosition();
			//enemy.GetComponent<EnemyAI>();
			//GameObject enemy
			//GameObject enemy = GameObject.FindGameObjectWithTag(Tags.enemy);
			//EnemyAI enemyai = enemy.GetComponent<EnemyAI>();
			//if(enemyai.state == "Chasing"){
			//	enemyai.state = "Patrolling";
			//}
			//GameObject enemy = GameObject.FindGameObjectWithTag(Tags.enemy);
			//EnemyAI enemyai = this.enemy.GetComponent<EnemyAI>();
			//enemyai.state("Patrolling");
			//enemyai.state("Patrolling");

			//anderer Ansatz
			//GameObject enemy2 = GameObject.FindGameObjectWithTag(Tags.enemy);
			//LastPlayerSighting lastPlayerSighting = enemy2.GetComponent<LastPlayerSighting>();
			//lastPlayerSighting.position = lastPlayerSighting.resetPosition;

			Application.LoadLevel("Main_Menu"); //go to Mainmenu
			//Application.LoadLevel (Application.loadedLevel); //restart level
		}
	}
}
