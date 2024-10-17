using UnityEngine;

public class CollisionSize : CollisionDetection
{
    private readonly Vector3 _ObjectSize = new (4,4,1);
    private readonly Vector3 _currentSize = new (2,2,1);
    private readonly int _currentYPos = -1;
    private readonly int _AfterYPos = 0;
    private bool _currentState = true;

    protected override void OnTriggerDo()
    {
        if(!_AllowToInteract) return;
        base.OnTriggerDo();
        OnInputRead(_currentState);
    }

    protected override void OnInputRead(bool state)
    {
        _currentState = !_currentState;
        transform.localScale = state ? _ObjectSize : _currentSize;
        transform.position = new Vector3(transform.position.x, state ? _AfterYPos : _currentYPos, transform.position.z);
    }
}
