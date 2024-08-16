using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitData
{
    public int maxHP;

    public ActionData[] actions;

}

public abstract class ActionData
{

    public int cooldown = 0;
}
