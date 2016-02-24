using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Snippets.Controllers
{
    public class WebsiteThumbnail
    {
        public string _url;
        public int _width, _height;
        public int _thumbWidth, _thumbHeight;
        public Bitmap _bmp;


        //public static Bitmap GetThumbnail(string url, int width, int height,int thumbWidth, int thumbHeight)
        //{
        //    WebsiteThumbnail thumbnail = new WebsiteThumbnail(url, width, height, thumbWidth, thumbHeight);
            
        //    return thumbnail.GetThumbnail();
        //}

        public WebsiteThumbnail(string url, int width, int height,int thumbWidth, int thumbHeight)
        {
            _url = url;
            _width = width;
            _height = height;
            _thumbWidth = thumbWidth;
            _thumbHeight = thumbHeight;
        }

        public Bitmap GetThumbnail()
        {
            // WebBrowser is an ActiveX control that must be run in a
            // single-threaded apartment so create a thread to create the
            // control and generate the thumbnail
            Thread thread = new Thread(new ThreadStart(GetThumbnailWorker));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            return _bmp.GetThumbnailImage(_thumbWidth, _thumbHeight, null, IntPtr.Zero) as Bitmap;
        }

        public void GetThumbnailWorker()
        {
            using (WebBrowser browser = new WebBrowser())
            {
                browser.ClientSize = new Size(_width, _height);
                browser.ScrollBarsEnabled = false;
                browser.ScriptErrorsSuppressed = true;
                browser.Navigate(_url);

                // Wait for control to load page
                while (browser.ReadyState != WebBrowserReadyState.Complete)
                    Application.DoEvents();

                // Render browser content to bitmap
                _bmp = new Bitmap(_width, _height);
                browser.DrawToBitmap(_bmp, new Rectangle(0, 0, _width, _height));
            }
        }

    }
}