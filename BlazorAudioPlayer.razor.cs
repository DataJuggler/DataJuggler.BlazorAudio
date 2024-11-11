

#region using statements

using DataJuggler.Blazor.Components;
using DataJuggler.Blazor.Components.Interfaces;
using DataJuggler.UltimateHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

#endregion

namespace DataJuggler.BlazorAudio
{

    #region class BlazorAudioPlayer
    /// <summary>
    /// This class is used to play audios and provide information on what button was clicked,
    /// so audio play time can be determined.
    /// </summary>
    public partial class BlazorAudioPlayer : IBlazorComponent
    {
        
        #region Private Variables
        private bool allowContextMenu;
        private bool allowDownloads;
        private string audioDuration;
        private AudioPlayer audioPlayer;
        private string audioUrl;
        private string contextMenuInfo;
        private string controlsList;
        private string displayTime;
        private TimeSpan duration;
        private string graphBackgroundStyle;
        private double graphHeight;
        private string graphHeightStyle;
        private string graphStyle;        
        private string name;
        private double currentTime;
        private IBlazorComponentParent parent;
        private bool showControlsContainer;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a BlazorAudioPlayer object.
        /// </summary>
        public BlazorAudioPlayer()
        {
            // Perform initializations for this object
            Init();
        }
        #endregion
        
        #region Events
            
            #region GraphClicked()
            /// <summary>
            /// This method is called when the Graph is Clicked
            /// </summary>
            public void GraphClicked()
            {
                // to do: Figure out the current time clicked
                // to do: Set current time in audio
            }
        #endregion

        #endregion
        
        #region Methods
            
            #region GetCurrentTime()
            /// <summary>
            /// This method returns the Current Time
            /// </summary>
            public int GetCurrentTime()
            {
                // initial value
                int currentTime = 0;
                
                // return value
                return currentTime;
            }
            #endregion
            
            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // Defaults
                AllowDownloads = false;
                AllowContextMenu = false;
                GraphHeight = .8;
                
                // Default Display Time until it is set by the audio since the Duration is displayed first
                displayTime = "0:00";
                
                // Default to true
                ShowControlsContainer = true;

                // If the AudioUrl string exists
                if (TextHelper.Exists(AudioUrl))
                {
                    AudioPlayer = new AudioPlayer(AudioUrl, this);
                    Duration = AudioPlayer.Duration;
                    AudioDuration = TimeHelper.FormatDisplayTime(Duration.TotalSeconds);
                    CurrentTime = 0;
                    DisplayTime = TimeHelper.FormatDisplayTime(CurrentTime) + " / ";
                    AudioPlayer.OnTimeUpdated += UpdateTimeLabel;
                }
            }
            #endregion
            
            #region PlayOrPause()
            /// <summary>
            /// method Play Or Pause
            /// </summary>
            public void PlayOrPause()
            {
                if (HasAudioPlayer)
                {
                    if (Playing)
                    {
                        // Stop Playing
                        AudioPlayer.Pause();
                    }
                    else
                    {
                        // Press Play
                        AudioPlayer.Play();
                    }
                }
            }
            #endregion
            
            #region ReceiveData(Message message)
            /// <summary>
            /// This method is used to receive messages from the parent page
            /// </summary>
            /// <param name="message"></param>
            public void ReceiveData(Message message)
            {
                // If the message object exists
                if (NullHelper.Exists(message))
                {
                    // If Refresh was sent
                    if (message.Text == "Refresh")
                    {
                        // Update
                        Refresh();
                    }
                }
            }
            #endregion

            #region Refresh()
            /// <summary>
            /// method Refresh
            /// </summary>
            public void Refresh()
            {
                // Update the UI
                InvokeAsync(() =>
                {
                    StateHasChanged();
                });
            }
            #endregion
            
            #region UpdateTimeLabel(TimeSpan currentTime)
            /// <summary>
            /// method Update Time Label
            /// </summary>
            private void UpdateTimeLabel(TimeSpan currentTime)
            {
                InvokeAsync(() =>
                {
                    CurrentTime = currentTime.TotalSeconds;

                    // Set the DisplayTime
                    DisplayTime = TimeHelper.FormatDisplayTime(CurrentTime) + " / ";

                    // Refresh
                    StateHasChanged();
                });
            }
            #endregion
                
        #endregion
            
        #region Properties
                
            #region AllowContextMenu
            /// <summary>
            /// This property gets or sets the value for 'AllowContextMenu'.
            /// </summary>
            [Parameter]
            public bool AllowContextMenu
            {
                get { return allowContextMenu; }
                set
                {
                    // set the value
                    allowContextMenu = value;
                        
                    // if the value for allowContextMenu is true
                    if (allowContextMenu)
                    {
                        // Erase
                        ContextMenuInfo = "return true;";
                    }
                    else
                    {
                        // This is the default value
                        ContextMenuInfo = "return false;";
                    }
                }
            }
            #endregion
                
            #region AllowDownloads
            /// <summary>
            /// This property gets or sets the value for 'AllowDownloads'.
            /// </summary>
            [Parameter]
            public bool AllowDownloads
            {
                get { return allowDownloads; }
                set
                {
                    // set the value
                    allowDownloads = value;
                        
                    // if the value for allowDownloads is true
                    if (allowDownloads)
                    {
                        // Use blank, I think this will still allow the download
                        ControlsList = "";
                    }
                    else
                    {
                        // Use blank, I think this will still allow the download
                        ControlsList = "nodownload";
                    }
                }
            }
            #endregion
                
