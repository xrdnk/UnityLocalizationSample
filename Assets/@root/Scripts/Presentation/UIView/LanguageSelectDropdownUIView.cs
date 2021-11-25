using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Deniverse.UnityLocalizationSample.Presentation.Presentation.UIView
{
    public sealed class LanguageSelectDropdownUIView : MonoBehaviour
    {
        [SerializeField] Dropdown _dropdown;

        public delegate void SelectionChanged(int index);
        public SelectionChanged SelectionChangedEvent;

        void Start()
        {
            _dropdown.onValueChanged.AddListener(OnSelectionChanged);

            // 最初はドロップダウンを非活性状態にする
            _dropdown.ClearOptions();
            _dropdown.options.Add(new Dropdown.OptionData("Loading..."));
            _dropdown.interactable = false;
        }

        void OnDestroy()
        {
            _dropdown.onValueChanged.RemoveListener(OnSelectionChanged);
        }

        void OnSelectionChanged(int index)
        {
            SelectionChangedEvent?.Invoke(index);
        }

        /// <summary>
        /// ドロップダウンの初期化処理(ドロップダウンの項目値の設定)
        /// </summary>
        /// <param name="locales"></param>
        /// <param name="defaultIndex"></param>
        public void InitializeDropdownValueWithoutNotify(List<Locale> locales, int defaultIndex)
        {
            var options = new List<string>();

            foreach (var locale in locales)
            {
                var displayName =
                    locale.Identifier.CultureInfo != null
                        ? locale.Identifier.CultureInfo.NativeName
                        : locale.ToString();
                options.Add(displayName);
            }

            if (options.Count == 0)
            {
                options.Add("No Locales Available");
                _dropdown.interactable = false;
            }
            else
            {
                _dropdown.interactable = true;
            }

            _dropdown.ClearOptions();
            _dropdown.AddOptions(options);
            _dropdown.SetValueWithoutNotify(defaultIndex);
        }

        /// <summary>
        /// ドロップダウン値の更新
        /// </summary>
        /// <param name="index"></param>
        public void UpdateDropdownValueWithoutNotify(int index) => _dropdown.SetValueWithoutNotify(index);
    }
}