using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor;


namespace Assets.Editor
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, GraphView.UxmlTraits> { }

        private UnityEditor.Editor editor;

        public InspectorView()
        {

        }

        public void UpdateSelection(NodeView node)
        {
            Clear();

            UnityEngine.Object.DestroyImmediate(editor);
            editor = UnityEditor.Editor.CreateEditor(node.node);
            IMGUIContainer container = new IMGUIContainer(() => { editor.OnInspectorGUI();});
            Add(container);
        }
    }
}
