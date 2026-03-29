using UnityEngine;



namespace Rune
{
    public class TimeManager : MonoPlusSingleton<TimeManager>
    {
        public override void InitCollection()
        {
            base.InitCollection();

            _timePauser.OnChange.AddListener(OnChangeTimePauser);
        }

        public override void Refresh()
        {
            base.Refresh();

            _timePauser.Refresh();
        }


        public static void PauseBy(MonoPlus pauser)
        {
            _timePauser.Register(pauser);
        }

        public static void ResumeBy(MonoPlus pauser)
        {
            _timePauser.Unregister(pauser);
        }



        private void OnChangeTimePauser(bool value)
        {
            Time.timeScale = value ? 0 : 1;
        }



        private static readonly Registry<MonoPlus> _timePauser = new();
    }
}