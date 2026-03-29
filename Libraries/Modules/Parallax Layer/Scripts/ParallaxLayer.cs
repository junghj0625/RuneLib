using UnityEngine;



namespace Rune
{
    public class ParallaxLayer : MonoPlus
    {
        public override void InitObjects()
        {
            base.InitObjects();

            if (CameraTransform == null) CameraTransform = Camera.main.transform;
        }

        public override void LateUpdate()
        {
            base.LateUpdate();

            Vector2 offset = (Vector2)CameraTransform.position * parallaxWeight;

            transform.position = Utils.Math.Vector3((Vector2)CameraTransform.position - offset, transform.position.z);
        }



        public Transform CameraTransform { get; set; } = null;

        public float parallaxWeight = 0.5f;
    }
}