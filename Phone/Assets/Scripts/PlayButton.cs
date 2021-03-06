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
		MessageManager messageManager = FindObjectOfType<MessageManager> ();
		messageManager.onRestart += Show;
 	}
 	
 	public void TouchPlayButton () 
 	{
		playText.enabled = false;
		playButton.enabled = false;
		playImage.enabled = false;
		if (onTouchPlay != null)
			onTouchPlay ();
 	}

	void Show()
	{
		playText.enabled = true;
		playButton.enabled = true;
		playImage.enabled = true;
	}
 }
  
