using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace p28
{
    // Destructible object class
    public class DestructibleObject : MonoBehaviour
    {
        // Restored object prefab
        public GameObject restoredPrefab;
        // Hierarchy destructible object holder
        public GameObject objHolder;

        // Initial position of the object
        private Vector3 _initPos;
        // Parts of the object
        private List<Rigidbody> _parts = new List<Rigidbody>();



        /// <summary>
        /// Make sure every rigidbody is kinematic on init
        /// </summary>
        void Start()
        {
            _initPos = transform.position;
            foreach(Transform obj in transform)
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if(rb != null)
                {
                    rb.isKinematic = true;
                    _parts.Add(rb);
                }
            }
        }


        /// <summary>
        /// Destroys the destructible object 
        /// </summary>        
        public void DestroyObject()
        {
            foreach(Rigidbody rb in _parts)
            {
                rb.isKinematic = false;
            }
        }

        /// <summary>
        /// Restores the destructible object
        /// </summary>
        public void RestoreObject()
        {
            GameObject newObj = Instantiate(restoredPrefab, _initPos, Quaternion.identity, objHolder.transform);
            newObj.GetComponent<DestructibleObject>().objHolder = this.objHolder;
            newObj.GetComponent<DestructibleObject>().restoredPrefab = this.restoredPrefab;
            Destroy(gameObject);
        }



        /// <summary>
        /// TODO > remove when EEG enabled
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("boom");
                DestroyObject();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("tada");
                RestoreObject();
            }
        }
    }
}
