using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace Deniverse.UnityLocalizationSample.Domain.Extension
{
    /// <summary>
    /// ScriptableObject 用の LocalizedAssetEvent
    /// </summary>
    [AddComponentMenu("Localization/Asset/Localize ScriptableObject Event")]
    public class LocalizeScriptableObjectEvent : LocalizedAssetEvent<ScriptableObject, LocalizedScriptableObject, UnityEventScriptableObject> {}

    /// <summary>
    /// ScriptableObject 用のローカライズオブジェクト
    /// </summary>
    [Serializable]
    public class LocalizedScriptableObject : LocalizedAsset<ScriptableObject> {}

    /// <summary>
    /// ScriptableObject を引数とする Unity Event
    /// </summary>
    [Serializable]
    public class UnityEventScriptableObject : UnityEvent<ScriptableObject> {}
}