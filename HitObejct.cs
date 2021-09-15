using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObejct : MonoBehaviour
{
    public enum HitGroup
    {
        Player, Damage, Other,
        EnumSize
    }

    public HitGroup m_hitGroup = HitGroup.Player;

    protected bool IsHitOK(GameObject hittedObject)
    {
        HitObejct hit = hittedObject.GetComponent<HitObejct>();

        if (null == hit) return false;

        if (m_hitGroup == hit.m_hitGroup)   return false;

        return true;
    }
}
