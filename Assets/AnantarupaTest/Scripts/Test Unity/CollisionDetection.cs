using UnityEngine;

public abstract class CollisionDetection : MonoBehaviour,ICollision
{
    [SerializeField] private RectTransform _UIButtonInformation;
    [SerializeField] private RectTransform _UIButtonAllowPressInformation;
    private bool IsPressAble;
    internal bool _AllowToInteract = true;
    private int CollisionTimer = 1;

    public virtual void Awake()
    {
        OnButtonUIPopUp(false);
        OnButtonXPopUp(false);
    }

    public void OnGettingCloseDo()
    {
        OnButtonXPopUp(!_AllowToInteract);
        OnButtonUIPopUp(true);
    }

    protected virtual void OnTriggerDo()
    {
        OnButtonUIPopUp(true);
        IsPressAble = true;
        _AllowToInteract = false;
        Invoke(nameof(OnTimerDone),CollisionTimer);
        OnButtonXPopUp(true);
    }

    public virtual void OnTriggerDo(Vector3 pos) => OnTriggerDo();
    public virtual void OnExitDo()
    {
        IsPressAble = false;
        OnButtonUIPopUp(false);
    }

    protected virtual void OnInputRead(bool state) { }
    
    private void OnTimerDone()
    {
        _AllowToInteract = true;
        OnButtonXPopUp(false);
    }

    private void OnButtonUIPopUp(bool state) => _UIButtonInformation.gameObject.SetActive(state);

    private void OnButtonXPopUp(bool state) => _UIButtonAllowPressInformation.gameObject.SetActive(state);
}
