using UnityEngine;
using VRBuilder.Core.Properties;

namespace VRBuilder.UI.Properties
{
    public interface IUiOutlineProperty : ISceneObjectProperty
    {
        void SetEffectColor(Color color);
        void SetHighlighted(bool highlight);
        void SetWidth(float width);
        float GetWidth();
        AnimationCurve GetOutlineCurve();
    }
}