using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Weapon extends Item and provides a Damage-Property
 * This is then added to the players default damage
 */
public class Weapon : Item {

	public int Damage = 1;

	override public void OnMessage(Telegram t) {
	}
	
	new public void Awake() {
		base.Awake();
	}

}