namespace evo.Core.Extensions
{
    public static class IntExtensions
    {
        public static bool Between(this int val, int x, int y)
        {
            if(x <= y)
            {
                return x <= val && val <= y;
            }

            return y <= val && val <= x;
        }
    }
}