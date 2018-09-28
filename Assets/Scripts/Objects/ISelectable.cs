namespace Hackle.Objects
{
    public interface ISelectable : IObject
    {
        /// <summary>
        /// Selects or deselects the unit by enabling or disabling the <code>Highlighter</code> component of this GameObject.
        /// </summary>
        /// <param name="selected">true to select this unit, false to deselect</param>
        void Select(bool selected);

        /// <summary>
        /// Determines if the unit is selected.
        /// </summary>
        /// <returns>true if the unit is selected, false else</returns>
        bool IsSelected();
    }
}