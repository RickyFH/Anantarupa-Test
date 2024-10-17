using UnityEngine;

public interface ICollision
{
    public void OnGettingCloseDo();
    public void OnTriggerDo(Vector3 pos);
    public void OnExitDo();
}
