using Helpers;
using UnityEngine;
public class StoryManager : Singleton<StoryManager>
    {
        [SerializeField] private GameObject m_StartJourney;
        [SerializeField] private GameObject m_EndingPath;
        [SerializeField] private GameObject m_FirstEncounter;
        [SerializeField] private GameObject m_SecondChances;
        public void StartJourneyPopup()
        {
            Instantiate(m_StartJourney, transform.position, Quaternion.identity);
            Destroy(m_StartJourney, 5f);
        }
        public void EndingPathPopup()
        {
            Instantiate(m_EndingPath, transform.position, Quaternion.identity);
            Destroy(m_EndingPath, 5f);
        }
        public void FirstEncounterPopup()
        {
            Instantiate(m_FirstEncounter, transform.position, Quaternion.identity);
            Destroy(m_FirstEncounter, 5f);
        }
        public void SecondChancesPopup()
        {
            Instantiate(m_SecondChances, transform.position, Quaternion.identity);
            Destroy(m_SecondChances, 5f);
        }
    }