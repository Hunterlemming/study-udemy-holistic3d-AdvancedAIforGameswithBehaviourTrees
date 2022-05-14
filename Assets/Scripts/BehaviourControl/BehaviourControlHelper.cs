using System.Collections.Generic;
using UnityEngine;

namespace BehaviourControl
{
    public static class BehaviourControlHelper
    {
        public static void PrintTree(this Node node, List<string> treeString = null, int indent = 0)
        {
            treeString ??= new List<string>();
            
            treeString.Add($"\n{new string('-', indent)}{node.Name}");
            foreach (var child in node.Children) PrintTree(child, treeString, indent + 1);

            if (indent == 0) Debug.Log(string.Join("", treeString));
        }
    }
}