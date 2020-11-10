using UnityEngine;
using UnityEngine.Events;

namespace InteractionSystem
{
    [RequireComponent(typeof(Collider))]
    public class Interactable : MonoBehaviour
    {
        public bool draggable = false;

        public UnityEvent hoverEnter;
        public UnityEvent hoverExit;
        public UnityEvent onPress;
        public UnityEvent onRelease;
        public UnityEvent onDrag;
        

        public virtual void OnHoverEnter()
        {
            hoverEnter.Invoke();
        }

        public virtual void OnHoverExit()
        {
            hoverExit.Invoke();
        }

        public virtual void OnPress()
        {
            onPress.Invoke();
        }

        public virtual void OnRelease()
        {
            onRelease.Invoke();
        }

        public virtual void OnDrag(Vector3 mousePosition, Vector3 previousMousePosition)
        {
            onDrag.Invoke();
        }
    }
}
