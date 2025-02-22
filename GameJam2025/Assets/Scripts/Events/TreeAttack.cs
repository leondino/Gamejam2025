using System.Collections.Generic;
using UnityEngine;

public class TreeAttack : FailEvent
{
    [SerializeField] private List<Rigidbody> flyingTrees = new List<Rigidbody>();

    [SerializeField] private float rizeSpeed = 1f;
    [SerializeField] private float rizeDuration = 3f;
    [SerializeField] private float rotationSpeed = 1f;

    private float timer = 0f;
    private bool isTimerRunning = false;
    private bool attackMode = false;
    private bool treeIsAttacking = false;

    private void FixedUpdate()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime;
            if (timer >= rizeDuration)
            {
                isTimerRunning = false;
                timer = 0f;
                foreach (Rigidbody tree in flyingTrees)
                {
                    if (attackMode)
                        TreeAttacksPlayer(tree);
                    else
                        DropTree(tree);
                }
            }
        }
        if (treeIsAttacking)
        {
            foreach (Rigidbody tree in flyingTrees)
            {
                TreeAttacksPlayer(tree);
            }
        }
    }

    public override void SuccesAction()
    {
        base.SuccesAction();
        attackMode = false;
        RaiseTrees();
    }

    public override void FailAction()
    {
        base.FailAction();
        attackMode = true;
        RaiseTrees();
    }

    private void RaiseTrees()
    {
        isTimerRunning = true;
        foreach (Rigidbody tree in flyingTrees)
        {
            tree.isKinematic = false;
            tree.linearVelocity = Vector3.up * rizeSpeed;
        }
    }

    private void DropTree(Rigidbody tree)
    {
        tree.linearVelocity = Vector3.zero;
        tree.isKinematic = false;
        tree.useGravity = true;
    }

    private void TreeAttacksPlayer(Rigidbody tree)
    {
        treeIsAttacking = true;
        tree.linearVelocity = Vector3.zero;
        Vector3 direction = tree.position - PlayerControler.Instance.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.down);
        Vector3 eulertargetRotation = targetRotation.eulerAngles;
        eulertargetRotation.x *= -1;
        targetRotation = Quaternion.Euler(eulertargetRotation);
        tree.rotation = targetRotation; //Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
