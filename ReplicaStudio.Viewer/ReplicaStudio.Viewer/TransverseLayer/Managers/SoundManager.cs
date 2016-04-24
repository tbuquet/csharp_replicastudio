using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMOD;
using ReplicaStudio.Shared.TransverseLayer.Tools;

namespace ReplicaStudio.Viewer.TransverseLayer.Managers
{
    class SoundManager
    {
        #region Members
        private static FMOD.System system = null;
        private static Sound music = null;
        private static Sound sound = null;
        private static Sound voice = null;
        private static Channel musicChannel = null;
        private static Channel soundChannel = null;
        private static Channel voiceChannel = null;
        public delegate void EndMusicEventHandler(object sender, EventArgs e);
        public delegate void ErrorEventHandler(RESULT result);
        private static CHANNEL_CALLBACK channelMusicCallback;
        private static CHANNEL_CALLBACK channelSoundCallback;
        private static CHANNEL_CALLBACK channelVoiceCallback;
        public static event EndMusicEventHandler EndMusic;
        public static event EndMusicEventHandler EndSound;
        public static event EndMusicEventHandler EndVoice;
        public static event ErrorEventHandler EngineError;
        private static float DefaultMusicFrequency;
        private static float CustomMusicFrequency;
        #endregion

        #region Properties
        public static string CurrentMusicPath { get; set; }
        public static string CurrentSoundPath { get; set; }
        public static string CurrentVoicePath { get; set; }
        #endregion

        #region Init-UpDate-Release
        public static void Initialize(int NbChannel)
        {
            if (ViewerSettings.ActivateSound)
            {
                RESULT result;
                result = Factory.System_Create(ref system);
                if (EngineError != null) EngineError(result);
                uint version = 0;
                result = system.getVersion(ref version);
                if (EngineError != null) EngineError(result);
                if (version < VERSION.number)
                {
                    LogTools.WriteError("Error! You are using an old version of FMOD " + version.ToString("X") + ". This program requires " + VERSION.number.ToString("X") + ".");
                    throw new ApplicationException("Error! You are using an old version of FMOD " + version.ToString("X") + ". This program requires " + VERSION.number.ToString("X") + ".");
                }
                result = system.init(NbChannel, INITFLAGS.NORMAL, (IntPtr)null);
                if (EngineError != null) EngineError(result);
                channelMusicCallback = new CHANNEL_CALLBACK(OnEndMusic);
                channelSoundCallback = new CHANNEL_CALLBACK(OnEndSound);
                channelVoiceCallback = new CHANNEL_CALLBACK(OnEndVoice);
            }
        }
        public static void Update()
        {
            if (ViewerSettings.ActivateSound)
            {
                RESULT result = system.update();
                if (EngineError != null) EngineError(result);
            }
        }
        public static void Release()
        {
            RESULT result = RESULT.OK;
            if (music != null)
            {
                result = music.release();
                if (EngineError != null) EngineError(result);
            }
            if (system != null)
                result = system.release();
            if (EngineError != null) EngineError(result);
        }
        #endregion

