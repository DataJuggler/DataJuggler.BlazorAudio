// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API

window.BlazorJSFunctions =
{
    GetCurrentTime: function()
    {
        // inititial value
        var returnValue = 0;

        // get the audioPlayer
        var audioPlayer = document.getElementById("audioPlayer"); 

        try
        {  
            // set the returnVaalue
            returnValue = audioPlayer.currentTime;
        }
        catch (err)
        {
            // return a negative value
            returnValue = -1;
        }

        // return the returnValue
        return returnValue;
    },
    IsAudioPlaying: function()
    {
        // inititial value
        var returnValue = 0;

        try
        {
            // get the audioPlayer
            var audioPlayer = document.getElementById("audioPlayer");

            // is the audio currently playing
            var isAudioPlaying = audio => !!(audioPlayer.currentTime > 0 && !audioPlayer.paused && !audioPlayer.ended && audioPlayer.readyState > 2);

            if (isAudioPlaying)
            {
                // set the returnVaalue
                returnValue = 1;
            }
            else
            {
                // set the returnVaalue
                returnValue = 0;
            }
        }
        catch (err) {
            // return a negative value
            returnValue = -1;
        }

        // return the returnValue
        return returnValue;
    },
    GetAudioDuration: function ()
    {
        // inititial value
        var returnValue = 0;

        // get the audioPlayer
        var audioPlayer = document.getElementById("audioPlayer");

        try
        {
            // set the returnVaalue
            returnValue = audioPlayer.duration;
        }
        catch (err)
        {
            // return a negative value
            returnValue = -1;
        }

        // return the returnValue
        return returnValue;
    },
    OnTimedUpdate: function()
    {
        // initial value
        var returnValue = 0;

        // get the audioPlayer
        var audioPlayer = document.getElementById("audioPlayer"); 

        audioPlayer.ontimeupdate = function () { hookAudio() };

        // return value
        return returnValue;

        function hookAudio()
        {
            var timestamp = Math.round(audioPlayer.currentTime);

            // get hours minutes and seconds
            var hours = Math.floor(timestamp / 3600);            
            var minutes = Math.floor(timestamp / 60) - (hours * 60);
            var seconds = Math.floor(timestamp % 60);

            // get the formatted time
            var formatted = hours.toString().padStart(2, '0') + ':' + minutes.toString().padStart(2, '0') + ':' + seconds.toString().padStart(2, '0');

            // if only seconds
            if (formatted.startsWith("00:00"))
            {
                // start after the 3 zeros
                formatted = formatted.substring(4);
            }
            else if (formatted.startsWith("00:0"))
            {
                // start after the 3 zeros
                formatted = formatted.substring(4);
            }
            else if (formatted.startsWith("00:"))
            {
                // now replace out any unneeded trailing 0's
                formatted = formatted.substring(3);
            }
            
            // Display the current position of the audio in a label element with id="audioTime"
            document.getElementById("audioTime").innerHTML = formatted;
        }
    },
    ShowPrompt: function (message)
    {
        return prompt(message, 'Message From Webpage');
    },
    PlayOrPause: function (text)
    {
        // original value
        var returnValue = 0;

        // get the audioPlayer
        var audioPlayer = document.getElementById("audioPlayer"); 

            try {
                if (audioPlayer.paused) {
                    audioPlayer.play();
                }
                else {
                    audioPlayer.pause()
                }

                // set to 1;
                returnValue = 1;
            }
            catch (err) {
                returnValue = -2;
            }

        // return value
        return returnValue;
    }
};