using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageManager : MonoBehaviour {
	private static string label = "";
	private static float duration = 0;
	private static float time_passed = 0;	
	private static int priority = 0;
	// How long shall a temporary message be displayed?
	public static float default_duration = 5f;
	public static int default_priority = 0;

	void Awake() {
	}

	void Update() {
		if(!MessageManager.HasLabel())
			return;
		
		MessageManager.time_passed += Time.deltaTime;
		if(time_passed <= MessageManager.duration)
			return;
		MessageManager.resetMessage();
	}

	public static void removeMessage(string _label) {
		if(MessageManager.label == _label)
			MessageManager.resetMessage();
	}

	private static void resetMessage() {
		MessageManager.label = "";
		MessageManager.duration = MessageManager.default_duration;
		MessageManager.priority = MessageManager.default_priority;
		MessageManager.time_passed = 0;
	}

	private static bool HasLabel() {
		return MessageManager.label != "";
	}

	public static void setMessage(string message) {
		MessageManager.setMessage (message, MessageManager.default_priority, MessageManager.default_duration);
	}

	public static void setMessage(string message, int _priority) {
		MessageManager.setMessage (message, _priority, MessageManager.default_duration);
	}

	public static void setMessage(string message, int _priority, float _duration) {
		if(MessageManager.priority > _priority)
			return;
		
		MessageManager.label = message;
		MessageManager.duration = _duration;
		MessageManager.priority = _priority;
		MessageManager.time_passed = 0;
	
	}

	public void OnGUI () {
		Debug.Log (MessageManager.HasLabel());
		if(!MessageManager.HasLabel())
			return;

		int label_width = 300;
		int label_height = 50;
		int label_padding = 10;
		Rect box_area = new Rect (0, 0, label_width, label_height);
		Rect label_area = new Rect (label_padding, label_padding, label_width - label_padding, label_height - label_padding);
		Rect draw_area = new Rect((Screen.width - label_width) / 2,  Screen.height-label_height, label_width, label_height);		

		GUI.BeginGroup (draw_area);
			GUI.Box (box_area, "");
		GUI.Label (label_area, MessageManager.label);
		GUI.EndGroup ();
		
	}

}