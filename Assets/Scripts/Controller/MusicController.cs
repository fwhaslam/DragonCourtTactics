using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller {

    /// <summary>
    /// We want exactly ONE music controller in each scene, which is never destroyed.
    /// </summary>
    public class MusicController : MonoBehaviour {

        internal static MusicController instance;

        internal static readonly string MusicPath = "Attributable/Music";
        internal static readonly string StartingSongName = "Adventure";

        // background music behavior vars
        internal static Dictionary<string,AudioClip> musicMap;
        internal static List<string> songNames;
        internal static AudioSource audioSource;
        internal static int currentSong;

        // keep one instance of the MusicController script.
        void Awake() {

            // slam the door on copies
            if (instance!=null) {
print(">>>>>>> Music Destroying");
                Destroy(this.gameObject);
                return;
            }

            // mark the current instance as the permament instance
            instance = this;
            DontDestroyOnLoad( this.gameObject );
            audioSource = this.GetComponent<AudioSource>();

            // load music from Resource folder.
            PrepareMusicMap();

            // coroutine continues to wait for song and start new one
            StartCoroutine( WaitForSongEndAndStartNewSong() );
        }

        void PrepareMusicMap() {
            musicMap = new Dictionary<string, AudioClip>();
            Object[] musicArray = Resources.LoadAll( MusicPath );
            foreach ( Object music in musicArray ) {
                if (music.GetType()!=typeof(AudioClip)) continue;
                string name = BeforeUnderstore(music.name);
                musicMap[name] = (AudioClip)music;
			}
            songNames = new List<string>(musicMap.Keys);
        }

        string BeforeUnderstore( string name ) {
            return name.Split('_')[0];
		}

        static void PlaySong( string name ) {
            PlaySong( songNames.IndexOf(name) );
		}

        static void PlaySong( int songIndex ) {
print("Music Playing ["+songIndex+"]");

            currentSong = songIndex;
            string songName = songNames[ songIndex ];
            AudioClip clip = musicMap[songName];
 
            audioSource.PlayOneShot( clip );
		}

        static void StopSong() {
            audioSource.Stop();
		}

        /// <summary>
        /// Background music default behavior:
        /// 1) wait 15 seconds for first song
        /// 2) check every 5 seconds for song completion
        /// 3) wait 30 seconds then play random song ( not the same one )
        /// 4) back to 2
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitForSongEndAndStartNewSong() {

            print("Music Waiting 15");
            yield return new WaitForSeconds(15);

            // play the starting song
            PlaySong( StartingSongName );

            // loop: wait and play, wait and play
            while (true) {

                // wait for song to end
                while ( audioSource.isPlaying) {
              print("Music Waiting 15");
                  yield return new WaitForSeconds(5);
			    }

                // wait a while, then start new one
               print("Music Waiting 30");
               yield return new WaitForSeconds(30);

                // select a new song
                int pickSong = currentSong;
                while ( pickSong==currentSong ) {
                    pickSong = Random.Range( 0, songNames.Count );
                }
                
                PlaySong( pickSong );
		    }
        }
    }

}