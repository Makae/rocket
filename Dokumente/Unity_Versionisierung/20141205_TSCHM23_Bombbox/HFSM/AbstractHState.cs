using UnityEngine;
using System.Collections;

//abstrakte Stateklasse, welche vorgibt, was die States alles implementieren müssen. Die beiden entry und exit Methoden sind nicht veränderbar 


public abstract class AbstractHState {

	protected HFSM sm;
	protected AbstractHState parentState;

	/* Wenn man bei den abgeleiteten Klassen keinen Basiskonstruktor angibt, 
	 * there is an implicit call to a parameterless parent constructor inserted. 
	 * That constructor does not exist, and so you get that error. Deshalb kann ich das zuweisen der Variable
	 gerade so gut hier machen, was jeweils eine Linie Code spart*/


	public AbstractHState(HFSM sm, AbstractHState parent) {
			this.sm = sm;
			this.parentState = parent;
		}

	public AbstractHState getParentState() {
		if (this.parentState != null)
						return this.parentState;
				else
						return sm.RechargingState; //standard state
	}


	public void entry() { 
		doEntry(); 
		startDo(); 
	} 
	public void exit() { 
		stopDo(); 
		doExit(); 
	} 

	public abstract void doEntry();
	public abstract void startDo(); 
	public abstract void stopDo();
	public abstract void doExit(); 

	public abstract bool onMessage(Telegram tg);

}
