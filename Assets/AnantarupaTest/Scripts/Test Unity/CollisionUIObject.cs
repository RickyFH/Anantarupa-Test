using UnityEngine;

public class CollisionUIObject : CollisionDetection
{
    [SerializeField] private RectTransform _uIPopUp;
    private const int CloseDelayDuration = 1;
    
    public override void Awake()
    {
        if(!_AllowToInteract) return;
        base.Awake();
        OnInputRead(false);
    }

    protected override void OnTriggerDo()
    {
        base.OnTriggerDo();
        OnInputRead(true);
        Invoke(nameof(SetUpDisableUI),CloseDelayDuration);
    }
    private void SetUpDisableUI() => OnInputRead(false);

    protected override void OnInputRead(bool state) => _uIPopUp.gameObject.SetActive(state);

}
