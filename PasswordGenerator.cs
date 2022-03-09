using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordGeneratorWPF
{
    public class PasswordGenerator
    {
        public PasswordGenerator()
        {
            this.length = defaultLength;
            this.hasLowercase = true;
            this.hasUppercase = true;
            this.hasNumbers = true;
            this.hasSymbols = true;
            this.hasRepeatings = true;
            this.hasSimilar = false;
            this.rnd = new Random();
        }
        public PasswordGenerator (int length, bool hasLowercase, bool hasUppercase, bool hasNumbers, bool hasSymbols, bool hasRepeatings, bool hasSimilar)
        {
            this.length = length;
            this.hasLowercase = hasLowercase;
            this.hasUppercase = hasUppercase;
            this.hasNumbers = hasNumbers;
            this.hasSymbols = hasSymbols;
            this.hasRepeatings = hasRepeatings;
            this.hasSimilar = hasSimilar;
            this.rnd = new Random();

        }

        protected char GetRandomChar(char[] charArray)
        {
            int randomPosition = rnd.Next(0, charArray.Length);
            char randomChar = charArray[randomPosition];
            return randomChar;
        }

        public string Generate()
        {
            StringBuilder password = new StringBuilder();
            StringBuilder charsSB = new StringBuilder();
            password.Capacity = this.length;
            char currentChar;
            currentChar = '\n';
            if (this.hasLowercase)
            {
                charsSB.Append(lowercaseChaArray);
            }
            if (this.hasUppercase)
            {
                charsSB.Append(uppercaseCharArray);
            }
            if (this.hasNumbers)
            {
                charsSB.Append(numbersCharArray);
            }
            if (this.hasSymbols)
            {
                charsSB.Append(symbolsCharArray);
            }
            string chars = charsSB.ToString();
            if (!this.hasSimilar)
            {
                foreach (char c in similarCharArray)
                {
                    chars = chars.Replace(c.ToString(), string.Empty);
                }
            }
            if ((!hasRepeatings)&&(chars.Length < this.length)||(chars.Length == 0))
            {
                throw new ArgumentException("There are not enough characters to create a password.\nTry to use more character types or use repeating characters.");
            }
            try
            {
                for (int i = 0; i < this.length; i++)
                {
                    currentChar = GetRandomChar(chars.ToCharArray());
                    if (!this.hasRepeatings)
                    {
                        chars = chars.Replace(currentChar.ToString(), string.Empty);
                    }
                    password.Append(currentChar);
                }
            }
            catch { };
            if (password != null)
            {
                return password.ToString();
            }
            else
            {
                return string.Empty;
            }

        }

        public int PasswordLength
        {
            get { return this.length; }
            set { this.length = value; }
        }

        public bool Lowercase
        {
            get { return this.hasLowercase; }
            set { this.hasLowercase = value; }
        }

        public bool Uppercase
        {
            get { return this.hasUppercase; }
            set { this.hasUppercase = value; }
        }

        public bool HasNumbers
        {
            get { return this.hasNumbers; }
            set { this.hasNumbers = value; }
        }

        public bool Symbols
        {
            get { return this.hasSymbols; }
            set { this.hasSymbols = value; }
        }

        public bool Reapeatings
        {
            get { return this.hasRepeatings; }
            set { this.hasRepeatings = value; }
        }

        public bool Similar
        {
            get { return this.hasSimilar; }
            set { this.hasSimilar = value; }
        }

        private const int defaultLength = 8;
        private int length;
        private bool hasLowercase;
        private bool hasUppercase;
        private bool hasNumbers;
        private bool hasSymbols;
        private bool hasRepeatings;
        private bool hasSimilar;
        private char[] lowercaseChaArray = @"abcdefghijklmnopqrstuvwxyz".ToCharArray();
        private char[] uppercaseCharArray = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private char[] numbersCharArray = @"1234567890".ToCharArray();
        private char[] symbolsCharArray = @"~`!@#$%^&*()_-+={[}]|\:;'<,>.?/".ToCharArray();
        private char[] similarCharArray = @"0Oo1Il5S".ToCharArray();
        private Random rnd = new Random();
    }
}
