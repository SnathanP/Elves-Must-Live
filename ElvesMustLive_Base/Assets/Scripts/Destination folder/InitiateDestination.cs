﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateDestination : MonoBehaviour {

	public GameObject FirstDestination;
	EnnemyMov1 script;
	void Start () 
	{
        script = GetComponentInChildren<EnnemyMov1>();
        script.ChangeDestination(FirstDestination);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
