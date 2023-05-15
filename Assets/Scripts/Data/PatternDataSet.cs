using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatternData", menuName = "ProjectForgotten/PatternDataSet")]
public class PatternDataSet : ScriptableObject
{
    public List<PatternData> patterns;
}