        #region MainFunctions
        #region Play Functions
        public static void PlayMusic(string path)
        {
            PlayMusic(path, false);
        }
        public static void PlayMusic(string path, bool paused)
        {
            if (ViewerSettings.ActivateSound)
            {
                bool isPlaying = false;
                RESULT result = RESULT.OK;
                if (musicChannel != null)
                {
                    //si la musique existe
                    result = musicChannel.isPlaying(ref isPlaying);
                }
                else
                {
                    isPlaying = false;
                }
                if (EngineError != null) EngineError(result);
                if ((CurrentMusicPath == path) && isPlaying)//si la musique du chemin courant est entrain detre joué
                {
                    return;
                }
                else if (CurrentMusicPath == path && music != null)//sinon la musique du chemin courant nest pas entrain detre joué
                {
                    result = system.playSound(CHANNELINDEX.FREE, music, false, ref musicChannel);
                    DefaultMusicFrequency = GetFrequency();
                    result = musicChannel.setFrequency((CustomMusicFrequency * DefaultMusicFrequency / 100.0f) * 48000 / 100.0f);
                    if (EngineError != null) EngineError(result);
                    /*if (Channel != null)
                        result = Channel.setCallback(FMOD.CHANNEL_CALLBACKTYPE.END, channelCallback, 0);*/
                    if (EngineError != null) EngineError(result);
                }
                else
                {
                    //si cest une nouvelle musique
                    if (music != null)
                    {
                        if (musicChannel != null)
                        {
                            musicChannel.stop();
                            musicChannel = null;
                        }
                        result = music.release();
                        music = null;
                        if (EngineError != null) EngineError(result);
                    }
                    result = system.createStream(path, MODE.SOFTWARE | MODE.CREATECOMPRESSEDSAMPLE | MODE.LOOP_OFF, ref music);
                    if (result == RESULT.OK)
                    {
                        if (EngineError != null) EngineError(result);
                        result = system.playSound(CHANNELINDEX.FREE, music, paused, ref musicChannel);
                        DefaultMusicFrequency = GetFrequency();
                        result = musicChannel.setFrequency((CustomMusicFrequency * DefaultMusicFrequency / 100.0f) * 48000 / 100.0f);
                        if (EngineError != null) EngineError(result);
                        if (musicChannel != null)
                            result = musicChannel.setCallback(channelMusicCallback);
                        if (EngineError != null) EngineError(result);
                        CurrentMusicPath = path;
                    }
                }
            }
        }

        public static void PlaySound(string path)
        {
            bool isPlaying = false;
            RESULT result = RESULT.OK;
            if (soundChannel != null)
            {
                //si la musique existe
                result = soundChannel.isPlaying(ref isPlaying);
            }
            else
            {
                isPlaying = false;
            }
            if (EngineError != null) EngineError(result);
            if ((CurrentSoundPath == path) && isPlaying)//si la musique du chemin courant est entrain detre joué
            {
                return;
            }
            else if (CurrentSoundPath == path && sound != null)//sinon la musique du chemin courant nest pas entrain detre joué
            {
                result = system.playSound(CHANNELINDEX.FREE, sound, false, ref soundChannel);
                if (EngineError != null) EngineError(result);
                /*if (Channel != null)
                    result = Channel.setCallback(FMOD.CHANNEL_CALLBACKTYPE.END, channelCallback, 0);*/
                if (EngineError != null) EngineError(result);
            }
            else
            {
                //si cest une nouvelle musique
                if (sound != null)
                {
                    if (soundChannel != null)
                    {
                        soundChannel.stop();
                        soundChannel = null;
                    }
                    result = sound.release();
                    sound = null;
                    if (EngineError != null) EngineError(result);
                }
                result = system.createStream(path, MODE.SOFTWARE | MODE.CREATECOMPRESSEDSAMPLE | MODE.LOOP_OFF, ref sound);
                if (result == RESULT.OK)
                {
                    if (EngineError != null) EngineError(result);
                    result = system.playSound(CHANNELINDEX.FREE, sound, false, ref soundChannel);
                    if (EngineError != null) EngineError(result);
                    if (soundChannel != null)
                        result = soundChannel.setCallback(channelSoundCallback);
                    if (EngineError != null) EngineError(result);
                    CurrentSoundPath = path;
                }
            }
        }

        public static void PlayVoice(string path)
        {
            bool isPlaying = false;
            RESULT result = RESULT.OK;
            if (voiceChannel != null)
            {
                //si la musique existe
                result = voiceChannel.isPlaying(ref isPlaying);
            }
            else
            {
                isPlaying = false;
            }
            if (EngineError != null) EngineError(result);
            if ((CurrentVoicePath == path) && isPlaying)//si la musique du chemin courant est entrain detre joué
            {
                return;
            }
            else if (CurrentVoicePath == path && voice != null)//sinon la musique du chemin courant nest pas entrain detre joué
            {
                result = system.playSound(CHANNELINDEX.FREE, voice, false, ref voiceChannel);
                if (EngineError != null) EngineError(result);
                /*if (Channel != null)
                    result = Channel.setCallback(FMOD.CHANNEL_CALLBACKTYPE.END, channelCallback, 0);*/
                if (EngineError != null) EngineError(result);
            }
            else
            {
                //si cest une nouvelle musique
                if (voice != null)
                {
                    if (voiceChannel != null)
                    {
                        voiceChannel.stop();
                        voiceChannel = null;
                    }
                    result = voice.release();
                    voice = null;
                    if (EngineError != null) EngineError(result);
                }
                result = system.createStream(path, MODE.SOFTWARE | MODE.CREATECOMPRESSEDSAMPLE | MODE.LOOP_OFF, ref voice);
                if (result == RESULT.OK)
                {
                    if (EngineError != null) EngineError(result);
                    result = system.playSound(CHANNELINDEX.FREE, voice, false, ref voiceChannel);
                    if (EngineError != null) EngineError(result);
                    if (voiceChannel != null)
                        result = voiceChannel.setCallback(channelVoiceCallback);
                    if (EngineError != null) EngineError(result);
                    CurrentVoicePath = path;
                }
            }
        }
        #endregion

