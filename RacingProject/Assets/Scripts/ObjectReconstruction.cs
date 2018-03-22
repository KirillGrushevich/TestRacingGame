using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectReconstruction : MonoBehaviour 
{

    private List<Transform> childs = new List<Transform>();

	private void Awake()
	{
        foreach (Transform transf in transform)
        {
            childs.Add(transf);
        }
	}

    private void OnBecameInvisible()
	{
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Health>().Start();
        foreach (Transform transf in childs)
        {
            transf.gameObject.SetActive(true);
        }

        Destroy(this);
	}

}
