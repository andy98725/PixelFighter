using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitData data;

    private List<Action> actions;

    // public void Awake()
    // {
    //     actions = new List<Action>();
    //     foreach (ActionData a in data.actions)
    //         actions.Add(new Action(a));
    // }

    public void AdvanceTurn()
    {
        foreach (Action a in actions)
            if (a.currentCooldown > 0) a.currentCooldown--;

    }


}

public class Action
{


    public ActionData data;
    public int currentCooldown = 0;

    public Action(ActionData data)
    {
        this.data = data;
    }
}
