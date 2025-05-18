using Newtonsoft.Json;
using UnityEngine;

public class InvernItem {
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int quantity { get; set; }
    public string imagePath { get; set; }

    public InvernItem() {}

    public InvernItem(int id, string name, string description, int quantity, string imagePath) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.quantity = quantity;
        this.imagePath = imagePath;
    }
    
    public override string ToString() {
        return JsonConvert.ToString(this);
    }
}
