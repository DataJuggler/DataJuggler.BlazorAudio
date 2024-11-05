

#region using statements

using DataJuggler.UltimateHelper;
using Microsoft.JSInterop;
using System.Threading.Tasks;

#endregion

namespace DataJuggler.BlazorAudio
{

    #region class BlazorJSBridge
    /// <summary>
    /// This method is used to communicate with the BlazorJSInterop file
    /// </summary>
    public class BlazorJSBridge
    {
        
        #region Private Variables
        #endregion
        
        #region Methods
            
            #region GetCurrentTime(IJSRuntime jsRuntime)
            /// <summary>
            /// method Get Current Time
            /// </summary>
            public async static Task<int> GetCurrentTime(IJSRuntime jsRuntime)
            {
                // set the value
                int currentTime = 0;
                
                try
                {
                    // set the value
                    var audioTime = await jsRuntime.InvokeAsync<double>("BlazorJSFunctions.GetCurrentTime");
                    
                    // return the value cast as an int
                    currentTime = (int) audioTime;
                }
                catch (System.Exception error)
                {
                    // for debugging only
                    DebugHelper.WriteDebugError("GetCurrentTime", "BlazorJSBridge", error);
                }
                
                // return value
                return currentTime;
            }
            #endregion

            #region GetAudioDuration(IJSRuntime jsRuntime)
            /// <summary>
            /// method Gets Audio Length
            /// </summary>
            public async static Task<double> GetAudioDuration(IJSRuntime jsRuntime)
            {
                // set the value
                double audioDuration = 0;
                
                try
                {
                    // set the value
                    audioDuration = await jsRuntime.InvokeAsync<double>("BlazorJSFunctions.GetAudioDuration");
                }
                catch (System.Exception error)
                {
                    // for debugging only
                    DebugHelper.WriteDebugError("GetAudioDuration", "BlazorJSBridge", error);
                }
                
                // return value
                return audioDuration;
            }
            #endregion

            #region IsAudioPlaying(IJSRuntime jsRuntime)
            /// <summary>
            /// method Gets Audio Length
            /// </summary>
            public async static Task<int> IsAudioPlaying(IJSRuntime jsRuntime)
            {
                // set the value
                int isAudioPlaying = 0;
                
                try
                {
                    // set the value
                    isAudioPlaying = await jsRuntime.InvokeAsync<int>("BlazorJSFunctions.IsAudioPlaying");
                }
                catch (System.Exception error)
                {
                    // for debugging only
                    DebugHelper.WriteDebugError("IsAudioPlaying", "BlazorJSBridge", error);
                }
                
                // return value
                return isAudioPlaying;
            }
            #endregion
            
            #region PlayOrPause(IJSRuntime jsRuntime)
            /// <summary>
            /// method Play Or Pause
            /// </summary>
            public async static Task<int> PlayOrPause(IJSRuntime jsRuntime)
            {
                // set the value
                int action = 0;
                
                try
                {
                    // set the value
                    action = await jsRuntime.InvokeAsync<int>("BlazorJSFunctions.PlayOrPause");
                    
                    // return true
                    action = 1;
                }
                catch
                {
                    
                }
                
                // return value
                return action;
            }
            #endregion
            
            #region Prompt(IJSRuntime jsRuntime, string message)
            /// <summary>
            /// method Prompt
            /// </summary>
            public static ValueTask<string> Prompt(IJSRuntime jsRuntime, string message)
            {
                // Implemented in exampleJsInterop.js
                return jsRuntime.InvokeAsync<string>(
                "exampleJsFunctions.showPrompt",
                message);
            }
            #endregion
            
            #region TimeMonitor(IJSRuntime jsRuntime)
            /// <summary>
            /// This method Time Monitor
            /// </summary>
            public static async Task<int> TimeMonitor(IJSRuntime jsRuntime)
            {
                 // set the value
                int action = 0;
                
                try
                {
                    await jsRuntime.InvokeAsync<int>("BlazorJSFunctions.OnTimedUpdate");
                    
                    // return true
                    action = 1;
                }
                catch
                {
                    
                }
                
                // return value
                return action;
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
