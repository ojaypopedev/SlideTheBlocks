using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour {

	public GameObject FloorPrefab;
	public float FloorThickness;
	public GameObject StartSpawn;
	private Vector3 StartSpawnPosition;

	public static int CurrentFloorTile;
	private int OldFloorTile = 0;
	public int CurrentMaterialNumber = 0;
	public int ColorMaterialTotal;
	public ColorManager colormanager;

	public enum FloorSpawningMode {Testing, Initial, Randomize, Comp_1, Comp_2};
	public FloorSpawningMode floorSpawningMode;

	public float ChanceToRandomize = 0.5f;
	public float MinSpawnDistance;
	public float CenterSpawnDistance;
	public float MaxSpawnDistance;
	public int SpawnDistanceAhead = 10;
	private float Pos_Distance = 0f;
	private float Pos_Intervel = 0f;
	private float Neg_Distance = 0f;
	private float Neg_Intervel = 0f;

	public int CompStartsAt_1_a = 50;
	public int CompStartsAt_1_b = 50;
	public int CompStartsAt_1_c = 50;
	public int CompStartsAt_1_d = 50;
	public int CompStartsAt_1_e = 50;
	public int CompStartsAt_2_a = 100;
	public int CompStartsAt_2_b = 100;
	public int CompStartsAt_2_c = 100;
	public int CompStartsAt_2_d = 100;
	public int CompStartsAt_2_e = 100;

	[Range(-10,10)]
	public List<int> StartingComposition;
	[Range(-10,10)]
	public List<int> Composition_1;
	[Range(-10,10)]
	public List<int> Composition_2;

	// Use this for initialization
	void Start () {
		//------
		StartSpawnPosition = StartSpawn.transform.position;

		Pos_Distance = Mathf.Abs (Mathf.Abs (CenterSpawnDistance) - Mathf.Abs (MaxSpawnDistance));
		Pos_Intervel = (Pos_Distance / 10);

		Neg_Distance = Mathf.Abs (Mathf.Abs (CenterSpawnDistance) - Mathf.Abs (MinSpawnDistance));
		Neg_Intervel = (Neg_Distance / 10);
		//------

		ColorMaterialTotal = colormanager.ColorMaterials.Count;

		if (floorSpawningMode == FloorSpawningMode.Testing) {
			for (int i = 0; i < SpawnDistanceAhead+1; i++) {
				GameObject Floor;
				Floor = Instantiate (FloorPrefab, StartSpawn.transform.position, transform.rotation) as GameObject;
				Floor.transform.parent = this.gameObject.transform;
				Floor.transform.position = new Vector3 (CenterSpawnDistance, StartSpawnPosition.y, StartSpawnPosition.z + ((FloorPrefab.transform.localScale.z) * i));
				Floor.name = "TestingFloorTiles_Initial";
				AssignMaterial(Floor);
			}
		} else {
			SpawnInitialFloors ();
		}
	}

	public void UpdateFloorTile () {
		CurrentFloorTile = (int)((float)PlayerMovement.howFarIGot / FloorThickness);
		if (OldFloorTile < CurrentFloorTile) {
			if (floorSpawningMode == FloorSpawningMode.Testing) {
				//Debug.LogError ("Testing Floor");
				SpawnFloor (CurrentFloorTile);
			} else {
				if ((CurrentFloorTile > CompStartsAt_1_a) && (CurrentFloorTile < (CompStartsAt_1_a + Composition_1.Count))) {
					//Debug.LogError ("1");
					Activate_Comp_1 ();
				} else if ((CurrentFloorTile > CompStartsAt_1_b) && (CurrentFloorTile < (CompStartsAt_1_b + Composition_2.Count))) {
					//Debug.LogError ("2");
					Activate_Comp_1 ();
				} else if ((CurrentFloorTile > CompStartsAt_1_c) && (CurrentFloorTile < (CompStartsAt_1_c + Composition_2.Count))) {
					//Debug.LogError ("3");
					Activate_Comp_1 ();
				} else if ((CurrentFloorTile > CompStartsAt_1_d) && (CurrentFloorTile < (CompStartsAt_1_d + Composition_2.Count))) {
					//Debug.LogError ("4");
					Activate_Comp_1 ();
				} else if ((CurrentFloorTile > CompStartsAt_1_e) && (CurrentFloorTile < (CompStartsAt_1_e + Composition_2.Count))) {
					//Debug.LogError ("5");
					Activate_Comp_1 ();
				} else if ((CurrentFloorTile > CompStartsAt_2_a) && (CurrentFloorTile < (CompStartsAt_2_a + Composition_2.Count))) {
					//Debug.LogError ("6");
					Activate_Comp_2 ();
				} else if ((CurrentFloorTile > CompStartsAt_2_b) && (CurrentFloorTile < (CompStartsAt_2_b + Composition_2.Count))) {
					//Debug.LogError ("7");
					Activate_Comp_2 ();
				} else if ((CurrentFloorTile > CompStartsAt_2_c) && (CurrentFloorTile < (CompStartsAt_2_c + Composition_2.Count))) {
					//Debug.LogError ("8");
					Activate_Comp_2 ();
				} else if ((CurrentFloorTile > CompStartsAt_2_d) && (CurrentFloorTile < (CompStartsAt_2_d + Composition_2.Count))) {
					//Debug.LogError ("9");
					Activate_Comp_2 ();
				} else if ((CurrentFloorTile > CompStartsAt_2_e) && (CurrentFloorTile < (CompStartsAt_2_e + Composition_2.Count))) {
					//Debug.LogError ("10");
					Activate_Comp_2 ();
				}else {
					//Debug.LogError ("Randomizing Floor");
					floorSpawningMode = FloorSpawningMode.Randomize;
					SpawnFloor (CurrentFloorTile);
				}
			}
			OldFloorTile = CurrentFloorTile;
			//Debug.LogError (CurrentFloorTile + "||" + floorSpawningMode);
		}
	}

	void SpawnInitialFloors () {
		Debug.Log ("Spawn initial Floors");
		for (int i = 0; i < StartingComposition.Count; i++) {
			SpawnFloor (i);
			if (i == (StartingComposition.Count-1)) {
				floorSpawningMode = FloorSpawningMode.Randomize;
			}
		}
	}

	void Activate_Comp_1 () {
		Debug.Log ("Composition_1_Start");
		floorSpawningMode = FloorSpawningMode.Comp_1;
		int i = 0;
		SpawnFloor (i);
		i++;
	}

	void Activate_Comp_2 () {
		Debug.Log ("Composition_2_Start");
		floorSpawningMode = FloorSpawningMode.Comp_2;
		int i = 0;
		SpawnFloor (i);
		i++;
	}
		
	void SpawnFloor (int FloorNumber) {
		
		if (floorSpawningMode == FloorSpawningMode.Testing) {
			Debug.Log ("Testing_NextFloorTile");
			GameObject Floor;
			Floor = Instantiate (FloorPrefab, StartSpawn.transform.position, transform.rotation) as GameObject;
			Floor.name = CurrentFloorTile + "_Testing";
			Floor.transform.parent = this.gameObject.transform;
			Floor.transform.position = new Vector3 (CenterSpawnDistance, StartSpawnPosition.y, StartSpawnPosition.z + (4 * (CurrentFloorTile + SpawnDistanceAhead)));
			AssignMaterial(Floor);
		}	

		if (floorSpawningMode == FloorSpawningMode.Initial) {
			Debug.Log ("Initializing_NextFloorTile");
			//if spawn composed initials floors
			if (StartingComposition [FloorNumber] == 0) {
				GameObject Floor;
				Floor = Instantiate (FloorPrefab, new Vector3 (CenterSpawnDistance, StartSpawnPosition.y, StartSpawnPosition.z + (4 * FloorNumber)), transform.rotation) as GameObject;
				Floor.name = CurrentFloorTile + "_Initializing";
				Floor.transform.parent = this.gameObject.transform;
				AssignMaterial(Floor);
			} else if (Mathf.Sign ((float)StartingComposition [FloorNumber]) == 1) {
				GameObject Floor;
				Floor = Instantiate (FloorPrefab, new Vector3 (CenterSpawnDistance + (StartingComposition [FloorNumber] * Pos_Intervel), StartSpawnPosition.y, StartSpawnPosition.z + ((FloorPrefab.transform.localScale.z) * FloorNumber)), transform.rotation) as GameObject;
				Floor.name = CurrentFloorTile + "_Initializing";
				Floor.transform.parent = this.gameObject.transform;
				AssignMaterial(Floor);
			} else if (Mathf.Sign ((float)StartingComposition [FloorNumber]) == -1) {
				GameObject Floor;
				Floor = Instantiate (FloorPrefab, new Vector3 (CenterSpawnDistance - (StartingComposition [FloorNumber] * Neg_Intervel), StartSpawnPosition.y, StartSpawnPosition.z + ((FloorPrefab.transform.localScale.z) * FloorNumber)), transform.rotation) as GameObject;
				Floor.name = CurrentFloorTile + "_Initializing";
				Floor.transform.parent = this.gameObject.transform;
				AssignMaterial(Floor);
			}
		}

		if (floorSpawningMode == FloorSpawningMode.Randomize) {
			//if spawn randomize floors
			Debug.Log ("Randomizing_NextFloorTile");
			GameObject Floor;
			float Randomizer = Random.Range (1f, 0f);
			if (Randomizer <= ChanceToRandomize) {
				Floor = Instantiate (FloorPrefab, new Vector3 (Random.Range (MaxSpawnDistance, MinSpawnDistance), StartSpawnPosition.y, StartSpawnPosition.z + ((FloorPrefab.transform.localScale.z) * (CurrentFloorTile + SpawnDistanceAhead))), transform.rotation) as GameObject;
				Floor.name = CurrentFloorTile + "_Randomizing";
				AssignMaterial(Floor);
			} else {
				Floor = Instantiate (FloorPrefab, new Vector3 (CenterSpawnDistance, StartSpawnPosition.y, StartSpawnPosition.z + ((FloorPrefab.transform.localScale.z) * (CurrentFloorTile + SpawnDistanceAhead))), transform.rotation) as GameObject;
				Floor.name = CurrentFloorTile + "_Randomizing";
				AssignMaterial(Floor);
			}
			Floor.transform.parent = this.gameObject.transform;
		}
			
		if (floorSpawningMode == FloorSpawningMode.Comp_1) {
			Debug.Log ("Comp_1_NextFloorTile");
			if (Composition_1 [FloorNumber] == 0) {
				//if the spawn point is 0
				GameObject Floor;
				Floor = Instantiate (FloorPrefab, new Vector3 (CenterSpawnDistance, StartSpawnPosition.y, StartSpawnPosition.z + ((FloorPrefab.transform.localScale.z) * (CurrentFloorTile + SpawnDistanceAhead))), transform.rotation) as GameObject;
				Floor.name = CurrentFloorTile + "_Comp-1";
				Floor.transform.parent = this.gameObject.transform;
				AssignMaterial(Floor);
			} else if (Mathf.Sign((float)Composition_1 [FloorNumber]) == 1) {
				//if the spawn point is +ve
				GameObject Floor;
				Floor = Instantiate (FloorPrefab, new Vector3 (CenterSpawnDistance + (Composition_1[FloorNumber]*Pos_Intervel), StartSpawnPosition.y, StartSpawnPosition.z + ((FloorPrefab.transform.localScale.z) * (CurrentFloorTile + SpawnDistanceAhead))), transform.rotation) as GameObject;
				Floor.name = CurrentFloorTile + "_Comp-1";
				Floor.transform.parent = this.gameObject.transform;
				AssignMaterial(Floor);
			} else if (Mathf.Sign((float)Composition_1 [FloorNumber]) == -1) {
				//if the spawn point is -ve
				GameObject Floor;
				Floor = Instantiate (FloorPrefab, new Vector3 (CenterSpawnDistance - (Composition_1[FloorNumber]*Neg_Intervel), StartSpawnPosition.y, StartSpawnPosition.z + ((FloorPrefab.transform.localScale.z) * (CurrentFloorTile + SpawnDistanceAhead))), transform.rotation) as GameObject;
				Floor.name = CurrentFloorTile + "_Comp-1";
				Floor.transform.parent = this.gameObject.transform;
				AssignMaterial(Floor);
			}
		}

		if (floorSpawningMode == FloorSpawningMode.Comp_2) {
			Debug.Log ("Comp_1_NextFloorTile");
			if (Composition_2 [FloorNumber] == 0) {
				//if the spawn point is 0
				GameObject Floor;
				Floor = Instantiate (FloorPrefab, new Vector3 (CenterSpawnDistance, StartSpawnPosition.y, StartSpawnPosition.z + ((FloorPrefab.transform.localScale.z) * (CurrentFloorTile + SpawnDistanceAhead))), transform.rotation) as GameObject;
				Floor.name = CurrentFloorTile + "_Comp-2";
				AssignMaterial(Floor);
				Floor.transform.parent = this.gameObject.transform;
			} else if (Mathf.Sign((float)Composition_2 [FloorNumber]) == 1) {
				//if the spawn point is +ve
				GameObject Floor;
				Floor = Instantiate (FloorPrefab, new Vector3 (CenterSpawnDistance + (Composition_1[FloorNumber]*Pos_Intervel), StartSpawnPosition.y, StartSpawnPosition.z + ((FloorPrefab.transform.localScale.z) * (CurrentFloorTile + SpawnDistanceAhead))), transform.rotation) as GameObject;
				Floor.name = CurrentFloorTile + "_Comp-2";
				AssignMaterial(Floor);
				Floor.transform.parent = this.gameObject.transform;
			} else if (Mathf.Sign((float)Composition_2 [FloorNumber]) == -1) {
				//if the spawn point is -ve
				GameObject Floor;
				Floor = Instantiate (FloorPrefab, new Vector3 (CenterSpawnDistance - (Composition_1[FloorNumber]*Neg_Intervel), StartSpawnPosition.y, StartSpawnPosition.z + ((FloorPrefab.transform.localScale.z) * (CurrentFloorTile + SpawnDistanceAhead))), transform.rotation) as GameObject;
				Floor.name = CurrentFloorTile + "_Comp-2";
				AssignMaterial(Floor);
				Floor.transform.parent = this.gameObject.transform;
			}
		}
			
		}
	void AssignMaterial (GameObject AssignToThis) {
		//Debug.LogError ("AssignMaterial to " + AssignToThis);
		//Debug.LogError ("AssignThismaterial " + colormanager.ColorMaterials [CurrentMaterialNumber]);
		if (CurrentMaterialNumber >= ColorMaterialTotal) {
			CurrentMaterialNumber = 0;
		}
		AssignToThis.transform.GetComponent<MeshRenderer> ().material = colormanager.ColorMaterials [CurrentMaterialNumber];
		CurrentMaterialNumber++;
	}
	}
