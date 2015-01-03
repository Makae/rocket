using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Extends the Item class for stackable items
 * the StackGroup determines which Items are Bundled together
 */
public class Stackable : Item {
	public int Amount = 10;
	public string StackGroup = "";

	new public void Awake() {
		base.Awake();
	}

}