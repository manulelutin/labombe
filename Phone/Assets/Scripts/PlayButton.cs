﻿using UnityEngine;
using System;

public class PlayButton : MonoBehaviour  
 {
	public Action onTouchPlay;

 	void Start () 
 	{
 		
 	}
 	
 	public void TouchPlayButton () 
 	{
		transform.parent.gameObject.SetActive (false);
		if (onTouchPlay != null)
			onTouchPlay ();
 	}
 }
  
