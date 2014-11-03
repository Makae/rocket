using UnityEngine;
using System.Collections;
using System;

public class StateTester : MonoBehaviour {

	HFSM stateMachine;
	// Use this for initialization
	void Start () {

		Debug.Log ("************************* HFSM **************************");
		this.stateMachine = new HFSM ();
		this.stateMachine.setState (this.stateMachine.PluggedInState);
		Debug.Log ("-----------TO CHASING STATE---------");
		this.stateMachine.setState (this.stateMachine.ChasingState);
		Debug.Log ("-----------BACK TO Plugged STATE---------");
		this.stateMachine.setState (this.stateMachine.PluggedInState);
		Debug.Log ("----------TO PREVIOUS ONE---------");
		this.stateMachine.goBacktoPrevState ();

		ArrayList currentState;
		currentState = this.stateMachine.returnCurrentState ();
		foreach (AbstractHState state in currentState) 
						Debug.Log ("" + state.GetType ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}



}
