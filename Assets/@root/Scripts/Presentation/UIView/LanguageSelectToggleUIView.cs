using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Deniverse.UnityLocalizationSample.Presentation.UIView
{
    public sealed class LanguageSelectToggleUIView : MonoBehaviour
    {
        [SerializeField] Transform _container;
        [SerializeField] GameObject _togglePrefab;

        readonly Dictionary<int, Toggle> _toggles = new();
        ToggleGroup _toggleGroup;

        public delegate void SelectionChanged(int index);
        public SelectionChanged SelectionChangedEvent;

        void Start()
        {
            _toggleGroup = _container.gameObject.AddComponent<ToggleGroup>();
        }

        /// <summary>
        /// トグルボタンの初期化処理（トグルの生成，値の設定）
        /// </summary>
        /// <param name="locales"></param>
        /// <param name="defaultIndex"></param>
        public void InitializeToggleValueWithoutNotify(List<Locale> locales, int defaultIndex)
        {
            for (var i = 0; i < locales.Count; ++i)
            {
                var locale = locales[i];

                // トグルの生成
                var languageToggle = Instantiate(_togglePrefab, _container);
                // トグルオブジェクトの名前とラベルにはネイティブネームを設定する
                languageToggle.name = locale.Identifier.CultureInfo != null
                    ? locale.Identifier.CultureInfo.NativeName
                    : locale.ToString();
                var label = languageToggle.GetComponentInChildren<Text>();
                label.text = languageToggle.name;

                // デフォルトのロケールインデックスの場合はトグルをONにする
                var toggle = languageToggle.GetComponent<Toggle>();
                toggle.SetIsOnWithoutNotify(i == defaultIndex);

                // 辞書データキャッシュ
                _toggles[i] = toggle;

                var localeIndex = i;
                toggle.onValueChanged.AddListener(val =>
                {
                    if (val)
                    {
                        SelectionChangedEvent?.Invoke(localeIndex);
                    }
                });

                toggle.group = _toggleGroup;
            }
        }

        /// <summary>
        /// トグル値の更新
        /// </summary>
        /// <param name="index"></param>
        public void UpdateToggleValueWithoutNotify(int index)
        {
            foreach (var toggle in _toggles.Values)
            {
                toggle.SetIsOnWithoutNotify(false);
            }
            _toggles[index].SetIsOnWithoutNotify(true);
        }
    }
}