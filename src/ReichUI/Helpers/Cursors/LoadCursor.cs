using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReichUI.Helpers.Cursors
{
    /// <summary>
    /// Allows you to retrieve a cursor file from embedded resource, supports .ani and .cur.
    /// </summary>
    public class LoadCursor
    {
        [DllImport("user32.dll")]
        private static extern IntPtr LoadCursorFromFile(string str);


        [DllImport("user32.dll")]
        private static extern IntPtr CreateIconFromResource(byte[] presbits, uint dwResSize, bool fIcon, uint dwVer);

        [DllImport("user32.dll")]
        private static extern IntPtr CreateIconFromResourceEx(byte[] pbIconBits, uint cbIconBits, bool fIcon, uint dwVersion,
            int cxDesired, int cyDesired, uint uFlags);


        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyIcon(IntPtr hIcon);



        public static Cursor CreateCursorFromFilePath(string filename)
        {


            IntPtr hCursor = LoadCursorFromFile(filename);

            if (!IntPtr.Zero.Equals(hCursor))
            {
                return new Cursor(hCursor);
            }
            else
            {
                throw new ApplicationException("Could not create cursor from file path" + filename);
            }
        }



        public static Cursor CreateCurFromEmbRc(byte[] resource)
        {
            try
            {
                IntPtr hCursor = CreateIconFromResource(resource, (uint)resource.Length, false, 0x00030000);
                return new Cursor(hCursor);
            }
            catch
            {
                return System.Windows.Forms.Cursors.Default;
            }
            //finally
            //{
            //    DestroyIcon(hCursor); // Probably a bad idea as it would destroy the resource that lays in memory.
            //}
        }


    }
}
