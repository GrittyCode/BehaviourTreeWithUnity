using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Assets.Editor
{
    public class SplitView : TwoPaneSplitView
    {
        public new class UxmlFactory : UxmlFactory<SplitView, TwoPaneSplitView.UxmlTraits> { }

   
    }
}
