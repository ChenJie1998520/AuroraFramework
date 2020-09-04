using AuroraFramework.Controls;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace AuroraFramework.Drawing
{
    /// <summary>
    /// 封装一个 GDI+ 绘图图面
    /// </summary>
    internal sealed class AuroraGraphics
    {
        /// <summary>
        /// GDI+ 绘图图面
        /// </summary>
        private readonly Graphics graphics;

        /// <summary>
        /// 初始化<see cref="AuroraFramework.Drawing.AuroraGraphics"/>结构的新实例
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        internal AuroraGraphics(Graphics graphics)
        {
            if (graphics != null)
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingQuality = CompositingQuality.HighQuality;

                this.graphics = graphics;
            }
        }

        /// <summary>
        /// 绘制简单图片(平铺绘制)
        /// </summary>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="img">图片</param>
        public void DrawImage(Rectangle rect, Image img)
        {
            this.DrawImage(rect, img, 1f);
        }

        /// <summary>
        /// 按照指定区域绘制指定大小的图片
        /// </summary>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="img">图片</param>
        /// <param name="imgSize">图片尺寸</param>
        public void DrawImage(Rectangle rect, Image img, Size imgSize)
        {
            if (graphics == null || img == null) return;

            int x = rect.X + rect.Width / 2 - imgSize.Width / 2, y = rect.Y;
            Rectangle imageRect = new Rectangle(x, y + rect.Height / 2 - imgSize.Height / 2, imgSize.Width, imgSize.Height);

            this.DrawImage(imageRect, img, 1f);
        }

        /// <summary>
        /// 在指定区域绘制图片(可设置图片透明度) (平铺绘制)
        /// </summary>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="img">图片</param>
        /// <param name="opacity">图片透明度</param>
        public void DrawImage(Rectangle rect, Image img, float opacity)
        {
            if (opacity <= 0) return;

            float[][] nArray ={
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, opacity >= 1 ? 1 : opacity, 0},
                new float[] {0, 0, 0, 0, 1}
            };
            ColorMatrix matrix = new ColorMatrix(nArray);

            using (ImageAttributes imgAttributes = new ImageAttributes())
            {
                imgAttributes.SetWrapMode(WrapMode.TileFlipXY); 
                imgAttributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                Rectangle imageRect = new Rectangle(rect.X, rect.Y + (rect.Height - img.Size.Height) / 2, img.Size.Width, img.Size.Height);
                this.graphics.DrawImage(img, rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttributes);
            }
        }

        /// <summary>
        /// 使用单色渲染矩形内部区域
        /// </summary>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="color">颜色</param>
        public void FillRectangle(Rectangle rect, Color color)
        {
            if (rect.Width <= 0 || rect.Height <= 0 || this.graphics == null)
            {
                return;
            }

            using (Brush brush = new SolidBrush(color))
            {
                this.graphics.FillRectangle(brush, rect);
            }
        }

        /// <summary>
        /// 使用渐变色渲染矩形内部区域
        /// </summary>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="gradientColor">渐变颜色</param>
        public void FillRectangle(Rectangle rect, AuroraGradientColor gradientColor)
        {
            if (rect.Width <= 0 || rect.Height <= 0 || this.graphics == null)
            {
                return;
            }

            using (LinearGradientBrush brush = new LinearGradientBrush(rect, gradientColor.FromColor, gradientColor.ToColor, gradientColor.GradientMode))
            {
                brush.Blend.Factors = gradientColor.Factors;
                brush.Blend.Positions = gradientColor.Positions;
                this.graphics.FillRectangle(brush, rect);
            }
        }

        /// <summary>
        ///绘制椭圆的边框
        /// </summary>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="color">颜色</param>
        /// <param name="borderWidth">边框宽度</param>
        public void DrawEllipseBorder(Rectangle rect, Color color, int borderWidth)
        {
            using (Pen pen = new Pen(color, borderWidth))
            {
                this.graphics.DrawEllipse(pen, rect);
            }
        }

        /// <summary>
        /// 渲染一个圆形区域(简单渲染)
        /// </summary>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="color">颜色</param>
        public void FillEllipse(Rectangle rect, Color color)
        {
            using (SolidBrush brush = new SolidBrush(color))
            {
                this.graphics.FillEllipse(brush, rect);
            }
        }

        /// <summary>
        /// 渲染一个圆形区域(高级渲染)
        /// </summary>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="centerColor">渐变中心颜色</param>
        /// <param name="surroundColors">填充的路径中的点相对应的颜色</param>
        public void FillEllipse(Rectangle rect, Color centerColor, Color surroundColors)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(rect);
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = centerColor;
                    brush.SurroundColors = new Color[] { surroundColors };
                    Blend blend = new Blend
                    {
                        Factors = new float[] { 0f, 0.8f, 1f },
                        Positions = new float[] { 0f, 0.5f, 1f }
                    };
                    brush.Blend = blend;
                    this.graphics.FillPath(brush, path);
                }
            }
        }

        /// <summary>
        /// 初始化<see cref="System.Drawing.Graphics"/>对象为高质量的绘制
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        public static void InitializeGraphics(Graphics graphics)
        {
            if (graphics != null)
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
            }
        }

        #region 设置图片透明度(SetImageOpacity)
        /// <summary>
        /// 设置图片透明度
        /// </summary>
        /// <param name="imgAttributes">包含有关在呈现时如何操作位图和图元文件颜色的信息</param>
        /// <param name="opacity">透明度 0:完全透明 1:不透明</param>
        public static void SetImageOpacity(ImageAttributes imgAttributes, float opacity)
        {
            float[][] nArray ={
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, opacity, 0},
                new float[] {0, 0, 0, 0, 1}
            };
            ColorMatrix matrix = new ColorMatrix(nArray);
            imgAttributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        }
        #endregion

        #region 绘制图片(DrawImage)
        /// <summary>
        /// 在指定区域绘制图片(可设置图片透明度) (平铺绘制)
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="img">图片</param>
        /// <param name="opacity">图片透明度</param>
        public static void DrawImage(Graphics graphics, Rectangle rect, Image img, float opacity)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (opacity <= 0)
            {
                return;
            }

            using (ImageAttributes imgAttributes = new ImageAttributes())
            {
                imgAttributes.SetWrapMode(WrapMode.TileFlipXY);
                SetImageOpacity(imgAttributes, opacity >= 1 ? 1 : opacity);
                Rectangle imageRect = new Rectangle(rect.X, rect.Y + (rect.Height - img.Size.Height) / 2, img.Size.Width, img.Size.Height);
                graphics.DrawImage(img, rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttributes);
            }
        }

        /// <summary>
        /// 绘制简单图片(平铺绘制)
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="img">图片</param>
        public static void DrawImage(Graphics graphics, Rectangle rect, Image img)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (ImageAttributes imgAttributes = new ImageAttributes())
            {
                imgAttributes.SetWrapMode(WrapMode.TileFlipXY);
                Rectangle imageRect = new Rectangle(rect.X, rect.Y + (rect.Height - img.Size.Height) / 2, img.Size.Width, img.Size.Height);
                graphics.DrawImage(img, rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttributes);
            }
        }

        /// <summary>
        /// 按照指定区域绘制指定大小的图片
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="img">图片</param>
        /// <param name="imgSize">图片尺寸</param>
        public static void DrawImage(Graphics graphics, Rectangle rect, Image img, Size imgSize)
        {
            if (graphics == null || img == null)
            {
                return;
            }

            int x = rect.X + rect.Width / 2 - imgSize.Width / 2, y = rect.Y;
            Rectangle imageRect = new Rectangle(x, y + rect.Height / 2 - imgSize.Height / 2, imgSize.Width, imgSize.Height);
            graphics.DrawImage(img, imageRect);
        }
        #endregion

        #region 渲染矩形内部区域(FillRectangle)

        #region 单色渲染
        /// <summary>
        /// 使用单色渲染矩形内部区域
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="color">颜色</param>
        public static void FillRectangle(Graphics graphics, Rectangle rect, Color color)
        {
            if (rect.Width <= 0 || rect.Height <= 0 || graphics == null)
            {
                return;
            }

            using (Brush brush = new SolidBrush(color))
            {
                graphics.FillRectangle(brush, rect);
            }
        }
        #endregion

        #region 渐变色渲染
        /// <summary>
        /// 使用渐变色渲染矩形内部区域
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="gradientColor">渐变颜色</param>
        public static void FillRectangle(Graphics graphics, Rectangle rect, AuroraGradientColor gradientColor)
        {
            if (rect.Width <= 0 || rect.Height <= 0 || graphics == null)
            {
                return;
            }

            using (LinearGradientBrush brush = new LinearGradientBrush(rect, gradientColor.FromColor, gradientColor.ToColor, gradientColor.GradientMode))
            {
                brush.Blend.Factors = gradientColor.Factors;
                brush.Blend.Positions = gradientColor.Positions;
                graphics.FillRectangle(brush, rect);
            }
        }
        #endregion  

        #endregion

        #region 渲染GraphicsPath内部区域(FillPath)

        #region 单色渲染
        /// <summary>
        /// 使用默认颜色(白色)渲染GraphicsPath内部区域
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="graphicsPath">表示一系列相互连接的直线和曲线</param>
        public static void FillPath(Graphics graphics, GraphicsPath graphicsPath)
        {
            FillPath(graphics, graphicsPath, Color.White);
        }

        /// <summary>
        /// 使用单色渲染GraphicsPath内部区域
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="graphicsPath">表示一系列相互连接的直线和曲线</param>
        /// <param name="brushColor">边框颜色</param>
        public static void FillPath(Graphics graphics, GraphicsPath graphicsPath, Color brushColor)
        {
            if (graphicsPath == null || graphics == null)
            {
                return;
            }

            using (Brush brush = new SolidBrush(brushColor))
            {
                graphics.FillPath(brush, graphicsPath);
            }
        }
        #endregion

        #region 渐变色渲染
        /// <summary>
        /// 使用渐变色渲染GraphicsPath内部区域
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="graphicsPath">表示一系列相互连接的直线和曲线</param>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="fColor">起始颜色</param>
        /// <param name="tColor">结束颜色</param>
        public static void FillPath(Graphics graphics, GraphicsPath graphicsPath, Rectangle rect, Color fColor, Color tColor)
        {
            FillPath(graphics, graphicsPath, rect, fColor, tColor, null, LinearGradientMode.Vertical);
        }

        /// <summary>
        /// 使用渐变色渲染GraphicsPath内部区域
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="graphicsPath">表示一系列相互连接的直线和曲线</param>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="fColor">起始颜色</param>
        /// <param name="tColor">结束颜色</param>
        /// <param name="blend">色彩混合渲染方案</param>
        public static void FillPath(Graphics graphics, GraphicsPath graphicsPath, Rectangle rect, Color fColor, Color tColor, Blend blend)
        {
            FillPath(graphics, graphicsPath, rect, fColor, tColor, blend, LinearGradientMode.Vertical);
        }

        /// <summary>
        /// 使用渐变色渲染GraphicsPath内部区域
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="graphicsPath">表示一系列相互连接的直线和曲线</param>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="fColor">起始颜色</param>
        /// <param name="tColor">结束颜色</param>
        /// <param name="gradientMode">线性渐变模式</param>
        public static void FillPath(Graphics graphics, GraphicsPath graphicsPath, Rectangle rect, Color fColor, Color tColor, LinearGradientMode gradientMode)
        {
            FillPath(graphics, graphicsPath, rect, fColor, tColor, null, gradientMode);
        }

        /// <summary>
        /// 使用渐变色渲染GraphicsPath内部区域
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="graphicsPath">表示一系列相互连接的直线和曲线</param>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="fColor">起始颜色</param>
        /// <param name="tColor">结束颜色</param>
        /// <param name="blend">色彩混合渲染方案</param>
        /// <param name="gradientMode">线性渐变模式</param>
        public static void FillPath(Graphics graphics, GraphicsPath graphicsPath, Rectangle rect, Color fColor, Color tColor, Blend blend, LinearGradientMode gradientMode)
        {
            AuroraGradientColor gradientColor = new AuroraGradientColor(fColor, tColor, gradientMode, blend);
            FillPath(graphics, graphicsPath, rect, gradientColor);
        }

        /// <summary>
        /// 使用渐变色渲染GraphicsPath内部区域
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="graphicsPath">表示一系列相互连接的直线和曲线</param>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="gradientColor">渐变颜色</param>
        public static void FillPath(Graphics graphics, GraphicsPath graphicsPath, Rectangle rect, AuroraGradientColor gradientColor)
        {
            if (graphicsPath == null || graphics == null || rect.Width <= 0 || rect.Height <= 0)
            {
                return;
            }

            using (LinearGradientBrush brush = new LinearGradientBrush(rect, gradientColor.FromColor, gradientColor.ToColor, gradientColor.GradientMode))
            {
                brush.Blend.Factors = gradientColor.Factors ?? (new float[] { });
                brush.Blend.Positions = gradientColor.Positions ?? (new float[] { });
                graphics.FillPath(brush, graphicsPath);
            }
        }

        #endregion

        #endregion

        #region 绘制椭圆(Ellipse Render)
        /// <summary>
        ///绘制椭圆的边框
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="color">颜色</param>
        /// <param name="borderWidth">边框宽度</param>
        public static void DrawEllipseBorder(Graphics graphics, Rectangle rect, Color color, int borderWidth)
        {
            using (Pen pen = new Pen(color, borderWidth))
            {
                graphics.DrawEllipse(pen, rect);
            }
        }

        /// <summary>
        /// 渲染一个圆形区域(简单渲染)
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="color">颜色</param>
        public static void FillEllipse(Graphics graphics, Rectangle rect, Color color)
        {
            using (SolidBrush brush = new SolidBrush(color))
            {
                graphics.FillEllipse(brush, rect);
            }
        }

        /// <summary>
        /// 渲染一个圆形区域(高级渲染)
        /// </summary>
        /// <param name="g">GDI+ 绘图图面</param>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="centerColor">渐变中心颜色</param>
        /// <param name="surroundColors">填充的路径中的点相对应的颜色</param>
        public static void FillEllipse(Graphics g, Rectangle rect, Color centerColor, Color surroundColors)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(rect);
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = centerColor;
                    brush.SurroundColors = new Color[] { surroundColors };
                    Blend blend = new Blend
                    {
                        Factors = new float[] { 0f, 0.8f, 1f },
                        Positions = new float[] { 0f, 0.5f, 1f }
                    };
                    brush.Blend = blend;
                    g.FillPath(brush, path);
                }
            }
        }
        #endregion

        #region 绘制边框(DrawBorder)
        /// <summary>
        /// 绘制边框
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        public static void DrawBorder(Graphics graphics, Rectangle rect)
        {
            DrawBorder(graphics, rect, AuroraRoundStyle.None, 0, Color.LightGray);
        }

        /// <summary>
        /// 绘制边框
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        public static void DrawBorder(Graphics graphics, Rectangle rect, Color borderColor)
        {
            DrawBorder(graphics, rect, AuroraRoundStyle.None, 0, borderColor);
        }

        /// <summary>
        /// 绘制边框
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="radius">圆角弧度大小</param>
        public static void DrawBorder(Graphics graphics, Rectangle rect, int radius)
        {
            DrawBorder(graphics, rect, AuroraRoundStyle.All, radius, Color.LightGray);
        }

        /// <summary>
        /// 绘制边框
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="radius">圆角弧度大小</param>
        /// <param name="borderColor">边框颜色</param>
        public static void DrawBorder(Graphics graphics, Rectangle rect, int radius, Color borderColor)
        {
            DrawBorder(graphics, rect, AuroraRoundStyle.All, radius, borderColor);
        }

        /// <summary>
        /// 绘制边框
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="roundStyle">圆角的样式</param>
        /// <param name="radius">圆角弧度大小</param>
        public static void DrawBorder(Graphics graphics, Rectangle rect, AuroraRoundStyle roundStyle, int radius)
        {
            DrawBorder(graphics, rect, roundStyle, radius, Color.LightGray);
        }

        /// <summary>
        /// 绘制边框
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="roundStyle">圆角的样式</param>
        /// <param name="radius">圆角弧度大小</param>
        /// <param name="borderColor">边框颜色</param>
        public static void DrawBorder(Graphics graphics, Rectangle rect, AuroraRoundStyle roundStyle, int radius, Color borderColor)
        {
            if (rect.Width <= 0 || rect.Height <= 0 || graphics == null)
            {
                return;
            }

            rect.Width -= 2;
            rect.Height -= 2;
            using (GraphicsPath path = CreateGraphicsPath(rect, radius, roundStyle, false))
            {
                using (Pen pen = new Pen(borderColor))
                {
                    graphics.DrawPath(pen, path);
                }
            }
        }
        #endregion

        #region 绘制水晶Logo(DrawCrystalLogo)
        /// <summary>
        /// 绘制水晶原形Logo
        /// </summary>
        /// <param name="graphics">GDI+ 绘图图面</param>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="surroundColor">环绕颜色</param>
        /// <param name="centerColor">中心颜色</param>
        /// <param name="lightColor">灯光的颜色</param>
        /// <param name="blend">混合图案</param>
        public static void DrawCrystalLogo(Graphics graphics, Rectangle rect, Color surroundColor, Color centerColor, Color lightColor, Blend blend)
        {
            int sweep, start;
            Point p1, p2, p3;
            Rectangle rinner = rect;
            rinner.Inflate(-1, -1);
            using (GraphicsPath p = new GraphicsPath())
            {
                p.AddEllipse(rect);

                using (PathGradientBrush gradient = new PathGradientBrush(p))
                {
                    gradient.WrapMode = WrapMode.Clamp;
                    gradient.CenterPoint = new PointF(Convert.ToSingle(rect.Left + rect.Width / 2), Convert.ToSingle(rect.Bottom));
                    gradient.CenterColor = centerColor;
                    gradient.SurroundColors = new Color[] { surroundColor };
                    gradient.Blend = blend;
                    graphics.FillPath(gradient, p);
                }
            }

            // Bottom round shine
            Rectangle bshine = new Rectangle(0, 0, rect.Width / 2, rect.Height / 2);
            bshine.X = rect.X + (rect.Width - bshine.Width) / 2;
            bshine.Y = rect.Y + rect.Height / 2;

            using (GraphicsPath p = new GraphicsPath())
            {
                p.AddEllipse(bshine);

                using (PathGradientBrush gradient = new PathGradientBrush(p))
                {
                    gradient.WrapMode = WrapMode.Clamp;
                    gradient.CenterPoint = new PointF(Convert.ToSingle(rect.Left + rect.Width / 2), Convert.ToSingle(rect.Bottom));
                    gradient.CenterColor = Color.White;
                    gradient.SurroundColors = new Color[] { Color.Transparent };

                    graphics.FillPath(gradient, p);
                }
            }

            // Upper Glossy
            using (GraphicsPath p = new GraphicsPath())
            {
                sweep = 160;
                start = 180 + (180 - sweep) / 2;
                p.AddArc(rinner, start, sweep);

                p1 = Point.Round(p.PathData.Points[0]);
                p2 = Point.Round(p.PathData.Points[p.PathData.Points.Length - 1]);
                p3 = new Point(rinner.Left + rinner.Width / 2, p2.Y - 3);
                p.AddCurve(new Point[] { p2, p3, p1 });

                using (PathGradientBrush gradient = new PathGradientBrush(p))
                {
                    gradient.WrapMode = WrapMode.Clamp;
                    gradient.CenterPoint = p3;
                    gradient.CenterColor = Color.Transparent;
                    gradient.SurroundColors = new Color[] { lightColor };

                    blend = new Blend(3)
                    {
                        Factors = new float[] { .3f, .8f, 1f },
                        Positions = new float[] { 0, 0.50f, 1f }
                    };
                    gradient.Blend = blend;

                    graphics.FillPath(gradient, p);
                }

                using (LinearGradientBrush b = new LinearGradientBrush(new Point(rect.Left, rect.Top), new Point(rect.Left, p1.Y), Color.White, Color.Transparent))
                {
                    blend = new Blend(4)
                    {
                        Factors = new float[] { 0f, .4f, .8f, 1f },
                        Positions = new float[] { 0f, .3f, .4f, 1f }
                    };
                    b.Blend = blend;
                    graphics.FillPath(b, p);
                }
            }

            // Upper shine
            using (GraphicsPath p = new GraphicsPath())
            {
                sweep = 160;
                start = 180 + (180 - sweep) / 2;
                p.AddArc(rinner, start, sweep);

                using (Pen pen = new Pen(Color.White))
                {
                    graphics.DrawPath(pen, p);
                }
            }

            // Lower Shine
            using (GraphicsPath p = new GraphicsPath())
            {
                sweep = 160;
                start = (180 - sweep) / 2;
                p.AddArc(rinner, start, sweep);
                Point pt = Point.Round(p.PathData.Points[0]);

                Rectangle rrinner = rinner; rrinner.Inflate(-1, -1);
                sweep = 160;
                start = (180 - sweep) / 2;
                p.AddArc(rrinner, start, sweep);

                using (LinearGradientBrush b = new LinearGradientBrush(
                    new Point(rinner.Left, rinner.Bottom),
                    new Point(rinner.Left, pt.Y - 1),
                    lightColor, Color.FromArgb(50, lightColor)))
                {
                    graphics.FillPath(b, p);
                }
            }
        }
        #endregion

        #region 绘制控制按钮图案
        /// <summary>
        /// 关闭图案
        /// </summary>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <returns></returns>
        internal static GraphicsPath CreateCloseFlag(Rectangle rect)
        {
            PointF centerPoint = new PointF(rect.X + rect.Width / 2.0f, rect.Y + rect.Height / 2.0f);
            GraphicsPath path = new GraphicsPath();
            path.AddLine(centerPoint.X, centerPoint.Y - 2, centerPoint.X - 2, centerPoint.Y - 4);
            path.AddLine(centerPoint.X - 2, centerPoint.Y - 4, centerPoint.X - 6, centerPoint.Y - 4);
            path.AddLine(centerPoint.X - 6, centerPoint.Y - 4, centerPoint.X - 2, centerPoint.Y);
            path.AddLine(centerPoint.X - 2, centerPoint.Y, centerPoint.X - 6, centerPoint.Y + 4);
            path.AddLine(centerPoint.X - 6, centerPoint.Y + 4, centerPoint.X - 2, centerPoint.Y + 4);
            path.AddLine(centerPoint.X - 2, centerPoint.Y + 4, centerPoint.X, centerPoint.Y + 2);
            path.AddLine(centerPoint.X, centerPoint.Y + 2, centerPoint.X + 2, centerPoint.Y + 4);
            path.AddLine(centerPoint.X + 2, centerPoint.Y + 4, centerPoint.X + 6, centerPoint.Y + 4);
            path.AddLine(centerPoint.X + 6, centerPoint.Y + 4, centerPoint.X + 2, centerPoint.Y);
            path.AddLine(centerPoint.X + 2, centerPoint.Y, centerPoint.X + 6, centerPoint.Y - 4);
            path.AddLine(centerPoint.X + 6, centerPoint.Y - 4, centerPoint.X + 2, centerPoint.Y - 4);
            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// 最大化图案
        /// </summary>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="maximize">是否已最大化</param>
        /// <returns></returns>
        internal static GraphicsPath CreateMaximizeFlag(Rectangle rect, bool maximize)
        {
            PointF centerPoint = new PointF(rect.X + rect.Width / 2.0f, rect.Y + rect.Height / 1.9f);

            GraphicsPath path = new GraphicsPath();

            if (maximize)
            {
                path.AddLine(centerPoint.X - 3, centerPoint.Y - 2, centerPoint.X - 6, centerPoint.Y - 2);
                path.AddLine(centerPoint.X - 6, centerPoint.Y - 3, centerPoint.X - 6, centerPoint.Y + 5);
                path.AddLine(centerPoint.X - 6, centerPoint.Y + 5, centerPoint.X + 3, centerPoint.Y + 5);
                path.AddLine(centerPoint.X + 3, centerPoint.Y + 5, centerPoint.X + 3, centerPoint.Y + 1);
                path.AddLine(centerPoint.X + 3, centerPoint.Y + 1, centerPoint.X + 6, centerPoint.Y + 1);
                path.AddLine(centerPoint.X + 6, centerPoint.Y + 1, centerPoint.X + 6, centerPoint.Y - 6);
                path.AddLine(centerPoint.X + 6, centerPoint.Y - 6, centerPoint.X - 3, centerPoint.Y - 6);
                path.CloseFigure();

                path.AddRectangle(new RectangleF(centerPoint.X - 4, centerPoint.Y, 5, 3));

                path.AddLine(centerPoint.X - 1, centerPoint.Y - 4, centerPoint.X + 4, centerPoint.Y - 4);
                path.AddLine(centerPoint.X + 4, centerPoint.Y - 4, centerPoint.X + 4, centerPoint.Y - 1);
                path.AddLine(centerPoint.X + 4, centerPoint.Y - 1, centerPoint.X + 3, centerPoint.Y - 1);
                path.AddLine(centerPoint.X + 3, centerPoint.Y - 1, centerPoint.X + 3, centerPoint.Y - 3);
                path.AddLine(centerPoint.X + 3, centerPoint.Y - 3, centerPoint.X - 1, centerPoint.Y - 3);
                path.CloseFigure();
            }
            else
            {
                path.AddRectangle(new RectangleF(centerPoint.X - 6, centerPoint.Y - 5, 12, 8));
                path.AddRectangle(new RectangleF(centerPoint.X - 5, centerPoint.Y - 2, 10, 4));
            }

            return path;
        }

        /// <summary>
        /// 最小化图案
        /// </summary>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <returns></returns>
        internal static GraphicsPath CreateMinimizeFlag(Rectangle rect)
        {
            PointF centerPoint = new PointF(rect.X + rect.Width / 2.0f, rect.Y + rect.Height / 2.5f);

            GraphicsPath path = new GraphicsPath();

            path.AddRectangle(new RectangleF(centerPoint.X - 6, centerPoint.Y + 1, 12, 2));
            return path;
        }

        /// <summary>
        /// 自定义控制按钮图案标志颜色转换
        /// </summary>
        /// <param name="bitmap">图片</param>
        /// <param name="color">颜色</param>
        /// <returns></returns>
        internal static Bitmap TransformBitmapFlagColor(Bitmap bitmap, Color color)
        {
            int width = bitmap.Width;//获取图片宽度  
            int height = bitmap.Height;//获取图片高度  

            Bitmap newBitmap = new Bitmap(width, height);//保存新图片  

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    //获取原图片的颜色值（ARGB存储方式）
                    Color pixel = bitmap.GetPixel(i, j);

                    int r = pixel.R, g = pixel.G, b = pixel.B, a = pixel.A;

                    //白色RGB(255,255,255),黑色RGB(0,0,0)                
                    //判断是否属于白色背景          
                    if (r == 255 && g == 255 && b == 255)
                    {
                        //设置新图片中指定像素的颜色为黑色              
                        newBitmap.SetPixel(i, j, color);
                    }
                }
            }

            return newBitmap;
        }
        #endregion

        #region 创建指定样式的GraphicsPath
        /// <summary>
        /// 创建指定样式的GraphicsPath
        /// </summary>
        /// <param name="rect">存储一组整数，共四个，表示一个矩形的位置和大小</param>
        /// <param name="radius">圆角弧度大小</param>
        /// <param name="roundStyle">圆角的样式</param>
        /// <param name="correction">是否把矩形长宽减 1,以便画出边框</param>
        /// <returns>建立的路径</returns>
        public static GraphicsPath CreateGraphicsPath(Rectangle rect, int radius, AuroraRoundStyle roundStyle, bool correction = true)
        {
            GraphicsPath path = new GraphicsPath();
            int radiusCorrection = correction ? 1 : 0;
            switch (roundStyle)
            {
                case AuroraRoundStyle.None:
                    path.AddRectangle(rect);
                    break;
                case AuroraRoundStyle.All:
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(rect.Right - radius - radiusCorrection, rect.Y, radius, radius, 270, 90);
                    path.AddArc(rect.Right - radius - radiusCorrection, rect.Bottom - radius - radiusCorrection, radius, radius, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - radius - radiusCorrection, radius, radius, 90, 90);
                    break;
                case AuroraRoundStyle.Left:
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddLine(rect.Right - radiusCorrection, rect.Y, rect.Right - radiusCorrection, rect.Bottom - radiusCorrection);
                    path.AddArc(rect.X, rect.Bottom - radius - radiusCorrection, radius, radius, 90, 90);
                    break;
                case AuroraRoundStyle.LeftTop:
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    break;
                case AuroraRoundStyle.LeftBottom:
                    path.AddArc(rect.X, rect.Bottom - radius - radiusCorrection, radius, radius, 90, 90);
                    break;
                case AuroraRoundStyle.Right:
                    path.AddArc(rect.Right - radius - radiusCorrection, rect.Y, radius, radius, 270, 90);
                    path.AddArc(rect.Right - radius - radiusCorrection, rect.Bottom - radius - radiusCorrection, radius, radius, 0, 90);
                    path.AddLine(rect.X, rect.Bottom - radiusCorrection, rect.X, rect.Y);
                    break;
                case AuroraRoundStyle.RightTop:
                    path.AddArc(rect.Right - radius - radiusCorrection, rect.Y, radius, radius, 270, 90);
                    break;
                case AuroraRoundStyle.RightBottom:
                    path.AddArc(rect.Right - radius - radiusCorrection, rect.Bottom - radius - radiusCorrection, radius, radius, 0, 90);
                    break;
                case AuroraRoundStyle.Top:
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(rect.Right - radius - radiusCorrection, rect.Y, radius, radius, 270, 90);
                    path.AddLine(rect.Right - radiusCorrection, rect.Bottom - radiusCorrection, rect.X, rect.Bottom - radiusCorrection);
                    break;
                case AuroraRoundStyle.Bottom:
                    path.AddArc(rect.Right - radius - radiusCorrection, rect.Bottom - radius - radiusCorrection, radius, radius, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - radius - radiusCorrection, radius, radius, 90, 90);
                    path.AddLine(rect.X, rect.Y, rect.Right - radiusCorrection, rect.Y);
                    break;
            }
            path.CloseFigure(); //这句很关键，缺少会没有左边线。
            return path;
        }
        #endregion
    }
}
