using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class PlayerData {
    public List<LevelResult> LevelResults;
}
[Serializable]
public class LevelResult
{
    public string LevelName;
    public int Score;
    public int TimePassed;
    public int Circles;
    public int Squares;

    public override bool Equals(Object obj)
    {
        // Check for null values and compare run-time types.
        if (obj == null || GetType() != obj.GetType())
            return false;

        LevelResult p = (LevelResult)obj;
        return p.LevelName == LevelName;
    }

    public override int GetHashCode()
    {
        return LevelName.GetHashCode();
    }
}


