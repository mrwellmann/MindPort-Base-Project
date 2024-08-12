using System.Collections;
using System.Runtime.Serialization;
using VRBuilder.Core.Attributes;
using VRBuilder.Core.SceneObjects;
using VRBuilder.Core.Utils;
using UnityEngine;
using VRBuilder.Core.Behaviors;
using VRBuilder.Core;
using VRBuilder.UI.Properties;
using System;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace VRBuilder.UI.Behaviors
{
    /// <summary>
    /// Highlight a UI Component via Outline
    /// </summary>
    public class UiOutlineBehavior : Behavior<UiOutlineBehavior.EntityData>
    {
        [DisplayName("UI Outline")]
        public class EntityData : IBehaviorData
        {
            [DataMember(IsRequired = false)]
            [DisplayName("Outline Element")]
            public MultipleScenePropertyReference<IUiOutlineProperty> Targets { get; set; }

            [DataMember]
            [HideInProcessInspector]
            [Obsolete("Use Target instead.")]
            public ScenePropertyReference<IUiOutlineProperty> Outline { get; set; }

            [DataMember(IsRequired = false)]
            [DisplayName("AnimationDelay (0 = instant)")]
            public float AnimationDelay = 1f;

            [DataMember(IsRequired = false)]
            [DisplayName("Outline Border Size")]
            public float OutlineSize = 2f;

            [DataMember(IsRequired = false)]
            [DisplayName("Effect Color")]
            public Color EffectColor = new Color(0,0,0,0.5f);

            [DataMember(IsRequired = false)]
            [DisplayName("Reset After Step")]
            public bool ResetAfterStep = true;

            [IgnoreDataMember]
            [HideInProcessInspector]
            public string Name => $"Show Outline for: {Targets}";

            public Metadata Metadata { get; set; }
        }

        private class ActivatingProcess : StageProcess<EntityData>
        {
            private float runtime = 0;

            public ActivatingProcess(EntityData data) : base(data)
            {
            }

            public override void Start()
            {
                foreach (var uiOutlineProperty in Data.Targets.Values)
                {
                    uiOutlineProperty.SetEffectColor(Data.EffectColor);
                    uiOutlineProperty.SetHighlighted(true);

                    if (Data.AnimationDelay == 0)
                        uiOutlineProperty.SetWidth(0);
                    else
                        uiOutlineProperty.SetWidth(Data.OutlineSize);
                }
            }

            public override IEnumerator Update()
            {
                if (Data.AnimationDelay == 0)
                    yield break;

                while (runtime < Data.AnimationDelay)
                {
                    runtime += Time.deltaTime;
                    foreach (var uiOutlineProperty in Data.Targets.Values)
                    {
                        uiOutlineProperty.SetWidth(Data.OutlineSize * uiOutlineProperty.GetOutlineCurve().Evaluate(runtime / Data.AnimationDelay));
                    }
                    yield return null;
                }
            }

            public override void End()
            {
                foreach (var uiOutlineProperty in Data.Targets.Values)
                {
                    uiOutlineProperty.SetWidth(Data.OutlineSize);
                }
            }

            public override void FastForward()
            {
                foreach (var uiOutlineProperty in Data.Targets.Values)
                {
                    uiOutlineProperty.SetWidth(Data.OutlineSize);
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
                if (Data.ResetAfterStep)
                {
                    foreach (var uiOutlineProperty in Data.Targets.Values)
                    {
                        uiOutlineProperty.SetWidth(0);
                    }
                }

            }
        }

        [JsonConstructor, Preserve]
        public UiOutlineBehavior() : this(Guid.Empty)
        {
        }

        public UiOutlineBehavior(IUiOutlineProperty target) : this(ProcessReferenceUtils.GetUniqueIdFrom(target))
        {
        }

        public UiOutlineBehavior(Guid guid)
        {
            Data.Targets = new MultipleScenePropertyReference<IUiOutlineProperty>(guid);
        }


        public override IStageProcess GetActivatingProcess()
        {
            return new ActivatingProcess(Data);
        }

        public override IStageProcess GetDeactivatingProcess()
        {
            return new DeactivatingProcess(Data);
        }
    }
}