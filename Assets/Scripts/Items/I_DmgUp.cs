using UnityEngine;

public class I_DmgUp : Item
{
    private float m_dmgUp = 10f;

    public override void ExecuteAction()
    {
        m_player.setDmgMultiplier(m_dmgUp);
        base.ExecuteAction();
    }
}