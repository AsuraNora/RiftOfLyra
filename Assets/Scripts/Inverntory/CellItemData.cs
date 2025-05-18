using PolyAndCode.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CellItemData : MonoBehaviour, ICell
{
    //UI
    public Text nameLabel;
    public Text deslABEL;
    // public Text genderLabel;
    // public Text idLabel;

    private InvernItem _contactInfo;
    private int _cellIndex;

    public void ConfiguerCell(InvernItem invernItem, int cellIndex){
        _cellIndex = cellIndex;
        _contactInfo = invernItem;
        nameLabel.text = invernItem.name;
        deslABEL.text = invernItem.description;
        // genderLabel.text = contactInfo.Gender;
        // idLabel.text = contactInfo.id;   
    }
}
