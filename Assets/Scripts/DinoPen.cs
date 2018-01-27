using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoPen : MonoBehaviour {

	HashSet<string> dinoNames;
	public GameObject FencePole;
	public GameObject FenceRail;
	public float FENCE_SECTION_LENGTH_MAX;

	// Use this for initialization
	void Start () {
		dinoNames = new HashSet<string> ();
		SpawnFence ();
	}

	void SpawnFence() {
		List<Vector3> polePositions = GetPolePositions ();
		// Spawn fence poles and rails at pole positions
		for (int i = 0; i < polePositions.Count; i++) {
			Vector3 position = polePositions [i];
			Instantiate(FencePole, transform.position + position, Quaternion.identity);

			if (Random.value < 0.8f) {
				Vector3 positionNext = polePositions [(i + 1) % polePositions.Count];
				Vector3 railPosition = Vector3.Lerp (position, positionNext, 0.5f);
				float yAngle = Vector3.SignedAngle (Vector3.forward, positionNext - position, Vector3.up);
				GameObject rail = Instantiate (
					                  FenceRail, transform.position + railPosition, Quaternion.AngleAxis (yAngle, Vector3.up));
				float length = FenceRail.GetComponent<Renderer> ().bounds.size.z;
				Vector3 scale = FenceRail.transform.localScale;
				scale.z = Vector3.Distance (position, positionNext) * scale.z / length;
				rail.transform.localScale = scale;
			}
		}
	}

	List<Vector3> GetPolePositions() {
		Mesh planeMesh = GetComponent<MeshFilter>().mesh;
		Bounds bounds = planeMesh.bounds;
		Vector3 size = Vector3.Scale(transform.localScale, bounds.size);
		int numSectionsX = Mathf.Max((int)(size.x / FENCE_SECTION_LENGTH_MAX), 1);
		float sectionLengthX = size.x / numSectionsX;
		int numSectionsZ = Mathf.Max((int)(size.z / FENCE_SECTION_LENGTH_MAX), 1);
		float sectionLengthZ = size.z / numSectionsZ;
		List<Vector3> positions = new List<Vector3> ();
		float positionRandomOffsetFactor = 0.2f;
		// Top
		for (int i = 0; i < numSectionsX; i++) {
			float dx = -size.x * 0.5f + sectionLengthX * i;
			Vector2 offset = Random.insideUnitCircle * positionRandomOffsetFactor * sectionLengthX;
			positions.Add (new Vector3 (dx, 0, -size.z * 0.5f) + new Vector3(offset.x, 0, offset.y));
		}
		// Right
		for (int i = 0; i < numSectionsZ; i++) {
			float dz = -size.z * 0.5f + sectionLengthZ * i;
			Vector2 offset = Random.insideUnitCircle * positionRandomOffsetFactor * sectionLengthZ;
			positions.Add (new Vector3 (size.x * 0.5f, 0, dz) + new Vector3(offset.x, 0, offset.y));
		}
		// Bottom
		for (int i = 0; i < numSectionsX; i++) {
			float dx = size.x * 0.5f - sectionLengthX * i;
			Vector2 offset = Random.insideUnitCircle * positionRandomOffsetFactor * sectionLengthX;
			positions.Add (new Vector3 (dx, 0, size.z * 0.5f) + new Vector3(offset.x, 0, offset.y));
		}
		// Left
		for (int i = 0; i < numSectionsZ; i++) {
			float dz = size.z * 0.5f - sectionLengthZ * i;
			Vector2 offset = Random.insideUnitCircle * positionRandomOffsetFactor * sectionLengthZ;
			positions.Add (new Vector3 (-size.x * 0.5f, 0, dz) + new Vector3(offset.x, 0, offset.y));
		}
		return positions;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DinoEnter(DinoBehaviour d) {
		if (!dinoNames.Contains (d.name)) {
			dinoNames.Add (d.name);
			int score = dinoNames.Count;
			Debug.Log ("Score: " + score);
		}
	}

	public void DinoExit(DinoBehaviour d) {
		if (dinoNames.Contains (d.name)) {
			dinoNames.Remove (d.name);
			int score = dinoNames.Count;
			Debug.Log ("Score: " + score);
		}
	}
}
