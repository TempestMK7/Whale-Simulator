using System;
using System.Collections.Generic;
using UnityEngine;

public class RecyclerView: MonoBehaviour {

    public RectTransform contentArea;

    private RecyclerViewAdapter adapter;
    private List<ViewHolder> viewHolders = new List<ViewHolder>();

    public void SetAdapter(RecyclerViewAdapter adapter) {
        this.adapter = adapter;
        NotifyDataSetChanged();
    }

    public void NotifyDataSetChanged() {
        int newCount = adapter == null ? 0 : adapter.GetItemCount();
        foreach (ViewHolder existing in viewHolders) {
            existing.CanBeRecycled = true;
            existing.gameObject.SetActive(false);
        }

        if (newCount == 0) return;
        var viewHolder = viewHolders.Count == 0 ? adapter.OnCreateViewHolder(contentArea) : viewHolders[0];
        var listItemTransform = viewHolder.transform as RectTransform;
        var listItemWidth = listItemTransform.rect.width;
        var listItemHeight = listItemTransform.rect.height;
        var listAreaWidth = contentArea.rect.width;

        int numItemsPerRow = (int)(listAreaWidth / (listItemWidth + 8));
        int numRows = newCount / numItemsPerRow;
        if (newCount % numItemsPerRow != 0) numRows++;
        float anchorMultiple = 1f / (numItemsPerRow + 1f);

        int heightPerRow = (int)listItemHeight + 8;
        int totalHeight = numRows * heightPerRow;
        contentArea.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, totalHeight);
        contentArea.anchoredPosition = new Vector3();
        viewHolder.CanBeRecycled = true;
        viewHolder.gameObject.SetActive(false);
        viewHolders.Add(viewHolder);

        for (int x = 0; x < newCount; x++) {
            var listItem = viewHolders.Find(delegate (ViewHolder holder) {
                return holder.CanBeRecycled;
            });
            if (listItem == null) {
                listItem = adapter.OnCreateViewHolder(contentArea);
                viewHolders.Add(listItem);
            }
            listItem.CanBeRecycled = false;
            listItem.gameObject.SetActive(true);
            listItem.transform.SetParent(contentArea);
            adapter.OnBindViewHolder(listItem, x);
            var transform = listItem.transform as RectTransform;
            float rowPosition = x % numItemsPerRow;
            transform.anchorMin = new Vector2((rowPosition + 1) * anchorMultiple, 1f);
            transform.anchorMax = transform.anchorMin;
            int rowNum = x / numItemsPerRow;
            float verticalPosition = (rowNum + 0.5f) * heightPerRow * -1f;
            transform.anchoredPosition = new Vector2(0f, verticalPosition);
        }
    }
}
