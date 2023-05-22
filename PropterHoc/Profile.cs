using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace PropterHoc
{
    public class Profile
    {
        public Profile(string profileName, ProfileSettings profileSettings, PersistedState persistedState)
        {
            ProfileName = profileName;
            ProfileSettings = profileSettings;
            PersistedState = persistedState;
        }

        public string ProfileName { get; }

        public ProfileSettings ProfileSettings { get; }

        // list of snapshots
        // list of sessions
        // state from state.json
        public PersistedState PersistedState { get; }

        public string ProfileDir => Path.Combine(
            ProfileSettings.RootSettings.ProfilesDir,
            ProfileName);

        public string GetCommandDir(string commandId)
        {
            return Path.Combine(ProfileDir, "Commands", commandId);
        }

        public string GetStepDir(string stepId)
        {
            return Path.Combine(ProfileDir, "Steps", stepId);
        }
    }
}
