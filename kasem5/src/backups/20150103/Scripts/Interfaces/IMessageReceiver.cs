using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IMessageReceiver  {
	string EntKey {
		get; 
	}
	void OnMessage(Telegram telegram);
}
