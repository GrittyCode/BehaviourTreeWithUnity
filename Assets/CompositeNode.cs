using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeNode : Node
{
    [HideInInspector] public List<Node> Childeren = new List<Node>();

    public override Node Clone()
    {
        CompositeNode node = Instantiate(this);
        node.Childeren = Childeren.ConvertAll(c => c.Clone());
        return node;
    }
}
