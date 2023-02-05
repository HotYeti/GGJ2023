using Helpers;
using UnityEngine;
public class StoryManager : Singleton<StoryManager>
    {
        [SerializeField] private GameObject m_StartJourney;
        [SerializeField] private GameObject m_EndingPath;
        [SerializeField] private GameObject m_FirstEncounter;
        [SerializeField] private GameObject m_SecondChances;
        public AudioClip music;
        private AudioSource audioSource;
        public void StartJourneyPopup()
        {
            var story = Instantiate(m_StartJourney, transform.position, Quaternion.identity);
            Destroy(story, 8f);
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = music;
            audioSource.loop = false;
            audioSource.volume = 0.08f;
            audioSource.Play();
        }
        public void EndingPathPopup()
        {
            var story = Instantiate(m_EndingPath, transform.position, Quaternion.identity);
            Destroy(story, 8f);
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = music;
            audioSource.loop = false;
            audioSource.volume = 0.08f;
            audioSource.Play();
        }
        public void FirstEncounterPopup()
        {
            var story = Instantiate(m_FirstEncounter, transform.position, Quaternion.identity);
            Destroy(story, 8f);
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = music;
            audioSource.loop = false;
            audioSource.volume = 0.08f;
            audioSource.Play();
        }
        public void SecondChancesPopup()
        {
            var story = Instantiate(m_SecondChances, transform.position, Quaternion.identity);
            Destroy(story, 8f);
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = music;
            audioSource.loop = false;
            audioSource.volume = 0.08f;
            audioSource.Play();
        }
    }