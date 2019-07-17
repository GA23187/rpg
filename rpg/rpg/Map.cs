using System.Drawing;

public class Map
{
    public static int current_map = 0;

    public string bitmap_path;   //地图层定义
    public Bitmap bitmap;
    public string shade_path;   //遮挡层定义
    public Bitmap shade;
    public string block_path;  //地图障碍层
    public Bitmap block;
    public string back_path;   //背景层
    public Bitmap back;

    //音乐
    public string music;

    public Map()
    {
        bitmap_path = "map1_b.gif";
    }

    //---------------------------------------------------------
    //        绘图
    //-----------------------------------------------------------
    public static void draw(Map[] map,Player[] player,Npc[] npc, Graphics g,Rectangle stage)
    {
        Map m=map[current_map];
 /*      //绘图位置X
        int map_sx = 0;
        int p_x = Player.get_pos_x(player);//角色坐标
        int map_w = m.bitmap.Width;

        if (p_x <= stage.Width / 2)
        {
            map_sx = 0;
        }
        else if (p_x >= map_w - stage.Width / 2)
        {
            map_sx = stage.Width - map_w;
        }
        else
        {
            map_sx = stage.Width / 2 - p_x;
        }

        //绘制位置y
        int map_sy= 0;     //地图屏幕坐标
        int p_y = Player.get_pos_y(player);        //角色坐标
        int map_h = m.bitmap.Height;

        if (p_y <= stage.Height/ 2)
        {
            map_sy = 0;
        }
        else if (p_y >= map_h - stage.Height / 2)
        {
            map_sy = stage.Height - map_h;
        }
        else
        {
            map_sy = stage.Height / 2 - p_y;
        }
        
 */   
    

        //坐标
        int map_sx = get_map_sx(map,player,stage);
        int map_sy = get_map_sy(map, player, stage);

  /*     //绘图
        g.DrawImage(m.bitmap,map_sx,map_sy);
        Player.draw(player,g,map_sx,map_sy);        //将player.draw放在map中
        for (int i = 0; i < npc.Length; i++)                //绘制处于本地图中的NPC
        {
            if (npc[i] == null)
                continue;
            if (npc[i].map != current_map)
                continue;
            npc[i].draw(g,map_sx,map_sy);
        }
            g.DrawImage(m.shade, map_sx, map_sy);     //遮挡层会遮挡人物所以在绘制player之后
*/


            //绘图
            if (m.back != null)
                g.DrawImage(m.back, 0, 0);
            if (m.bitmap != null)
                g.DrawImage(m.bitmap, map_sx, map_sy);

            Player.draw_flag(g,map_sx,map_sy);                   //画鼠标

            draw_player_npc(map, player, npc, g, map_sx, map_sy);              //画主角和npc

            if (m.shade != null)
                g.DrawImage(m.shade, map_sx, map_sy);


    }

   public static int get_map_sx(Map[] map, Player[] player, Rectangle stage)
    {
        Map m=map[current_map];
        if (m.bitmap == null)
            return 0;
        int map_sx = 0;
        int p_x = Player.get_pos_x(player);  //角色坐标
        int map_w = m.bitmap.Width;

        if (p_x <= stage.Width / 2)
        {
            map_sx = 0;
        }
        else if (p_x >= map_w - stage.Width / 2)
        {
            map_sx = stage.Width - map_w;
        }
        else
        {
            map_sx = stage.Width / 2 - p_x;
        }
        return map_sx;
    }

    public static int get_map_sy(Map[] map, Player[] player, Rectangle stage)
    {
        Map m = map[current_map];
        if (m.bitmap == null)
            return 0;
        int map_sy = 0;
        int p_y = Player.get_pos_y(player);        //角色坐标
        int map_h = m.bitmap.Height;

        if (p_y <= stage.Height / 2)
        {
            map_sy = 0;
        }
        else if (p_y >= map_h - stage.Height / 2)
        {
            map_sy = stage.Height - map_h;
        }
        else
        {
            map_sy = stage.Height / 2 - p_y;
        }
        return map_sy;
    }

