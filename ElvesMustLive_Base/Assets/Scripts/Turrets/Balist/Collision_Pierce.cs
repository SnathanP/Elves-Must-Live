﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Pierce : MonoBehaviour {

	int damage; //dommage du piercing
	GameObject Pierce; //gameobject des particules de piercing
	bool FirstHit;//bool indiquant s'il y a eu premier contact avec une cible
	Health script; //script de la vie de la cible touchee
	float timerbeforedestruct;

	void Start () 
	{
		FirstHit = false;
		timerbeforedestruct = 0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timerbeforedestruct += Time.deltaTime;
		if (timerbeforedestruct > 2f) 
		{
			Destroy (gameObject);
		}
	}

	public void SetPiercing(GameObject piercing)
	{
		this.Pierce = piercing; //accesseur des particules pour le script de shoot de la baliste
	}

	public void SetDamage(int dmg)
	{
		this.damage = dmg; //accesseur des dommages pour le script de shoot de la baliste
	}

	void OnTriggerEnter(Collider coll)
	{
		
		if (coll.tag == "Shootable") 
		{
			Physics.IgnoreCollision (gameObject.GetComponent<Collider> (), coll);
			if (FirstHit == false)
			{
				FirstHit = true;
				//gameObject.GetComponent<Rigidbody> ().i = true; on permet au carreau de traverser les cibles
				Instantiate (Pierce, coll.transform.position,coll.transform.rotation,coll.transform); //on declenche les particules
			}
			Piercetouch (coll); //on declenche les degats
		}
	}

	void Piercetouch (Collider coll)
	{
		script = coll.gameObject.GetComponent<Health>();
		script.TakeDamage(damage);
	}
}
