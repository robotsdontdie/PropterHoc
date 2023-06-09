using System.Collections;
using PropterHocPluginBase;

namespace PropterHoc
{
    public class EphemeralState : IState
    {
        private readonly Dictionary<string, string> state;

        public EphemeralState(IState other)
        {
            state = new Dictionary<string, string>(other);
        }

        public string this[string key]
        {
            get => state[key];
            set => state[key] = value;
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return state.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)state).GetEnumerator();
        }
    }
}
