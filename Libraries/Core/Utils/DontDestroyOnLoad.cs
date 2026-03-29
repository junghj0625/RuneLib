namespace Rune
{
    public class DontDestroyOnLoad : MonoPlus
    {
        public override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(gameObject);
        }

        public override void Start()
        {
            base.Start();

            Destroy(this);
        }
    }
}