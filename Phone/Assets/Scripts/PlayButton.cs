﻿using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayButton : MonoBehaviour  
 {
	[SerializeField] Text playText;
	Button playButton;
	Image playImage;

	public Action onTouchPlay;

 	void Start () 
 	{
		playButton = GetComponent<Button> ();
		playImage = GetComponent<Image> ();
 	}
 	
 	public void TouchPlayButton () 
 	{
//		transform.parent.gameObject.SetActive (false);
		playText.enabled = false;
		playButton.enabled = false;
		playImage.enabled = false;
		if (onTouchPlay != null)
			onTouchPlay ();
 	}
 }
  
