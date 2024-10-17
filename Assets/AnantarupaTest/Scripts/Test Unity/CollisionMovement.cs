using System.Collections;
using UnityEngine;

public class CollisionMovement : CollisionDetection
{
    private readonly int MoveAmountSpace = 2;
    private readonly float speed = 10f;
    
    public override void OnTriggerDo(Vector3 pos)
    {
        if(!_AllowToInteract) return;
        base.OnTriggerDo();
        OnInputRead(pos.x > transform.position.x);
    }

    protected override void OnInputRead(bool state)
    {
        base.OnInputRead(state);
        var Xpos = state ? transform.position.x - MoveAmountSpace : transform.position.x + MoveAmountSpace;
        var targetPosition = new Vector3(Xpos, transform.position.y, transform.position.z);
        StartCoroutine(OnMovementUpdate(targetPosition));
    }

    IEnumerator OnMovementUpdate(Vector3 targetPos)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1)
        {
            float t = elapsedTime / 1;
            transform.position = Vector3.Lerp(transform.position, targetPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
