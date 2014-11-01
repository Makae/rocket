using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractGameEntity : MonoBehaviour, IMessageReceiver {
	
	protected abstract string EntKey {
		get; 
	}
	
	protected abstract List<string> Groups {
		get;
	}
	
    public virtual void OnMessage(Telegram telegram) {

	}

	public void Awake() {
		EntityManager.RegisterEntity(this.EntKey, this);
		this.Groups.Add("global_group");
		
		foreach(string group in this.Groups) {
			EntityManager.AddToGroup(group, this);
		}
	}
}