            #region AudioDuration
            /// <summary>
            /// This property gets or sets the value for 'AudioDuration'.
            /// </summary>
            public string AudioDuration
            {
                get { return audioDuration; }
                set { audioDuration = value; }
            }
            #endregion
                
            #region AudioPlayer
            /// <summary>
            /// This property gets or sets the value for 'AudioPlayer'.
            /// </summary>
            public AudioPlayer AudioPlayer
            {
                get { return audioPlayer; }
                set { audioPlayer = value; }
            }
            #endregion
                
            #region AudioUrl
            /// <summary>
            /// This property gets or sets the value for 'AudioUrl'.
            /// </summary>
            [Parameter]
            public string AudioUrl
            {
                get { return audioUrl; }
                set 
                {
                    // set the value
                    audioUrl = value;

                    // Perform initializations for this object
                    Init();
                }
            }
            #endregion
                
            #region ContextMenuInfo
            /// <summary>
            /// This property gets or sets the value for 'ContextMenuInfo'.
            /// </summary>
            public string ContextMenuInfo
            {
                get { return contextMenuInfo; }
                set { contextMenuInfo = value; }
            }
            #endregion
                
            #region ControlsList
            /// <summary>
            /// This property gets or sets the value for 'ControlsList'.
            /// </summary>
            public string ControlsList
            {
                get { return controlsList; }
                set { controlsList = value; }
            }
            #endregion
                
            #region CurrentTime
            /// <summary>
            /// This property gets or sets the value for 'CurrentTime'.
            /// </summary>
            public double CurrentTime
            {
                get { return currentTime; }
                set { currentTime = value; }
            }
            #endregion
            
            #region DisplayTime
            /// <summary>
            /// This property gets or sets the value for 'DisplayTime'.
            /// </summary>
            public string DisplayTime
            {
                get { return displayTime; }
                set { displayTime = value; }
            }
            #endregion
                
            #region Duration
            /// <summary>
            /// This property gets or sets the value for 'Duration'.
            /// </summary>
            public TimeSpan Duration
            {
                get { return duration; }
                set { duration = value; }
            }
            #endregion
                
            #region GraphBackgroundStyle
            /// <summary>
            /// This property gets or sets the value for 'GraphBackgroundStyle'.
            /// </summary>
            public string GraphBackgroundStyle
            {
                get { return graphBackgroundStyle; }
                set { graphBackgroundStyle = value; }
            }
            #endregion
                
            #region GraphHeight
            /// <summary>
            /// This property gets or sets the value for 'GraphHeight'.
            /// </summary>
            public double GraphHeight
            {
                get { return graphHeight; }
                set
                {
                    // set the value
                    graphHeight = value;
                        
                    // set the string value
                    graphHeightStyle = graphHeight.ToString() + "vh";
                }
            }
            #endregion
                
            #region GraphHeightStyle
            /// <summary>
            /// This property gets or sets the value for 'GraphHeightStyle'.
            /// </summary>
            public string GraphHeightStyle
            {
                get { return graphHeightStyle; }
                set { graphHeightStyle = value; }
            }
            #endregion
                
            #region GraphStyle
            /// <summary>
            /// This property gets or sets the value for 'GraphStyle'.
            /// </summary>
            public string GraphStyle
            {
                get { return graphStyle; }
                set { graphStyle = value; }
            }
            #endregion
                
            #region HasAudioPlayer
            /// <summary>
            /// This property returns true if this object has an 'AudioPlayer'.
            /// </summary>
            public bool HasAudioPlayer
            {
                get
                {
                    // initial value
                    bool hasAudioPlayer = (this.AudioPlayer != null);
                        
                    // return value
                    return hasAudioPlayer;
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
                
            #region Name
            /// <summary>
            /// This property gets or sets the value for 'Name'.
            /// </summary>
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            #endregion
                
            #region Parent
            /// <summary>
            /// This property gets or sets the value for 'Parent'.
            /// </summary>
            [Parameter]
            public IBlazorComponentParent Parent
            {
                get { return parent; }
                set
                {
                    parent = value;
                        
                    // if the value for HasParent is true
                    if (HasParent)
                    {
                        // Register with the parent
                        Parent.Register(this);
                    }
                }
            }
            #endregion
                
            #region Playing
            /// <summary>
            /// This property gets or sets the value for 'Playing'.
            /// </summary>
            public bool Playing
            {
                get
                {
                    // initial value
                    bool isPlaying = false;
                        
                    // if the value for HasAudioPlayer is true
                    if (HasAudioPlayer)
                    {
                        // Set the return value
                        isPlaying = AudioPlayer.IsPlaying;
                    }
                        
                    // return value
                    return isPlaying;
                }
            }
            #endregion
                
            #region ShowControlsContainer
            /// <summary>
            /// This property gets or sets the value for 'ShowControlsContainer'.
            /// </summary>
            [Parameter]
            public bool ShowControlsContainer
            {
                get { return showControlsContainer; }
                set { showControlsContainer = value; }
            }
            #endregion
                
        #endregion
            
    }
    #endregion
    
}
