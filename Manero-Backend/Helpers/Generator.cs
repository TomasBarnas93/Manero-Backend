namespace Manero_Backend.Helpers
{
    public class Generator
    {
        public static string CodeGenerator(int length)
        {
            string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            Random r = new Random();
            string code = "";
            for (int i = 0; i < length; i++)
            {
                int pos = r.Next(0, chars.Length);

                code += chars[pos];
            }

            return code;
        }
    }
}
