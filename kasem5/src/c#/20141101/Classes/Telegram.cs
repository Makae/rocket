using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Telegram {
	public float DispatchTime { get; set; }
	public string From { get; set; }
	public string To { get; set; }
	public string Message { get; set; }
	public Hashtable Args { get; set; }

 	public Telegram(float dispatchTime, string from, string to, string message, Hashtable args) {
		this.DispatchTime = dispatchTime;
		this.From = from;
		this.To = to;
		this.Message = message;
		this.Args = args;
	}

}