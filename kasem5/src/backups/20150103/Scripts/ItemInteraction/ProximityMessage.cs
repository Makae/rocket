using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProximityMessage : MonoBehaviour {
	public float MaxDistance = 2.5f;	
	public string label;
	
	// Is the message visible on the gui?
	private PlayerObject player = null;
	
	void Start() {
		this.LoadPlayer();
	}
	
	public void LoadPlayer() {
		if(this.player != null)
			return;
		
		this.player = (PlayerObject) EntityManager.GetEntity("player");
	}
	
	void Update() {
		if(this.Distance () >= this.MaxDistance) {
			TextDisplayer.removeMessage(this.label);
			return;
		}
		
		if(!this.player.LookingAt(this.gameObject, this.MaxDistance)) {
			TextDisplayer.removeMessage(this.label);
			return;
		}
		
		TextDisplayer.setMessage (this.label);
	}
	
	private float Distance() {
		return Mathf.Sqrt((this.transform.position - this.player.transform.position).sqrMagnitude);
	}	
	
}