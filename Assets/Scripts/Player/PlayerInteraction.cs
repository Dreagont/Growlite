using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private GameObject arrowPrefab; // Reference to the arrow prefab

    private GameObject currentArrow; // The currently instantiated arrow
    private IInteractable currentInteractable; // The current interactable object in range

    private void Update()
    {
        CheckForInteractableInRange();

        if (Input.GetMouseButtonDown(1)) // Right mouse button
        {
            InteractWithObjectInRange();
        }
    }

    private void CheckForInteractableInRange()
    {
        Collider2D[] interactablesInRange = Physics2D.OverlapCircleAll(transform.position, interactionRange, interactableLayer);
        IInteractable nearestInteractable = null;
        float nearestDistance = interactionRange;

        foreach (Collider2D collider in interactablesInRange)
        {
            IInteractable interactableObject = collider.GetComponent<IInteractable>();
            if (interactableObject != null)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestInteractable = interactableObject;
                    nearestDistance = distance;
                }
            }
        }

        if (nearestInteractable != currentInteractable)
        {
            if (currentArrow != null)
            {
                Destroy(currentArrow);
            }

            currentInteractable = nearestInteractable;

            if (currentInteractable != null)
            {
                // Calculate the top position of the interactable object
                Collider2D collider = (nearestInteractable as MonoBehaviour).GetComponent<Collider2D>();
                Bounds bounds = collider.bounds;
                Vector3 arrowPosition = new Vector3(bounds.center.x, bounds.max.y + 0.5f, bounds.center.z); // Adjust the offset as needed
                currentArrow = Instantiate(arrowPrefab, arrowPosition, Quaternion.identity);
            }
        }
    }

    private void InteractWithObjectInRange()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
            Destroy(currentArrow); // Remove the arrow after interaction
            currentInteractable = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
