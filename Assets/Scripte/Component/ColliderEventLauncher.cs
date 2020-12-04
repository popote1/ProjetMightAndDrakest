using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scripte.Component
{
    [Serializable]
    public class ColliderEvent : UnityEvent<Collider> {}
    public class ColliderEventLauncher : MonoBehaviour
    {

        public ColliderEvent OnEnterColliderEvent;
        public ColliderEvent OnStayColliderEvent;
        public ColliderEvent OnExitColliderEvent;
    
        private void OnTriggerEnter(Collider other)
        {
            OnEnterColliderEvent.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            
        }

        private void OnTriggerExit(Collider other)
        {
           OnExitColliderEvent.Invoke(other);
        }
    }
}