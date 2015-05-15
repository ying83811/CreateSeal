using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace WinFormTest
{
    public class CreatPublicSeal
    {
        Font Var_Font = new Font("Arial", 12, FontStyle.Bold);//定义字符串的字体样式
        //Rectangle rect = new Rectangle(10, 10, 160, 160);//实例化Rectangle类
        /// <summary>
        /// 记录圆的直径，同时也是生成图片的大小
        /// </summary>
        private static int circleDiameter = 118;

        /// <summary>
        /// 设置圆画笔的粗细
        /// </summary>
        private static int circlePanSize = 3;

        /// <summary>
        /// 设置圆的绘制区域
        /// </summary>
        private static Rectangle rect = new Rectangle(circlePanSize, circlePanSize, circleDiameter - circlePanSize * 2, circleDiameter - circlePanSize * 2);//
        private static int _letterspace = -4;//字体间距
        private static Char_Direction _chardirect = Char_Direction.OutSide;
        private static int _degree = 90;
        //字体圆弧所在圆
        /// <summary>
        /// 距离外圈的距离。比外面圆圈小
        /// </summary>
        private static int space = 15;//
        private static Rectangle NewRect = new Rectangle(new Point(rect.X + space, rect.Y + space), new Size(rect.Width - 2 * space, rect.Height - 2 * space));
        /// <summary>
        /// 指定星星的位置和部门文字之间的距离
        /// </summary>
        private static int starAndDeptHeight = 1; // 

        /// <summary>
        /// 创建公司公共印章得到gif图片存储地址
        /// </summary>
        /// <param name="company">公司名字</param>
        /// <param name="department">部门名字</param>
        /// <param name="Url">图片保存路径</param>
        /// <param name="isTransparent">图片保存路径</param>
        public static void CreatSeal(string company, string department, string Url, bool isTransparent = false)
        {
            CreatSeal(company, department, isTransparent).Save(Url);
        }

        /// <summary>
        /// 将印章盖在原图片上
        /// </summary>
        /// <param name="sourceImg">源图片.</param>
        /// <param name="company">公司名字</param>
        /// <param name="department">部门名字</param>
        /// <param name="isTransparent">图片保存路径</param>
        public static void StampSealOnImage(Bitmap sourceImg, string company, string department, bool isTransparent = false)
        {
            Bitmap bmpSeal = CreatPublicSeal.GetSealBitmap(company, department, isTransparent);
            Graphics g = Graphics.FromImage(sourceImg);
            g.DrawImage(bmpSeal, sourceImg.Width - bmpSeal.Width - 30, sourceImg.Height - bmpSeal.Height - 30, bmpSeal.Width, bmpSeal.Height);
            g.Dispose();
        }

        /// <summary>
        /// 读取图片生成印章
        /// </summary>
        /// <param name="centerImgStr">中间图片路径.</param>
        /// <param name="width">指定图片显示宽度.如果≤0，则取图片的宽度</param>
        /// <param name="height">指定图片显示高度.如果≤0，则取图片的宽度</param>
        /// <param name="company">公司名字</param>
        /// <param name="department">部门名字</param>
        /// <param name="isTransparent">图片保存路径</param>
        public static Bitmap GetSealByLoadCenterPic(string centerImgStr, float width, float height, string company, string department, bool isTransparent = false)
        {
            return CreatPublicSeal.CreatSealByLoadCenterPic(centerImgStr, width, height, company, department, true);
        }

        /// <summary>
        /// 创建公司公共印章得到gif图片存储地址
        /// </summary>
        /// <param name="company">公司名字</param>
        /// <param name="department">部门名字</param>
        /// <param name="isTransparent">图片保存路径</param>
        /// <returns></returns>
        public static Bitmap GetSealBitmap(string company, string department, bool isTransparent = false)
        {
            return CreatSeal(company, department, isTransparent);
        }

        /// <summary>
        /// 创建公司公共印章得到gif图片存储地址
        /// 一定要把公司信息放在最后一步进行，不然图像会偏移的厉害
        /// </summary>
        /// <param name="company">公司名字</param>
        /// <param name="department">部门名字</param>
        /// <param name="isTransparent">图片保存路径</param>
        /// <returns></returns>
        private static Bitmap CreatSealByLoadCenterPic(string centerImgStr, float width, float height, string company, string department, bool isTransparent = false)
        {
            Bitmap bMap = new Bitmap(circleDiameter, circleDiameter);//画图初始化
            Graphics g = Graphics.FromImage(bMap);
            g.SmoothingMode = SmoothingMode.AntiAlias;//消除绘制图形的锯齿
            g.Clear(Color.White);//以白色清空panel1控件的背景
            Pen myPen = new Pen(Color.Red, circlePanSize);//设置画笔的颜色
            g.DrawEllipse(myPen, rect); //绘制圆 

            // 绘制中间图片
            Bitmap centerImg = new Bitmap(centerImgStr);
            centerImg.MakeTransparent(Color.White);
            // 保持图片纵横比
            AutoAdjustSize(centerImg, ref width, ref height);
            // 开始绘制中间的图片
            g.DrawImage(centerImg, bMap.Width / 2 - width / 2, bMap.Height / 2 - height / 2, width, height);

            // 绘制部门文字
            string dept_txt = department;
            int dept_len = dept_txt.Length;
            //定义部门字体的字体样式
            Font dept_Font = new Font("Arial", 14 - dept_len * 2, FontStyle.Regular);
            //对指定字符串进行测量
            SizeF deptSize = g.MeasureString(dept_txt, dept_Font);
            //要指定的位置绘制中间文字  ,+下移，-上移          
            PointF dept_Point = new PointF(circleDiameter / 2 - deptSize.Width / 2, circleDiameter / 2 + height / 2 + starAndDeptHeight);
            g.DrawString(dept_txt, dept_Font, myPen.Brush, dept_Point);

            // 绘制公司信息
            string company_txt = company;
            int company_len = company_txt.Length;//获取字符串的长度
            //公司名字
            Font company_Font = new Font("Arial", 28 - company_len, FontStyle.Regular);
            //定义公司名字的字体的样式
            Pen myPenbush = new Pen(Color.Transparent, circlePanSize);
            float[] fCharWidth = new float[company_len];
            float fTotalWidth = ComputeStringLength(company_txt, g, fCharWidth, _letterspace, _chardirect, company_Font);
            // Compute arc's start-angle and end-angle
            double fStartAngle, fSweepAngle;
            fSweepAngle = fTotalWidth * 360 / (NewRect.Width * Math.PI);
            fStartAngle = 270 - fSweepAngle / 2;// 文字的开始位置
            // Compute every character's position and angle
            PointF[] pntChars = new PointF[company_len];
            double[] fCharAngle = new double[company_len];
            // 开始写公司信息
            ComputeCharPos(fCharWidth, pntChars, fCharAngle, fStartAngle);
            for (int i = 0; i < company_len; i++)
            {
                DrawRotatedText(g, company_txt[i].ToString(), (float)(fCharAngle[i] + _degree), pntChars[i], company_Font, myPenbush);
            }

            // 是否透明处理
            if (isTransparent)
            {
                bMap.MakeTransparent(Color.White);
            }

            bMap.Save(@"E:/SealLoad.png", ImageFormat.Png);
            return bMap;
        }

        /// <summary>
        /// 保持图片的纵横比
        /// </summary>
        /// <param name="centerImg">图片.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        private static void AutoAdjustSize(Bitmap centerImg, ref float width, ref float height)
        {
            if (width <= 0 && height > 0)
            {
                // 宽度自适应
                width = centerImg.Width * height / centerImg.Height;
            }
            else if (height <= 0 && width > 0)
            {
                // 高度自适应
                height = centerImg.Height * width / centerImg.Width;
            }
            else if (height <= 0 && width <= 0)
            {
                // 采用原高宽
                width = centerImg.Width;
                height = centerImg.Height;
            }
        }
        /// <summary>
        /// 创建公司公共印章得到gif图片存储地址
        /// </summary>
        /// <param name="company">公司名字</param>
        /// <param name="department">部门名字</param>
        /// <param name="isTransparent">图片保存路径</param>
        /// <returns></returns>
        private static Bitmap CreatSeal(string company, string department, bool isTransparent = false)
        {
            string star_Str = "★";//"☭";
            Bitmap bMap = new Bitmap(circleDiameter, circleDiameter);//画图初始化
            Graphics g = Graphics.FromImage(bMap);
            //Graphics g = this.panel1.CreateGraphics();//实例化Graphics类
            g.SmoothingMode = SmoothingMode.AntiAlias;//消除绘制图形的锯齿
            g.Clear(Color.White);//以白色清空panel1控件的背景
            Pen myPen = new Pen(Color.Red, circlePanSize);//设置画笔的颜色
            g.DrawEllipse(myPen, rect); //绘制圆 

            Font star_Font = new Font("Arial", 30, FontStyle.Regular);//设置星号的字体样式
            SizeF star_Size = g.MeasureString(star_Str, star_Font);//对指定字符串进行测量
            //要指定的位置绘制星号
            PointF star_xy = new PointF(circleDiameter / 2 - star_Size.Width / 2, circleDiameter / 2 - star_Size.Height / 2);
            g.DrawString(star_Str, star_Font, myPen.Brush, star_xy);

            // 绘制公司信息
            string company_txt = company;
            int company_len = company_txt.Length;//获取字符串的长度
            //公司名字的字体的样式
            Font company_Font = new Font("Arial", 28 - company_len, FontStyle.Bold);
            Pen myPenbush = new Pen(Color.Transparent, circlePanSize);

            // 绘制部门文字
            string dept_txt = department;
            int dept_len = dept_txt.Length;
            //定义部门字体的字体样式
            Font Var_Font = new Font("Arial", 14 - dept_len * 2, FontStyle.Regular);
            //对指定字符串进行测量
            SizeF Var_Size = g.MeasureString(dept_txt, Var_Font);
            //要指定的位置绘制中间文字            
            PointF Var_xy = new PointF(circleDiameter / 2 - Var_Size.Width / 2, circleDiameter / 2 + star_Size.Height / 2 - Var_Size.Height / 2 + starAndDeptHeight);
            g.DrawString(dept_txt, Var_Font, myPen.Brush, Var_xy);

            float[] fCharWidth = new float[company_len];
            float fTotalWidth = ComputeStringLength(company_txt, g, fCharWidth, _letterspace, _chardirect, company_Font);
            // Compute arc's start-angle and end-angle
            double fStartAngle, fSweepAngle;
            fSweepAngle = fTotalWidth * 360 / (NewRect.Width * Math.PI);
            fStartAngle = 270 - fSweepAngle / 2;// 文字的开始位置
            // Compute every character's position and angle
            PointF[] pntChars = new PointF[company_len];
            double[] fCharAngle = new double[company_len];
            // 开始写公司信息
            ComputeCharPos(fCharWidth, pntChars, fCharAngle, fStartAngle);
            for (int i = 0; i < company_len; i++)
            {
                DrawRotatedText(g, company_txt[i].ToString(), (float)(fCharAngle[i] + _degree), pntChars[i], company_Font, myPenbush);
            }
            // 是否透明处理
            if (isTransparent)
            {
                bMap.MakeTransparent(Color.White);
            }

            return bMap;
        }
        /// <summary>
        /// 计算字符串总长度和每个字符长度
        /// </summary>
        /// <param name="sText"></param>
        /// <param name="g"></param>
        /// <param name="fCharWidth"></param>
        /// <param name="fIntervalWidth"></param>
        /// <returns></returns>
        private static float ComputeStringLength(string sText, Graphics g, float[] fCharWidth, float fIntervalWidth, Char_Direction Direction, Font text_Font)
        {
            // Init字符串格式
            StringFormat sf = new StringFormat();
            sf.Trimming = StringTrimming.None;
            sf.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap
                | StringFormatFlags.LineLimit;
            // 衡量整个字符串长度
            SizeF size = g.MeasureString(sText, text_Font, (int)text_Font.Style);
            RectangleF rect = new RectangleF(0f, 0f, size.Width, size.Height);
            // 测量每个字符大小
            CharacterRange[] crs = new CharacterRange[sText.Length];
            for (int i = 0; i < sText.Length; i++)
                crs[i] = new CharacterRange(i, 1);
            // 复位字符串格式
            sf.FormatFlags = StringFormatFlags.NoClip;
            sf.SetMeasurableCharacterRanges(crs);
            sf.Alignment = StringAlignment.Near;
            // 得到每一个字符大小
            Region[] regs = g.MeasureCharacterRanges(sText, text_Font, rect, sf);
            // Re-compute whole string length with space interval width
            float fTotalWidth = 0f;
            for (int i = 0; i < regs.Length; i++)
            {
                if (Direction == Char_Direction.Center || Direction == Char_Direction.OutSide)
                    fCharWidth[i] = regs[i].GetBounds(g).Width;
                else
                    fCharWidth[i] = regs[i].GetBounds(g).Height;
                fTotalWidth += fCharWidth[i] + fIntervalWidth;
            }
            fTotalWidth -= fIntervalWidth;//Remove the last interval width
            return fTotalWidth;
        }

        /// <summary>
        /// 求出每个字符的所在的点，以及相对于中心的角度
        ///1．  通过字符长度，求出字符所跨的弧度；
        ///2．  根据字符所跨的弧度，以及字符起始位置，算出字符的中心位置所对应的角度；
        ///3．  由于相对中心的角度已知，根据三角公式很容易算出字符所在弧上的点，如下图所示；
        ///4．  根据字符长度以及间隔距离，算出下一个字符的起始角度；
        ///5．  重复1直至整个字符串结束。
        /// </summary>
        /// <param name="CharWidth"></param>
        /// <param name="recChars"></param>
        /// <param name="CharAngle"></param>
        /// <param name="StartAngle"></param>
        private static void ComputeCharPos(float[] CharWidth, PointF[] recChars, double[] CharAngle, double StartAngle)
        {
            double fSweepAngle, fCircleLength;
            //Compute the circumference
            fCircleLength = NewRect.Width * Math.PI;

            for (int i = 0; i < CharWidth.Length; i++)
            {
                //Get char sweep angle
                fSweepAngle = CharWidth[i] * 360 / fCircleLength;

                //Set point angle
                CharAngle[i] = StartAngle + fSweepAngle / 2;

                //Get char position
                if (CharAngle[i] < 270f)
                    recChars[i] = new PointF(
                        NewRect.X + NewRect.Width / 2
                        - (float)(NewRect.Width / 2 *
                        Math.Sin(Math.Abs(CharAngle[i] - 270) * Math.PI / 180)),
                        NewRect.Y + NewRect.Width / 2
                        - (float)(NewRect.Width / 2 * Math.Cos(
                        Math.Abs(CharAngle[i] - 270) * Math.PI / 180)));
                else
                    recChars[i] = new PointF(
                        NewRect.X + NewRect.Width / 2
                        + (float)(NewRect.Width / 2 *
                        Math.Sin(Math.Abs(CharAngle[i] - 270) * Math.PI / 180)),
                        NewRect.Y + NewRect.Width / 2
                        - (float)(NewRect.Width / 2 * Math.Cos(
                        Math.Abs(CharAngle[i] - 270) * Math.PI / 180)));

                //Get total sweep angle with interval space
                fSweepAngle = (CharWidth[i] + _letterspace) * 360 / fCircleLength;
                StartAngle += fSweepAngle;
            }
        }
        /// <summary>
        /// 绘制每个字符
        /// </summary>
        /// <param name="g"></param>
        /// <param name="_text"></param>
        /// <param name="_angle"></param>
        /// <param name="text_Point"></param>
        /// <param name="text_Font"></param>
        /// <param name="myPen"></param>
        private static void DrawRotatedText(Graphics g, string _text, float _angle, PointF text_Point, Font text_Font, Pen myPen)
        {
            // Init format
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            // Create graphics path
            GraphicsPath gp = new GraphicsPath(System.Drawing.Drawing2D.FillMode.Winding);
            int x = (int)text_Point.X;
            int y = (int)text_Point.Y;

            // Add string
            gp.AddString(_text, text_Font.FontFamily, (int)text_Font.Style, text_Font.Size, new Point(x, y), sf);

            // Rotate string and draw it
            Matrix m = new Matrix();
            m.RotateAt(_angle, new PointF(x, y));
            g.Transform = m;
            g.DrawPath(myPen, gp);
            g.FillPath(new SolidBrush(Color.Red), gp);
        }

        public enum Char_Direction
        {
            Center = 0,
            OutSide = 1,
            ClockWise = 2,
            AntiClockWise = 3,
        }
    }
}
