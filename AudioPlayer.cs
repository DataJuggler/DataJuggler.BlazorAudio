

#region using statements

using System;
using NAudio.Wave;
using System.Timers;
using DataJuggler.Blazor.Components;

#endregion

namespace DataJuggler.BlazorAudio
{

    #region class AudioPlayer
    /// <summary>
    /// This class is used to play audio
    /// </summary>
    public class AudioPlayer : IDisposable
    {
        
        #region Private Variables
        private AudioFileReader audioFileReader;
        private IWavePlayer player;
        private Timer timer;
        private BlazorAudioPlayer parent;
        public event Action<TimeSpan> OnTimeUpdated;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a 'AudioPlayer' object.
        /// </summary>
        public AudioPlayer(string filePath, BlazorAudioPlayer blazorParent)
        {
            // Store the parent
            Parent = blazorParent;

            Player = new WaveOutEvent();
            AudioFileReader = new AudioFileReader(filePath);
            Player.Init(audioFileReader);
            player.PlaybackStopped += OnPlaybackStopped;
            
            Timer = new Timer(1000); // Update every second
            Timer.Elapsed += (sender, e) => OnTimeUpdated?.Invoke(CurrentTime);
        }
        #endregion
        
        #region Events
            
            #region OnPlaybackStopped(object sender, StoppedEventArgs e)
            /// <summary>
            /// event is fired when On Playback Stopped
            /// </summary>
            private void OnPlaybackStopped(object sender, StoppedEventArgs e)
            {
                // Stop
                Stop();

                // if the value for HasParent is true
                if (HasParent)
                {
                    // Create a new instance of a 'Message' object.
                    Message message = new Message();                   

                    // Set the Text
                    message.Text = "Refresh";

                    // Send a message
                    Parent.ReceiveData(message);
                }
            }
            #endregion
            
        #endregion
        
        #region Methods
            
            #region Dispose()
            /// <summary>
            /// method Dispose
            /// </summary>
            public void Dispose()
            {
                player.Dispose();
                audioFileReader.Dispose();
                timer.Dispose();
            }
            #endregion
            
            #region Pause()
            /// <summary>
            /// method Pause
            /// </summary>
            public void Pause()
            {
                // if the value for HasPlayer is true
                if (HasPlayer)
                {
                    // Start the Player
                    Player.Pause();

                    // if the value for HasTimer is true
                    if (HasTimer)
                    {
                        // Start the Timer
                        Timer.Stop();
                    }
                }
            }
            #endregion
            
            #region Play()
            /// <summary>
            /// method Play
            /// </summary>
            public void Play()
            {
                // if the value for HasPlayer is true
                if (HasPlayer)
                {
                    // Start the Player
                    Player.Play();

                    // if the value for HasTimer is true
                    if (HasTimer)
                    {
                        // Start the Timer
                        Timer.Start();
                    }
                }
            }
            #endregion
            
            #region Stop()
            /// <summary>
            /// method Stop
            /// </summary>
            public void Stop()
            {
                // if the value for HasPlayer is true
                if (HasPlayer)
                {
                    // Start the Player
                    Player.Stop();

                    // if the value for HasAudioFileReader is true
                    if (HasAudioFileReader)
                    {
                        // Reset
                        AudioFileReader.Position = 0;
                    }

                    // if the value for HasTimer is true
                    if (HasTimer)
                    {
                        // Stop the Timer
                        Timer.Stop();
                    }
                }
            }
            #endregion
            
        #endregion
        
        #region Properties
            
            #region AudioFileReader
            /// <summary>
            /// This property gets or sets the value for 'AudioFileReader'.
            /// </summary>
            public AudioFileReader AudioFileReader
            {
                get { return audioFileReader; }
                set { audioFileReader = value; }
            }
            #endregion
            
            #region CurrentTime
            /// <summary>
            /// This property gets or sets the value for CurrentTime.
            /// </summary>
            public TimeSpan CurrentTime
            {
                get
                {
                    // initial value
                    TimeSpan currentTime = new TimeSpan();

                    // if the value for HasAudioFileReader is true
                    if (HasAudioFileReader)
                    {
                        // Set the return value
                        currentTime = AudioFileReader.CurrentTime;
                    }

                    // return value
                    return currentTime;
                }
            }
            #endregion
            
            #region Duration
            /// <summary>
            /// This property gets or sets the value for Duration.
            /// </summary>
            public TimeSpan Duration
            {
                get
                {
                    // initial value
                    TimeSpan duration = new TimeSpan();

                    // if the value for HasAudioFileReader is true
                    if (HasAudioFileReader)
                    {
                        // Set the duration
                        duration = AudioFileReader.TotalTime;
                    }

                    // return value
                    return duration;
                }
            }
            #endregion
            
            #region HasAudioFileReader
            /// <summary>
            /// This property returns true if this object has an 'AudioFileReader'.
            /// </summary>
            public bool HasAudioFileReader
            {
                get
                {
                    // initial value
                    bool hasAudioFileReader = (this.AudioFileReader != null);
                    
                    // return value
                    return hasAudioFileReader;
                }
            }
            #endregion

            #region HasParent
            /// <summary>
            /// This property returns true if this object has a 'Parent'.
            /// </summary>
            public bool HasParent
            {
                get
                {
                    // initial value
                    bool hasParent = (this.Parent != null);
                    
                    // return value
                    return hasParent;
                }
            }
            #endregion
            
            #region HasTimer
            /// <summary>
            /// This property returns true if this object has a 'Timer'.
            /// </summary>
            public bool HasTimer
            {
                get
                {
                    // initial value
                    bool hasTimer = (this.Timer != null);
                    
                    // return value
                    return hasTimer;
                }
            }
            #endregion
            
            #region IsPlaying
            /// <summary>
            /// This read only property returns true if PlayBackState equals Playing
            /// </summary>
            public bool IsPlaying
            {  
                get
                {
                    // set value
                    bool isPlaying = (PlaybackState == PlaybackState.Playing);
                    
                    // return value
                    return isPlaying;
                }
            }
            #endregion
            
            #region Parent
            /// <summary>
            /// This property gets or sets the value for 'Parent'.
            /// </summary>
            public BlazorAudioPlayer Parent
            {
                get { return parent; }
                set { parent = value; }
            }
            #endregion
            
            #region PlaybackState
            /// <summary>
            /// This property returns the PlayBackState from the Player
            /// </summary>
            public PlaybackState PlaybackState
            {
                get
                {
                    // initial value
                    PlaybackState playbackState = PlaybackState.Stopped;

                    // if the value for HasPlayer is true
                    if (HasPlayer)
                    {
                        // set the return value
                        playbackState = Player.PlaybackState;
                    }
                    
                    // return value
                    return playbackState;
                }
            }
            #endregion
            
            #region HasPlayer
            /// <summary>
            /// This property returns true if this object has a 'Player'.
            /// </summary>
            public bool HasPlayer
            {
                get
                {
                    // initial value
                    bool hasPlayer = (this.Player != null);
                    
                    // return value
                    return hasPlayer;
                }
            }
            #endregion
            
            #region Player
            /// <summary>
            /// This property gets or sets the value for 'Player'.
            /// </summary>
            public IWavePlayer Player
            {
                get { return player; }
                set { player = value; }
            }
            #endregion
            
            #region Timer
            /// <summary>
            /// This property gets or sets the value for 'Timer'.
            /// </summary>
            public Timer Timer
            {
                get { return timer; }
                set { timer = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
