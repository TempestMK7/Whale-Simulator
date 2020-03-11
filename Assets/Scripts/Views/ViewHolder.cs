using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewHolder : MonoBehaviour {

    public const int Unbound = -1;

    public bool CanBeRecycled { get; set; }
    public int LastBoundPosition { get; set; }
}
