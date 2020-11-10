using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InteractionSystem
{
    public class InteractionManager : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float dragSensitivity = 10f;
        [SerializeField] bool disableOverUI = false;

        private Interactable currentInteraction;
        private Interactable newInteraction;

        private bool pressedDown = false;
        
        /// <summary>
        /// Makes sure that release is only called on something that was clicked on and not dragged to.
        /// </summary>
        private bool clickedInteractable = false;

        private void Awake()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
                Debug.LogWarning("Set camera on Interaction Manager or it will be set to Camera.main by default.");
            }
        }

        void Update()
        {
            if (disableOverUI && IsPointerOverUIObject())
            {
                if (currentInteraction != null)
                {
                    ChangeInteraction(null, currentInteraction);
                }
                return;
            }
            
            
            if (!pressedDown)
            {
                HandleHoverOver();

                if (Input.GetMouseButtonDown(0))
                {
                    pressedDown = true;
                    if (currentInteraction != null)
                    {
                        clickedInteractable = true;
                        currentInteraction.OnPress();
                    }
                }
            }
            else
            {
                HoverOverAndPressed();
            }
        }

        /// <summary>
        /// Raycasts for the current interaction and changes the interaction if its not the same as the previous one.
        /// </summary>
        void HandleHoverOver()
        {
            newInteraction = RaycastForNewInteraction();

            if (newInteraction != currentInteraction)
            {
                ChangeInteraction(newInteraction, currentInteraction);
            }
        }
        
        public Interactable RaycastForNewInteraction()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return hit.collider.GetComponent<Interactable>();
            }
            return null;
        }

        void ChangeInteraction(Interactable newInteraction, Interactable currentInteraction)
        {
            if (currentInteraction != null)
            {
                currentInteraction.OnHoverExit();
            }

            clickedInteractable = false;
            this.currentInteraction = newInteraction;
            
            if(newInteraction != null)
            {
                newInteraction.OnHoverEnter();
            }
        }

        void HoverOverAndPressed()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Release();
            }
            else if (currentInteraction != null && currentInteraction.draggable)
            {
                Drag();
            }
            else
            {
                HandleHoverOver();
            }
        }

        void Release()
        {
            pressedDown = false;
            if (clickedInteractable && currentInteraction != null)
            {
                currentInteraction.OnRelease();
            }
        }

        private Vector3 mousePosition;
        private Vector3 previousMousePos;
        void Drag()
        {
            previousMousePos = mousePosition;
            mousePosition = Input.mousePosition;

            if (Vector3.Distance(previousMousePos, mousePosition) > dragSensitivity)
            {
                currentInteraction.OnDrag(mousePosition, previousMousePos);
            }
        }
        
        PointerEventData eventDataCurrentPosition;
        List<RaycastResult> results;
        /// <summary>
        /// Returns true if the pointer is over a UI Object.
        /// </summary>
        private bool IsPointerOverUIObject() {
            eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}