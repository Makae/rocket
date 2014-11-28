using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// MonoBehaviour is necessary because the Update Method has to be called
public class MessageDispatcher : MonoBehaviour {
  private static MessageDispatcher Inst = null;
  private List<Telegram> Telegrams = new List<Telegram>();

  private MessageDispatcher() {}

  public static MessageDispatcher Instance() {
	return MessageDispatcher.Inst;
  }

	public void Awake() {
		MessageDispatcher.Inst = this;
	}

  public void Update() {
    List<Telegram> new_telegrams = new List<Telegram>();

    for(int i = 0; i < this.Telegrams.Count; i++) {
      Telegram telegram = this.Telegrams[i];
      if(telegram.DispatchTime <= Time.time) {
        this.Discharge(telegram);
      } else {
        new_telegrams.Add(telegram);
      }
    }
    this.Telegrams = new_telegrams;
  }

  public void Discharge(Telegram telegram) {
	List<object> group;

    if(EntityManager.IsGroup(telegram.To)) {
      group = EntityManager.GetGroup(telegram.To);
    } else {
      object to = EntityManager.GetEntity(telegram.To);
	  group = new List<object>();
	  group.Add(to);
    }

	foreach(IMessageReceiver entity in group) {
//	  Debug.Log ("Sending Message To");
//	  Debug.Log(entity);
	  if(entity != null && entity is IMessageReceiver) {
		
		((IMessageReceiver) entity).OnMessage(telegram);
	  }
    }

  }

  public void Dispatch(float delay, string from, string to, string message) {
	this.Dispatch(delay, from, to, message, new Hashtable());
  }

  public void Dispatch(float delay, string from, string to, string message, Hashtable args) {
    Telegram telegram = new Telegram(Time.time + delay, from, to, message, args);

    if(delay <= 0) {
      this.Discharge(telegram);
    } else {
      this.Telegrams.Add(telegram);
    }

  }

}
