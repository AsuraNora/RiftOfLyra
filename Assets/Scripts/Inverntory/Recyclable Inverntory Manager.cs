using System.Collections.Generic;
using PolyAndCode.UI;
using UnityEngine;

public class RecyclableInverntoryManager : MonoBehaviour, IRecyclableScrollRectDataSource
{
   [SerializeField]
   RecyclableScrollRect recyclableScrollRect;
   [SerializeField]
   private int dataLength;
   private List<InvernItem> invernItems = new List<InvernItem>();

    private void Awake()
    {
        recyclableScrollRect.DataSource = this;
    }

    public int GetItemCount()
    {
        return invernItems.Count;
    }

    public void SetCell(ICell cell, int index){
        var item = cell as CellItemData;
        item.ConfiguerCell(invernItems[index], index);
    }

    private void Start()
    {
        List<InvernItem> lstItems = new List<InvernItem>();
        for (int i = 0; i < dataLength; i++) {
            InvernItem invernItem = new InvernItem();
            invernItem.name = "Item " + i;
            invernItem.description = "Description" + i;
            lstItems.Add(invernItem);
        }
        SetLstItem(lstItems);
        recyclableScrollRect.ReloadData();
    }

    public void SetLstItem(List<InvernItem> lstItems) {
        invernItems = lstItems;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)) {
            InvernItem invernItem = new InvernItem();
            invernItem.name = "New Item";
            invernItem.description = "New Description";
            invernItems.Add(invernItem);
            recyclableScrollRect.ReloadData();
        }
    }

    private void addItem(InvernItem invernItem) {
        invernItems.Add(invernItem);
        recyclableScrollRect.ReloadData();
    }
}
