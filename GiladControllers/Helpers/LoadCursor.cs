using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiladControllers.Helpers
{
    /// <summary>
    /// Allows you to retrieve a cursor file from embedded resource, supports .ani and .cur.
    /// </summary>
    public class LoadCursor
    {
        [DllImport("User32.dll")]
        private static extern IntPtr LoadCursorFromFile(String str);


        [DllImport("user32.dll")]
        private static extern IntPtr CreateIconFromResource(byte[] presbits, uint dwResSize, bool fIcon, uint dwVer);


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
            IntPtr customCursor = CreateIconFromResource(resource, (uint)resource.Length, false, 0x00030000);


            if (!IntPtr.Zero.Equals(customCursor))
            {
                return new Cursor(customCursor);
            }
            else
            {
                throw new ApplicationException("Could not create cursor from Embedded resource ");
            }
        }
    }
}
