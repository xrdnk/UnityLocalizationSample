using UnityEngine;
using UnityEngine.UI;

namespace Deniverse.UnityLocalizationSample.Presentation.View
{
    /// <summary>
    /// サンプル用のローカライズビュー
    /// </summary>
    public sealed class SampleLocalizationUIView : MonoBehaviour
    {
        [SerializeField] Image _image_Flag;
        [SerializeField] Text _text_HelloWorld;
        [SerializeField] AudioSource _audioSource;

        /// <summary>
        /// 国旗イメージの設定
        /// </summary>
        /// <param name="sprite"></param>
        public void SetFlagImage(Sprite sprite) => _image_Flag.sprite = sprite;

        /// <summary>
        /// テキストの設定
        /// </summary>
        /// <param name="text"></param>
        public void SetTextMessage(string text) => _text_HelloWorld.text = text;

        /// <summary>
        /// ハローワールドの声を出す
        /// </summary>
        /// <param name="audioClip"></param>
        public void PlayHelloWorld(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }
}