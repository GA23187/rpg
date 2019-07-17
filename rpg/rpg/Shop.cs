using System.Drawing;
using rpg;

public class Shop
{
    public static Panel shop = new Panel();

    public static int page = 1;             //页数
    public static int selnow = 1;           //当前选中了第几个物品
    public static Bitmap bitmap_sel;
    public static int[] list;             //静态数组来指明当前商店所买的物品id

    public static void init()
    {
        bitmap_sel = new Bitmap(@"item/sbt7_2.png");
        bitmap_sel.SetResolution(96, 96);

        Button previous_page = new Button();
        previous_page.set(20, 326, 0, 0, "item/sbt3_1.png", "item/sbt3_2.png", "item/sbt3_2.png", -1, -1, -1, -1);
        previous_page.click_event += new Button.Click_event(click_previous_page);

        Button next_page = new Button();
        next_page.set(220, 326, 0, 0, "item/sbt5_1.png", "item/sbt5_2.png", "item/sbt5_2.png", -1, -1, -1, -1);
        next_page.click_event += new Button.Click_event(click_next_page);

        Button buy = new Button();
        buy.set(120, 326, 0, 0, "item/sbt4_1.png", "item/sbt4_2.png", "item/sbt4_2.png", -1, -1, -1, -1);
        buy.click_event += new Button.Click_event(click_buy);

        Button close = new Button();
        close.set(350, 10, 0, 0, "item/sbt6_1.png", "item/sbt6_2.png", "item/sbt6_2.png", -1, -1, -1, -1);
        close.click_event += new Button.Click_event(click_close);

        Button sel1 = new Button();
        sel1.set(35, 38, 0, 0, "item/sbt7_1.png", "item/sbt7_2.png", "item/sbt7_2.png", -1, -1, -1, -1);
        sel1.click_event += new Button.Click_event(click_sel1);

        Button sel2 = new Button();
        sel2.set(35, 133, 0, 0, "item/sbt7_1.png", "item/sbt7_2.png", "item/sbt7_2.png", -1, -1, -1, -1);
        sel2.click_event += new Button.Click_event(click_sel2);

        Button sel3 = new Button();
        sel3.set(35, 226, 0, 0, "item/sbt7_1.png", "item/sbt7_2.png", "item/sbt7_2.png", -1, -1, -1, -1);
        sel3.click_event += new Button.Click_event(click_sel3);

        Button under = new Button();
        under.set(-100, -100, 2000, 2000, "", "", "", -1, -1, -1, -1);

        shop.button = new Button[8];
        shop.button[0] = previous_page;
        shop.button[1] = next_page;
        shop.button[2] = buy;
        shop.button[3] = close;
        shop.button[4] = sel1;
        shop.button[5] = sel2;
        shop.button[6] = sel3;
        shop.button[7] = under;
        shop.set(300, 25, "item/shop_bg.png", 2, 3);
        shop.draw_event += new Panel.Draw_event(draw);            //绘图
        shop.init();
    }

    public static void show(int[] list)
    {
        Shop.list = list;
        page = 1;
        shop.show();
    }

    public static void click_previous_page()
    {
        page--;
        if (page < 1) page = 1;
    }
    public static void click_next_page()
    {
        page++;
    }
    public static void click_close()
    {
        shop.hide();
    }
    public static void click_sel1()
    {
        selnow = 1;
    }
    public static void click_sel2()
    {
        selnow = 2;
    }
    public static void click_sel3()
    {
        selnow = 3;
    }
    public static void click_buy()
    {     
            int index = -1;
            for (int i = 0, count = 0; i < Item.item.Length; i++)
            {
                if (Item.item[i].num <= 0)
                    continue;
                count++;

                if (count <= (page - 1) * 3 + selnow - 1)
                    continue;
                index = i;
                break;
            }
            if (index >= 0)
            {
                if (Player.money >= Item.item[index].cost)
                {
                    Player.money -= Item.item[index].cost;
                    Item.add_item(index,1);
                }
            }
            
    }
    public static void draw(Graphics g, int x_offset, int y_offset)
    {
        //绘制金钱
        Font font_m = new Font("黑体", 16);
        Brush brush_m = Brushes.DarkOrange;
        g.DrawString(Player.money.ToString(), font_m, brush_m, x_offset + 160, y_offset + 390, new StringFormat());

        //显示物品
            for (int i = 0, count = 0, showcount = 0; i < Item.item.Length && showcount < 3; i++)
            {
               // if (Item.item[i].num <= 0)
               //     continue;
                count++;

                if (count <= (page - 1) * 3)
                    continue;

                if (Shop.list[i] != -1)
                {
                    g.DrawImage(Item.item[Shop.list[i]].bitmap, x_offset + 36, y_offset + 48 + showcount * 96);
                    Font font_n = new Font("黑体", 12);
                    Brush brush_n = Brushes.GreenYellow;
                    g.DrawString(Item.item[Shop.list[i]].name + " $" + Item.item[Shop.list[i]].cost, font_n, brush_n, x_offset + 150, y_offset + 48 + showcount * 96, new StringFormat());
                    Font font_d = new Font("黑体", 10);
                    Brush brush_d = Brushes.LawnGreen;
                    g.DrawString(Item.item[Shop.list[i]].description, font_d, brush_d, x_offset + 150, y_offset + 75 + showcount * 96, new StringFormat());
                    showcount++;    
                }
                //showcount++;       
        }
        //显示选择框
        g.DrawImage(StatusMenu.bitmap_sel, x_offset + 35, y_offset + 38 + (selnow - 1) * 95);
    }
    
}