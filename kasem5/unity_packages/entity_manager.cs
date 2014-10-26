using UnityEngine;
using System.Collections;

public class EntityManager : MonoBehaviour {
  private static EntityManager instance = null;
  private HashTable entities = new HashTable();
  private static prefix_group = 'group:';
  private EntityManager() {}

  public static EntityManager instance() {
    if(instance == null)
      return new EntityManager();
    return instance;
  }

  public void registerEntity(String key, AbstractGameEntity entity) {
    if(isGroup(key))
      return;

    entitites.Add(key, entity);
  }

  public void addToGroup(String key, AbstractGameEntity entity) {
    String group_key = groupPrefix(key);
    if(!entitites.ContainsKey(group_key))
      entities.Add(group_key, new List<AbstractGameEntity>());
    entities.Get(group_key).Push(entity);
  }

  private void groupPrefix(String str) {
    return prefix_group + str;
  }

  public void isGroup(String key) {
    if(key.indexOf(prefix_group) == 0)
      return true;
    return false;
  }

}