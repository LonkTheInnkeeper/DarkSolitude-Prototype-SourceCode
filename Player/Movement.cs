using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;

public class Movement : MonoBehaviour
{
    [SerializeField] float walkingSpeed;
    [SerializeField] float runningSpeed;
    [SerializeField] AudioEmitter steps;

    bool canIdle = false;

    NavMeshAgent navigation;
    Animator animator;
    RaycastHit hit;

    IInteractable interactable;
    IInteractable targetInteractable;

    GameManager gameMan;

    public PlayerState currentPlayerState;

    public enum PlayerState
    {
        Idle,
        Walking,
        Running
    }

    private void Start()
    {
        gameMan = GameManager.Instance;

        navigation = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        navigation.enabled = true;
        navigation.destination = transform.position;
    }

    private void Update()
    {
        SetAnimation();

        if (gameMan.GetGameState() != GameManager.GameState.Navigation &&
            gameMan.GetGameState() != GameManager.GameState.Closeup &&
            gameMan.GetGameState() != GameManager.GameState.StoryEvent &&
            gameMan.GetGameState() != GameManager.GameState.ItemHandling) return;

        CheckInteraction();
    }

    private bool CheckReachable(Vector3 target)
    {
        if (navigation == null)
            navigation = GetComponent<NavMeshAgent>();

        NavMeshPath path = new NavMeshPath();
        bool reachable = navigation.CalculatePath(target, path) && path.status == NavMeshPathStatus.PathComplete;
        if (!reachable) Debug.LogWarning("Target unreachable");

        StartCoroutine(IdleRoutine());

        return reachable;
    }

    public void ForceSetDestination(Vector3 target)
    {
        if (CheckReachable(target))
        {
            interactable = null;
            targetInteractable = null;
            navigation.destination = target;
        }
    }

    public void ForceStop()
    {
        SetDestination(transform.position);
        ResetInteractable();
    }

    public void SetPlayerState(PlayerState state)
    {
        currentPlayerState = state;
    }

    public void SetDestination(Vector3 target)
    {
        if (gameMan == null)
            gameMan = GameManager.Instance;

        var state = gameMan.GetGameState();

        bool canMove =
            state == GameManager.GameState.Navigation ||
            state == GameManager.GameState.StoryEvent;

        if (!canMove || MouseTools.IsMouseOverUI())
            return;

        if (CheckReachable(target))
        {
            interactable = null;
            targetInteractable = null;
            navigation.destination = target;
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
                print("Checking interaction");
                interactable.Interact();
                interactable = null;
            }
        }
    }

    public void SetInteractable(IInteractable interactable)
    {
        if (interactable.GetInteractionPoints().Count == 0)
        {
            print("Setting interactable");
            this.interactable = interactable;
            targetInteractable = interactable;

            interactable.Interact();
            return;
        }

        foreach (var point in interactable.GetInteractionPoints())
        {
            Vector3 target = new Vector3(point.position.x, transform.position.y, point.position.z);

            if (CheckReachable(target))
            {
                this.interactable = interactable;
                targetInteractable = interactable;
                navigation.destination = target;

                SetPlayerState(PlayerState.Running);

                break;
            }
        }
    }

    public void ResetInteractable()
    {
        interactable = null;
        targetInteractable = null;
    }

    void SetAnimation()
    {
        float currentSpeed = Mathf.Lerp(animator.GetFloat("Run"), navigation.velocity.magnitude, 5f * Time.deltaTime);
        animator.SetFloat("Run", currentSpeed);

        if (currentPlayerState == PlayerState.Walking)
        {
            navigation.speed = walkingSpeed;
        }

        else if (currentPlayerState == PlayerState.Running)
        {
            navigation.speed = runningSpeed;
        }
        
        if (canIdle && currentSpeed < 0.5f)
        {
            currentPlayerState = PlayerState.Idle;
        }

        if (targetInteractable != null && currentSpeed == 0)
        {
            Vector3 direction = targetInteractable.GetPosition() - transform.position; direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 8);
            }
        }
    }

    public void WarpTo(Vector3 position)
    {
        navigation.Warp(position);
    }

    IEnumerator IdleRoutine()
    {
        canIdle = false;
        yield return new WaitForSeconds(0.5f);
        canIdle = true;
    }
}
