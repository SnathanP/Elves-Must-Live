﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon_Aim : MonoBehaviour {

	public int DirectHitDamage;
	public GameObject currentTarget;
	private Vector3 LastKnownPosition;
	private Quaternion LookAtRotation;
	private Quaternion temporaire;
	public float TurretsSpeed;
	Transform children;
	float timerbeforeshot;
	public float reloadtime;
	Transform sortie;
	public GameObject Bullet;
	Health script;
	bool engage; //ca sert a bidouiller 
	public GameObject explosion;

	void Start () 
	{
		LastKnownPosition = Vector3.zero;
		timerbeforeshot = 0f;
		engage = false;
	}

	void Update () 
	//il est a noter que ce script a pour seul but de changer l'orientation de la tourelle selon l'axe y!
	{
		if (currentTarget != null)
		{
            /*
			if (currentTarget.transform.position != LastKnownPosition) 
			{
				LastKnownPosition = currentTarget.transform.position;
				LookAtRotation = Quaternion.LookRotation (LastKnownPosition - transform.position);
			}
			if (transform.rotation != LookAtRotation) 
			{
				//attention gros bidouillage en approche
				temporaire = transform.rotation;
				temporaire[1] = LookAtRotation.y;
				transform.rotation = Quaternion.RotateTowards(transform.rotation,temporaire,TurretsSpeed* Time.deltaTime);
				transform.GetChild (0).rotation = new Quaternion (LookAtRotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);  
			}*/

            //Seum que mes deux lignes remplacent toute ta merde ?
            var targetRotation = Quaternion.LookRotation(currentTarget.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, TurretsSpeed * Time.deltaTime);

            timerbeforeshot += Time.deltaTime;
			if (timerbeforeshot > reloadtime) 
			{
				Shoot (transform.GetChild (0).GetChild (1));
				timerbeforeshot = 0f;
			}
		}
	}
		

	void OnTriggerEnter(Collider coll)
	{
		if (engage == false && coll.tag == "Shootable") 
		{
			currentTarget = coll.gameObject;
			LastKnownPosition = currentTarget.transform.position;
			script = coll.GetComponent<Health> ();
			engage = true;
		}
	}
	void OnTriggerStay(Collider coll)
	{
		if (engage==false && coll.tag == "Shootable") 
		{
			currentTarget = coll.gameObject;
			LastKnownPosition = currentTarget.transform.position;
			script = coll.GetComponent<Health> ();
			engage = true;
		}
		if (engage && script.health <= 0) 
		{
			currentTarget = null;
			engage = false;
		}

	}
	void OnTriggerExit(Collider coll)
	{
		if (coll.gameObject == currentTarget) 
		{
			currentTarget = null;
			engage = false;
		}
	}

	void Shoot(Transform hole)
	{
		GameObject Shoot = Instantiate (Bullet,hole.position,hole.rotation) as GameObject;
		Shoot.GetComponent<Rigidbody> ().AddForce (hole.forward * 2500);
		Shoot.AddComponent<Collisionexplode> ();
		Shoot.GetComponent<Collisionexplode> ().SetExplosion (explosion);
	}
}