using System;

namespace HabboBOT.Core
{
    internal class Account
    {
        public string Email { get; }
        public string Password { get; }

        public Account(string line)
        {
            string[] split = line.Split(new char[] { ':', ':', ';', '-' }, StringSplitOptions.RemoveEmptyEntries);

            if (split.Length != 2) return;

            Email = split[0];
            Password = split[1];
        }

        public bool IsValid => Email != null && Password != null;
    }
}