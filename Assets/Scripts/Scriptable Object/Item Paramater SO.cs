using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class ItemParamaterSO : ScriptableObject
    {
       [field: SerializeField] public string ParamaterName { get; private set; }
       
    }
}

