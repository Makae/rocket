using UnityEngine;
using System.Collections;

public class HFSM {

	//Hier alle States registrieren, die von der HFSM classe verwaltet werden soll.
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
	public string userObjectName;

	public HFSM(string userName)
	{
		userObjectName = userName;
		Debug.Log ("XXXXXXXXXX OBJECT USER NAME XX");
		Debug.Log (userObjectName);
		//ACHTUNG REIHENFOLGE, intialisiere immer zuerst die States, die dann zu Parentstates werden.!!
		//level 0
		TopLevel = new TopLevel (this, this.TopLevel); 
		AkkuState = new AkkuState (this, this.TopLevel);
		PluggedInState = new PluggedInState (this, this.TopLevel);

		//level 2
		OnDutyState = new OnDutyState (this, this.AkkuState);
		IdleState = new IdleState (this, this.AkkuState);
		InitState = new InitState (this, this.AkkuState);
	
		//level 3
		PatrolState = new PatrolState (this, this.OnDutyState);
		ChasingState = new ChasingState (this, this.OnDutyState);

		//level 2
		RechargingState = new RechargingState (this, this.PluggedInState);
		SearchState = new SearchState (this, this.AkkuState);

		this.currentState = TopLevel;
		this.previousState = TopLevel;

		this.currentState.entry ();
	}


	/* Einfaches Einsteigen in einen State. Ist der State bereits ein verschachtelter State, werden auf dem Weg dortin alle StateEntries durchlaufen  */

	public void setState(AbstractHState newState) {

		//abgefangen, wenn vom gleichen State in denselben State gewechselt wird, nichts machen.
		if(this.currentState == newState){
			this.currentState.startDo();
			//Debug.Log("neuer State setzen, aber in dem state befinde ich mich bereits...");
			return;
		}
	
		/* Wenn die Level gleich sind, wird ein normales exit des current states und ein entry des new states ausgeführt
		---> AUSNAHME: Gleicher Level, aber in einem anderen Hierarchiezweig: Dann zum TopLevel und von dort runter zum Ziel	  */

	if(this.currentState!=this.TopLevel) //Nur speichern, wenn es nicht ein call ist, bei dem man beim TopLevel vorbeikam
			this.previousState = this.currentState;
			//Debug.Log ("Entering setState Method: previous State gesetzt als: " + this.previousState);


		if (getLevel (newState) == getLevel (this.currentState)) {
			Debug.Log("Gleiches Level..");
			//Prüfen ob innerhalb des gleichen gleichem States, falls nein, bis zum Toplevel rauf und auf dem Weg alles exiten:

			if(!checkOnSubState(newState)) {
				Debug.Log("Gleiches Level, aber anderer Zweig");

				//go to TopLevel
				while(this.currentState!=this.TopLevel) {
					this.currentState.exit();
					this.currentState = this.currentState.getParentState();
				} //Am Ende der While schlaufe ist man beim TopLevel und hat alle States verlassen.

			}

			else {
			//Und sonst ein einfacher Wechsel:
			Debug.Log ("Wechsel auf gleichem Level");
			this.currentState.doExit();
			this.currentState = newState;
			this.currentState.entry();
			return;
			}
		}

		//Wenn der Level vom Ziel grösser ist, befindet es sich tiefer in der Hierachie:
		if (getLevel (newState) > getLevel (this.currentState)) { 
						Debug.Log ("Zielstate ist weiter unten in der Hierarchie."); 
						Debug.Log ("Ist das Ziel ein Substate?: " + checkOnSubState (newState));

						ArrayList stations = getStatesAbove (newState); 

						int statesToPass = (stations.Count - 1);
		
						//Wenn der Zielstate im gleichen Branch ist:
						if (!checkOnSubState (newState)) 
										this.currentState.doExit ();

				while (statesToPass>=0) {

				this.currentState = (AbstractHState)stations [statesToPass]; //Nimmt die länge des Arrays-1, also die Position des letzten Elements
					stations.RemoveAt (statesToPass);
					statesToPass--;
					this.currentState.entry ();

				}

		}
		else
		{
		
			Debug.Log ("Der ZielState ist auf einem kleineren Level, also weiter oben");
			setStateWithExit (newState); }
			




	}//END OF SET STATE


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
		if (this.currentState != newState) {
			Debug.Log("der currentState ist nicht gleich dem newState.");	
			this.currentState.exit ();
		}
						this.currentState = newState;
						newState.entry ();	
		}


	public ArrayList getStatesAbove(AbstractHState target) {
		Debug.Log("aufruf getStatesAbove");
		//Gibt eine Arrayliste zurück, mit allen States, die vom origin zum target durchlaufen werden müssen
		//Vom target nach oben - das muss dann umgedreht werden
		ArrayList list = new ArrayList ();

		while (target.getParentState()!=this.currentState.getParentState()) {
			list.Add(target);
			target = target.getParentState();
		}
		return list;
	}




	public bool checkOnSubState(AbstractHState newState) {
		bool isSubstate = false;

		if (newState.getParentState () == this.TopLevel || this.currentState == this.TopLevel)
						return true;

		while (newState.getParentState()!=this.TopLevel) { //Solange nach oben loopen bis man beim Root ist
			newState = newState.getParentState();
			//Debug.Log("Neuer Newstate: "+newState + "  ___ CurrentState von dem ausgegeangen wird: "+this.currentState);

			if(newState == this.currentState){
				isSubstate=true;
			}
		
		}
		return isSubstate;

	}


	public ArrayList returnCurrentState() { //Das letzte Element im Array ist der aktuelle Zustand. Der Rest ist das, was oberhalb liegt (ohne TopLevel)
		ArrayList list = new ArrayList ();
		//AbstractHState tempState = this.currentState; //wird nicht gebraucht?

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


