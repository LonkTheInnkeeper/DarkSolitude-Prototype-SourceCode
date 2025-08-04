using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    NavMeshAgent navigation;
    Animator animator;
    RaycastHit hit;

    IInteractable interactable;
    IInteractable lastInteractable;

    private void Start()
    {
        navigation = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        navigation.enabled = true;
        navigation.destination = transform.position;
    }

    private void Update()
    {
        SetAnimation();

        if (GameManager.Instance.gameState != GameManager.GameState.Navigation) return;

        CheckInteraction();
    }

    private bool CheckReachable(Vector3 target)
    {
        NavMeshPath path = new NavMeshPath();
        bool reachable = navigation.CalculatePath(target, path) && path.status == NavMeshPathStatus.PathComplete;
        if (!reachable) Debug.LogWarning("Target unreachable");
        return reachable;
    }

    public void SetDestination(Vector3 target)
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Navigation || MouseTools.IsMouseOverUI()) return;

        if (target != null)
        {
            if (CheckReachable(target))
            {
                interactable = null;
                lastInteractable = null;
                navigation.destination = target;
            }
        }
    }

    private void CheckInteraction()
    {
        if (interactable != null)
        {
            if (!navigation.pathPending &&
                navigation.remainingDistance <= navigation.stoppingDistance &&
                navigation.velocity.magnitude < 0.1f)
            {
                interactable.Interact();
                interactable = null;
            }
        }
    }

    public void SetInteractable(IInteractable interactable)
    {
        if (interactable.GetInteractionPoints().Count == 0)
        {
            this.interactable = interactable;
            lastInteractable = interactable;

            interactable.Interact();
            return;
        }

        foreach (var point in interactable.GetInteractionPoints())
        {
            Vector3 target = new Vector3(point.position.x, transform.position.y, point.position.z);

            if (CheckReachable(target))
            {
                this.interactable = interactable;
                lastInteractable = interactable;
                navigation.destination = target;

                break;
            }
        }
    }

    void SetAnimation()
    {
        float currentSpeed = Mathf.Clamp(navigation.velocity.magnitude, 0f, 1f);
        //animator.SetFloat("Velocity", currentSpeed);

        if (lastInteractable != null && currentSpeed == 0)
        {
            Vector3 direction = lastInteractable.GetPosition() - transform.position;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 8);
            }
        }
    }
}