        #region Stop Functions
        public static void StopMusic()
        {
            if (ViewerSettings.ActivateSound && musicChannel != null)
            {
                RESULT result = musicChannel.stop();
                //Channel = null;
                CurrentMusicPath = string.Empty;
                if (EngineError != null) EngineError(result);
            }
        }
        public static void StopSound()
        {
            if (soundChannel != null)
            {
                RESULT result = soundChannel.stop();
                //Channel = null;
                CurrentSoundPath = string.Empty;
                if (EngineError != null) EngineError(result);
            }
        }
        public static void StopVoice()
        {
            if (voiceChannel != null)
            {
                RESULT result = voiceChannel.stop();
                //Channel = null;
                CurrentVoicePath = string.Empty;
                if (EngineError != null) EngineError(result);
            }
        }
        #endregion

        #region Pause Functions
        public static bool GetPaused()
        {
            bool pause = false;
            if (musicChannel != null)
            {
                RESULT result = musicChannel.getPaused(ref pause);
                if (EngineError != null) EngineError(result);
            }
            return pause;
        }
        public static void SetPaused(bool stat)
        {
            if (musicChannel != null)
            {
                RESULT result = musicChannel.setPaused(stat);
                if (EngineError != null) EngineError(result);
            }
        }
        public static void SetPaused()
        {
            bool paused = false;
            if (musicChannel != null)
            {
                RESULT result = musicChannel.getPaused(ref paused);
                if (EngineError != null) EngineError(result);
                result = musicChannel.setPaused(!paused);
                if (EngineError != null) EngineError(result);
            }
        }
        #endregion

        #region Volume Functions
        public static void SetMute()
        {
            bool mute = false;
            if (musicChannel != null)
            {
                RESULT result = musicChannel.getMute(ref mute);
                if (EngineError != null) EngineError(result);
                result = musicChannel.setMute(!mute);
                if (EngineError != null) EngineError(result);
            }
        }
        public static void SetMute(bool value)
        {
            if (musicChannel != null)
            {
                RESULT result = musicChannel.setMute(value);
                if (EngineError != null) EngineError(result);
            }
        }
        public static bool GetMute()
        {
            bool mute = false;
            if (musicChannel != null)
            {
                RESULT result = musicChannel.getMute(ref mute);
                if (EngineError != null) EngineError(result);
            }
            return mute;
        }
        public static float GetVolume()
        {
            float vol = 1.0f;
            if (musicChannel != null)
            {
                RESULT result = musicChannel.getVolume(ref vol);
                if (EngineError != null) EngineError(result);
            }
            return vol;
        }
        public static void SetVolume(float Value)
        {
            Value = Math.Abs(Value);
            if (musicChannel != null)
            {
                RESULT result = musicChannel.setVolume(Value);
                if (EngineError != null) EngineError(result);
            }
        }
        #endregion

        #region Frequence Functions
        public static float GetFrequency()
        {
            float frq = 0;
            if (musicChannel != null)
            {
                RESULT result = musicChannel.getFrequency(ref frq);
                if (EngineError != null) EngineError(result);
            }
            return ((frq * 100.0f) / 48000);
        }
        public static void SetFrequency(float freq)
        {
            if (ViewerSettings.ActivateSound && musicChannel != null)
            {
                CustomMusicFrequency = freq;

                RESULT result = musicChannel.setFrequency((CustomMusicFrequency * DefaultMusicFrequency / 100.0f) * 48000 / 100.0f);
                if (EngineError != null) EngineError(result);
            }
        }
        #endregion

