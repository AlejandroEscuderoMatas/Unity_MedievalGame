using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface I_Item
{
    public void OnTriggerWithPlayer(Player player);
    public void ExecuteAction();
    public void ExitAction();
    
}