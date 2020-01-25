using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {

	public List<Material> ColorMaterials;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < ColorMaterials.Count; i++) {
			string myNewString = "Gradient" + (i+2);
			ColorMaterials[i] = Resources.Load(myNewString) as Material;
		}
	}
}
