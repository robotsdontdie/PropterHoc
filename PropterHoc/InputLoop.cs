using System;

namespace PropterHoc
{
    public class InputLoop
    {
        private Session session;

        public InputLoop(Session session)
        {
            this.session = session;
        }

        public void DoLoop()
        {
            while (true)
            {
                string line = Prompt();
                string[] tokens = Tokenize(line);

                if (session.ExecuteCommand(tokens))
                {
                    break;
                }
            }
        }

        private string Prompt()
        {
            Console.Write($"{session.Profile.ProfileName}>");
            return Console.ReadLine() ?? "";
        }

        private string[] Tokenize(string? line)
        {
            return line?.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                ?? Array.Empty<string>();
        }
    }
}