using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Interaction : MonoBehaviour {
	private Transform Camera;
	private PlayerObject Handler;
	public float MaxReach = 2.5f;

	public void Start() {
		this.Camera = this.transform.Find("Main Camera");
		this.Handler = (PlayerObject) EntityManager.GetEntity("player");
			
	}

	public void Update () {
		if(Input.GetMouseButtonUp(0))
			this.ExecuteInteraction();
	}

	public void ExecuteInteraction() {
		Vector3 fwd = this.Camera.TransformDirection(Vector3.forward);
		RaycastHit hit = new RaycastHit();

		if(Physics.Raycast(this.Camera.position, fwd, out hit, (float) this.MaxReach)) {
			GameObject obj = hit.transform.gameObject;
			Debug.Log (obj);
			Debug.Log (this.Handler);
			
			this.Handler.Interact(obj);
		}
	}
}