        #region Position Functions
        public static void SetPosition(uint pos)
        {
            uint ln = 0;
            if (music != null) music.getLength(ref ln, TIMEUNIT.MS);
            pos = (ln * pos / 100);
            if (musicChannel != null)
            {
                RESULT result = musicChannel.setPosition(pos, TIMEUNIT.MS);
                if (EngineError != null) EngineError(result);
            }
        }
        public static uint GetPosition()
        {
            uint ln = 0;
            uint ms = 0;
            if (music != null)
            {
                RESULT result = music.getLength(ref ln, TIMEUNIT.MS);
                if (EngineError != null) EngineError(result);
                ms = GetMsPosition();
                ms = (100 * ms / ln);
            }
            else
                ms = 0;
            return ms;
        }
        public static void SetMsPosition(uint ms)
        {
            if (musicChannel != null)
            {
                RESULT result = musicChannel.setPosition(ms, TIMEUNIT.MS);
                if (EngineError != null) EngineError(result);
            }
        }
        public static uint GetMsPosition()
        {
            uint ms = 0;
            if (musicChannel != null)
            {
                RESULT result = musicChannel.getPosition(ref ms, TIMEUNIT.MS);
                if (EngineError != null) EngineError(result);
            }
            return ms;
        }
        public static uint GetLength()
        {
            uint ln = 0;
            if (music != null) music.getLength(ref ln, TIMEUNIT.MS);
            return ln;
        }
        #endregion
        #endregion

        #region Event Functions
        private static RESULT OnEndMusic(IntPtr channelraw, FMOD.CHANNEL_CALLBACKTYPE type, IntPtr pt1, IntPtr pt2)//int command, uint commanddata1, uint commanddata2)
        {
            musicChannel = null;// en premier pour ne pas avoir erreur lors d'un MessageBox :) ; cas particulier
            if (EndMusic != null)
            {
                EndMusic(CurrentMusicPath, new EventArgs());
            }
            return RESULT.OK;
        }
        private static RESULT OnEndSound(IntPtr channelraw, FMOD.CHANNEL_CALLBACKTYPE type, IntPtr pt1, IntPtr pt2)//int command, uint commanddata1, uint commanddata2)
        {
            soundChannel = null;// en premier pour ne pas avoir erreur lors d'un MessageBox :) ; cas particulier
            if (EndSound != null)
            {
                EndSound(CurrentSoundPath, new EventArgs());
            }
            return RESULT.OK;
        }
        private static RESULT OnEndVoice(IntPtr channelraw, FMOD.CHANNEL_CALLBACKTYPE type, IntPtr pt1, IntPtr pt2)//int command, uint commanddata1, uint commanddata2)
        {
            voiceChannel = null;// en premier pour ne pas avoir erreur lors d'un MessageBox :) ; cas particulier
            if (EndVoice != null)
            {
                EndVoice(CurrentVoicePath, new EventArgs());
            }
            return RESULT.OK;
        }
        #endregion

        #region Error Functions
        public static void ShowErrorException(RESULT result)
        {
            if (result != RESULT.OK)
            {
                throw new ApplicationException("Sound Manager Error! " + result + " - " + Error.String(result));
            }
        }
        public static void ShowErrorDialogBox(RESULT result, string AppName)
        {
            if (result != RESULT.OK)
            {
                //System.Windows.Forms.MessageBox.Show("Sound Manager Error! " + result + " - " + Error.String(result), AppName, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Other Functions
        public static bool IsMusicPlaying()
        {
            bool ply = false;
            if (musicChannel != null)
            {
                RESULT result = musicChannel.isPlaying(ref ply);
                if (EngineError != null) EngineError(result);
            }
            return ply;
        }

        public static bool IsSoundPlaying()
        {
            bool ply = false;
            if (soundChannel != null)
            {
                RESULT result = soundChannel.isPlaying(ref ply);
                if (EngineError != null) EngineError(result);
            }
            return ply;
        }
        #endregion 
    }
}
