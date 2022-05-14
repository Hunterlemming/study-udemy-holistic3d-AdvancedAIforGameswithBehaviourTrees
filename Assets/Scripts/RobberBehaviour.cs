using BehaviourControl;
using UnityEngine;
using UnityEngine.AI;

public class RobberBehaviour : MonoBehaviour
{

    #region Editor

    [SerializeField] private Transform diamond;
    [SerializeField] private Transform van;

    #endregion

    #region Variables
    
    private const float BakedNavMeshDistance = 2f;

    private readonly BehaviourTree _tree = new BehaviourTree();

    private NavMeshAgent _agent;

    private ActionState _state = ActionState.Idle;
    private Status _treeStatus = Status.Running;

    #endregion

    private Status GoToDiamond()
    {
        return GoToLocation(diamond.position);
    }

    private Status GoToVan()
    {
        return GoToLocation(van.position);
    }

    private Status GoToLocation(Vector3 destination)
    {
        switch (_state)
        {
            case ActionState.Idle:
                _agent.SetDestination(destination);
                _state = ActionState.Working;
                break;
            case ActionState.Working:
                if (Vector3.Distance(_agent.pathEndPosition, destination) >= BakedNavMeshDistance)
                {
                    // Target changed before completion
                    _state = ActionState.Idle;
                    return Status.Failure;
                }
                else if (Vector3.Distance(destination, transform.position) < BakedNavMeshDistance)
                {
                    // Reached target successfully
                    _state = ActionState.Idle;
                    return Status.Success;
                }
                break;
        }
        return Status.Running;
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        var stealNode = new Sequence("Steal Something");
        var goToDiamondNode = new Leaf("Go To Diamond", GoToDiamond);
        var goToVan = new Leaf("Go To Van", GoToVan);
        
        stealNode.AddChildren(new [] {goToDiamondNode, goToVan});
        _tree.AddChild(stealNode);

        var eat = new Node("Eat Something");
        var pizza = new Node("Go to Pizza Shop");
        var buy = new Node("Buy Pizza");
        
        eat.AddChildren(new [] {pizza, buy});
        _tree.AddChild(eat);
        
        _tree.PrintTree();
    }

    private void Update()
    {
        if (_treeStatus == Status.Running)
        {
            _treeStatus = _tree.Process();
        }
    }
}