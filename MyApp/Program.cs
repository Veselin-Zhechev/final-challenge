using System.Text.RegularExpressions;

namespace _05.NetherRealms
{
    internal class Program
    {
        static void Main()
        {
            string healthPattern = @"[^\+\-\*\/\.,0-9]";
            string splitPattern = @"[,\s]+";
            string damagePattern = @"-?\d+\.?\d*";
            string multiplyingOrDividingPattern = @"[\*\/]";

            string input = Console.ReadLine();
            string[] demonNames = Regex.Split(input, splitPattern).OrderBy(s => s).ToArray();

            SortedDictionary<string, Dictionary<int, decimal>> demonsNamesAndAtributes = new();

            for (int i = 0; i < demonNames.Length; i++)
            {
                //Name

                string currentName = demonNames[i];

                //Health

                int health = 0;

                MatchCollection currentDemonHealth = Regex.Matches(demonNames[i], healthPattern);

                char[] demonHealthDigits = string.Join("", currentDemonHealth).ToCharArray();

                foreach (char ch in demonHealthDigits)
                {
                    health += ch;
                }

                //Damage

                MatchCollection currentDemonDamage = Regex.Matches(demonNames[i], damagePattern);

                decimal damage = 0;

                damage = currentDemonDamage.Select(digit => decimal.Parse((digit).ToString())).ToList().Sum();

                MatchCollection additionalOperationsData = Regex.Matches(demonNames[i], multiplyingOrDividingPattern);
                char[] additionalOperationsChars = string.Join("", additionalOperationsData).ToCharArray();

                foreach (char ch in additionalOperationsChars)
                {
                    if (ch == '*')
                    {
                        damage *= 2;
                    }
                    else
                    {
                        damage /= (decimal)2;
                    }
                }

                Console.WriteLine($"{currentName} - {health} health, {damage:F2} damage");
            }

        }
    }
}

