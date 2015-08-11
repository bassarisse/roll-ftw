using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public struct RayCastResult {
	public Vector2 point;
	public float distance;
}

public class CameraFollow : MonoBehaviour {
	
	int _layerMask;
	Vector3 _lastPosition;

	public GameObject target;
	
	// Use this for initialization
	void Start () {
		_layerMask = 1 << LayerMask.NameToLayer("Camera Path");
		_lastPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		UpdatePosition ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePosition ();
	}
	
	void UpdatePosition() {
		if (target == null)
			return;
		
		transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

		CorrectPosition ();

	}
	
	void CorrectPosition() {

		var distances = new Dictionary<Vector2, RayCastResult> ();

		AddDistanceResult (distances, Vector2.left, 2.5f);
		AddDistanceResult (distances, Vector2.right, 2.5f);
		AddDistanceResult (distances, Vector2.up, 2.25f);
		AddDistanceResult (distances, Vector2.down, 2.25f);

		var hits = distances.Where (o => o.Value.distance >= 0.0f).OrderByDescending (o => o.Value.distance).Take(2).ToList();

		if (hits.Count > 1) {
			
			var newOrigin = Vector2.zero;
			var pointRange = new Vector2(2.5f, 2.25f);

			foreach (var hit in hits) {
				var dir = hit.Key;
				var point = hit.Value.point;
				newOrigin += new Vector2(Mathf.Abs(dir.x) * point.x, Mathf.Abs(dir.y) * point.y);
				newOrigin -= new Vector2(dir.x * pointRange.x, dir.y * pointRange.y);
			}

			var newDir = new Vector2(transform.position.x, transform.position.y) - newOrigin;

			distances.Clear();

			AddDistanceResult (distances, newOrigin, newDir, 5.0f);

		}
		
		var result = distances.OrderByDescending (o => o.Value.distance).FirstOrDefault ().Value;
		
		if (result.distance >= 0) {
			
			_lastPosition = new Vector3(result.point.x, result.point.y, transform.position.z);
			transform.position = new Vector3(result.point.x, result.point.y, transform.position.z);
			
			
		} else {
			
			transform.position = _lastPosition;
			
		}
		
	}
	
	void AddDistanceResult(Dictionary<Vector2, RayCastResult> dict, Vector2 dir, float range) {
		dict.Add (dir, GetDistance (dir, range));
	}
	
	void AddDistanceResult(Dictionary<Vector2, RayCastResult> dict, Vector2 origin, Vector2 dir, float range) {
		dict.Add (dir, GetDistance (origin, dir, range));
	}
	
	RayCastResult GetDistance(Vector2 dir, float range) {
		return GetDistance (transform.position, dir, range);
	}
	
	RayCastResult GetDistance(Vector2 origin, Vector2 dir, float range) {
		
		var hit = Physics2D.Raycast (origin, dir, range, _layerMask);
		
		if (hit.collider != null) {
			return new RayCastResult {
				distance = Vector2.Distance(hit.point, new Vector2(origin.x, origin.y)),
				point = hit.point
			};
		}
		
		return new RayCastResult {
			distance = -1.0f,
			point = Vector2.zero
		};
	}


}
