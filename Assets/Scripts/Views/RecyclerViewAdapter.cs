using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RecyclerViewAdapter {

    public abstract ViewHolder OnCreateViewHolder(RectTransform contentHolder);

    public abstract void OnBindViewHolder(ViewHolder viewHolder, int position);

    public abstract int GetItemCount();
}
