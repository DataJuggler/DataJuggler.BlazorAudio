

#region using statements

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DataJuggler.UltimateHelper;

#endregion

namespace DataJuggler.BlazorAudio
{

    #region class TimeHelper
    /// <summary>
    /// This class is used to display a time.
    /// </summary>
    public class TimeHelper
    {
        
        #region Private Variables
        #endregion
        
        #region Events
            
        #endregion
        
        #region Methods
            
            #region FormatDisplayTime(double totalSeconds)
            /// <summary>
            /// This method formats the seconds into Display Time
            /// </summary>
            public static string FormatDisplayTime(double timeInSeconds)
            {
                // initial value
                string displayTime = "";
                
                try
                {
                    // Create a new instance of a 'StringBuilder' object.
                    StringBuilder sb = new StringBuilder();
                    
                    if (timeInSeconds > 0)
                    {
                        // locals
                        int hours = (int) Math.Floor(timeInSeconds / 3600);
                        int minutes = (int) Math.Floor(timeInSeconds / 60);
                        int seconds = (int) Math.Floor(timeInSeconds % 60);
                    
                        // only include hours if it is set
                        if (hours > 0)
                        {
                            // Append the hours and colon
                            sb.Append(hours);
                            sb.Append(":");
                        
                            // reduce the minutes
                            minutes -= (hours * 60);
                        
                            // if 1:09 minutes for example
                            if (minutes < 10)
                            {
                                // Append a preceding 0
                                sb.Append("0");
                            }
                        }
                    
                        // Append the minutes and colon
                        sb.Append(minutes);
                        sb.Append(":");
                    
                        // if the audio is 25:05 for example
                        if (seconds < 10)
                        {
                            // Append a preceding 0
                            sb.Append("0");
                        }

                        // Append the Seconds
                        sb.Append(seconds);
                    
                        // Set the DisplayTime
                        displayTime = sb.ToString();
                    }
                    else
                    {
                        // Set to 0
                        displayTime = "0:00";
                    }
                }
                catch (Exception error)
                {
                    // for debugging only for now
                    DebugHelper.WriteDebugError("FormatDisplayTime", "TimeHelper.cs", error);
                }
                
                // return value
                return displayTime;
            }
            #endregion
            
        #endregion
        
        #region Properties
            
        #endregion
        
    }
    #endregion

}
