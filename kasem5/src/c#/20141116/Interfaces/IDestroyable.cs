using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// MonoBehaviour is necessary because the Update Method has to be called
public interface IDestroyable  {
	void OnMessage(Telegram telegram);
}