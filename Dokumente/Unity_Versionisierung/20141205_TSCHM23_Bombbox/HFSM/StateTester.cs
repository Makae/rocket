using UnityEngine;
using System.Collections;

public class StateTester : MonoBehaviour {

	HFSM stateMachine;
	// Use this for initialization
	void Start () {

		Debug.Log ("************************* HFSM **************************");
		this.stateMachine = new HFSM ("tester");
		this.stateMachine.setState (this.stateMachine.PatrolState);
		//this.stateMachine.setState (this.stateMachine.PluggedInState);

//		Debug.Log ("Now remember what I was doing:");
//		Debug.Log ("******************************");
//		this.stateMachine.goBacktoPrevState ();


		ArrayList currentState;
		currentState = this.stateMachine.returnCurrentState ();
		foreach (AbstractHState state in currentState) 
						Debug.Log ("" + state.GetType ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
