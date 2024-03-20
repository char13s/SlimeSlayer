using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class StatusEffects
{
    public enum Statuses { neutral, stunned, burned, frozen, gravityless, bleeding,invisible }
    private Statuses status;
    public static UnityAction onStatusUpdate;
    public Statuses Status { get => status; set { status = value; if(onStatusUpdate!=null) onStatusUpdate(); } }
}
