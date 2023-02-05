using System;
using System.Collections;
using Helpers;
using UnityEngine;
public class StoryManager : Singleton<StoryManager>
    {
        [SerializeField] private GameObject m_StartJourney;
        [SerializeField] private GameObject m_EndingPath;
        [SerializeField] private GameObject m_FirstEncounter;
        [SerializeField] private GameObject m_SecondChances;
        [SerializeField] private GameObject m_TimesUp;

        private bool m_ShowedFirstEncounter = false;
        private bool m_ShowedSecondChances = false;
        
        public AudioClip music;
        private AudioSource audioSource;

        private GameObject m_ActiveStory;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = music;
            audioSource.loop = false;
            audioSource.volume = 0.04f;
        }

        private void SetActiveStory(GameObject story, float destroyTime)
        {

            StartCoroutine(_SetActiveStory());

            IEnumerator _SetActiveStory()
            {
                m_ActiveStory = story;
                yield return new WaitForSeconds(destroyTime);
                Destroy(m_ActiveStory);
                m_ActiveStory = null;
            }
           
        }
        
        public IEnumerator StartJourneyPopup()
        {
            if (m_ActiveStory)
                yield return new WaitUntil(() => m_ActiveStory is null);
            
            SetActiveStory(Instantiate(m_StartJourney, transform.position, Quaternion.identity), 5f);
            
            audioSource.Play();
        }
        public IEnumerator EndingPathPopup()
        {
            if (m_ActiveStory)
                yield return new WaitUntil(() => m_ActiveStory is null);

            m_ActiveStory = Instantiate(m_EndingPath, transform.position, Quaternion.identity);

            audioSource.Play();
        }
        public IEnumerator FirstEncounterPopup()
        {
            if (m_ShowedFirstEncounter)
                yield break;

            m_ShowedFirstEncounter = true;
            
            if (m_ActiveStory)
                yield return new WaitUntil(() => m_ActiveStory is null);
            
            SetActiveStory(Instantiate(m_FirstEncounter, transform.position, Quaternion.identity), 5f);

            audioSource.Play();
        }
        public IEnumerator SecondChancesPopup()
        {
            if (m_ShowedSecondChances)
                yield break;

            m_ShowedSecondChances = true;
            
            if (m_ActiveStory)
                yield return new WaitUntil(() => m_ActiveStory is null);
            
            SetActiveStory(Instantiate(m_SecondChances, transform.position, Quaternion.identity), 5f);
            
            audioSource.Play();
        }
        
        public IEnumerator TimesUpPopup()
        {
            if (m_ActiveStory)
                yield return new WaitUntil(() => m_ActiveStory is null);
            
            SetActiveStory(Instantiate(m_TimesUp, transform.position, Quaternion.identity), 5f);
            
            audioSource.Play();
        }
    }