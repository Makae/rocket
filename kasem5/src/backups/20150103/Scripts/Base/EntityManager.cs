using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * The Entitymanager handles the registration and addressing of different GameEntitites.
 * Game Entitites can be registered as a group, such as the "group:power" for light switches and light bulbs / computer
 * 
 */
public class EntityManager : MonoBehaviour {
	private static Hashtable Entities = new Hashtable();
	private static string GroupPrefix = "group:";
	
	private EntityManager() {}
	
    /**
     * Registers a game entity doesnt allow grop registrations
     */
	public static void RegisterEntity(string key, object entity) {
		if(EntityManager.IsGroup(key))
			return;
	
		EntityManager.Entities.Add(key, entity);
	}
	
    /**
     * Add an entitty to a new or an existing group. The Prefix "group:" doesn't have to be added
     */
	public static void AddToGroup(string key, object entity) {
		string group_key = EntityManager.PrefixGroup(key);
		
		if(!EntityManager.Entities.ContainsKey(group_key))
			EntityManager.Entities.Add(group_key, new List<object>());
		
		((List<object>) EntityManager.Entities[group_key]).Add(entity);
	}

	/** 
	 * Sugar for prefixing a group 
     */
	private static string PrefixGroup(string str) {
		return EntityManager.GroupPrefix + str;
	}
	
	/**
     * Checks if a provided key is a group or not
     */
	public static bool IsGroup(string key) {
		if(key.IndexOf (EntityManager.GroupPrefix, System.StringComparison.Ordinal) == 0)
			return true;
		
		return false;
	}
	
    /**
     * Gets a GameObject of a registered GameEntity by its key
     * if none is found null is returned
     */
	public static GameObject GetEntityGameObject(string key) {
		MonoBehaviour go = (MonoBehaviour) EntityManager.GetEntity(key);
		if(go == null)
			return null;
		return go.gameObject;
	}

	/**
	 * Gets a GameEntity by its key
	 */
	public static object GetEntity(string key) {
		if(!EntityManager.Entities.ContainsKey(key) || EntityManager.IsGroup (key))
			return null;
		return (object) EntityManager.Entities[key];
	}

	/**
	 * Gets a Group of GameEntity by their key
     * @return List<object> entitites
	 */
	public static List<object> GetGroup(string key) {
		if(!EntityManager.Entities.ContainsKey(key) || !EntityManager.IsGroup(key))
			return new List<object>();
		
		return (List<object>)EntityManager.Entities[key];
	}
	
}