     //---------------------------------------
    //         npc层次排序
    //----------------------------------------

    public struct Layer_sort
    {
        public int y;
        public int index;
        //0--猪脚 1--npc
        public int type;
    }


    public class Layer_sort_comparer : System.Collections.IComparer
    {
        public int Compare(object s1, object s2)
        {
            return ((Layer_sort)s1).y-((Layer_sort)s2).y;
        }
    }


    public static void draw_player_npc(Map[] map, Player[] player, Npc[] npc, Graphics g, int map_sx, int map_sy)
    {
        //绘制主角和npc
        Layer_sort[] layer_sort =new Layer_sort[npc.Length+1];             //定义数组
        for (int i = 0; i < npc.Length; i++)                                             //npc数据
        {
            if (npc[i] != null)
            {
                layer_sort[i].y = npc[i].y;
                layer_sort[i].index = i;
                layer_sort[i].type = 1;
            }
            else
            {
                layer_sort[i].y = int.MaxValue;
                layer_sort[i].index = i;
                layer_sort[i].type = 1;
            }
        }
        layer_sort[npc.Length].y = Player.get_pos_y(player);             //角色数据
        layer_sort[npc.Length].index = 0;
        layer_sort[npc.Length].type = 0;

        System.Array.Sort(layer_sort, new Layer_sort_comparer());    //绘图
        for (int i = 0; i < layer_sort.Length;i++ )                                //排序
        {
            //画主角
            if (layer_sort[i].type == 0)
            {
                Player.draw(player,g,map_sx,map_sy);
            }
            //画npc
            else if (layer_sort[i].type == 1)
            {
                int index = layer_sort[i].index;
                if (npc[index] == null)
                    continue;
                if (npc[index].map != current_map)
                    continue;
                npc[index].draw(g,map_sx,map_sy);
            }
        }

    }


    //newindex代表新地图下标，x,y,face代表切换到新地图下的坐标和方向
    public static void change_map(Map[] map, Player[] player,Npc[] npc, int newindex, int x, int y, int face,WMPLib.WindowsMediaPlayer music_player)
    {
        //卸载旧地图
        if (map[current_map].bitmap != null)
        {
            map[current_map].bitmap = null;
        }

        if (map[current_map].shade != null)
        {
            map[current_map].shade = null;
        }

        if (map[current_map].block != null)
        {
            map[current_map].block = null;
        }

        if (map[current_map].back != null)
        {
            map[current_map].back = null;
        }
        //加载新地图
        map[newindex].bitmap = new Bitmap(map[newindex].bitmap_path);
        map[newindex].bitmap.SetResolution(96,96);

        map[newindex].shade = new Bitmap(map[newindex].shade_path);
        map[newindex].shade.SetResolution(96,96);

        map[newindex].block = new Bitmap(map[newindex].block_path);
        map[newindex].block.SetResolution(96,96);
        //current_map
        current_map = newindex;
        //位置设置
        Player.set_pos(player, x, y, face);

        //npc资源
        for (int i = 0; i < npc.Length; i++)
        {
            if (npc[i] == null)
                continue;

            if (npc[i].map == current_map)
                npc[i].unload();

            if (npc[i].map == newindex)
                npc[i].load();
        }

            //音乐
            music_player.URL = map[current_map].music;
        }

    //是否可通行
    public static bool can_through(Map[] map, int x, int y)
    {
        Map m = map[current_map];

        if (x < 0) return false;                           //是否在图片范围内
        else if (x >= m.block.Width) return false;
        else if (y<0) return false;
        else if (y>=m.block.Height) return false;

        if (m.block.GetPixel(x, y).B == 0)             //是否为黑色来实现障碍层判断
            return false;
        else
            return true;
    }

}