using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KaiTool.Utilities
{
    public struct SteppedObjectEventArgs { }
    public struct HighlightObjectArgs { }
    public struct BeatableObjectArgs { }
    public interface ISteppedObject
    {
        bool IsStarted
        {
            get; set;
        }
        bool IsFinished
        {
            get; set;
        }
        bool IsClean
        {
            get; set;
        }
        void ToggleMarked(bool toggle);
        void Reset();
        void OnSteppedObjectStart(UnityEngine.Object sender, SteppedObjectEventArgs e);
        void OnSteppedObjectFinished(UnityEngine.Object sender, SteppedObjectEventArgs e);
        void OnSteppedObjectUnFinished(UnityEngine.Object sender, SteppedObjectEventArgs e);
    }
    public interface IHighlightObject
    {
        void ToggleHighlight(bool on);
    }
    public interface IScrewedObject
    {
        EnumScrewType ScrewType
        {
            get; set;
        }
        bool IsCanbeScrewed
        {
            get; set;
        }
        bool IsScrewed
        {
            get; set;
        }

    }
    public interface IScrewingObject
    {
        EnumScrewType ScrewType
        {
            get; set;
        }
    }
    public interface IBeatableObject
    {
        bool IsBeaten
        {
            get;
        }
        void OnBeaten(UnityEngine.Object sender, BeatableObjectArgs e);
    }
    [Flags]
    public enum EnumScrewType
    {
        None,
        ToolA,
        ToolB,
        ToolC
    }

}
namespace KaiTool.PC.Absorption
{
    public enum EnumFixType
    {
        Kinematic,
        FixJoint
    }
    public interface IAbsorber
    {
        EnumAbsorbType AbsorbType { get; }
        bool IsCanAbsorb { get; set; }
        bool IsHovering { get; }
        bool IsAbsorbing { get; }
        IAbsorbTarget HoveringTarget { get; }
        IAbsorbTarget AbsorbingTarget { get; }
        Transform Anchor { get; }
        Action<UnityEngine.Object, AbsorberEventArgs> Absorbing { get; set; }
        Action<UnityEngine.Object, AbsorberEventArgs> Releasing { get; set; }
        Action<UnityEngine.Object, AbsorberEventArgs> HoveringIn { get; set; }
        Action<UnityEngine.Object, AbsorberEventArgs> HoveringOut { get; set; }

        void OnAbsorbbing(UnityEngine.Object sender, AbsorberEventArgs e);
        void OnReleasing(UnityEngine.Object sender, AbsorberEventArgs e);
        void OnHoveringIn(UnityEngine.Object sender, AbsorberEventArgs e);
        void OnHoveringOut(UnityEngine.Object sender, AbsorberEventArgs e);
    }
    public struct AbsorbTargetEventArgs
    {
        public GameObject m_grabbingHand;
        public IAbsorber m_absorber;
        public AbsorbTargetEventArgs(GameObject grabbingHand, IAbsorber absorber)
        {
            m_grabbingHand = grabbingHand;
            m_absorber = absorber;
        }
    }
    public interface IAbsorbTarget
    {
        EnumAbsorbType AbsorbType { get; }
        float AbsorptionRadius { get; set; }
        bool IsCanbeAbsorbed { get; set; }
        bool IsHovered { get; }
        bool IsAbsorbed { get; }
        IAbsorber HoveringAbsorber { get; }
        IAbsorber CurrentAbsorber { get; }

        Transform Transform { get; }
        GameObject GameObject { get; }
        Rigidbody Rigidbody { get; }

        Action<UnityEngine.Object, AbsorbTargetEventArgs> Absorbbed { get; set; }
        Action<UnityEngine.Object, AbsorbTargetEventArgs> Released { get; set; }
        Action<UnityEngine.Object, AbsorbTargetEventArgs> HoveredIn { get; set; }
        Action<UnityEngine.Object, AbsorbTargetEventArgs> HoveredOut { get; set; }
        void ForcedAbsorbed(AbsorbTargetEventArgs e);
        void ForcedReleased(AbsorbTargetEventArgs e);

        //void OnAbsorbbed(UnityEngine.Object sender, AbsorbTargetEventArgs e);
        //void OnReleased(UnityEngine.Object sender, AbsorbTargetEventArgs e);
        //void OnHoveredIn(UnityEngine.Object sender, AbsorbTargetEventArgs e);
        //void OnHoveredOut(UnityEngine.Object sender, AbsorbTargetEventArgs e);
    }
    public enum EnumAbsorbType
    {
        None,
        LaserLauncher
    }
    public struct AbsorberEventArgs
    {
        public IAbsorbTarget m_absorbTarget;
        public AbsorberEventArgs(IAbsorbTarget absorbTarget)
        {
            m_absorbTarget = absorbTarget;
        }
    }
}