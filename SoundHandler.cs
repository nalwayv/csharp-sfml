using System;
using System.Collections.Generic;
using System.IO;
using SFML.Audio;

namespace csharp_sfml
{

    public enum SoundType
    {
        Music, Sound
    }

    public class SoundHandler
    {
        /// <summary>
        ///     Store sound data
        /// </summary>
        struct SoundData
        {
            public Sound Sound { get; set; }
            public SoundBuffer Buffer { get; set; }

            public SoundData(Sound sound, SoundBuffer buffer)
            {
                Sound = sound;
                Buffer = buffer;
            }
        }

        /// <summary>
        ///     Store music data
        /// </summary>
        struct MusicData
        {
            public Music Music { get; set; }

            public MusicData(Music music)
            {
                Music = music;
            }
        }

        Dictionary<string, SoundData> soundData;
        Dictionary<string, MusicData> musicData;

        // --- Singleton
        private static readonly Lazy<SoundHandler> instance = new Lazy<SoundHandler>(() => new SoundHandler());
        public static SoundHandler Instance { get => instance.Value; }
        private SoundHandler()
        {
            soundData = new Dictionary<string, SoundData>();
            musicData = new Dictionary<string, MusicData>();
        }
        // ---

        /// <summary>
        ///     Load sounds or music
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="id"></param>
        /// <param name="soundtype"></param>
        public void Load(string filename, string id, SoundType soundtype)
        {
            try
            {
                // sound
                if (soundtype == SoundType.Sound)
                {
                    if (soundData.ContainsKey(id))
                    {
                        Logger.Instance.Log.Information($"SOUND '{id}' ALREADY ADDED");
                        return;
                    }

                    var buffer = new SoundBuffer(filename);
                    var sound = new Sound();

                    soundData.Add(id, new SoundData(sound, buffer));
                }
                // music
                else if (soundtype == SoundType.Music)
                {
                    if (musicData.ContainsKey(id))
                    {
                        Logger.Instance.Log.Information($"MUSIC '{id}' ALREADY ADDED");
                        return;
                    }

                    var music = new Music(filename);

                    musicData.Add(id, new MusicData(music));
                }
            }
            catch (FileNotFoundException e)
            {
                Logger.Instance.Log.Debug($"FILE '{filename}' NOT FOUND");
                throw new Exception($"{e}");
            }
        }

        public bool ContainsSoundID(string id)
        {
            if (soundData.ContainsKey(id))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Clear sound from database
        /// </summary>
        /// <param name="id">sound id</param>
        public void ClearSound(string id)
        {
            if (soundData.ContainsKey(id))
            {
                soundData[id].Sound.Dispose();
                soundData[id].Buffer.Dispose();
                soundData.Remove(id);
            }
        }

        public void ClearMusic(string id)
        {
            if (musicData.ContainsKey(id))
            {
                musicData[id].Music.Dispose();
                soundData.Remove(id);
            }
        }

        /// <summary>
        ///     Play chosen sound
        /// </summary>
        /// <param name="id">sound id</param>
        /// <param name="volume">sound volume</param>
        /// <param name="loop">is looping</param>
        public void PlaySound(string id, float volume = 50f, bool loop = false)
        {
            if (soundData.ContainsKey(id))
            {
                var data = soundData[id];

                data.Sound.SoundBuffer = data.Buffer;

                data.Sound.Loop = loop;

                data.Sound.Volume = volume;

                data.Sound.Play();
            }
        }

        /// <summary>
        ///     Play chosen music
        /// </summary>
        /// <param name="id"></param>
        /// <param name="volume"></param>
        /// <param name="loop"></param>
        public void PlayMusic(string id, float volume = 50f, bool loop = true)
        {
            if (musicData.ContainsKey(id))
            {
                var data = musicData[id];

                data.Music.Loop = loop;

                data.Music.Volume = volume;

                data.Music.Play();
            }
        }

        /// <summary>
        ///     Is already playing
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsPlaying(string id, SoundType soundtype)
        {

            if (soundtype == SoundType.Sound)
            {
                if (soundData.ContainsKey(id))
                {
                    return soundData[id].Sound.Status == SoundStatus.Playing;
                }
            }

            if (soundtype == SoundType.Music)
            {
                if (musicData.ContainsKey(id))
                {
                    return musicData[id].Music.Status == SoundStatus.Playing;
                }
            }

            return false;
        }

        /// <summary>
        ///     Stop all currently playing type
        /// </summary>
        /// <param name="soundtype">SoundTYpe</param>
        public void StopAll(SoundType soundtype)
        {
            if (soundtype == SoundType.Sound && soundData.Count > 0)
            {
                foreach (var sound in soundData.Values)
                {
                    if (sound.Sound.Status == SoundStatus.Playing)
                    {
                        sound.Sound.Stop();
                    }
                }
            }

            if (soundtype == SoundType.Music && musicData.Count > 0)
            {
                foreach (var sound in musicData.Values)
                {
                    if (sound.Music.Status == SoundStatus.Playing)
                    {
                        sound.Music.Stop();
                    }
                }
            }
        }
    }
}