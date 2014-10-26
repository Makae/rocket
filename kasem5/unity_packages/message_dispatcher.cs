using UnityEngine;
using System.Collections;

public class MessageDispatcher : MonoBehaviour {
  private static MessageDispatcher instance = null;
  private List<Telegram> telegrams = new List<Telegram>();

  private MessageDispatcher() {}

  public static MessageDispatcher instance() {
    if(instance == null)
      return new MessageDispatcher();
    return instance;
  }

  public void Update() {
    List<Telegram> new_telegrams = new List<Telegram>();

    for(int i = 0; i < telegrams.Count; i++) {
      Telegram telegram = telegrams[i];
      if(telegram.dispatchTime <= Time.now) {
        dispatch(telegram);
      } else {
        new_telegrams.push(telegram);
      }
    }
    telegrams = new_telegrams;

  }

  public void discharge(telegram) {
    List<AbstractGameEntity> group;

    if(EntityManager.instance().isGroup(telegram.to)) {
      group = EntityManager.getGroup(telegram.to);
    } else {
      AbstractGameEntity to = EntityManager.getEntityById(telegram.to);
      group = new List<AbstractGameEntity>(to);
    }

    foreach(AbstractGameEntity entity in group) {
      entity.onMessage(telegram);
    }

  }

  public void dispach(float delay, String from, String to, String message, HashTable args) {
    Telegram telegram = new Telegram(Time.now + delay, from, to, message, args);

    if(delay <= 0) {
      discharge(telegram)
    } else {
      telegrams.push(telegram);
    }

  }

}
