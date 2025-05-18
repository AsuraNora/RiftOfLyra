using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public enum TileMapState {
    Ground,
    Grass,
    Forest,
}

public class TileMapDetail 
{
    public int x { get; set; }
    public int y { get; set; }
    public TileMapState tileMapState { get; set; }

    public TileMapDetail(){}

    public TileMapDetail(int x, int y, TileMapState tileMapState)
    {   
        this.x = x;
        this.y = y;
        this.tileMapState = tileMapState;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
