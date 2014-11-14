using UnityEngine;
using System.Collections;

public class HFSM {

	public AbstractHState TopLevel;
	public AbstractHState OnDutyState;
	public AbstractHState AkkuState;
	public AbstractHState ChasingState;
	public AbstractHState IdleState;
	public AbstractHState InitState;
	public AbstractHState PatrolState;
	public AbstractHState PluggedInState;
	public AbstractHState RechargingState;
	public AbstractHState SearchState;
	private AbstractHState currentState;
	private AbstractHState previousState;
	private int currentStateLevel;

	public HFSM()
	{
		//ACHTUNG REIHENFOLGE!!
		TopLevel = new TopLevel (this, this.TopLevel);
		AkkuState = new AkkuState (this, this.TopLevel);
		PluggedInState = new PluggedInState (this, this.TopLevel);

		OnDutyState = new OnDutyState (this, this.AkkuState);
		IdleState = new IdleState (this, this.AkkuState);
		InitState = new InitState (this, this.AkkuState);

		PatrolState = new PatrolState (this, this.OnDutyState);
		ChasingState = new ChasingState (this, this.OnDutyState);

		RechargingState = new RechargingState (this, this.PluggedInState);
		SearchState = new SearchState (this, this.AkkuState);

		this.currentState = TopLevel;
		this.previousState = TopLevel;

		this.currentState.entry ();
	}


	/* Einfaches Einsteigen in einen State. Ist der State bereits ein verschachtelter State, werden auf dem Weg dortin alle StateEntries durchlaufen  */

	public void setState(AbstractHState newState) {

		if(this.currentState!=this.TopLevel) //Nur speichern, wenn es nicht ein call ist, bei dem man beim TopLevel vorbeikam
			this.previousState = this.currentState;

		if (getLevel (newState) == getLevel (this.currentState)) {

			//Prüfen ob innerhalb des gleichen gleichem States, falls nein, bis zum Toplevel rauf und auf dem Weg alles exiten:
			if(!checkOnSubState(newState)) {
				Debug.Log("Gleiches Level, aber anderer Zweig");

				//go to TopLevel
				while(this.currentState!=this.TopLevel) {
					this.currentState.exit();
					this.currentState = this.currentState.getParentState();
				} //Am Ende der While schlaufe ist man beim TopLevel und hat alle States verlassen.

			}

			else { //Normaler Switch auf gleichem Level
				changeStateOnSameLevel(newState);
				return;
			}
		}

		//Wenn der Level vom Ziel grösser ist, befindet es sich tiefer in der Hierachie:
		if (getLevel (newState) > getLevel (this.currentState)) {

			changeStateToDeeperLevel(newState);
	}


		else {

			Debug.Log ("Der ZielState ist auf einem kleineren Level, also weiter oben");
			setStateWithExit (newState); }


}//END OF SET STATE




	public void changeStateToDeeperLevel(AbstractHState newState) {

		Debug.Log ("Zielstate ist weiter unten in der Hierarchie. Ist es ein Substate?: " + checkOnSubState (newState));
		//In dem Array sind alle States vom Zielstate bis rauf zum TopLevel

		//Wenn der Zielstate nicht im gleichen Branch ist, muss zuers talles bis zum TopLevel verlassen werden
		if (!checkOnSubState (newState)) {

			while(this.currentState != this.TopLevel) {
				this.currentState.doExit ();
				this.currentState=this.currentState.getParentState();
			}
			//Und nachdem man im TopLevel angelangt ist, von dort runter zum Ziel
			Debug.Log ("NUN IM: " + this.currentState.ToString ());

		}

		ArrayList stations = getStatesAbove (newState);
		int statesToPass = (stations.Count - 1);

		//Falls das Ziel in einem anderen Branch war, hat man mit dem if oben in den TopLevel gewechselt. Ansonsten ist man in einem State der den ZielState als substate hat
//		Debug.Log ("Array sieht so aus, in dessen States man nun wechselt:");
//		foreach (AbstractHState value in stations) {
//			Debug.Log (value.ToString());
//		}
		while (statesToPass>=0) {
			this.currentState = (AbstractHState)stations [statesToPass]; //Nimmt die länge des Arrays-1, also die Position des letzten Elements
			stations.RemoveAt (statesToPass);
			statesToPass--;
			this.currentState.entry ();

		}

	}

  // @mkaeser: wie sieht es mit states in trees auf dem 3tem level aus?
	public void changeStateOnSameLevel(AbstractHState newState) {
		Debug.Log ("Wechsel auf gleichem Level");
		this.currentState.doExit();
		this.currentState = newState;
		this.currentState.entry();
		return;
	}



	public void goBacktoPrevState() {
		Debug.Log ("Gehe zurück zu: " + this.previousState);
		setState (this.previousState);

	}


	/* Hilfsmethode von setState: Wenn das Zielstatelevel kleiner ist, gehts nach oben in der Hierachie und alle ExitStates auf dem Weg werden ausgeführt
	 Dann noch eine Methode machen, die sich den Stand nur merkt und einfach wechselt ohne Exits*/
	public void setStateWithExit(AbstractHState newState) {


		while (currentState.getParentState()!=newState.getParentState()) {
						//Debug.Log ("Aktueller Status" + this.currentState.ToString ());

			if(this.currentState == this.TopLevel) {
						Debug.Log ("Toplevel erreicht: Also liegt es auf anderem Branch");
						setState(newState);
						return;
			}

						this.currentState.exit ();
						this.currentState = this.currentState.getParentState ();
				}

		//Nur verlassen, wenn man unterhalb des TopLevels wechselt
		if(this.currentState!=newState)
			this.currentState.exit ();

						this.currentState = newState;
						newState.entry ();
		}


	public ArrayList getStatesAbove(AbstractHState target) {
		//Gibt eine Arrayliste zurück, mit allen States, die vom origin zum target durchlaufen werden müssen
		//Vom target nach oben - das muss dann umgedreht werden
		ArrayList list = new ArrayList ();

		Debug.Log("ARRAY FüR DIE STATEWECHSEL:");
		while (target.getParentState()!=this.currentState.getParentState()) {
			Debug.Log (target.ToString());
			list.Add(target);
			target = target.getParentState();

		}

		Debug.Log ("--------------- ENDE DES ARRAYS ---------------");
		return list;
	}




	public bool checkOnSubState(AbstractHState newState) {

		bool isSubstate = false;

		if (newState.getParentState () == this.TopLevel || this.currentState == this.TopLevel)
						return true;
		if (newState.getParentState () == this.currentState.getParentState ())
						return true;

		while (newState.getParentState()!=this.TopLevel) {
			newState = newState.getParentState();

			if(newState == this.currentState){
				isSubstate=true;
			}

		}
		return isSubstate;

	}


	public ArrayList returnCurrentState() { //Das letzte Element im Array ist der aktuelle Zustand. Der Rest ist das, was oberhalb liegt (ohne TopLevel)
		ArrayList list = new ArrayList ();

		while (currentState!=this.TopLevel) {
			list.Add(currentState);
			currentState = currentState.getParentState();
		}
		return list;
	}



	public int getLevel(AbstractHState state) {

		int i = 0;

		if (state == this.TopLevel)
				return i;

		else {
			while (state.getParentState()!=this.TopLevel) {
				i++;
				state = state.getParentState ();
			}
					}
		return i+1;
	}

}


