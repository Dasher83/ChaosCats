using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace ChaosCats
{
    public class BreakableObject : MonoBehaviour
    {
        [SerializeField] private GameObject brokenObjectPrefab;

        private void OnCollisionEnter(Collision obj) {
            if (obj.gameObject.CompareTag("Floor")) {
                Vector3 positionOnFloor = new Vector3(transform.position.x, 0, transform.position.z);
                Instantiate(brokenObjectPrefab, positionOnFloor, Quaternion.identity);
                Debug.Log("Object broken at " + positionOnFloor + "!");
                GameManager.Instance.MakeNoise(positionOnFloor);
                Destroy(gameObject);
            }
        }
    }
}
