This project is designed to play audio for a Blazor project.

# Updates

6.16.2026: This project has been updated for .NET 10.
Also some new properties to make this applear invisible

    <BlazorAudioPlayer Name="AudioPlayer" Parent="this" Visible="false"></BlazorAudioPlayer>

New method: Set ImageUrl 

    // Change Audio
	AudioPlayer.SetImageUrl(path);

I am using this to play sounds for a game. This is all new code so use at your own risk for now.
Let me know if it works.


