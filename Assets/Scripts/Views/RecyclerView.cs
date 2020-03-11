using System;
using System.Collections.Generic;
using UnityEngine;

public class RecyclerView: MonoBehaviour {

    public RectTransform viewport;
    public RectTransform contentArea;

    private RecyclerViewAdapter adapter;
    private ViewHolder[] currentViewHolders;
    private List<ViewHolder> recyclableViewHolders = new List<ViewHolder>();

    private int currentCount;
    private int numItemsPerRow;
    private float anchorMultiple;
    private int heightPerRow;

    public void SetAdapter(RecyclerViewAdapter adapter) {
        this.adapter = adapter;
        NotifyDataSetChanged();
    }

    public void NotifyDataSetChanged() {
        currentCount = adapter == null ? 0 : adapter.GetItemCount();

        // This clears all existing view holders from the list and adds them to the recyclable pool.
        if (currentViewHolders != null) {
            foreach (ViewHolder existing in currentViewHolders) {
                if (existing == null) continue;
                existing.CanBeRecycled = true;
                existing.gameObject.SetActive(false);
                recyclableViewHolders.Add(existing);
            }
        }
        currentViewHolders = new ViewHolder[currentCount];
        if (currentCount == 0) return;

        // This loads a sample view holder from the pool if available and uses it to size the scrollable content area.
        ViewHolder sampleHolder;
        if (recyclableViewHolders.Count > 0) {
            sampleHolder = recyclableViewHolders[0];
        } else {
            sampleHolder = adapter.OnCreateViewHolder(contentArea);
            sampleHolder.CanBeRecycled = true;
            recyclableViewHolders.Add(sampleHolder);
        }
        var listItemTransform = sampleHolder.transform as RectTransform;
        var listItemWidth = listItemTransform.rect.width;
        var listItemHeight = listItemTransform.rect.height;
        var listAreaWidth = contentArea.rect.width;

        numItemsPerRow = (int)(listAreaWidth / (listItemWidth + 8));
        int numRows = currentCount / numItemsPerRow;
        if (currentCount % numItemsPerRow != 0) numRows++;
        anchorMultiple = 1f / (numItemsPerRow + 1f);

        heightPerRow = (int)listItemHeight + 8;
        contentArea.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, numRows * heightPerRow);
        contentArea.anchoredPosition = new Vector3();
        BindViewHoldersInViewport();
    }

    public void BindViewHoldersInViewport() {
        var viewportHeight = viewport.rect.height;
        var highestViewable = contentArea.anchoredPosition.y * -1f;
        var lowestViewable = highestViewable - viewportHeight;

        highestViewable += heightPerRow * 2f;
        lowestViewable -= heightPerRow * 2f;

        for (int x = 0; x < currentCount; x++) {
            int rowNum = x / numItemsPerRow;
            float verticalPosition = (rowNum + 0.5f) * heightPerRow * -1f;
            bool validPosition = verticalPosition <= highestViewable && verticalPosition >= lowestViewable;

            if (validPosition) {
                var current = currentViewHolders[x];
                if (current != null && current.LastBoundPosition == x) continue;
                if (current == null) {
                    if (recyclableViewHolders.Count > 0) {
                        current = recyclableViewHolders[0];
                        recyclableViewHolders.RemoveAt(0);
                    } else {
                        current = adapter.OnCreateViewHolder(contentArea);
                    }
                    currentViewHolders[x] = current;
                }
                current.CanBeRecycled = false;
                current.gameObject.SetActive(true);
                current.transform.SetParent(contentArea);
                adapter.OnBindViewHolder(current, x);

                var transform = current.transform as RectTransform;
                float rowPosition = x % numItemsPerRow;
                transform.anchorMin = new Vector2((rowPosition + 1) * anchorMultiple, 1f);
                transform.anchorMax = transform.anchorMin;
                transform.anchoredPosition = new Vector2(0f, verticalPosition);
            } else {
                var current = currentViewHolders[x];
                if (current != null) {
                    currentViewHolders[x] = null;
                    current.CanBeRecycled = true;
                    current.LastBoundPosition = ViewHolder.Unbound;
                    current.gameObject.SetActive(false);
                    recyclableViewHolders.Add(current);
                }
            }
        }
    }
}
