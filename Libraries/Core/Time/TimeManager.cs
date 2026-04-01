using UnityEngine;



namespace Rune
{
    public class TimeManager : MonoPlusSingleton<TimeManager>
    {
        public override void InitCollection()
        {
            base.InitCollection();

            _timeScale.OnChange.AddListener(OnChangeTimeScale);
            _timePauser.OnChange.AddListener(OnChangeTimePauser);
        }

        public override void Refresh()
        {
            base.Refresh();

            _timeScale.Refresh();
            _timePauser.Refresh();
        }


        public static void PauseBy(MonoPlus pauser)
        {
            if (SingletonInstance == null) return;

            SingletonInstance._timePauser.Register(pauser);
        }

        public static void ResumeBy(MonoPlus pauser)
        {
            if (SingletonInstance == null) return;

            SingletonInstance._timePauser.Unregister(pauser);
        }

        public static void SetTimeScale(float value)
        {
            if (SingletonInstance == null) return;

            SingletonInstance.TimeScale = value;
        }



        private void RefreshTimeScale()
        {
            Time.timeScale = _timePauser.IsRegistered ? 0 : TimeScale;
        }

        private void OnChangeTimeScale(float value)
        {
            RefreshTimeScale();
        }

        private void OnChangeTimePauser(bool value)
        {
            RefreshTimeScale();
        }



        private readonly Attribute<float> _timeScale = new(1.0f);
        public float TimeScale { get => _timeScale.Value; set => _timeScale.Value = value; }



        private readonly Registry<MonoPlus> _timePauser = new();
    }
}