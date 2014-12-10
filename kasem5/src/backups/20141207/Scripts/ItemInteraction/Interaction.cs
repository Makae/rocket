using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Interaction : MonoBehaviour, IMessageReceiver {
	private Transform Camera;
	private PlayerObject Handler;
	public float MaxReach = 2.5f;
	private bool active = true;
	
	public string EntKey {
		get { 
			return "interaction"; 
		} 
	}

	public void Start() {
		this.Camera = this.transform.Find("Main Camera");
		this.Handler = (PlayerObject) EntityManager.GetEntity("player");
		EntityManager.AddToGroup("group:interaction", this);
	}

	public void Update () {
		if(Input.GetMouseButtonUp(0))
			this.ExecuteInteraction();
	}

	public void OnMessage(Telegram t) {
		Debug.Log (this.EntKey + "_" + t.Message);
		if(t.Message == "interrupt") {
			this.active = false;
		} else if(t.Message == "proceed") {
			this.active = true;
		}
	}

	public void ExecuteInteraction() {
		if(!this.active)
			return;

		Vector3 fwd = this.Camera.TransformDirection(Vector3.forward);
		RaycastHit hit = new RaycastHit();

		if(Physics.Raycast(this.Camera.position, fwd, out hit, (float) this.MaxReach)) {
			GameObject obj = hit.transform.gameObject;
			this.Handler.Interact(obj);
		}
	}
}