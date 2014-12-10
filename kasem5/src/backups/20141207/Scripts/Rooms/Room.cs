using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Room : AbstractGameEntity {

	public Environment environment;
	protected GameObject[] doors;


	public void closeDoor() {

		for (int i = 0; i < this.doors.Length; i++) {
						GameObject door = this.doors [i];
						MeshRenderer MeshComponent = door.GetComponent<MeshRenderer> ();
						MeshComponent.enabled = true;
						door.collider.enabled = true;
				}

	}

	public void openDoor() {
		
		for (int i = 0; i < this.doors.Length; i++) {
				print ("Türen werden geöffnet");
						GameObject door = this.doors [i];
						MeshRenderer MeshComponent = door.GetComponent<MeshRenderer> ();
						MeshComponent.enabled = false;
						door.collider.enabled = false;
				}
	}


}