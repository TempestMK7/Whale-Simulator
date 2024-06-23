using System;
using System.Collections.Generic;
using UnityEngine;

public class RecyclerView: MonoBehaviour {

    public RectTransform viewport;
    public RectTransform contentArea;

    private RecyclerViewAdapter adapter;
    private GameObject[] currentViewHolders;
    private List<GameObject> recyclableViewHolders = new List<GameObject>();

    private int currentCount;
    private int numItemsPerRow;
    private float anchorMultiple;
    private int heightPerRow;

    private int lastNumberOfRows;

    public void SetAdapter(RecyclerViewAdapter adapter) {
        this.adapter = adapter;
        NotifyDataSetChanged();
    }

    public void NotifyDataSetChanged() {
        currentCount = adapter == null ? 0 : adapter.GetItemCount();

        // This clears all existing view holders from the list and adds them to the recyclable pool.
        if (currentViewHolders != null) {
            foreach (GameObject existing in currentViewHolders) {
                if (existing == null) continue;
                existing.gameObject.SetActive(false);
                if (!recyclableViewHolders.Contains(existing)) recyclableViewHolders.Add(existing);
            }
        }
        currentViewHolders = new GameObject[currentCount];
        if (currentCount == 0) return;

        // This loads a sample view holder from the pool if available and uses it to size the scrollable content area.
        GameObject sampleHolder;
        if (recyclableViewHolders.Count > 0) {
            sampleHolder = recyclableViewHolders[0];
        } else {
            sampleHolder = adapter.OnCreateViewHolder(contentArea);
            recyclableViewHolders.Add(sampleHolder);
        }
        var listItemTransform = sampleHolder.transform as RectTransform;
        var listItemWidth = listItemTransform.rect.width;
        var listItemHeight = listItemTransform.rect.height;
        var listAreaWidth = contentArea.rect.width;

        numItemsPerRow = (int)(listAreaWidth / (listItemWidth + 8));
        if (numItemsPerRow == 0) numItemsPerRow = 1;
        int numRows = currentCount / numItemsPerRow;
        if (currentCount % numItemsPerRow != 0) numRows++;
        anchorMultiple = 1f / (numItemsPerRow + 1f);

        heightPerRow = (int)listItemHeight + 8;
        contentArea.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, numRows * heightPerRow);
        if (numRows != lastNumberOfRows) {
            contentArea.anchoredPosition = new Vector3();
            lastNumberOfRows = numRows;
        }
        BindViewHoldersInViewport();
    }

    public void BindViewHoldersInViewport() {
        var viewportHeight = viewport.rect.height;
        var highestViewable = contentArea.anchoredPosition.y * -1f;
        var lowestViewable = highestViewable - viewportHeight;

        highestViewable += heightPerRow * 1f;
        lowestViewable -= heightPerRow * 1f;

        for (int x = 0; x < currentCount; x++) {
            int rowNum = x / numItemsPerRow;
            float verticalPosition = (rowNum + 0.5f) * heightPerRow * -1f;
            bool validPosition = verticalPosition <= highestViewable && verticalPosition >= lowestViewable;

            if (validPosition) {
                var current = currentViewHolders[x];
                if (current == null) {
                    if (recyclableViewHolders.Count > 0) {
                        current = recyclableViewHolders[0];
                        recyclableViewHolders.RemoveAt(0);
                    } else {
                        current = adapter.OnCreateViewHolder(contentArea);
                    }
                    currentViewHolders[x] = current;
                }
                
                current.gameObject.SetActive(true);
                current.transform.SetParent(contentArea);
                adapter.OnBindViewHolder(current, x);

                var transform = current.transform as RectTransform;
                if (numItemsPerRow == 1) {
                    transform.anchorMin = new Vector2(0, 1);
                    transform.anchorMax = new Vector2(1, 1);
                } else {
                    float rowPosition = x % numItemsPerRow;
                    transform.anchorMin = new Vector2((rowPosition + 1) * anchorMultiple, 1f);
                    transform.anchorMax = transform.anchorMin;
                }
                transform.anchoredPosition = new Vector2(0f, verticalPosition);
            } else {
                var current = currentViewHolders[x];
                if (current != null) {
                    currentViewHolders[x] = null;
                    current.gameObject.SetActive(false);
                    recyclableViewHolders.Add(current);
                }
            }
        }
    }

    public void PlayAnimations() {
        for (int x = 0; x < currentViewHolders.Length; x++) {
            if (currentViewHolders[x] != null) {
                adapter.PlayAnimations(currentViewHolders[x], x);
            }
        }
    }
}
