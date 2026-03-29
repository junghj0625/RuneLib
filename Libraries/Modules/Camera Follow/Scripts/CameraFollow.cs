using UnityEngine;



namespace Rune
{
    public class CameraFollow : MonoPlus
    {
        public override void LateUpdate()
        {
            base.LateUpdate();


            float deltaTime = UseUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

            Vector3 desiredPosition = GetDesiredPosition();

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Speed * deltaTime);

            transform.position = smoothedPosition;
        }

        public void MoveToTargetImmediately()
        {
            Vector3 desiredPosition = GetDesiredPosition();

            transform.position = desiredPosition;
        }



        public FollowMode Mode { get; set; } = FollowMode.None;

        public Vector2 Position { get; set; } = Vector2.zero;

        public Transform Target { get; set; } = null;

        public bool UseUnscaledTime { get; set; } = false;

        public float Speed { get; set; } = 3.0f;

        public Vector2 Offset = new(0, 0);



        private Vector3 GetDesiredPosition()
        {
            Vector3 desiredPosition = new();

            if (Mode == FollowMode.None)
            {
                desiredPosition = transform.position;
            }
            else if (Mode == FollowMode.Origin)
            {
                desiredPosition = new(0, 0, transform.position.z);
            }
            else if (Mode == FollowMode.Target)
            {
                if (Target == null)
                {
                    desiredPosition = new(0, 0, transform.position.z);
                }
                else
                {
                    desiredPosition = new(Target.position.x + Offset.x, Target.position.y + Offset.y, transform.position.z);
                }
            }  

            return desiredPosition;
        }



        public enum FollowMode
        {
            None,
            Origin,
            Target,
        }
    }
}