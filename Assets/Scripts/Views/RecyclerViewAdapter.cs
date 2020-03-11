using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RecyclerViewAdapter {

    public abstract GameObject OnCreateViewHolder(RectTransform contentHolder);

    public abstract void OnBindViewHolder(GameObject viewHolder, int position);

    public abstract int GetItemCount();
}
