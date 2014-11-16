using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProximityMessage : MonoBehaviour {
	public float MaxDistance = 3.5f;	
	public string label;

	// Is the message visible on the gui?
	private PlayerObject player = null;

	void Awake() {
		this.player = (PlayerObject) EntityManager.GetEntity("player");
	}

	void Update() {
		if(this.Distance () >= this.MaxDistance) {
			MessageManager.removeMessage(this.label);
			return;
		}
		if(!this.player.LookingAt(this.gameObject, this.MaxDistance)) {
			MessageManager.removeMessage(this.label);
			return;
		}

		MessageManager.setMessage (this.label);
	}
	
	private float Distance() {
		return Mathf.Sqrt((this.transform.position - this.player.transform.position).sqrMagnitude);
	}	

}