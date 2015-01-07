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
	//otherwise the alarmsound is played..
	void resetPosition(){
		lastPlayerSighting = GameObject.Find("GameController").GetComponent<LastPlayerSighting>();
		lastPlayerSighting.position = lastPlayerSighting.resetPosition;
	}

	//shut down skynet to deactivate the robots
	void deactivateSkynet(){
		Skynet skynet = GameObject.Find("GameController").GetComponent<Skynet>();
		skynet.robotsActiv = false;
	}




	//if trigger is activated
	void OnTriggerStay (Collider other)
	{
		if(other.gameObject.tag == Tags.player) //.. from player object
		{
			Debug.Log ("player in Deadzone!");
			//lastPlayerSighting = GameObject.Find("GameController").GetComponent<LastPlayerSighting>();
			//deactivateSkynet ();
			resetPosition();

			AudioSource alertAudio = GameObject.Find("music_secondaryMusic").audio;
			alertAudio.volume = 0;
			Application.LoadLevel("Menu_Main"); //go to Mainmenu
		}
	}
}
