using System.Windows.Forms;
using System.Drawing;

public static class Message
{
    public static Panel message = new Panel();
    public static Panel messagetip = new Panel();
    public static Bitmap face;

    public enum Face            //立绘图片的位置显示
    {
        LEFT=1,
        RIGHT=2,
    }
    public static Face face_pos = Face.LEFT;

    public static string name = "";
    public static string content = "";

    public static void init()
    {
        Button btn_ok = new Button();
        btn_ok.set(-1000, -1000, 2000, 2000, "", "", "", -1, -1, -1, -1);             //定义一个全屏的按钮，单击后使对话框消失
        btn_ok.click_event += new Button.Click_event(btn_ok_event);                  

        message.button=new Button[1];
        message.button[0] = btn_ok;
        message.set(150,315,"ui/msg.png",0,-1);
        message.draw_event += new Panel.Draw_event(msgdraw);
        message.init();

        Button btn_ok_tip = new Button();
        btn_ok_tip.set(-1000,-1000,2000,2000,"","","",-1,-1,-1,-1);
        btn_ok_tip.click_event += new Button.Click_event(btntip_ok_event);
        messagetip.button=new Button[1];
        messagetip.button[0] = btn_ok_tip;
        messagetip.set(300,250,"ui/msgtip.png",0,-1);
        messagetip.draw_event += new Panel.Draw_event(msgdrawtip);
        messagetip.init();
    }

    public static void btn_ok_event()    //关闭对话框
    {
        message.hide();
    }

    public static void show(string name0, string content0, string face_path, Face face_pos0)
    {
        //名字，对话内容
        name = name0;
        content = content0;
        //立绘图片
        if (face_path != null && face_path != "")
        {
            face = new Bitmap(face_path);
            face.SetResolution(96, 96);
        }
        else
        {
            face = null;
        }
        face_pos = face_pos0;
        message.show();  //显示面板
    }

    //自动换行
    //str-源字符串 num-每行字数
    public static string linefeed(string str, int num)
    {
        if (str == null)
            return null;

        string ret = "";
        int start_pos = 0;
        while (start_pos < str.Length)                                     //遍历，每个num个字符添加\n
        {
            if (start_pos + num > str.Length)
                num = str.Length - start_pos;
            ret = ret + str.Substring(start_pos, num) + "\n";
                start_pos=start_pos+num;
        }
        return ret;
    }

    //绘图方法
    public static void msgdraw(Graphics g, int x_offset, int y_offset)
    {
        //立绘
        if (face != null)
        {
            if (face_pos == Face.LEFT)
                g.DrawImage(face, 0, 245);
            else if (face_pos == Face.RIGHT)
                g.DrawImage(face,620,245);
        }
        //名字
        Font name_font = new Font("黑体",14);
        Brush name_brush = Brushes.Peru;
        StringFormat name_sf = new StringFormat();
        if (face_pos == Face.LEFT)                              //立绘图片的位置会影响文本的位置
            g.DrawString(name, name_font, name_brush, x_offset + 45, y_offset + 25, name_sf);
        else
            g.DrawString(name, name_font, name_brush, x_offset + 50, y_offset + 25, name_sf);
        //内容
        Font content_font = new Font("黑体",12);
        Brush content_brush = Brushes.WhiteSmoke;
        StringFormat content_sf = new StringFormat();
        string show_content = linefeed(content,22);                     //文本换行
        if (face_pos == Face.LEFT)
            g.DrawString(show_content, content_font, content_brush, x_offset + 60, y_offset + 55, content_sf);
        else
            g.DrawString(show_content, content_font, content_brush, x_offset + 70, y_offset + 55, content_sf);

    }


    public static void btntip_ok_event()
    {
        messagetip.hide();
    }

    public static void msgdrawtip(Graphics g,int x_offset,int y_offset)
    {
        //画文字
        Font content_font = new Font("黑体",12);
        Brush content_brush = new SolidBrush(Color.WhiteSmoke);
        StringFormat sf = new StringFormat();
        sf.Alignment = StringAlignment.Center;

        g.DrawString(content,content_font,content_brush,new Rectangle(x_offset,y_offset+12,244,22),sf);
 
    }

    public static void showtip(string content0)
    {
        content=content0;
        messagetip.show();
    }




}