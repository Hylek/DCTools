using UnityEngine;

namespace DCTools
{
    [ExecuteInEditMode]
    public class BlankParentObject : MonoBehaviour
    {
        private void Start() => ZeroTransform();

        private void OnDestroy()
        {
#if UNITY_EDITOR
            transform.hideFlags = HideFlags.None;
#endif
        }

        public void ZeroTransform()
        {
            var cachedTrans = transform;
            cachedTrans.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            cachedTrans.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            cachedTrans.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
}