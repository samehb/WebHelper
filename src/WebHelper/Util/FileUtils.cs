using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace WebHelper.Util
{
    // We use this class to effieciently allow multiple threads to write to files. We are mainly concerned with multiple threads writing to the same file at the 
    //same time. That is why it is used.
    public static class FileUtils
    {
        private static ReaderWriterLockSlim Lock = new ReaderWriterLockSlim();

        public static void WriteLine(string path, string text)
        {
            try
            {
                Lock.EnterWriteLock();
                File.AppendAllText(path, text);
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        public static void WriteLines(string path, List<string> text)
        {
            try
            {
                Lock.EnterWriteLock();
                File.AppendAllLines(path, text);
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        public static void WriteBytes(string path, byte[] bytes)
        {
            try
            {
                Lock.EnterWriteLock();
                File.WriteAllBytes(path, bytes);
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }
    }
}
