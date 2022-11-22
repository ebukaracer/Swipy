using UnityEngine;

namespace Racer.LoadManager
{
    internal class LoadingFx : MonoBehaviour
    {
        private ParticleSystem _fx;

        private void Awake()
        {
            _fx = GetComponent<ParticleSystem>();
        }

        private void Start()
        {
            _fx.Play();

            LoadManager.Instance.OnLoadFinished += Instance_OnLoadFinished;
        }

        private void Instance_OnLoadFinished()
        {
            _fx.Stop();
        }
    }
}
