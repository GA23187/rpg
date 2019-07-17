using System.Drawing;

public class Animation                       //动作类
{
    public static long RATE = 100;                 //作为动画的基准速，单位为毫秒
    public string bitmap_path;                          //动画路径
    public Bitmap bitmap;                               //动画图像
    public int row = 2;                                  //bitmap的列数
    public int col = 2;                                      //行数
    public int max_frame = 3;                     //  动画帧数
    public int anm_rate;                           //以rate为基准的播放速


    //加载
    public void load()
    {
        if (bitmap_path != null && bitmap_path != "")
        {
            bitmap = new Bitmap(bitmap_path);
            bitmap.SetResolution(96,96);
        }
    }

    //卸载
    public void unload()
    {
        if (bitmap != null)
        {
            bitmap = null;
        }
    }
    //获取图片
    public Bitmap get_bitmap(int frame)
    {
        if (bitmap == null)
            return null;
        if (frame >= max_frame)
            return null;
        //定义区域
        Rectangle rect = new Rectangle(bitmap.Width/row*(frame%row),bitmap.Height/col*(frame/row),bitmap.Width/row,bitmap.Height/col);
        //返回图像
        return bitmap.Clone(rect,bitmap.PixelFormat);
    }

    public void draw(Graphics g, int frame, int x, int y)
    {
        Bitmap bitmap = get_bitmap(frame/anm_rate);   //anm_rate可以调播放速度，1为全速
        if (bitmap == null)
            return;
        g.DrawImage(bitmap,x,y);
    }



}