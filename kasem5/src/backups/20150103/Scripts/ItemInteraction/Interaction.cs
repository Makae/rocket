using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/** 
 * The Interaction Script is used for determine if an
 * Interaction with an Game Object is made (On a mouse click)
 */
public class Interaction : MonoBehaviour, IMessageReceiver {
	private Transform Camera;
	private PlayerObject Handler;
	public float MaxReach = 3f;
	private bool active = true;

	public string EntKey {
		get {
			return "interaction";
		}
	}

	public void Start() {
		this.Camera = this.transform.Find("Main Camera");
		this.Handler = (PlayerObject) EntityManager.GetEntity("player");
		EntityManager.AddToGroup("interaction", this);
	}

	public void Update () {
		if(Input.GetMouseButtonUp(0))
			this.ExecuteInteraction();
	}

	/**
	 * The functionality can be interrupted by other gameobjects
	 */
	public void OnMessage(Telegram t) {
		if(t.Message == "interrupt") {
			this.active = false;
		} else if(t.Message == "proceed") {
			this.active = true;
		}
	}

	/**
	 * If the Player clicked a Ray cast is sent out up to the max reach
     * the Vector for the cast is the camera-forward vector
     * If an object is in the way the Hander.Interact is then called
	 */
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