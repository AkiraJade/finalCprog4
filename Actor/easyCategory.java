import greenfoot.*;

public class easyCategory extends Actor
{
    public easyCategory() {
        GreenfootImage img = getImage(); 
        img.scale(170, 50); 
        setImage(img);
    }
    
    public void act()
    {
        // Easy category is always unlocked
        if (Greenfoot.mouseClicked(this))
        {
            Greenfoot.setWorld(new categoryEasy());
        }
    }
}