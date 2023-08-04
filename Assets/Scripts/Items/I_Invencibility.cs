public class I_Invencibility : Item
{
    protected override void Start()
    {
        base.Start();
        m_expiresImmediately = false;
        m_counterTime = 0;
        m_expiresWithTime = 10;
    }
    
    public override void ExecuteAction()
    {
        m_player.setInvencibility(true);
        base.ExecuteAction();
    }

    public override void ExitAction()
    {
        m_player.setInvencibility(false);
        base.ExitAction();
    }
}