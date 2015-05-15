// ***********************************************************************
// Assembly         : WinFormTest
// Author           : libin
// Created          : 03-24-2015
//
// Last Modified By : libin
// Last Modified On : 03-24-2015
// ***********************************************************************
// <copyright file="Form1.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// The WinFormTest namespace.
/// </summary>
namespace WinFormTest
{
    /// <summary>
    /// Class Form1.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            string signaturePicPath = @"E:/" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".jpg";
            Bitmap bmp = new Bitmap(@"bg.jpg");
            CreatPublicSeal.StampSealOnImage(bmp, "温江区指挥党建网络党组织关系转接", "专用章", true);
            //bmp.Save(signaturePicPath);
            this.pictureBox1.Image = bmp;
        }

        /// <summary>
        /// 透明化
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            openFileDialog.InitialDirectory = "E:\\";//注意这里写路径时要用c:\\而不是c:\
            //openFileDialog.Filter = "文本文件|*.*|C#文件|*.cs|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
            }

            if (string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("请选择图片！");
                return;
            }
            Bitmap bt = new Bitmap(fileName);
            bt.MakeTransparent(Color.White);
            bt.Save(fileName, ImageFormat.Png);
        }

        /// <summary>
        /// 印章的中间部分根据图片加载而来.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Image = null;
            // 公章的图片
            string sealImageUrl = string.Empty;
            // 原图片地址
            string sourceUrl = string.Empty;
            string locationWidth = string.Empty;
            string locationHeight = string.Empty;

            Bitmap bmp = CreatPublicSeal.GetSealByLoadCenterPic(@"./seal.png", 30, 30, "深圳市腾讯计算机公司", "财务章", true);
            //bmp.Save(signaturePicPath);
            this.pictureBox1.Image = bmp;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WaterMark.BuildWatermark(@"./bg.jpg", "./WaterMark.png", "水印文字水印文字水印文字", "E:/WaterResult.png");
        }
    }
}
