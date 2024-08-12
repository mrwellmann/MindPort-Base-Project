using System;
using VRBuilder.Core.Properties;

namespace VRBuilder.UI.Properties
{
    public interface IUiButtonProperty : ISceneObjectProperty
    {
        event EventHandler<EventArgs> OnClicked;

        void SetText(string text);
        bool IsClicked { get; set; }
        void FastForwardClick();
    }
}
