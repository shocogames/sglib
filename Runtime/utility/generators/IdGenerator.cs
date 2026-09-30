using System.Threading;

namespace SGLib.Utility.Generators
{
    public static class IdGenerator
    {
        private static int currentId = -1;

        public static int GenerateId()
        {
            return Interlocked.Increment(ref currentId);
        }
    }
}