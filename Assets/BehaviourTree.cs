using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using static UnityEditor.UIElements.ToolbarMenu;

[CreateAssetMenu()]
public class BehaviourTree : ScriptableObject
{
    public Node rootNode;
    public Node.State treeState = Node.State.Running;
    public List<Node> nodes = new List<Node>();

    public Node.State Update()
    {
        if (rootNode.state == Node.State.Running)
            treeState = rootNode.Update();

        return treeState;
    }

    public Node CreateNode(System.Type type)
    {
        Node node = ScriptableObject.CreateInstance(type) as Node;
        node.name = type.Name;
        node.guid = GUID.Generate().ToString();
        nodes.Add(node);

        AssetDatabase.AddObjectToAsset(node, this);
        AssetDatabase.SaveAssets();
        return node;
    }

    public void DeleteNode(Node node)
    {
        nodes.Remove(node);
        AssetDatabase.RemoveObjectFromAsset(node);
        AssetDatabase.SaveAssets();
    }

    public void AddChild(Node parent, Node child)
    {
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator)
            decorator.Child = child;

        RootNode rootNode = parent as RootNode;
        if (rootNode)
            rootNode.Child = child;

        CompositeNode composite = parent as CompositeNode;
        if(composite)
            composite.Childeren.Add(child);
    }

    public void RemoveChild(Node parent, Node child)
    {
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator)
            decorator.Child = null;

        RootNode rootNode = parent as RootNode;
        if (rootNode)
            rootNode.Child = null;

        CompositeNode composite = parent as CompositeNode;
        if (composite)
            composite.Childeren.Remove(child);
    }

    public List<Node> GetChildren(Node parent)
    {
        List<Node> children = new List<Node>();

        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator && decorator.Child != null)
            children.Add(decorator.Child);

        RootNode rootNode = parent as RootNode;
        if (rootNode && rootNode.Child != null)
            children.Add(rootNode.Child);

        CompositeNode composite = parent as CompositeNode;
        if (composite)
            return composite.Childeren;

        return children;
    }

    public BehaviourTree Clone()
    {
        BehaviourTree tree = Instantiate(this);
        tree.rootNode = tree.rootNode.Clone();
        return tree;
    }
}
