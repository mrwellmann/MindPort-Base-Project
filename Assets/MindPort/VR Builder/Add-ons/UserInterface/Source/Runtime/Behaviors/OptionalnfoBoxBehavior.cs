using Newtonsoft.Json;
using System;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine.Scripting;
using VRBuilder.Core;
using VRBuilder.Core.Attributes;
using VRBuilder.Core.Behaviors;
using VRBuilder.Core.Configuration.Modes;
using VRBuilder.Core.SceneObjects;
using VRBuilder.Core.Utils;
using VRBuilder.UI.Properties;
using VRBuilder.UI.Utils;

namespace VRBuilder.UI.Behaviors
{
    /// <summary>
    /// Show an optional Infobox with all the provided Texts, Images and Settings
    /// </summary>
    [DataContract(IsReference = true)]
    public class OptionalnfoBoxBehavior : Behavior<OptionalnfoBoxBehavior.EntityData>, IOptional
    {
        [DisplayName("Optional Infobox")]
        [DataContract(IsReference = true)]
        public class EntityData : IBehaviorData
        {
            [DataMember(IsRequired = false)]
            [DisplayName("Infobox")]
            public SingleScenePropertyReference<IInfoBoxProperty> Target { get; set; }

            [DataMember]
            [HideInProcessInspector]
            [Obsolete("Use Target instead.")]
            public ScenePropertyReference<IInfoBoxProperty> InfoBox { get; set; }

            [DataMember(IsRequired = false)]
            [DisplayName("Delay (in Seconds)")]
            public float Delay = 0f;

            [DataMember]
            [DisplayName("Hide After Step")]
            public bool HideAfterStep = false;

            [IgnoreDataMember]
            [HideInProcessInspector]
            public string Name => $"Show Infobox: {Target}";

            public Metadata Metadata { get; set; }
        }

        private class ActivatingProcess : InstantProcess<EntityData>
        {
            public ActivatingProcess(EntityData data) : base(data)
            {
            }

            public override void Start()
            {
                var infoBox = Data.Target.Value;
                if(infoBox != null) 
                { 
                    infoBox.SetInfoBoxType(InfoBoxType.Optional);
                    infoBox.ShowInfoBox(true, Data.Delay);
                }
                else
                {
                    var sceneBox = FindValidInfobox();
                    sceneBox?.SetInfoBoxType(InfoBoxType.Optional);
                    sceneBox?.ShowInfoBox(true, Data.Delay);
                }
            }
        }

        private class DeactivatingProcess : InstantProcess<EntityData>
        {
            public DeactivatingProcess(EntityData data) : base(data)
            {
            }

            public override void Start()
            {
                if (Data.HideAfterStep)
                {
                    var infoBox = Data.Target.Value;
                    if (infoBox != null)
                    {
                        infoBox.ShowInfoBox(false);
                    }
                    else
                    {
                        var sceneBox = FindValidInfobox();
                        sceneBox?.ShowInfoBox(false);
                    }
                }
            }
        }


        [JsonConstructor, Preserve]
        public OptionalnfoBoxBehavior() : this(Guid.Empty)
        {
        }

        public OptionalnfoBoxBehavior(IInfoBoxProperty target) : this(ProcessReferenceUtils.GetUniqueIdFrom(target))
        {
        }

        public OptionalnfoBoxBehavior(Guid guid)
        {
            Data.Target = new SingleScenePropertyReference<IInfoBoxProperty>(guid);
        }

        public override IStageProcess GetActivatingProcess()
        {
            return new ActivatingProcess(Data);
        }

        public override IStageProcess GetDeactivatingProcess()
        {
            return new DeactivatingProcess(Data);
        }

        protected static IInfoBoxProperty FindValidInfobox()
        {
            return InterfaceExtensions.FindInterfaceOfType<IInfoBoxProperty>().FirstOrDefault(infoBox => infoBox.IsValid && infoBox.InfoBoxType == InfoBoxType.Optional);
        }
    }
}
