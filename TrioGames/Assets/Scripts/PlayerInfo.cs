using System;
using System.Collections.Generic;

[Serializable]
public class PlayerInfo
{
    public string playFabID;
    public int level;
    public List<int> stars = new List<int>();
}