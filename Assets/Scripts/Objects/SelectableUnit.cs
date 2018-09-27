namespace Hackle.Objects
{
    using Hackle.Util;
    using UnityEngine;

    /// <summary>
    /// Base class for all units which can be selected.
    /// Therefore it is assumed that the GameObject contains an element which is tagged with <code>Highlighter</code>.
    /// </summary>
    public class SelectableUnit : Unit, ISelectable
    {
        public void Select(bool selected)
        {
            foreach (Transform t in gameObject.transform)
            {
                if (t.CompareTag(Tag.Highlighter))
                {
                    t.gameObject.GetComponent<MeshRenderer>().enabled = selected;
                }
            }
        }

        public bool IsSelected()
        {
            foreach (Transform t in gameObject.transform)
            {
                if (t.CompareTag(Tag.Highlighter))
                {
                    return t.gameObject.GetComponent<MeshRenderer>().enabled;
                }
            }
            return false;
        }
    }
}