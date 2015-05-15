using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class SealImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Bitmap image = null;
            try
            {
                image = WinFormTest.CreatPublicSeal.GetSealBitmap("温江区指挥党建网络党组织关系转接", "专用章", true);  
                System.Drawing.Image imageNew = image;                
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                imageNew.Save(stream, ImageFormat.Png);
                Response.Cache.SetNoStore();
                Response.ClearContent();
                Response.ContentType = "image/png";
                Response.BinaryWrite(stream.ToArray());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (image != null)
                {
                    image.Dispose();
                }                
            }
        }
    }
}