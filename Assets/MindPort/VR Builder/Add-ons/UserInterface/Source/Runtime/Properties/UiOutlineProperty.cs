using System;
using UnityEngine;
using UnityEngine.UI;
using VRBuilder.Core.Properties;

namespace VRBuilder.UI.Properties
{
    /// <summary>
    /// <see cref="IUiOutlineProperty{T}"/> implementation for any UI Element
    /// </summary>
    [RequireComponent(typeof(Outline))]
    public class UiOutlineProperty : ProcessSceneObjectProperty, IUiOutlineProperty
    {
        public AnimationCurve OutlineCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        public Outline Outline
        {
            get
            {
                if (SceneObject.GameObject.TryGetComponent(out Outline outline))
                    _outline = outline;
                else
                    throw new Exception($"{SceneObject?.GameObject.name} does not have an Outline Component");

                return _outline;
            }
        }
        private Outline _outline;

        private void Start()
        {
            SetWidth(0);
        }

        public AnimationCurve GetOutlineCurve()
        {
            return OutlineCurve;
        }
        
        public bool HighlightActive => _outline.enabled;

        public void SetHighlighted(bool highlight)
        {
            Outline.enabled = highlight;
        }

        public void SetEffectColor(Color color)
        {
            Outline.effectColor = color;    
        }

        public void SetWidth(float width)
        {
            Outline.effectDistance = new Vector2(width, width);
        }

        public float GetWidth()
        {
            return Outline.effectDistance.magnitude;
        }
    }
}