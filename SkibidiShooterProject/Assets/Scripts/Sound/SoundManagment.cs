using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundManage
{
    public class SoundManagment : MonoBehaviour
    {

        [SerializeField] private List<SoundKeyVal> soundKeyVals = new List<SoundKeyVal>();



  

        public AudioClip RequestSound(string key)
        {
         foreach(SoundKeyVal keyVal in soundKeyVals)
            {
                if(keyVal.soundKey == key)
                {
                    if(keyVal.clips.Count == 0) return null;
                    int rand = Random.Range(0,keyVal.clips.Count);
                    return keyVal.clips[rand];
                }
                
            }
         return null;
      
        }
        public void PlayOnShot(AudioSource source,string key)
        {
            if (RequestSound(key))
            {
                source.clip = RequestSound(key);
                source.PlayOneShot(source.clip);
            }
        }
    }
    
    
}
[System.Serializable]
struct SoundKeyVal
{
    public string soundKey;
    public List<AudioClip> clips;
}