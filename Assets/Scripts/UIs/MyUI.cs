namespace CoolGoal.UI
{
    using UnityEngine;

    public class MyUI : MonoBehaviour
    {
        protected void Active()
        {
            gameObject.SetActive(true);
        }

        protected void DeActive()
        {
            gameObject.SetActive(false);
        }
    }
}
