using Helpers;
using UnityEngine;

namespace DefaultNamespace
{
    public class StoryManager : Singleton<StoryManager>
    {
        [SerializeField] private GameObject StartJourney;
        
        public void StartJourneyPopup()
        {
            Instantiate(StartJourney, transform.position, Quaternion.identity);
        }

    }
}