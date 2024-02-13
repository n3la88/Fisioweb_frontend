using com.sun.org.apache.xml.@internal.resolver.helpers;
using com.sun.xml.@internal.fastinfoset.util;

namespace FisioWebFront.Class
{
    public class ClaveAleatoria
    {
        public string resultString;
        public void ContrasenaRandom()
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var charArray = new char[8];
            var random = new Random();
            for (int i = 0; i < charArray.Length; i++)
            {
                charArray[i] = characters[random.Next(characters.Length)];
            }
            resultString = new String(charArray);
            Console.WriteLine(resultString);
        }
    }
}
