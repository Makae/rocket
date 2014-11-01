using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityManager : MonoBehaviour {
	private static Hashtable Entities = new Hashtable();
	private static string GroupPrefix = "group:";
	
	private EntityManager() {}
	
	public static void RegisterEntity(string key, object entity) {
		if(EntityManager.IsGroup(key))
			return;
		
		EntityManager.Entities.Add(key, entity);
	}
	
	public static void AddToGroup(string key, object entity) {
		string group_key = EntityManager.PrefixGroup(key);
		
		if(!EntityManager.Entities.ContainsKey(group_key))
			EntityManager.Entities.Add(group_key, new List<object>());
		
		((List<object>) EntityManager.Entities[group_key]).Add(entity);
	}
	
	private static string PrefixGroup(string str) {
		return EntityManager.GroupPrefix + str;
	}
	
	public static bool IsGroup(string key) {
		if(key.IndexOf (EntityManager.GroupPrefix, System.StringComparison.Ordinal) == 0)
			return true;
		
		return false;
	}
	
	public static object GetEntity(string key) {
		if(!EntityManager.Entities.ContainsKey(key) || EntityManager.IsGroup (key))
			return null;
		
		return (object) EntityManager.Entities[key];
	}
	
	public static List<object> GetGroup(string key) {
		if(!EntityManager.Entities.ContainsKey(key) || !EntityManager.IsGroup(key))
			return new List<object>();
		
		return (List<object>)EntityManager.Entities[key];
	}
	
}