﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Viktor Dahlberg
public class FixZWrite : MonoBehaviour {

	private int count;

	private List<string> warnings = new List<string>();
	
	private void Start() {
		GameObject[] allGameObjects = FindObjectsOfType<GameObject>();
		for (int i = 0; i < allGameObjects.Length; i++) {
			if (allGameObjects[i].activeInHierarchy) EnableZWrite(allGameObjects[i].GetComponent<MeshRenderer>());
		}
		Debug.Log("Changed _ZWrite for " + count + " objects.");
	}

	private void EnableZWrite(MeshRenderer mr) {
		if (mr == null) return;
		switch (mr.material.shader.name) {
			case "Standard":
				if (mr.material.GetFloat("_Mode") == 3f) {
					Material m = new Material(mr.material);
					mr.material = m;
					m.SetInt("_ZWrite", 1);
					count++;
				}
				break;
			default:
				if (!warnings.Contains(mr.material.shader.name)) {
					Debug.LogWarning("Unsupported shader: " + mr.material.shader.name);
					warnings.Add(mr.material.shader.name);
				}
				break;
		}
	}

}