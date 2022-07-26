using static Racer.Utilities.SingletonPattern;

public class DontDestroy : SingletonPersistent<DontDestroy>
{
    protected override void Awake()
    {
        gameObject.transform.parent = null;

        base.Awake();
    }
}

