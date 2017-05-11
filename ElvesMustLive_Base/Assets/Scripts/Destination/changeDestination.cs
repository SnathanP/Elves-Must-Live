using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeDestination : MonoBehaviour {

	public Transform NextPosition;
    public Transform RandomPosition = null; // Laisser null si non utiliser
    Mov script;

	void Start () 
	{
	}

	void OnTriggerEnter(Collider coll)
    {
        
		if (coll.gameObject.tag == "Shootable")
		{
			script = coll.GetComponentInChildren<Mov> ();
            if (RandomPosition != null && Random.Range(0, 2) == 1)
            {
                script.ChangeDestination(RandomPosition);
            }
            else
            {
                script.ChangeDestination(NextPosition);
            }
		}
	}
}
