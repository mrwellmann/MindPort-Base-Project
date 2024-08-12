using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using UnityEngine.Scripting;
using VRBuilder.Core;
using VRBuilder.Core.Attributes;
using VRBuilder.Core.Conditions;
using VRBuilder.Core.SceneObjects;
using VRBuilder.Core.Utils;
using VRBuilder.UI.Properties;

namespace VRBuilder.UI.Conditions
{
    /// <summary>
    /// Condition which is completed when UI Button is Clicked
    /// </summary>
    [DataContract(IsReference = true)]
    public class UiButtonCondition : Condition<UiButtonCondition.EntityData>
    {
        [DisplayName("UI Button Click")]
        public class EntityData : IConditionData
        {
            [DataMember]
            [DisplayName("UI Button")]
            public SingleScenePropertyReference<IUiButtonProperty> Target { get; set; }

            [DataMember]
            [HideInProcessInspector]
            [Obsolete("Use Target instead.")]
            public ScenePropertyReference<IUiButtonProperty> UiButton { get; set; }

            public bool IsCompleted { get; set; }

            [IgnoreDataMember]
            [HideInProcessInspector]
            public string Name => $"UI Button Click: {Target}";

            public Metadata Metadata { get; set; }
        }

        private class ActiveProcess : BaseActiveProcessOverCompletable<EntityData>
        {
            public ActiveProcess(EntityData data) : base(data)
            {
            }

            protected override bool CheckIfCompleted()
            {
                return Data.Target.Value.IsClicked;
            }
        }

        private class EntityAutocompleter : Autocompleter<EntityData>
        {
            public EntityAutocompleter(EntityData data) : base(data)
            {
            }

            public override void Complete()
            {
                Data.Target.Value.FastForwardClick();
            }
        }

        [JsonConstructor, Preserve]
        public UiButtonCondition() : this(Guid.Empty)
        {
        }

        public UiButtonCondition(IUiButtonProperty target) : this(ProcessReferenceUtils.GetUniqueIdFrom(target))
        {
        }

        public UiButtonCondition(Guid guid)
        {
            Data.Target = new SingleScenePropertyReference<IUiButtonProperty>(guid);
        }

        public override IStageProcess GetActiveProcess()
        {
            return new ActiveProcess(Data);
        }

        protected override IAutocompleter GetAutocompleter()
        {
            return new EntityAutocompleter(Data);
        }
    }
}