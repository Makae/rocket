using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Serves as a Parent Class for automatically registering a GameEntity 
 * to the EntityManager
 */
public abstract class AbstractGameEntity : MonoBehaviour, IMessageReceiver {
	
	public abstract string EntKey {
		get; 
	}
	
	public abstract List<string> Groups {
		get;
	}
	
    public virtual void OnMessage(Telegram telegram) {
	
	}

	public void Awake() {
		if(this.EntKey != null)
			EntityManager.RegisterEntity(this.EntKey, this);
		//print ("AbstractGameEntity Class registrierte " + this.EntKey);
		this.Groups.Add("global_group");
		
		foreach(string group in this.Groups) {
			EntityManager.AddToGroup(group, this);
		}
	}
}
