using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Is used to Display a Message box at the bottom of the screen. This class
 * decides which message has to be displayed based on priority and duration
 */
public class TextDisplayer : MonoBehaviour {
	private static string label = "";
	private static float duration = 0;
	private static float time_passed = 0;  
	private static int priority = 0;
	// How long shall a temporary message be displayed?
	public static float default_duration = 5f;
	public static int default_priority = 0;
	
	void Awake() {
	}
	
	/**
	 * Checks if the current message has to be displayed or the duration is already passed
	 */
	void Update() {
		if(!TextDisplayer.HasLabel())
			return;
		
		TextDisplayer.time_passed += Time.deltaTime;
		if(time_passed <= TextDisplayer.duration)
			return;
		TextDisplayer.resetMessage();
	}
	
	/**
     * Removes a message if it matches an existing label
	 */
	public static void removeMessage(string _label) {
		if(TextDisplayer.label == _label)
			TextDisplayer.resetMessage();
	}
	
	/**
	 * Resets the message
	 */
	private static void resetMessage() {
		TextDisplayer.label = "";
		TextDisplayer.duration = TextDisplayer.default_duration;
		TextDisplayer.priority = TextDisplayer.default_priority;
		TextDisplayer.time_passed = 0;
	}
	
	private static bool HasLabel() {
		return TextDisplayer.label != "";
	}
	
	public static void setMessage(string message) {
		TextDisplayer.setMessage (message, TextDisplayer.default_priority, TextDisplayer.default_duration);
	}
	
	public static void setMessage(string message, int _priority) {
		TextDisplayer.setMessage (message, _priority, TextDisplayer.default_duration);
	}

	/**
	 * Sets a message of the display if the bigger or equal the current message
	 */
	public static void setMessage(string message, int _priority, float _duration) {
		if(TextDisplayer.priority > _priority)
			return;
		
		TextDisplayer.label = message;
		TextDisplayer.duration = _duration;
		TextDisplayer.priority = _priority;
		TextDisplayer.time_passed = 0;
	}
	
	public void OnGUI () {
		if(!TextDisplayer.HasLabel())
			return;
		
		int label_width = 300;
		int label_height = 50;
		int label_padding = 10;
		Rect box_area = new Rect (0, 0, label_width, label_height);
		Rect label_area = new Rect (label_padding, label_padding, label_width - label_padding, label_height - label_padding);
		Rect draw_area = new Rect((Screen.width - label_width) / 2,  Screen.height-label_height, label_width, label_height);    
		
		GUI.BeginGroup (draw_area);
		GUI.Box (box_area, "");
		GUI.Label (label_area, TextDisplayer.label);
		GUI.EndGroup ();
		
	}
	
}